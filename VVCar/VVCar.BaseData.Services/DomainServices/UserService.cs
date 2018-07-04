using System;
using System.Collections.Generic;
using System.Linq;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Filters;
using VVCar.Shop.Domain.Services;
using VVCar.Shop.Domain.Entities;
using VVCar.VIP.Domain.Entities;

namespace VVCar.BaseData.Services.DomainServices
{
    public partial class UserService : DomainServiceBase<IRepository<User>, User, Guid>, IUserService
    {
        public UserService()
        {
        }

        #region properties

        IPermissionFuncService PermissionFuncService
        {
            get
            {
                return ServiceLocator.Instance.GetService<IPermissionFuncService>();
            }
        }

        IUserRoleService _UserRoleService;

        /// <summary>
        /// UserRole 领域服务
        /// </summary>
        IUserRoleService UserRoleService
        {
            get
            {
                if (_UserRoleService == null)
                    _UserRoleService = ServiceLocator.Instance.GetService<IUserRoleService>();
                return _UserRoleService;
            }
        }

        IReportingService ReportingService { get => ServiceLocator.Instance.GetService<IReportingService>(); }

        IRepository<PickUpOrder> PickUpOrderRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrder>>(); }

        IRepository<Appointment> AppointmentRepo { get => UnitOfWork.GetRepository<IRepository<Appointment>>(); }

        IRepository<User> UserRepo { get => UnitOfWork.GetRepository<IRepository<User>>(); }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        IRepository<CouponTemplate> CouponTemplateRepo { get => UnitOfWork.GetRepository<IRepository<CouponTemplate>>(); }

        IRepository<Merchant> MerchantRepo { get => UnitOfWork.GetRepository<IRepository<Merchant>>(); }

        #endregion

        #region methods

        public override User Add(User entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.Password = Util.EncryptPassword(entity.Code, entity.Password);
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public override bool Delete(Guid key)
        {
            var user = this.Repository.GetByKey(key);
            if (user == null)
                throw new DomainException("删除失败，数据不存在");
            if (user.DataSource == YEF.Core.Enums.EDataSource.External)
                throw new DomainException("该数据不允许删除");
            user.IsDeleted = true;
            return this.Repository.Update(user) > 0;
        }

        public override bool Update(User entity)
        {
            if (entity == null)
                return false;
            var user = this.Repository.GetByKey(entity.ID);
            if (user.DataSource == YEF.Core.Enums.EDataSource.Default)//如果是外部数据不允许修改部分字段
            {
                user.Code = entity.Code;
                user.Name = entity.Name;
                user.DepartmentID = entity.DepartmentID;
                user.MobilePhoneNo = entity.MobilePhoneNo;
                if (user.Password != entity.Password)
                {
                    user.Password = Util.EncryptPassword(entity.Code, entity.Password);
                }
            }
            user.Sex = entity.Sex;
            user.PhoneNo = entity.PhoneNo;
            user.EmailAddress = entity.EmailAddress;
            user.IsAvailable = entity.IsAvailable;
            user.CanLoginAdminPortal = entity.CanLoginAdminPortal;
            user.DutyTime = entity.DutyTime;
            user.Age = entity.Age;
            user.BasicSalary = entity.BasicSalary;
            if (user.AuthorityCard != entity.AuthorityCard)
            {
                if (string.IsNullOrEmpty(entity.AuthorityCard))
                    user.AuthorityCard = string.Empty;
                else
                    user.AuthorityCard = Util.EncryptPassword(AppContext.SuperScanCardAccount, entity.AuthorityCard);
            }
            user.LastUpdateUserID = AppContext.CurrentSession.UserID;
            user.LastUpdateUser = AppContext.CurrentSession.UserName;
            user.LastUpdateDate = DateTime.Now;
            return base.Update(user);
        }

        public override User Get(Guid key)
        {
            return this.Repository.GetIncludes(false, "UserRoles", "UserRoles.Role").FirstOrDefault(t => t.ID == key);
        }

        protected override bool DoValidate(User entity)
        {
            bool exists = this.Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (exists)
                throw new DomainException(String.Format("代码 {0} 已使用，不能重复添加。", entity.Code));
            return true;
        }

        #endregion

        #region IUserService 成员

        public UserInfoDto Login(string account, string password, string companycode)
        {
            if (string.IsNullOrEmpty(companycode))
                throw new DomainException("商户号不能为空");
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
                throw new DomainException("用户名或密码不能为空");

            var merchant = MerchantRepo.GetQueryable(false).FirstOrDefault(t => t.Code == companycode);
            if (merchant == null)
                throw new DomainException("商户不存在");
            if (merchant.Status == YEF.Core.Enums.EMerchantStatus.UnActivate)
                throw new ExecutionEngineException("商户未激活");
            if (merchant.Status == YEF.Core.Enums.EMerchantStatus.Freeze)
                throw new ExecutionEngineException("商户已冻结");

            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == merchant.ID);
            //if (AppContext.SuperScanCardAccount.Equals(account))
            //{
            //    queryable = queryable.Where(t => t.AuthorityCard == password);
            //}
            //else
            //{
            queryable = queryable.Where(t => t.Code == account && t.Password == password);
            //}
            var user = queryable.Select(t => new UserInfoDto
            {
                ID = t.ID,
                Code = t.Code,
                Name = t.Name,
                IsAvailable = t.IsAvailable,
                CanLoginAdminPortal = t.CanLoginAdminPortal,
                DepartmentID = t.DepartmentID,
                DepartmentCode = t.Department.Code,
                DepartmentName = t.Department.Name,
                MerchantID = t.MerchantID,
            }).FirstOrDefault();
            if (user == null)
                throw new DomainException("用户名或密码不正确");
            if (!user.IsAvailable)
                throw new DomainException("没有权限");
            return user;
        }

