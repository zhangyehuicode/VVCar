using System;
using System.Data.Entity;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Enums;
using YEF.Core;
using YEF.Core.Data;
using YEF.Data.Initializer;

namespace VVCar.BaseData.Data
{
    /// <summary>
    /// 创建数据库初始化设置种子数据
    /// </summary>
    public class CreateDBSeedAction : ICreateDBSeedAction
    {
        const string _systemUserName = "system";

        /// <summary>
        /// 操作顺序，数值越小越先执行
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Order
        {
            get { return 0; }
        }

        /// <summary>
        /// 向上下文添加种子数据
        /// </summary>
        /// <param name="context">数据上下文</param>
        public void Seed(DbContext context)
        {
            //SeedDictType(context);
            //SeedDictValue(context);
            SeedDepartment(context);
            SeedUser(context);
            SeedRole(context);
            SeedUserRole(context);
            SeedSysMenu(context);
            //SeedSystemSetting(context);
            //SeedPermissionFunc(context);
            //SeedMakeCodeRule(context);
            SeedMemberCardType(context);
            //SeedMemberGrade(context);
            //SeedMemberGroup(context);
            //SeedMemberCardTheme(context);
            //SeedCardThemeCategory(context);
        }

        void SeedMerchant(DbContext context)
        {
            var merchantSet = context.Set<Merchant>();
            merchantSet.Add(new Merchant
            {
                ID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Code = "VVCar",
                Name = "VVCar",
                IsHQ = true,
                CreatedDate = DateTime.Now,
            });
        }

        //void SeedDictType(DbContext context)
        //{
        //    var dictTypeSet = context.Set<DataDictType>();
        //    dictTypeSet.Add(new DataDictType { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Code = "RoleType", Name = "角色类型", Index = 1 });
        //    dictTypeSet.Add(new DataDictType { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Code = "DistrictRegion", Name = "地区分区", Index = 2 });
        //    dictTypeSet.Add(new DataDictType { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Code = "AdministrationRegion", Name = "管理分区", Index = 3 });
        //}

        //void SeedDictValue(DbContext context)
        //{
        //    var dictValueSet = context.Set<DataDictValue>();
        //    //RoleType
        //    dictValueSet.Add(new DataDictValue { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), DictType = "RoleType", DictValue = "Admin", DictName = "管理员", Index = 1, IsAvailable = false });

        //    //DistrictRegion
        //    dictValueSet.Add(new DataDictValue { ID = Guid.Parse("00000000-0000-0000-0000-000000000100"), DictType = "DistrictRegion", DictValue = "HQ", DictName = "总部", Index = 1, IsAvailable = true });

        //    //AdministrationRegion
        //    dictValueSet.Add(new DataDictValue { ID = Guid.Parse("00000000-0000-0000-0000-000000000200"), DictType = "AdministrationRegion", DictValue = "HQ", DictName = "总部", Index = 1, IsAvailable = true });
        //}

        void SeedDepartment(DbContext context)
        {
            var departmentSet = context.Set<Department>();
            departmentSet.Add(new Department
            {
                ID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Code = "ZB001",
                Name = "总部",
                DistrictRegion = "ZB",
                AdministrationRegion = "ZB",
                CreatedUserID = Guid.Empty,
                CreatedUser = _systemUserName,
                CreatedDate = DateTime.Now,
                MerchantID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            });
        }

        void SeedUser(DbContext context)
        {
            if (AppContext.Settings.IsDynamicCompany)
                return;
            var userSet = context.Set<User>();
            userSet.Add(new User
            {
                ID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Code = "admin",
                Name = "系统管理员",
                DepartmentID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAvailable = true,
                CanLoginAdminPortal = true,
                Password = "AD0A291E6FCB621F836866D99F62D2C0",
                CreatedUserID = Guid.Empty,
                CreatedUser = _systemUserName,
                CreatedDate = DateTime.Now,
                MerchantID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            });
        }

        void SeedRole(DbContext context)
        {
            var roleSet = context.Set<Role>();
            roleSet.Add(new Role
            {
                ID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Code = "admin",
                Name = "超级管理员",
                RoleType = "Admin",
                IsAdmin = true,
                CreatedUserID = Guid.Empty,
                CreatedUser = _systemUserName,
                CreatedDate = DateTime.Now
            });
        }

