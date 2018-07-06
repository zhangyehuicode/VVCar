using System;
using System.Collections.Generic;
using System.Linq;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Enums;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using VVCar.VIP.Domain.Entities;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using YEF.Core.Enums;

namespace VVCar.BaseData.Services.DomainServices
{
    /// <summary>
    /// 商户领域服务
    /// </summary>
    public class MerchantService : DomainServiceBase<IRepository<Merchant>, Merchant, Guid>, IMerchantService
    {
        public MerchantService()
        {
        }

        #region properties

        IRepository<Department> DepartmentRepo { get => UnitOfWork.GetRepository<IRepository<Department>>(); }

        IRepository<User> UserRepo { get => UnitOfWork.GetRepository<IRepository<User>>(); }

        IRepository<UserRole> UserRoleRepo { get => UnitOfWork.GetRepository<IRepository<UserRole>>(); }

        IRepository<SystemSetting> SystemSettingRepo { get => UnitOfWork.GetRepository<IRepository<SystemSetting>>(); }

        IRepository<GameSetting> GameSettingRepo { get => UnitOfWork.GetRepository<IRepository<GameSetting>>(); }

        #endregion

        protected override bool DoValidate(Merchant entity)
        {
            if (Repository.Exists(t => (t.Code == entity.Code || t.Name == entity.Name) && t.ID != entity.ID))
            {
                throw new DomainException("商户号或商户名称重复");
            }
            var merchant = Repository.GetByKey(entity.ID, false);
            if (merchant != null)
            {
                if (merchant.Code != entity.Code)
                    throw new DomainException("商户号不允许修改");
            }
            return true;
        }

        /// <summary>
        /// 生成9位随机商户号
        /// </summary>
        /// <returns></returns>
        public string GenerateCode()
        {
            var code = string.Empty;
            for (var i = 0; i < 9; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                code += r.Next(0, 10);
            }
            return code;
        }

        /// <summary>
        /// 生成商户号
        /// </summary>
        /// <returns></returns>
        public string GenerateMerchantCode()
        {
            var count = 0;
            var code = GenerateCode();
            var exists = Repository.Exists(t => t.Code == code);
            while (exists && count < 20)
            {
                code = GenerateCode();
                exists = Repository.Exists(t => t.Code == code);
                count++;
            }
            return code;
        }