        public UserInfoDto WeChatLogin(WeChatLoginParams param)
        {
            var password = Util.EncryptPassword(param.UserName, param.Password);
            var result = Login(param.UserName, password, param.MerchantCode);

            var user = Repository.GetInclude(t => t.UserRoles, false).Where(t => t.ID == result.ID).FirstOrDefault();
            if (user != null)
            {
                var userRoles = user.UserRoles;
                if (param.IsManager)
                {
                    var ismanager = false;
                    foreach (var role in userRoles)
                    {
                        if (role.RoleID == Guid.Parse("00000000-0000-0000-0000-000000000002"))
                        {
                            ismanager = true;
                            break;
                        }
                    }
                    if (!ismanager)
                        throw new DomainException("非店长登录");
                }
                else
                {
                    var isstaff = false;
                    foreach (var role in userRoles)
                    {
                        if (role.RoleID == Guid.Parse("00000000-0000-0000-0000-000000000003"))
                        {
                            isstaff = true;
                            break;
                        }
                    }
                    if (!isstaff)
                        throw new DomainException("非店员登录");
                }
                if (!string.IsNullOrEmpty(param.OpenID) && param.OpenID != user.OpenID)
                {
                    user.OpenID = param.OpenID;
                    base.Update(user);
                }
            }

            return result;
        }

