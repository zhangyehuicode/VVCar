using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YEF.Core;
using YEF.Core.Data;

namespace YEF.Data
{
    public class EfDbContext : DbContext, IUnitOfWork, IDependency
    {
        int _TranscationLocker = 0;

        DbContextTransaction _dbContextTransaction;

        readonly static object _tranLock = new object();

        #region ctor.

        /// <summary>
        /// 初始化一个<see cref="CodeFirstDbContext"/>类型的新实例
        /// </summary>
        public EfDbContext()
            : this(GetNameOrConnectionString())
        {
        }

        /// <summary>
        /// 使用连接名称或连接字符串 初始化一个<see cref="CodeFirstDbContext"/>类型的新实例
        /// </summary>
        public EfDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Database.CommandTimeout = 180;
            DatabaseInitializer.InitializeDatabase(this);
        }

        #endregion

        #region IUnitOfWork 成员

        public T GetRepository<T>()
        {
            return ServiceLocator.Instance.GetService<T>(new { unitOfWork = this });
        }

        public int ExecuteSqlCommand(YEF.Core.Data.TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            System.Data.Entity.TransactionalBehavior behavior = transactionalBehavior == YEF.Core.Data.TransactionalBehavior.DoNotEnsureTransaction
                ? System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction
                : System.Data.Entity.TransactionalBehavior.EnsureTransaction;
            return Database.ExecuteSqlCommand(behavior, sql, parameters);
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<TElement>(sql, parameters);
        }

        public IEnumerable<object> SqlQuery(Type elementType, string sql, params object[] parameters)
        {
            return Database.SqlQuery(elementType, sql, parameters).Cast<object>();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                var errorBuilder = new StringBuilder();
                if (dbEx.EntityValidationErrors.Count() > 0)
                {
                    IList<string> s = new List<string>();
                    dbEx.EntityValidationErrors.ForEach(eve =>
                        eve.ValidationErrors.ForEach(ve => errorBuilder.AppendLine(ve.ErrorMessage)));
                }
                AppContext.Logger.Error("提交到数据库失败，数据校验失败。", errorBuilder.ToString());
                throw new DomainException(errorBuilder.ToString());
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException dbEx)
            {
                AppContext.Logger.Error("提交到数据库失败, 异常原因", dbEx);
                return 0;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
            {
                AppContext.Logger.Error("提交到数据库失败, 异常原因", dbEx);
                throw new DomainException(dbEx.ToString());
            }
        }

        public async Task<int> ExecuteSqlCommandAsync(YEF.Core.Data.TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            System.Data.Entity.TransactionalBehavior behavior = transactionalBehavior == YEF.Core.Data.TransactionalBehavior.DoNotEnsureTransaction
                ? System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction
                : System.Data.Entity.TransactionalBehavior.EnsureTransaction;
            return await Database.ExecuteSqlCommandAsync(behavior, sql, parameters);
        }

        public override async Task<int> SaveChangesAsync()
        {
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                var errorBuilder = new StringBuilder();
                if (dbEx.EntityValidationErrors.Count() > 0)
                {
                    IList<string> s = new List<string>();
                    dbEx.EntityValidationErrors.ForEach(eve =>
                        eve.ValidationErrors.ForEach(ve => errorBuilder.AppendLine(ve.ErrorMessage)));
                }
                AppContext.Logger.Error("提交到数据库失败，数据校验失败。", errorBuilder.ToString());
                throw new DomainException(errorBuilder.ToString());
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException dbEx)
            {
                //AppContext.Logger.Error("提交到数据库失败, 异常原因", dbEx);
                return 0;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
            {
                AppContext.Logger.Error("提交到数据库失败, 异常原因", dbEx);
                throw new DomainException(dbEx.ToString());
            }
        }

        public void BeginTransaction()
        {
            lock (_tranLock)
            {
                if (this._dbContextTransaction == null)
                {
                    this._dbContextTransaction = this.Database.BeginTransaction();
                }
                this._TranscationLocker++;
            }
        }

        public void CommitTransaction()
        {
            if (this._dbContextTransaction == null)
                return;
            lock (_tranLock)
            {
                this._TranscationLocker--;
                if (_TranscationLocker == 0)
                {
                    this._dbContextTransaction.Commit();
                    this._dbContextTransaction.Dispose();
                    this._dbContextTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            if (this._dbContextTransaction == null)
                return;
            lock (_tranLock)
            {
                this._dbContextTransaction.Rollback();
                this._dbContextTransaction.Dispose();
                this._dbContextTransaction = null;
                this._TranscationLocker = 0;
            }
        }

        #endregion

        #region methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//移除一对多的级联删除的契约
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//移除复数表名的契约
            
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(18, 6));

            var files = System.IO.Directory.GetFiles(System.AppDomain.CurrentDomain.RelativeSearchPath, "*.Data.dll");
#if DEBUG
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
#endif
            if (files.Length > 0)
            {
                //Parallel.ForEach(files, file =>
                //{
                //    var assembly = Assembly.LoadFrom(file);
                //    if (assembly != null)
                //        modelBuilder.Configurations.AddFromAssembly(assembly);
                //});
                foreach (var file in files)
                {
                    var assembly = Assembly.LoadFrom(file);
                    if (assembly != null)
                        modelBuilder.Configurations.AddFromAssembly(assembly);
                }
            }
#if DEBUG
            sw.Stop();
            YEF.Core.Logging.LoggerManager.GetLogger("System").Debug("add configurations from assemblies cost time = {0} (ms)", sw.ElapsedMilliseconds);
#endif
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        static string GetNameOrConnectionString()
        {
            var nameOrConnectionString = "Name=default";
            var dbSetting = AppContext.Settings.DbSetting;
            if (AppContext.Settings.IsDynamicCompany)
            {
                if (dbSetting == null)
                    AppContext.Logger.Error("数据库信息未正确配置");
                nameOrConnectionString = string.Format("Data Source={0};Initial Catalog=YEF_{1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=true",
                    dbSetting.DbServer, AppContext.CurrentSession.CompanyCode, dbSetting.UserID, dbSetting.Password);
            }
            else if (dbSetting != null && !string.IsNullOrEmpty(dbSetting.DbName))
            {
                nameOrConnectionString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=true",
                    dbSetting.DbServer, dbSetting.DbName, dbSetting.UserID, dbSetting.Password);
            }
            return nameOrConnectionString;
        }

        #endregion


    }
}
