using System;
using System.Data.Entity;
using VVCar.VIP.Domain;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using YEF.Core;
using YEF.Core.Data;
using YEF.Data.Initializer;

namespace VVCar.VIP.Data
{
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
            SeedMemberCardType(context);
            SeedMemberGrade(context);
            SeedMemberGroup(context);
            SeedMemberCardTheme(context);
            SeedCardThemeCategory(context);
            SeedGameSetting(context);
        }

        void SeedMemberCardType(DbContext context)
        {
            var memberCardTypeSet = context.Set<MemberCardType>();
            memberCardTypeSet.Add(new MemberCardType { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "储值卡", AllowStoreActivate = true, AllowDiscount = false, AllowRecharge = true, MaxRecharge = 0, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
            memberCardTypeSet.Add(new MemberCardType { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "折扣卡", AllowStoreActivate = true, AllowDiscount = true, AllowRecharge = false, MaxRecharge = 0, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
            memberCardTypeSet.Add(new MemberCardType { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "礼品卡", AllowStoreActivate = true, AllowDiscount = false, AllowRecharge = false, MaxRecharge = 0, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
        }

        void SeedMemberGrade(DbContext context)
        {
            var memberGradeSet = context.Set<MemberGrade>();
            memberGradeSet.Add(new MemberGrade { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "默认等级", IsDefault = true, Level = 1, IsNeverExpires = true, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
        }

        void SeedMemberGroup(DbContext context)
        {
            var memberGroupSet = context.Set<MemberGroup>();
            memberGroupSet.Add(new MemberGroup { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Code = "000", Name = "普通会员", Index = -1 });
        }

        void SeedMemberCardTheme(DbContext context)
        {
            var memberCardThemeSet = context.Set<MemberCardTheme>();
            memberCardThemeSet.Add(new MemberCardTheme { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Index = 1, CardTypeID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "默认主题", ImgUrl = "/Pictures/MemberCardTheme/default_gift_card_theme.png", IsDefault = true });
        }

        void SeedCardThemeCategory(DbContext context)
        {
            var cardThemeCategorySet = context.Set<CardThemeCategory>();
            //cardThemeCategorySet.Add(new CardThemeCategory { ID = Guid.Parse("00000000-0000-0000-0000-000000000000"), Name = "全部", Grade = 0 });
            //cardThemeCategorySet.Add(new CardThemeCategory { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "1类推荐", Grade = 1 });
            //cardThemeCategorySet.Add(new CardThemeCategory { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "2类推荐", Grade = 2 });
            //cardThemeCategorySet.Add(new CardThemeCategory { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "3类推荐", Grade = 3 });
        }

        void SeedGameSetting(DbContext context)
        {
            var gameSettingSet = context.Set<GameSetting>();
            gameSettingSet.Add(new GameSetting {
                ID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                GameType = EGameType.AttractWheel,
                PeriodDays = 10,
                PeriodCounts = 20,
                Limit = 100,
                IsShare = true,
                ShareTitle = "拓客转盘",
                IsOrderShow = true,
                CreatedUserID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedUser = "admin",
                CreatedDate = DateTime.Now,
            });
            gameSettingSet.Add(new GameSetting
            {
                ID = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                GameType = EGameType.ActivityWheel,
                PeriodDays = 10,
                PeriodCounts = 20,
                Limit = 100,
                IsShare = true,
                ShareTitle = "活动转盘",
                IsOrderShow = true,
                CreatedUserID = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedUser = "admin",
                CreatedDate = DateTime.Now,
            });
        }
    }
}
