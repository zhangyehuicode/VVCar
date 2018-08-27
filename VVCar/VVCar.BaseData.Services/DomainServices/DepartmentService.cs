using System;
using System.Collections.Generic;
using System.Linq;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Dtos;

namespace VVCar.BaseData.Services.DomainServices
{
    public partial class DepartmentService : DomainServiceBase<IRepository<Department>, Department, Guid>, IDepartmentService
    {
        public DepartmentService()
        {
        }

        #region properties
        IUserService _UserService;
        /// <summary>
        /// User 领域服务
        /// </summary>
        IUserService UserService
        {
            get
            {
                if (_UserService == null)
                    _UserService = ServiceLocator.Instance.GetService<IUserService>();
                return _UserService;
            }
        }

        IRepository<Merchant> MerchantRepo { get => UnitOfWork.GetRepository<IRepository<Merchant>>(); }

        #endregion

        #region methods

        public override Department Add(Department entity)
        {
            if (entity == null)
                return null;
            if (AppContext.Settings.ServiceRole == YEF.Core.Config.EServiceRole.PublicMaster)
                throw new DomainException("当前不允许此操作");
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public override bool Delete(Guid key)
        {
            var department = this.Repository.GetByKey(key);
            if (department == null)
                throw new DomainException("删除失败，数据不存在");
            if (department.DataSource == YEF.Core.Enums.EDataSource.External)
                throw new DomainException("该数据不允许删除");
            department.IsDeleted = true;
            return this.Repository.Update(department) > 0;
        }

        public override bool Update(Department entity)
        {
            if (entity == null)
                return false;
            var department = this.Repository.GetByKey(entity.ID);
            if (department == null)
                throw new DomainException("数据不存在");
            if (department.DataSource == YEF.Core.Enums.EDataSource.Default)//只有内部创建的数据才可以修改一下字段
            {
                department.Name = entity.Name;
                department.ContactPerson = entity.ContactPerson;
                department.ContactPhoneNo = entity.ContactPhoneNo;
                department.MobilePhoneNo = entity.MobilePhoneNo;
                department.Address = entity.Address;
            }
            department.EmailAddress = entity.EmailAddress;
            department.DistrictRegion = entity.DistrictRegion;
            department.AdministrationRegion = entity.AdministrationRegion;
            department.Remark = entity.Remark;
            department.LastUpdateUserID = AppContext.CurrentSession.UserID;
            department.LastUpdateUser = AppContext.CurrentSession.UserName;
            department.LastUpdateDate = DateTime.Now;
            return base.Update(department);
        }

        protected override bool DoValidate(Department entity)
        {
            bool exists = this.Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID);//&& t.MerchantID == AppContext.CurrentSession.MerchantID
            if (exists)
                throw new DomainException(String.Format("代码 {0} 已使用，不能重复添加。", entity.Code));
            exists = this.Repository.Exists(t => t.Name == entity.Name && t.ID != entity.ID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (exists)
                throw new DomainException(String.Format("名称 {0} 已使用，不能重复添加。", entity.Name));
            return true;
        }

        #endregion

        #region IDepartmentService 成员

        public PagedResultDto<Department> QueryData(DepartmentFilter filter)
        {
            var result = new PagedResultDto<Department>();
            var queryable = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    queryable = queryable.Where(t => t.Code.Contains(filter.Keyword) || t.Name.Contains(filter.Keyword));
                }
                else
                {
                    if (!string.IsNullOrEmpty(filter.Code))
                        queryable = queryable.Where(t => t.Code.Contains(filter.Code));
                    if (!string.IsNullOrEmpty(filter.Name))
                        queryable = queryable.Where(t => t.Name.Contains(filter.Name));
                }
                if (!string.IsNullOrEmpty(filter.DistrictRegion))
                    queryable = queryable.Where(t => t.DistrictRegion == filter.DistrictRegion);
                if (!string.IsNullOrEmpty(filter.AdministrationRegion))
                    queryable = queryable.Where(t => t.AdministrationRegion == filter.AdministrationRegion);
                if (filter.Start.HasValue && filter.Limit.HasValue)
                {
                    result.TotalCount = queryable.Count();
                    result.Items = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
                }
                else
                {
                    result.Items = queryable.ToArray();
                    result.TotalCount = result.Items.Count();
                }
            }
            else
            {
                result.Items = queryable.ToArray();
                result.TotalCount = result.Items.Count();
            }
            return result;
        }



