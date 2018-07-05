using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using VVCar.VIP.Domain.Entities;
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

        #region properties

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        IRepository<Order> OrderRepo { get => UnitOfWork.GetRepository<IRepository<Order>>(); }

        IRepository<PickUpOrder> PickUpOrderRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrder>>(); }

        IRepository<User> UserRepo { get => UnitOfWork.GetRepository<IRepository<User>>(); }

        IRepository<CarBitCoinRecord> CarBitCoinRecordRepo { get => UnitOfWork.GetRepository<IRepository<CarBitCoinRecord>>(); }

        ICarBitCoinDistributionService CarBitCoinDistributionService { get => ServiceLocator.Instance.GetService<ICarBitCoinDistributionService>(); }

        IRepository<CarBitCoinMember> CarBitCoinMemberRepo { get => UnitOfWork.GetRepository<IRepository<CarBitCoinMember>>(); }

        IRepository<CarBitCoinMemberEngine> CarBitCoinMemberEngineRepo { get => UnitOfWork.GetRepository<IRepository<CarBitCoinMemberEngine>>(); }

        #endregion

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
            if (!string.IsNullOrEmpty(entity.OpenID))
            {
                member.OpenID = entity.OpenID;
            }
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
            var member = Repository.GetQueryable(false).FirstOrDefault(t => t.MobilePhoneNo == entity.MobilePhoneNo);
            if (member != null)
            {
                entity.ID = member.ID;
                Update(entity);
                return entity;
            }
            var result = Add(new CarBitCoinMember
            {
                Name = entity.Name,
                MobilePhoneNo = entity.MobilePhoneNo,
                Sex = entity.Sex,
                OpenID = entity.OpenID,
                Horsepower = 60,
                //Horsepower = CalculateHorsepower(entity.MobilePhoneNo),
            });
            CarBitCoinDistributionService.DistributionCarBitCoin(result.ID);
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

        public int CalculateHorsepower(string mobilePhoneNo)
        {
            var result = 0;
            if (string.IsNullOrEmpty(mobilePhoneNo))
                return result;
            var members = MemberRepo.GetInclude(t => t.MemberPlateList, false).Where(t => t.MobilePhoneNo == mobilePhoneNo).ToList();
            var openids = members.Select(t => t.WeChatOpenID).ToList();
            var plates = new List<string>();
            members.ForEach(t =>
            {
                if (t.MemberPlateList != null && t.MemberPlateList.Count() > 0)
                    plates.AddRange(t.MemberPlateList.Select(p => p.PlateNumber));
            });
            var ordersmoney = OrderRepo.GetQueryable(false).Where(t => openids.Contains(t.OpenID)).GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            var pickuoordersmoney = PickUpOrderRepo.GetQueryable(false).Where(t => plates.Contains(t.PlateNumber)).GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            var users = UserRepo.GetQueryable(false).Where(t => t.MobilePhoneNo == mobilePhoneNo).ToList();
            var userids = users.Select(t => t.ID).ToList();
            var userpickuoordersmoney = PickUpOrderRepo.GetQueryable(false).Where(t => userids.Contains(t.StaffID)).GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            var carBitCoinMember = CarBitCoinMemberRepo.GetQueryable(false).Where(t => t.MobilePhoneNo == mobilePhoneNo).FirstOrDefault();
            //var carBitCoinRecords = CarBitCoinRecordRepo.GetQueryable(false).Where(t => t.CarBitCoinMemberID == carBitCoinMember.ID && t.CarBitCoinRecordType == ECarBitCoinRecordType.BuyEngine).ToList();
            var carBitCoinMemberEngines = CarBitCoinMemberEngineRepo.GetQueryable(false).Where(t => t.CarBitCoinMemberID == carBitCoinMember.ID).ToList();
            var engineHorsepower = carBitCoinMemberEngines.GroupBy(g => 1).Select(t => t.Sum(s => s.Horsepower * s.Quantity)).FirstOrDefault();
            result = (int)Math.Floor(ordersmoney + pickuoordersmoney + userpickuoordersmoney + engineHorsepower + 60);
            return result;
        }

        public bool CalculateHorsepowerSave(Guid carBitMemberId)
        {
            if (carBitMemberId == null)
                return false;
            var cbcmember = Repository.GetByKey(carBitMemberId);
            if (cbcmember == null)
                return false;
            var horsepower = CalculateHorsepower(cbcmember.MobilePhoneNo);
            if (horsepower < 60)
                horsepower = 60;
            if (cbcmember.Horsepower >= horsepower)
                return true;
            cbcmember.Horsepower = horsepower;
            Repository.Update(cbcmember);
            return true;
        }

        public bool ChangeHorsepowerCarBitCoin(Guid? cbcmemberId, string mobilePhoneNo, ECarBitCoinRecordType carBitCoinRecordType, int horsepower, decimal carBitCoin, string tradeNo, string remark)
        {
            if (((!cbcmemberId.HasValue) && string.IsNullOrEmpty(mobilePhoneNo)) || (horsepower == 0 && carBitCoin == 0))
                return false;
            CarBitCoinMember cbcmember = null;
            if (cbcmemberId.HasValue)
                cbcmember = Repository.GetByKey(cbcmemberId.Value);
            else if (!string.IsNullOrEmpty(mobilePhoneNo))
                cbcmember = Repository.GetQueryable().Where(t => t.MobilePhoneNo == mobilePhoneNo).FirstOrDefault();
            if (cbcmember == null)
                return false;
            UnitOfWork.BeginTransaction();
            try
            {
                cbcmember.Horsepower += horsepower;
                if (carBitCoinRecordType == ECarBitCoinRecordType.Give)
                    cbcmember.FrozenCoin += carBitCoin;
                else
                    cbcmember.CarBitCoin += carBitCoin;
                Repository.Update(cbcmember);
                CarBitCoinRecordRepo.Add(new CarBitCoinRecord
                {
                    ID = Util.NewID(),
                    CarBitCoinMemberID = cbcmember.ID,
                    CarBitCoinRecordType = carBitCoinRecordType,
                    Horsepower = horsepower,
                    CarBitCoin = carBitCoin,
                    TradeNo = tradeNo,
                    Remark = remark,
                    CreatedDate = DateTime.Now,
                });
                UnitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
            return true;
        }

        public bool GiveAwayCarBitCoin(GiveAwayCarBitCoinParam param)
        {
            if (param == null || param.CarBitCoinMemberID == null || param.CarBitCoin <= 0)
                return false;
            return ChangeHorsepowerCarBitCoin(param.CarBitCoinMemberID, string.Empty, ECarBitCoinRecordType.Give, 0, param.CarBitCoin, string.Empty, param.Remark);
        }

        public IEnumerable<CarBitCoinMember> Search(CarBitCoinMemberFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            if (!string.IsNullOrEmpty(filter.OpenID))
                queryable = queryable.Where(t => t.OpenID == filter.OpenID);
            if (!string.IsNullOrEmpty(filter.MobilePhoneNo))
                queryable = queryable.Where(t => t.MobilePhoneNo.Contains(filter.MobilePhoneNo));
            totalCount = queryable.Count();
            if (filter.SortDirection == ESortDirection.Horsepower)
                queryable = queryable.OrderByDescending(t => t.Horsepower);
            else
                queryable = queryable.OrderByDescending(t => t.CarBitCoin);
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }

        /// <summary>
        /// 查询车比特记录
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<CarBitCoinRecordDto> SearchCarBitCoinRecord(CarBitCoinRecordFilter filter, out int totalCount)
        {
            var queryable = CarBitCoinRecordRepo.GetInclude(t => t.CarBitCoinMember, false);
            if (filter.CarBitCoinRecordType.HasValue)
                queryable = queryable.Where(t => t.CarBitCoinRecordType == filter.CarBitCoinRecordType);
            if (!string.IsNullOrEmpty(filter.NamePhone))
                queryable = queryable.Where(t => t.CarBitCoinMember.Name.Contains(filter.NamePhone) || t.CarBitCoinMember.MobilePhoneNo.Contains(filter.NamePhone));
            if (filter.CarBitCoinMemberID.HasValue)
                queryable = queryable.Where(t => t.CarBitCoinMemberID == filter.CarBitCoinMemberID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).MapTo<CarBitCoinRecordDto>().ToArray();
        }
    }
}