        void SeedUserRole(DbContext context)
        {
            if (AppContext.Settings.IsDynamicCompany)
                return;
            var userRoleSet = context.Set<UserRole>();
            userRoleSet.Add(new UserRole
            {
                ID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UserID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                RoleID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedUserID = Guid.Empty,
                CreatedUser = _systemUserName,
                CreatedDate = DateTime.Now
            });
        }

        void SeedSysMenu(DbContext context)
        {
            var sysMenuSet = context.Set<SysMenu>();
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "会员管理", Component = "MemberManage", SysMenuUrl = "MemberManage", ParentID = null, IsLeaf = false, Type = ESysMenuType.Component, IsAvailable = true, Index = 1 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "卡片制作", Component = "MemberCard", SysMenuUrl = "MemberCard", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000001"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 1 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "卡片类型管理", Component = "MemberCardType", SysMenuUrl = "MemberCardTypeList", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000001"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 2 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "会员信息", Component = "Member", SysMenuUrl = "Member", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000001"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 2 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "会员激活", Component = "MemberCardActivate", SysMenuUrl = "MemberCardActivate", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000001"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 4 });

            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000100"), Name = "会员营销", Component = "MemberMarketing", SysMenuUrl = "MemberMarketing", ParentID = null, IsLeaf = false, Type = ESysMenuType.Component, IsAvailable = true, Index = 2 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "储值方案", Component = "RechargePlan", SysMenuUrl = "RechargePlanList", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000001"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 3 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000102"), Name = "定期推送", Component = "/CouponAdmin/CouponPush", SysMenuUrl = "/CouponAdmin/CouponPush", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000100"), IsLeaf = true, Type = ESysMenuType.Path, IsAvailable = true, Index = 2 });

            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000200"), Name = "POS功能", Component = "PosCommand", SysMenuUrl = "PosCommand", ParentID = null, IsLeaf = false, Type = ESysMenuType.Component, IsAvailable = true, Index = 3 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000201"), Name = "会员储值", Component = "MemberRecharge", SysMenuUrl = "MemberRecharge", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000200"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 1 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000202"), Name = "会员消费", Component = "MemberTrade", SysMenuUrl = "MemberTrade", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000200"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 2 });

            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000300"), Name = "分析统计", Component = "Report", SysMenuUrl = "Report", ParentID = null, IsLeaf = false, Type = ESysMenuType.Component, IsAvailable = true, Index = 4 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "会员储值记录", Component = "MemberRecharge", SysMenuUrl = "RechargeHistory", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000001"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 4 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "会员消费记录", Component = "MemberTrade", SysMenuUrl = "TradeHistory", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000001"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 5 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000303"), Name = "会员储值统计", Component = "RechargeReport", SysMenuUrl = "RechargeReport", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000300"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 3 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000304"), Name = "会员消费统计", Component = "ConsumeReport", SysMenuUrl = "ConsumeReport", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000300"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 4 });

            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000400"), Name = "基础信息设置", Component = "BaseData", SysMenuUrl = "BaseData", ParentID = null, IsLeaf = false, Type = ESysMenuType.Component, IsAvailable = true, Index = 5 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000401"), Name = "数据字典", Component = "DataDict", SysMenuUrl = "DataDictList", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000400"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 1 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000401"), Name = "门店管理", Component = "Department", SysMenuUrl = "DepartmentList", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000400"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 2 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000403"), Name = "系统参数设置", Component = "SystemSetting", SysMenuUrl = "SystemSetting", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000400"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 3 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000404"), Name = "单据编码规则", Component = "MakeCodeRule", SysMenuUrl = "MakeCodeRule", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000400"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 4 });

            sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000500"), Name = "系统管理", Component = "SystemManage", SysMenuUrl = "SystemManage", ParentID = null, IsLeaf = false, Type = ESysMenuType.Component, IsAvailable = true, Index = 6 });
            sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000501"), Name = "用户管理", Component = "User", SysMenuUrl = "UserList", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000500"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 1 });
            sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000502"), Name = "角色管理", Component = "Role", SysMenuUrl = "RoleList", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000500"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 2 });
            sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000503"), Name = "用户角色关联", Component = "UserRole", SysMenuUrl = "UserRole", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000500"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 3 });
            sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000504"), Name = "权限配置", Component = "RolePermission", SysMenuUrl = "RolePermission", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000500"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 4 });
            sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000505"), Name = "权限列表", Component = "Permission", SysMenuUrl = "PermissionList", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000500"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 5 });

            sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000600"), Name = "菜单管理", Component = "SystemMenu", SysMenuUrl = "SystemMenu", ParentID = null, IsLeaf = false, Type = ESysMenuType.Component, IsAvailable = true, Index = 10 });
            sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000601"), Name = "菜单管理", Component = "SysMenu", SysMenuUrl = "SysMenu", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000600"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 1 });

            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000700"), Name = "优惠券管理", Component = "Coupon", SysMenuUrl = "Coupon", ParentID = null, IsLeaf = false, Type = ESysMenuType.Component, IsAvailable = true, Index = 7 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000701"), Name = "创建优惠券", Component = "/CouponAdmin/Index", SysMenuUrl = "/CouponAdmin/Index", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000700"), IsLeaf = true, Type = ESysMenuType.Path, IsAvailable = true, Index = 1 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000702"), Name = "报表", Component = "/CouponAdmin/Report", SysMenuUrl = "/CouponAdmin/Report", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000700"), IsLeaf = true, Type = ESysMenuType.Path, IsAvailable = true, Index = 2 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000703"), Name = "整体报表", Component = "/CouponAdmin/TotalReport", SysMenuUrl = "/CouponAdmin/TotalReport", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000700"), IsLeaf = true, Type = ESysMenuType.Path, IsAvailable = true, Index = 3 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000704"), Name = "优惠券列表", Component = "/CouponAdmin/CouponTemplate", SysMenuUrl = "/CouponAdmin/CouponTemplate", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000700"), IsLeaf = true, Type = ESysMenuType.Path, IsAvailable = true, Index = 4 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000705"), Name = "核销码", Component = "/CouponAdmin/VerificationCode", SysMenuUrl = "/CouponAdmin/VerificationCode", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000700"), IsLeaf = true, Type = ESysMenuType.Path, IsAvailable = true, Index = 5 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000706"), Name = "审核", Component = "/CouponAdmin/Verification", SysMenuUrl = "/CouponAdmin/Verification", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000700"), IsLeaf = true, Type = ESysMenuType.Path, IsAvailable = true, Index = 6 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000707"), Name = "随单派券设置", Component = "FollowBillCoupon", SysMenuUrl = "/CouponAdmin/FollowBillCoupon", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000700"), IsLeaf = true, Type = ESysMenuType.Path, IsAvailable = true, Index = 7 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000708"), Name = "领取记录", Component = "/CouponAdmin/ReceivedCouponRecord", SysMenuUrl = "/CouponAdmin/ReceivedCouponRecord", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000700"), IsLeaf = true, Type = ESysMenuType.Path, IsAvailable = true, Index = 8 });

            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000800"), Name = "人员管理", Component = "UserManager", SysMenuUrl = "UserManager", ParentID = null, IsLeaf = false, Type = ESysMenuType.Component, IsAvailable = true, Index = 6 });
            //sysMenuSet.Add(new SysMenu { ID = Guid.Parse("00000000-0000-0000-0000-000000000801"), Name = "微信粉丝管理", Component = "WeChatFans", SysMenuUrl = "WeChatFans", ParentID = Guid.Parse("00000000-0000-0000-0000-000000000800"), IsLeaf = true, Type = ESysMenuType.Component, IsAvailable = true, Index = 1 });

        }

        //void SeedSystemSetting(DbContext context)
        //{
        //    var systemSettingSet = context.Set<SystemSetting>();
        //    systemSettingSet.Add(new SystemSetting { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Index = 1, Name = SysSettingTypes.WXMsg_MemberRecharge, Caption = "会员储值微信通知消息模板", DefaultValue = string.Empty, IsVisible = true, IsAvailable = true, Type = ESystemSettingType.Parameter, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
        //    systemSettingSet.Add(new SystemSetting { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Index = 2, Name = SysSettingTypes.WXMsg_MemberConsume, Caption = "会员消费微信通知消息模板", DefaultValue = string.Empty, IsVisible = true, IsAvailable = true, Type = ESystemSettingType.Parameter, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
        //    systemSettingSet.Add(new SystemSetting { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Index = 3, Name = SysSettingTypes.WXMsg_MemberAdjust, Caption = "会员账户调整微信通知消息模板", DefaultValue = string.Empty, IsVisible = true, IsAvailable = true, Type = ESystemSettingType.Parameter, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
        //    systemSettingSet.Add(new SystemSetting { ID = Guid.Parse("00000000-0000-0000-0000-000000000004"), Index = 4, Name = SysSettingTypes.WXMsg_CouponReceived, Caption = "优惠券领取微信通知消息模板", DefaultValue = string.Empty, IsVisible = true, IsAvailable = true, Type = ESystemSettingType.Parameter, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
        //    systemSettingSet.Add(new SystemSetting { ID = Guid.Parse("00000000-0000-0000-0000-000000000005"), Index = 5, Name = SysSettingTypes.WXMsg_CouponUsed, Caption = "优惠券使用微信通知消息模板", DefaultValue = string.Empty, IsVisible = true, IsAvailable = true, Type = ESystemSettingType.Parameter, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
        //    systemSettingSet.Add(new SystemSetting { ID = Guid.Parse("00000000-0000-0000-0000-000000000006"), Index = 6, Name = SysSettingTypes.WXMsg_CouponWillExpire, Caption = "优惠券即将过期微信通知消息模板", DefaultValue = string.Empty, IsVisible = true, IsAvailable = true, Type = ESystemSettingType.Parameter, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
        //}

        //void SeedPermissionFunc(DbContext context)
        //{
        //    var permissionFuncSet = context.Set<PermissionFunc>();
        //    permissionFuncSet.Add(new PermissionFunc { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Code = "Portal.BaseDataEdit", Name = "管理后台-基础数据维护", PermissionType = EPermissionType.PortalAction, IsManual = true, IsAvailable = true });
        //    permissionFuncSet.Add(new PermissionFunc { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Code = "Member.Member.adjustBalance", Name = "会员资料-余额调整", PermissionType = EPermissionType.PortalAction, IsManual = true, IsAvailable = true });
        //    permissionFuncSet.Add(new PermissionFunc { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Code = "Member.Member.ExportMember", Name = "会员管理-资料维护-导出会员", PermissionType = EPermissionType.PortalAction, IsManual = true, IsAvailable = true });
        //    permissionFuncSet.Add(new PermissionFunc { ID = Guid.Parse("00000000-0000-0000-0000-000000000004"), Code = "Portal.TradeHistory.InvoiceEdit", Name = "会员储值-开发票", PermissionType = EPermissionType.PortalAction, IsManual = true, IsAvailable = true });
        //    permissionFuncSet.Add(new PermissionFunc { ID = Guid.Parse("00000000-0000-0000-0000-000000000005"), Code = "MemberCard.AdjustBalance", Name = "卡片制作-调整余额", PermissionType = EPermissionType.PortalAction, IsManual = true, IsAvailable = true });

        //    var rolePermissionSet = context.Set<RolePermission>();
        //    rolePermissionSet.Add(new RolePermission { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), RoleCode = "admin", PermissionCode = "Portal.BaseDataEdit", PermissionType = EPermissionType.PortalAction });
        //    rolePermissionSet.Add(new RolePermission { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), RoleCode = "admin", PermissionCode = "Member.Member.adjustBalance", PermissionType = EPermissionType.PortalAction });
        //    rolePermissionSet.Add(new RolePermission { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), RoleCode = "admin", PermissionCode = "Member.Member.ExportMember", PermissionType = EPermissionType.PortalAction });
        //    rolePermissionSet.Add(new RolePermission { ID = Guid.Parse("00000000-0000-0000-0000-000000000004"), RoleCode = "admin", PermissionCode = "Portal.TradeHistory.InvoiceEdit", PermissionType = EPermissionType.PortalAction });
        //    rolePermissionSet.Add(new RolePermission { ID = Guid.Parse("00000000-0000-0000-0000-000000000005"), RoleCode = "admin", PermissionCode = "MemberCard.AdjustBalance", PermissionType = EPermissionType.PortalAction });
        //}

        //void SeedMakeCodeRule(DbContext context)
        //{
        //    var makeCodeRuleSet = context.Set<MakeCodeRule>();
        //    makeCodeRuleSet.Add(new MakeCodeRule { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Code = "WeChatMemberCard", Name = "微信会员卡", IsAvailable = true, IsManualMake = false, Length = 10, CurrentValue = 0, Prefix1Rule = ECodePrefixRule.Fixed, Prefix1Length = 2, Prefix1 = "WX", Prefix2Rule = ECodePrefixRule.None, Prefix2Length = 0, Prefix2 = "", Prefix3Rule = ECodePrefixRule.None, Prefix3Length = 0, Prefix3 = "" });
        //    makeCodeRuleSet.Add(new MakeCodeRule { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Code = "RechargeBill", Name = "储值订单", IsAvailable = true, IsManualMake = false, Length = 10, CurrentValue = 0, Prefix1Rule = ECodePrefixRule.Fixed, Prefix1Length = 2, Prefix1 = "CZ", Prefix2Rule = ECodePrefixRule.None, Prefix2Length = 0, Prefix2 = "", Prefix3Rule = ECodePrefixRule.None, Prefix3Length = 0, Prefix3 = "" });
        //    makeCodeRuleSet.Add(new MakeCodeRule { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Code = "ConsumeBill", Name = "消费订单", IsAvailable = true, IsManualMake = false, Length = 10, CurrentValue = 0, Prefix1Rule = ECodePrefixRule.Fixed, Prefix1Length = 2, Prefix1 = "XF", Prefix2Rule = ECodePrefixRule.None, Prefix2Length = 0, Prefix2 = "", Prefix3Rule = ECodePrefixRule.None, Prefix3Length = 0, Prefix3 = "" });
        //    makeCodeRuleSet.Add(new MakeCodeRule { ID = Guid.Parse("00000000-0000-0000-0000-000000000004"), Code = "MemberCardBatchCode", Name = "卡片批次代码", IsAvailable = true, IsManualMake = false, Length = 9, CurrentValue = 0, Prefix1Rule = ECodePrefixRule.Date, Prefix1Length = 7, Prefix1 = "yyMMdd-", Prefix2Rule = ECodePrefixRule.None, Prefix2Length = 0, Prefix2 = "", Prefix3Rule = ECodePrefixRule.None, Prefix3Length = 0, Prefix3 = "" });
        //    makeCodeRuleSet.Add(new MakeCodeRule { ID = Guid.Parse("00000000-0000-0000-0000-000000000005"), Code = "CouponTemplateCode", Name = "优惠券模板代码", IsAvailable = true, IsManualMake = false, Length = 12, CurrentValue = 0, Prefix1Rule = ECodePrefixRule.Date, Prefix1Length = 8, Prefix1 = "yyyyMMdd", Prefix2Rule = ECodePrefixRule.None, Prefix2Length = 0, Prefix2 = "", Prefix3Rule = ECodePrefixRule.None, Prefix3Length = 0, Prefix3 = "" });
        //}

        void SeedMemberCardType(DbContext context)
        {
            var memberCardTypeSet = context.Set<MemberCardType>();
            memberCardTypeSet.Add(new MemberCardType { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "储值卡", AllowStoreActivate = true, AllowDiscount = false, AllowRecharge = true, MaxRecharge = 0, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
            memberCardTypeSet.Add(new MemberCardType { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "折扣卡", AllowStoreActivate = true, AllowDiscount = true, AllowRecharge = false, MaxRecharge = 0, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
            memberCardTypeSet.Add(new MemberCardType { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "礼品卡", AllowStoreActivate = true, AllowDiscount = false, AllowRecharge = false, MaxRecharge = 0, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
        }

        //void SeedMemberGrade(DbContext context)
        //{
        //    var memberGradeSet = context.Set<MemberGrade>();
        //    memberGradeSet.Add(new MemberGrade { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "默认等级", IsDefault = true, Level = 1, IsNeverExpires = true, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
        //}

        //void SeedMemberGroup(DbContext context)
        //{
        //    var memberGroupSet = context.Set<MemberGroup>();
        //    memberGroupSet.Add(new MemberGroup { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Code = "000", Name = "普通会员", Index = -1 });
        //}

        //void SeedMemberCardTheme(DbContext context)
        //{
        //    var memberCardThemeSet = context.Set<MemberCardTheme>();
        //    memberCardThemeSet.Add(new MemberCardTheme { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Index = 1, CardTypeID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "默认主题", ImgUrl = "/Pictures/MemberCardTheme/default_gift_card_theme.png", IsDefault = true });
        //}

        //void SeedCardThemeCategory(DbContext context)
        //{
        //    var cardThemeCategorySet = context.Set<CardThemeCategory>();
        //    cardThemeCategorySet.Add(new CardThemeCategory { ID = Guid.Parse("00000000-0000-0000-0000-000000000000"), Name = "全部", Grade = 0 });
        //    cardThemeCategorySet.Add(new CardThemeCategory { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "1类推荐", Grade = 1 });
        //    cardThemeCategorySet.Add(new CardThemeCategory { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "2类推荐", Grade = 2 });
        //    cardThemeCategorySet.Add(new CardThemeCategory { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "3类推荐", Grade = 3 });
        //}
    }
}