        public PagedResultDto<Department> QueryCodeData(DepartmentFilter filter)
        {
            var result = new PagedResultDto<Department>();
            var queryable = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    queryable = queryable.Where(t => t.Code.Contains(filter.Keyword) || t.Name.Contains(filter.Keyword));
                }
                else
                {
                    if (!string.IsNullOrEmpty(filter.Code))
                        queryable = queryable.Where(t => t.Code.Contains(filter.Code));
                    if (!string.IsNullOrEmpty(filter.Name))
                        queryable = queryable.Where(t => t.Name.Contains(filter.Name));
                }
                if (!string.IsNullOrEmpty(filter.DistrictRegion))
                    queryable = queryable.Where(t => t.DistrictRegion == filter.DistrictRegion);
                if (!string.IsNullOrEmpty(filter.AdministrationRegion))
                    queryable = queryable.Where(t => t.AdministrationRegion == filter.AdministrationRegion);
                if (filter.Start.HasValue && filter.Limit.HasValue)
                {
                    result.TotalCount = queryable.Count();
                    result.Items = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
                }
                else
                {
                    result.Items = queryable.ToArray();
                    result.TotalCount = result.Items.Count();
                }
            }
            else
            {
                result.Items = queryable.ToArray();
                result.TotalCount = result.Items.Count();
            }
            return result;
        }

        public IEnumerable<DepartmentTreeDto> GetTreeData(Guid? parentID)
        {
            var departments = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .Select(t => new DepartmentTreeDto()
                {
                    ID = t.ID,
                    Text = t.Name,
                    ParentId = null,
                    expanded = true,
                }).ToList();
            return departments;
        }

        public IEnumerable<Department> GetDepartmentInfo()
        {
            return this.Repository.GetQueryable(false).Where(t => t.IsDeleted == false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
        }

        /// <summary>
        /// 创建商户店铺
        /// </summary>
        /// <param name="newStoreData"></param>
        /// <returns></returns>
        public bool CreateMchStore(MchCreateStoreDto newStoreData)
        {
            if (newStoreData == null)
                return false;
            this.UnitOfWork.BeginTransaction();
            try
            {
                var newDepartment = new Department
                {
                    ID = Util.NewID(),
                    Code = newStoreData.Code,
                    Name = newStoreData.Name,
                    MobilePhoneNo = newStoreData.StoreAdminPhoneNumber,
                    Address = newStoreData.Address,
                    ContactPerson = newStoreData.StoreAdmin,
                    DistrictRegion = "HQ",
                    AdministrationRegion = "HQ",
                    CreatedUserID = AppContext.CurrentSession.UserID,
                    CreatedUser = AppContext.CurrentSession.UserName,
                    CreatedDate = DateTime.Now,
                    DataSource = YEF.Core.Enums.EDataSource.External,
                    MerchantID = AppContext.CurrentSession.MerchantID,
                };
                var dept = this.Repository.Add(newDepartment);
                var existUser = UserService.Exists(t => t.Code == newStoreData.StoreAdminPhoneNumber && t.MerchantID == AppContext.CurrentSession.MerchantID);
                if (!existUser)
                {
                    var newUser = new User
                    {
                        ID = Util.NewID(),
                        Code = newStoreData.StoreAdminPhoneNumber,
                        Name = newStoreData.StoreAdmin,
                        DepartmentID = dept.ID,
                        MobilePhoneNo = newStoreData.StoreAdminPhoneNumber,
                        IsAvailable = true,
                        CanLoginAdminPortal = true,
                        Password = newStoreData.StoreAdminPassword,
                        CreatedUserID = Guid.Empty,
                        CreatedUser = string.Empty,
                        CreatedDate = DateTime.Now,
                        DataSource = YEF.Core.Enums.EDataSource.External,
                        MerchantID = AppContext.CurrentSession.MerchantID,
                    };
                    UserService.AddMchUser(newUser);
                }
                this.UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                this.UnitOfWork.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// 获取门店列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StoreInfoDto> GetStoreList()
        {
            var defaultDeptId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var deptQueryable = Repository.GetQueryable(false).Where(d => d.ID != defaultDeptId).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            return deptQueryable.Select(d => new StoreInfoDto
            {
                Name = d.Name,
                Address = d.Address,
                TelPhone = d.ContactPhoneNo,
            }).ToArray();
        }

        public IList<DepartmentLiteDto> GetDepartmentLiteData()
        {
            //var defaultDeptId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            return Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                //.Where(d => d.ID != defaultDeptId)
                .MapTo<DepartmentLiteDto>()
                .ToArray();
        }

        public DepartmentLiteDto GetDepartmentLiteData(string deptCode)
        {
            return Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .Where(d => d.Code == deptCode)
                .MapTo<DepartmentLiteDto>()
                .FirstOrDefault();
        }

        /// <summary>
        /// 设置门店位置信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool SetDepartmentLocation(DepartmentLocationDto param)
        {
            if (param == null)
                return false;
            var merchantid = AppContext.CurrentSession.MerchantID;
            if (merchantid == null)
                return false;
            var department = Repository.GetQueryable().FirstOrDefault(t => t.MerchantID == merchantid);
            if (department == null)
                return false;
            AppContext.Logger.Info($"Longitude:{param.Longitude},Latitude:{param.Latitude}");
            department.Longitude = param.Longitude;
            department.Latitude = param.Latitude;
            department.LocationName = param.LocationName;
            department.InfoUrl = param.InfoUrl;
            department.LastUpdateDate = DateTime.Now;
            department.LastUpdateUser = param.UpdateUser;
            department.LastUpdateUserID = param.UpdateUserID;
            return Repository.Update(department) > 0;
        }

        /// <summary>
        /// 获取门店地理位置
        /// </summary>
        /// <returns></returns>
        public DepartmentLocationDto GetDepartmentLocation()
        {
            var merchantid = AppContext.CurrentSession.MerchantID;
            if (merchantid == null)
                return null;
            var department = Repository.GetQueryable().FirstOrDefault(t => t.MerchantID == merchantid);
            if (department != null)
            {
                return new DepartmentLocationDto
                {
                    Longitude = department.Longitude,
                    Latitude = department.Latitude,
                    LocationName = department.LocationName,
                    InfoUrl = department.InfoUrl,

                };
            }
            return null;
        }

        #endregion
    }
}
