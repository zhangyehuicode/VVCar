using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using VVCar.VIP.Domain.Entities;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using YEF.Utility;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 车比特分配
    /// </summary>
    public class CarBitCoinDistributionService : DomainServiceBase<IRepository<CarBitCoinDistribution>, CarBitCoinDistribution, Guid>, ICarBitCoinDistributionService
    {
        public CarBitCoinDistributionService()
        {
        }

        #region properties

        IRepository<CarBitCoinMember> CarBitCoinMemberRepo { get => UnitOfWork.GetRepository<IRepository<CarBitCoinMember>>(); }

        ICarBitCoinMemberService CarBitCoinMemberService { get => ServiceLocator.Instance.GetService<ICarBitCoinMemberService>(); }

        IRepository<CarBitCoinRecord> CarBitCoinRecordRepo { get => UnitOfWork.GetRepository<IRepository<CarBitCoinRecord>>(); }

        #endregion

        /// <summary>
        /// 使用RNGCryptoServiceProvider生成种子
        /// </summary>
        /// <returns></returns>
        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);

        }

        public double GenerateRandom(int min, int max)
        {
            var random = new Random(GetRandomSeed());
            return random.Next(min, max);
        }

        public override CarBitCoinDistribution Add(CarBitCoinDistribution entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.PositionX = GenerateRandom(10, 90) / 100;
            entity.PositionY = GenerateRandom(15, 90) / 100;
            return base.Add(entity);
        }

        public bool CarBitCoinTransform(Guid id)
        {
            var entity = Repository.GetByKey(id);
            if (entity == null)
                throw new DomainException("数据不存在");
            if (entity.Status == ECarBitCoinDistributionStatus.Transformed)
                return true;
            var member = CarBitCoinMemberRepo.GetQueryable().Where(t => t.ID == entity.CarBitCoinMemberID).FirstOrDefault();
            if (member == null)
                throw new DomainException("会员不存在");
            UnitOfWork.BeginTransaction();
            try
            {
                member.CarBitCoin += entity.CarBitCoin;
                entity.Status = ECarBitCoinDistributionStatus.Transformed;
                Repository.Update(entity);
                CarBitCoinMemberRepo.Update(member);
                CarBitCoinRecordRepo.Add(new CarBitCoinRecord
                {
                    ID = Util.NewID(),
                    CarBitCoinMemberID = member.ID,
                    CarBitCoinRecordType = ECarBitCoinRecordType.SystemDistribution,
                    CarBitCoin = entity.CarBitCoin,
                    CreatedDate = DateTime.Now,
                });
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public bool DistributionCarBitCoin(Guid? cbcmemberId)
        {
            var result = new List<CarBitCoinDistribution>();
            var carBitCoinMemberQueryable = CarBitCoinMemberRepo.GetQueryable(false).Where(t => t.Horsepower > 0);
            if (cbcmemberId != null)
                carBitCoinMemberQueryable = CarBitCoinMemberRepo.GetQueryable(false).Where(t => t.ID == cbcmemberId);
            var cbcmemberids = carBitCoinMemberQueryable.Select(t => t.ID).ToList();
            cbcmemberids.ForEach(t =>
            {
                CarBitCoinMemberService.CalculateHorsepowerSave(t);
            });
            var carBitCoinMembers = carBitCoinMemberQueryable.ToList();
            for (var i = 0; i < 5; i++)
            {
                var carBitCoinDistributions = carBitCoinMembers.Select(t => new CarBitCoinDistribution
                {
                    ID = Util.NewID(),
                    CarBitCoinMemberID = t.ID,
                    MobilePhoneNo = t.MobilePhoneNo,
                    CarBitCoin = (decimal)GenerateRandom(100, 600) / 100000,
                    PositionX = GenerateRandom(10, 90) / 100,
                    PositionY = GenerateRandom(15, 90) / 100,
                    CreatedDate = DateTime.Now,
                }).ToList();
                result.AddRange(carBitCoinDistributions);
            }
            AppContext.Logger.Info($"DistributionCarBitCoin carBitCoinMembers count:{carBitCoinMembers.Count()};carBitCoinDistributions count:{result.Count}");
            Repository.AddRange(result);
            return true;
        }

        public IEnumerable<CarBitCoinDistribution> Search(CarBitCoinDistributionFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            if (filter.CarBitCoinMemberID.HasValue)
                queryable = queryable.Where(t => t.CarBitCoinMemberID == filter.CarBitCoinMemberID.Value);
            if (!string.IsNullOrEmpty(filter.MobilePhoneNo))
                queryable = queryable.Where(t => t.MobilePhoneNo == filter.MobilePhoneNo);
            if (filter.Status.HasValue)
                queryable = queryable.Where(t => t.Status == filter.Status.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