        public UserInfoDto GetUserByOpenID(WeChatLoginParams param)
        {
            var users = Repository.GetInclude(t => t.UserRoles, false).Where(t => t.OpenID == param.OpenID && t.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
            User user = null;
            if (users != null && users.Count > 0)
            {
                var userRoles = new List<UserRole>();
                users.ForEach(t =>
                {
                    userRoles.AddRange(t.UserRoles);
                });
                UserRole userrole = null;
                if (param.IsManager)
                    userrole = userRoles.Where(t => t.RoleID == Guid.Parse("00000000-0000-0000-0000-000000000002")).FirstOrDefault();
                else
                    userrole = userRoles.Where(t => t.RoleID == Guid.Parse("00000000-0000-0000-0000-000000000003")).FirstOrDefault();
                if (userrole == null)
                    return null;
                else
                {
                    user = users.FirstOrDefault(t => t.ID == userrole.UserID);
                    if (user == null)
                        return null;
                }
                if (user != null && !user.IsAvailable)
                    throw new DomainException("没有权限");
            }

            if (user != null)
            {
                var result = user.MapTo<UserInfoDto>();

                //var staffPerformance = ReportingService.GetStaffPerformance(user.ID, null);

                var totalCount = 0;
                var usersPerformance = GetUsers(new UserFilter { All = false }, ref totalCount).ToList();
                var userPerformance = usersPerformance.FirstOrDefault(t => t.ID == user.ID);

                result.TotalPerformance = userPerformance.TotalPerformance;
                result.MonthPerformance = userPerformance.MonthPerformance;
                result.TotalCommission = userPerformance.TotalCommission;
                result.MonthCommission = userPerformance.MonthCommission;
                result.Subsidy = userPerformance.Subsidy;
                result.BasicSalary = userPerformance.BasicSalary;
                result.CustomerServiceCount = userPerformance.CustomerServiceCount;
                result.MonthCustomerServiceCount = userPerformance.MonthCustomerServiceCount;
                result.PerformanceRanking = userPerformance.PerformanceRanking;
                result.MonthIncome = userPerformance.MonthIncome;

                var starttime = DateTime.Now.Date;
                var endtime = starttime.AddDays(1);
                result.DailyPickUpOrderCount = PickUpOrderRepo.GetQueryable(false).Count(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedDate >= starttime && t.CreatedDate < endtime);
                result.CustomerAppointmentCount = AppointmentRepo.GetQueryable(false).Count(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
                result.StaffCount = UserRepo.GetQueryable(false).Count(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
                result.MemberCount = MemberRepo.GetQueryable(false).Count(t => t.MerchantID == AppContext.CurrentSession.MerchantID);

                var couponTemplateStock = CouponTemplateRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.Nature == VIP.Domain.Enums.ENature.Card).Select(t => t.Stock).GroupBy(g => 1).Select(t => t.Sum(s => s.Stock)).FirstOrDefault();
                result.MemberCardStockCount = couponTemplateStock;

                return result;
            }

            return null;
        }

        public UserInfoDto SsoLogin(string userCode, string password, string companycode)
        {
            if (string.IsNullOrEmpty(companycode))
                throw new DomainException("商户号不能为空");
            if (string.IsNullOrEmpty(userCode) || string.IsNullOrEmpty(password))
                throw new DomainException("用户名或密码不能为空");
            var merchant = MerchantRepo.GetQueryable(false).FirstOrDefault(t => t.Code == companycode);
            if (merchant == null)
                throw new DomainException("商户不存在");
            if (merchant.Status == YEF.Core.Enums.EMerchantStatus.UnActivate)
                throw new ExecutionEngineException("商户未激活");
            if (merchant.Status == YEF.Core.Enums.EMerchantStatus.Freeze)
                throw new ExecutionEngineException("商户已冻结");
            var queryable = Repository.GetQueryable(false).Where(t => t.Code == userCode && t.MerchantID == merchant.ID);
            var user = queryable.Select(t => new UserInfoDto
            {
                ID = t.ID,
                Code = t.Code,
                Name = t.Name,
                IsAvailable = t.IsAvailable,
                CanLoginAdminPortal = t.CanLoginAdminPortal,
                DepartmentID = t.DepartmentID,
                DepartmentCode = t.Department.Code,
                DepartmentName = t.Department.Name,
                MerchantID = t.MerchantID,
            }).FirstOrDefault();
            if (user == null)
                throw new DomainException("用户名或密码不正确");
            if (!user.IsAvailable)
                throw new DomainException("没有权限");
            return user;
        }

        public bool ChangePassword(Guid userID, string oldPassword, string password)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(password))
                throw new DomainException("密码不能为空");
            var user = this.Repository.GetByKey(userID);
            if (user == null)
                throw new DomainException("修改密码失败，用户不存在");
            oldPassword = Util.EncryptPassword(user.Code, oldPassword);
            if (!user.Password.Equals(oldPassword, StringComparison.OrdinalIgnoreCase))
                throw new DomainException("修改密码失败，原密码不正确");

            user.Password = Util.EncryptPassword(user.Code, password);
            return this.Repository.Update(user) > 0;
        }