        public override Merchant Add(Merchant entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.IsHQ = false;
            entity.Code = GenerateMerchantCode();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = Guid.Parse("00000000-0000-0000-0000-000000000001");
            return base.Add(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                return false;
            entity.IsDeleted = true;
            entity.LastUpdatedDate = DateTime.Now;
            entity.LastUpdatedUserID = AppContext.CurrentSession.UserID;
            entity.LastUpdatedUser = AppContext.CurrentSession.UserName;
            return Repository.Update(entity) > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(Merchant entity)
        {
            if (entity == null)
                return false;
            var merchant = Repository.GetByKey(entity.ID);
            if (merchant == null)
                return false;
            merchant.Name = entity.Name;
            merchant.LegalPerson = entity.LegalPerson;
            merchant.IDNumber = entity.IDNumber;
            merchant.Email = entity.Email;
            merchant.WeChatOAPassword = entity.WeChatOAPassword;
            merchant.MobilePhoneNo = entity.MobilePhoneNo;
            merchant.BusinessLicenseImgUrl = entity.BusinessLicenseImgUrl;
            merchant.LegalPersonIDCardFrontImgUrl = entity.LegalPersonIDCardFrontImgUrl;
            merchant.LegalPersonIDCardBehindImgUrl = entity.LegalPersonIDCardBehindImgUrl;
            merchant.CompanyAddress = entity.CompanyAddress;
            merchant.WeChatAppID = entity.WeChatAppID;
            merchant.WeChatAppSecret = entity.WeChatAppSecret;
            merchant.MeChatMchPassword = entity.MeChatMchPassword;
            merchant.IsAgent = entity.IsAgent;
            merchant.IsGeneralMerchant = entity.IsGeneralMerchant;
            merchant.WeChatMchID = entity.WeChatMchID;
            merchant.WeChatMchKey = entity.WeChatMchKey;
            merchant.Bank = entity.Bank;
            merchant.BankCard = entity.BankCard;
            merchant.LastUpdatedDate = DateTime.Now;
            merchant.LastUpdatedUserID = AppContext.CurrentSession.UserID;
            merchant.LastUpdatedUser = AppContext.CurrentSession.UserName;
            return Repository.Update(merchant) > 0;
        }

        /// <summary>
        /// 激活商户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool ActivateMerchant(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var merchantList = this.Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (merchantList == null || merchantList.Count() < 1)
                throw new DomainException("数据不存在");
            UnitOfWork.BeginTransaction();
            try
            {
                merchantList.ForEach(t =>
                {
                    var existsDept = DepartmentRepo.Exists(d => d.MerchantID == t.ID);
                    if (!existsDept)
                    {
                        var dept = DepartmentRepo.Add(new Department
                        {
                            ID = Util.NewID(),
                            Code = $"{t.Code}001",
                            Name = t.Name,
                            DistrictRegion = "",
                            AdministrationRegion = "",
                            CreatedUserID = AppContext.CurrentSession.UserID,
                            CreatedUser = AppContext.CurrentSession.UserName,
                            CreatedDate = DateTime.Now,
                            MerchantID = t.ID,
                        });

                        var user = UserRepo.Add(new User
                        {
                            ID = Util.NewID(),
                            Code = "admin",
                            Name = "管理员",
                            DepartmentID = dept.ID,
                            IsAvailable = true,
                            CanLoginAdminPortal = true,
                            Password = Util.EncryptPassword("admin", $"{t.Code}@{DateTime.Now.ToString("yyyyMMdd")}"),//"AD0A291E6FCB621F836866D99F62D2C0",
                            CreatedUserID = AppContext.CurrentSession.UserID,
                            CreatedUser = AppContext.CurrentSession.UserName,
                            CreatedDate = DateTime.Now,
                            MerchantID = t.ID,
                        });

                        if (t.IsGeneralMerchant)
                        {
                            UserRoleRepo.Add(new UserRole
                            {
                                ID = Util.NewID(),
                                UserID = user.ID,
                                RoleID = Guid.Parse("00000000-0000-0000-0000-000000000002"),//店长
                                CreatedUserID = AppContext.CurrentSession.UserID,
                                CreatedUser = AppContext.CurrentSession.UserName,
                                CreatedDate = DateTime.Now,
                                MerchantID = t.ID,
                            });
                        }

                        if (t.IsAgent)
                        {
                            UserRoleRepo.Add(new UserRole
                            {
                                ID = Util.NewID(),
                                UserID = user.ID,
                                RoleID = Guid.Parse("00000000-0000-0000-0000-000000000006"),//总经理
                                CreatedUserID = AppContext.CurrentSession.UserID,
                                CreatedUser = AppContext.CurrentSession.UserName,
                                CreatedDate = DateTime.Now,
                                MerchantID = t.ID,
                            });
                        }

                        ActivateMerchantDataOperation(t.ID);
                    }
                    t.Status = EMerchantStatus.Activated;
                });
                this.Repository.UpdateRange(merchantList);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        private bool ActivateMerchantDataOperation(Guid merchantId)
        {
            if (merchantId == null)
                return false;
            SystemSettingRepo.Add(new SystemSetting
            {
                ID = Util.NewID(),
                Index = 1,
                Name = SysSettingTypes.WXMsg_CouponWillExpire,
                Caption = "优惠券即将过期微信通知消息模板",
                DefaultValue = string.Empty,
                SettingValue = string.Empty,
                IsVisible = true,
                IsAvailable = true,
                Type = ESystemSettingType.Parameter,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
                MerchantID = merchantId,
            });
            SystemSettingRepo.Add(new SystemSetting
            {
                ID = Util.NewID(),
                Index = 2,
                Name = SysSettingTypes.WXMsg_OrderSuccess,
                Caption = "下单成功通知消息模板",
                DefaultValue = string.Empty,
                SettingValue = string.Empty,
                IsVisible = true,
                IsAvailable = true,
                Type = ESystemSettingType.Parameter,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
                MerchantID = merchantId,
            });
            SystemSettingRepo.Add(new SystemSetting
            {
                ID = Util.NewID(),
                Index = 3,
                Name = SysSettingTypes.WXMsg_ReceivedSuccess,
                Caption = "礼品领取成功通知消息模板",
                DefaultValue = string.Empty,
                SettingValue = string.Empty,
                IsVisible = true,
                IsAvailable = true,
                Type = ESystemSettingType.Parameter,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
                MerchantID = merchantId,
            });
            SystemSettingRepo.Add(new SystemSetting
            {
                ID = Util.NewID(),
                Index = 4,
                Name = SysSettingTypes.WXMsg_AppointmentRemind,
                Caption = "预约提醒通知消息模板",
                DefaultValue = string.Empty,
                SettingValue = string.Empty,
                IsVisible = true,
                IsAvailable = true,
                Type = ESystemSettingType.Parameter,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
                MerchantID = merchantId,
            });
            SystemSettingRepo.Add(new SystemSetting
            {
                ID = Util.NewID(),
                Index = 5,
                Name = SysSettingTypes.WXMsg_AppointmentSuccess,
                Caption = "预约成功通知消息模板",
                DefaultValue = string.Empty,
                SettingValue = string.Empty,
                IsVisible = true,
                IsAvailable = true,
                Type = ESystemSettingType.Parameter,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
                MerchantID = merchantId,
            });
            SystemSettingRepo.Add(new SystemSetting
            {
                ID = Util.NewID(),
                Index = 6,
                Name = SysSettingTypes.WXMsg_VerificationSuccess,
                Caption = "核销成功通知消息模板",
                DefaultValue = string.Empty,
                SettingValue = string.Empty,
                IsVisible = true,
                IsAvailable = true,
                Type = ESystemSettingType.Parameter,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
                MerchantID = merchantId,
            });
            SystemSettingRepo.Add(new SystemSetting
            {
                ID = Util.NewID(),
                Index = 7,
                Name = SysSettingTypes.WXMsg_ServiceExpiredRemind,
                Caption = "服务到期提醒消息模板",
                DefaultValue = string.Empty,
                SettingValue = string.Empty,
                IsVisible = true,
                IsAvailable = true,
                Type = ESystemSettingType.Parameter,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
                MerchantID = merchantId,
            });
            SystemSettingRepo.Add(new SystemSetting
            {
                ID = Util.NewID(),
                Index = 8,
                Name = SysSettingTypes.WXMsg_OrderRemind,
                Caption = "新订单提醒消息模板",
                DefaultValue = string.Empty,
                SettingValue = string.Empty,
                IsVisible = true,
                IsAvailable = true,
                Type = ESystemSettingType.Parameter,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
                MerchantID = merchantId,
            });
            GameSettingRepo.Add(new GameSetting
            {
                ID = Util.NewID(),
                GameType = VIP.Domain.Enums.EGameType.AttractWheel,
                PeriodDays = 0,
                PeriodCounts = 0,
                Limit = 0,
                IsShare = false,
                ShareTitle = "拓客转盘",
                IsOrderShow = false,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
                MerchantID = merchantId,
            });
            GameSettingRepo.Add(new GameSetting
            {
                ID = Util.NewID(),
                GameType = VIP.Domain.Enums.EGameType.ActivityWheel,
                PeriodDays = 0,
                PeriodCounts = 0,
                Limit = 0,
                IsShare = false,
                ShareTitle = "活动转盘",
                IsOrderShow = false,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                CreatedUserID = AppContext.CurrentSession.UserID,
                CreatedUser = AppContext.CurrentSession.UserName,
                CreatedDate = DateTime.Now,
                MerchantID = merchantId,
            });
            return true;
        }

        /// <summary>
        /// 冻结商户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool FreezeMerchant(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var merchantList = this.Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (merchantList.Count() < 1)
                throw new DomainException("数据不存在");
            merchantList.ForEach(t =>
            {
                t.Status = EMerchantStatus.Freeze;
            });
            return this.Repository.UpdateRange(merchantList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<Merchant> Search(MerchantFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            if (filter.Status.HasValue)
                queryable = queryable.Where(t => t.Status == filter.Status.Value);
            if (filter.ID.HasValue)
                queryable = queryable.Where(t => t.ID == filter.ID.Value);
            if (!string.IsNullOrEmpty(filter.Code))
                queryable = queryable.Where(t => t.Code.Contains(filter.Code));
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
