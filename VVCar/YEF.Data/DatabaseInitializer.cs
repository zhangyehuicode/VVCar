using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using YEF.Data.Initializer;

namespace YEF.Data
{
    /// <summary>
    /// 数据库初始化操作类
    /// </summary>
    public class DatabaseInitializer
    {
        static readonly object _lockObj = new object();
        static readonly List<string> _InitializedDbList;

        static DatabaseInitializer()
        {
            _InitializedDbList = new List<string>();
        }

        /// <summary>
        /// 设置数据库初始化，策略为自动迁移到最新版本
        /// </summary>
        public static void Initialize()
        {
            if (Core.AppContext.Settings.IsDynamicCompany)
                return;
            var context = new EfDbContext();
            context.Database.CommandTimeout = 180;
            IDatabaseInitializer<EfDbContext> initializer;
            if (!context.Database.Exists())
            {
                initializer = new CreateDbIfNotExists<EfDbContext>();
            }
            else
            {
                initializer = new MigrateDatabaseToLatestVersion<EfDbContext, MigrationsConfiguration>();
            }
            Database.SetInitializer(initializer);

            //EF预热，解决EF6第一次加载慢的问题
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
            var mappingItemCollection = (StorageMappingItemCollection)objectContext.ObjectStateManager
                .MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            mappingItemCollection.GenerateViews(new List<EdmSchemaError>());
            context.Dispose();
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="dbContext"></param>
        public static void InitializeDatabase(EfDbContext dbContext)
        {
            if (!Core.AppContext.Settings.IsDynamicCompany)
                return;
            if(string.IsNullOrEmpty(Core.AppContext.CurrentSession.CompanyCode))
            {
                Database.SetInitializer<EfDbContext>(null);
                return;
            }
            lock (_lockObj)
            {
                if (_InitializedDbList.Contains(Core.AppContext.CurrentSession.CompanyCode))
                    return;
                _InitializedDbList.Add(Core.AppContext.CurrentSession.CompanyCode);
            }
            IDatabaseInitializer<EfDbContext> initializer;
            if (!dbContext.Database.Exists())
            {
                initializer = new CreateDbIfNotExists<EfDbContext>();
            }
            else
            {
                initializer = new MigrateDatabaseToLatestVersion<EfDbContext, MigrationsConfiguration>();
            }
            Database.SetInitializer(initializer);
        }
    }
}