        public PagedResultDto<User> QueryUser(Domain.Filters.UserFilter filter)
        {
            var result = new PagedResultDto<User>();
            var queryable = this.Repository.GetQueryable(false).Where(p => !p.IsDeleted).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (AppContext.CurrentSession.UserID != Guid.Parse("00000000-0000-0000-0000-000000000001"))
            {
                var adminId = Guid.Parse("00000000-0000-0000-0000-000000000001");
                queryable = queryable.Where(t => t.ID != adminId);
            }
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Code))
                    queryable = queryable.Where(t => t.Code.Contains(filter.Code));
                if (!string.IsNullOrEmpty(filter.Name))
                    queryable = queryable.Where(t => t.Name.Contains(filter.Name));
                if (filter.Department.HasValue)
                    queryable = queryable.Where(t => t.DepartmentID == filter.Department);
            }
            result.TotalCount = queryable.Count();
            queryable = queryable.OrderBy(t => t.Code);
            if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            result.Items = queryable.ToArray();
            return result;
        }

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<UserInfoDto> GetUsers(UserFilter filter, ref int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Code))
                queryable = queryable.Where(t => t.Code.Contains(filter.Code));
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            if (filter.Department.HasValue)
                queryable = queryable.Where(t => t.DepartmentID == filter.Department);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.OrderBy(t => t.Code).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            var result = queryable.OrderBy(t => t.Code).ToArray().MapTo<List<UserInfoDto>>();
            result.ForEach(t =>
            {
                var staffPerformance = ReportingService.GetStaffPerformance(t.ID, null);

                t.TotalPerformance = staffPerformance.TotalPerformance;
                t.MonthPerformance = staffPerformance.MonthPerformance;
                t.TotalCommission = staffPerformance.TotalCommission;
                t.MonthCommission = staffPerformance.MonthCommission;
                t.Subsidy = staffPerformance.Subsidy;
                t.BasicSalary = staffPerformance.BasicSalary;
                t.CustomerServiceCount = staffPerformance.CustomerServiceCount;
                t.MonthCustomerServiceCount = staffPerformance.MonthCustomerServiceCount;
                t.MonthIncome = staffPerformance.MonthIncome;
            });
            result = result.OrderByDescending(t => t.MonthPerformance).ToList();
            var ranking = 1;
            result.ForEach(t =>
            {
                t.PerformanceRanking = ranking;
                ranking++;
            });
            return result;
        }

        /// <summary>
        /// 更改是否可用
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ChangeAvailable(Guid userId)
        {
            var user = Repository.GetByKey(userId);
            if (user == null)
                throw new DomainException("用户不存在");
            user.IsAvailable = !user.IsAvailable;
            return Repository.Update(user) > 0;
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteUsers(Guid[] ids)
        {
            UnitOfWork.BeginTransaction();
            try
            {
                foreach (var id in ids)
                {
                    Delete(id);
                }
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException("批量删除失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 批量重置密码
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool ResetPwds(Guid[] ids)
        {
            UnitOfWork.BeginTransaction();
            try
            {
                foreach (var id in ids)
                {
                    var user = Get(id);
                    user.Password = Util.EncryptPassword(user.Code, "123456");
                    base.Update(user);
                }
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException("批量重置密码失败：" + ex.Message);
            }
        }

        public IEnumerable<AssignPermissionDto> GetUserFuncPermissionList(Guid userID)
        {
            IRepository<UserRole> userRoleRepo = ServiceLocator.Instance.GetService<IRepository<UserRole>>();
            var roles = userRoleRepo.GetQueryable(false)
                .Where(t => t.UserID == userID)
                .Select(t => t.Role.Code)
                .ToArray();
            if (roles.Length < 1)
                return null;
            return PermissionFuncService.GetRolePermissionList(roles);
        }

        public IEnumerable<IDCodeNameDto> GetUserNameCollect(Guid? departmentID)
        {
            var queryable = this.Repository.GetQueryable(false);
            if (departmentID.HasValue)
            {
                queryable = queryable.Where(user => user.DepartmentID == departmentID);
            }
            return queryable.Select(t => new IDCodeNameDto
            {
                ID = t.ID,
                Code = t.Code,
                Name = t.Name,
            }).ToArray();
        }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsAdmin(Guid userID)
        {
            IRepository<UserRole> userRoleRepo = ServiceLocator.Instance.GetService<IRepository<UserRole>>();
            return userRoleRepo.Exists(t => t.UserID == userID && t.Role.IsAdmin);
        }

        /// <summary>
        /// 新增人员信息，商户接口
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public User AddMchUser(User newUser)
        {
            if (newUser == null)
                return null;
            newUser.ID = Util.NewID();
            newUser.CreatedUserID = AppContext.CurrentSession.UserID;
            newUser.CreatedUser = AppContext.CurrentSession.UserName;
            newUser.CreatedDate = DateTime.Now;
            newUser.DataSource = YEF.Core.Enums.EDataSource.External;
            return base.Add(newUser);
        }

        /// <summary>
        /// 更新人员信息，商户接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateMchUser(MchUserDto userDto)
        {
            if (userDto == null)
                return false;
            var user = Repository.Get(t => t.Code == userDto.UserCode);
            if (user == null)
                throw new DomainException("数据不存在");
            user.Code = userDto.PhoneNumber;
            user.MobilePhoneNo = userDto.PhoneNumber;
            user.Password = userDto.Password;
            Repository.Update(user);
            return true;
        }

        /// <summary>
        /// 设置超级管理员
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public bool SetSuperAdmin(MchUserDto userDto)
        {
            var superAdmin = Repository.Get(t => t.Code == userDto.UserCode);
            if (superAdmin == null)
            {
                var newUser = new User
                {
                    ID = Util.NewID(),
                    Code = userDto.UserCode,
                    Name = userDto.UserName,
                    DepartmentID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    MobilePhoneNo = userDto.PhoneNumber,
                    IsAvailable = true,
                    CanLoginAdminPortal = true,
                    Password = userDto.Password,
                    CreatedUserID = Guid.Empty,
                    CreatedUser = string.Empty,
                    CreatedDate = DateTime.Now,
                    DataSource = YEF.Core.Enums.EDataSource.External
                };
                superAdmin = AddMchUser(newUser);
            }
            var adminRoleId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var roleExist = UserRoleService.Exists(t => t.UserID == superAdmin.ID && t.RoleID == adminRoleId);
            if (!roleExist)
            {
                var userRole = new UserRole
                {
                    UserID = superAdmin.ID,
                    RoleID = adminRoleId,
                };
                UserRoleService.Add(userRole);
            }
            return true;
        }

        public List<User> GetManagerUser()
        {
            return Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
        }

        public bool BindingMobilePhone(BindingMobilePhoneParam param)
        {
            if (param == null || param.ID == null || string.IsNullOrEmpty(param.MobilePhoneNo))
                throw new DomainException("参数错误");
            var user = Repository.GetByKey(param.ID);
            if (user == null)
                return false;
            user.MobilePhoneNo = param.MobilePhoneNo;
            return Repository.Update(user) > 0;
        }

        #endregion
    }
}
