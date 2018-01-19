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
        }

        void SeedMemberCardType(DbContext context)
        {
            var memberCardTypeSet = context.Set<MemberCardType>();
            memberCardTypeSet.Add(new MemberCardType { ID = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "储值卡", AllowStoreActivate = true, AllowDiscount = false, AllowRecharge = true, MaxRecharge = 0, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
            memberCardTypeSet.Add(new MemberCardType { ID = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "折扣卡", AllowStoreActivate = true, AllowDiscount = true, AllowRecharge = false, MaxRecharge = 0, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
            memberCardTypeSet.Add(new MemberCardType { ID = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "礼品卡", AllowStoreActivate = true, AllowDiscount = false, AllowRecharge = false, MaxRecharge = 0, CreatedUserID = Guid.Empty, CreatedUser = _systemUserName, CreatedDate = DateTime.Now });
        }
    }
}
