using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    public class CarBitCoinMemberService : DomainServiceBase<IRepository<CarBitCoinMember>, CarBitCoinMember, Guid>, ICarBitCoinMemberService
    {
        public CarBitCoinMemberService()
        {
        }

        protected override bool DoValidate(CarBitCoinMember entity)
        {
            if (entity == null)
                return false;
            var exists = Repository.Exists(t => t.MobilePhoneNo == entity.MobilePhoneNo && t.ID != entity.ID);
            if (exists)
                throw new DomainException("该手机号码已注册");
            return true;
        }

        public override CarBitCoinMember Add(CarBitCoinMember entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public override bool Update(CarBitCoinMember entity)
        {
            if (entity == null)
                return false;
            var member = Repository.GetByKey(entity.ID);
            if (member == null)
                return false;
            member.Name = entity.Name;
            member.Sex = entity.Sex;
            return base.Update(member);
        }

        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                throw new DomainException("数据不存在");
            entity.IsDeleted = true;
            return Repository.Update(entity) > 0;
        }

        public CarBitCoinMember Register(CarBitCoinMember entity)
        {
            if (entity == null)
                return null;
            var result = Add(new CarBitCoinMember
            {
                Name = entity.Name,
                MobilePhoneNo = entity.MobilePhoneNo,
                Sex = entity.Sex,
                OpenID = entity.OpenID,
            });
            return result;
        }

        public CarBitCoinMember GetCarBitCoinMember(Guid id)
        {
            return Repository.GetByKey(id);
        }

        public CarBitCoinMember GetCarBitCoinMember(string mobilePhoneNo)
        {
            return Repository.GetQueryable(false).Where(t => t.MobilePhoneNo == mobilePhoneNo).FirstOrDefault();
        }

        public CarBitCoinMember GetCarBitCoinMemberByOpenID(string openId)
        {
            return Repository.GetQueryable(false).Where(t => t.OpenID == openId).FirstOrDefault();
        }
    }
}
