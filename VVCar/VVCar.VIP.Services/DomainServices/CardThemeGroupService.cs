using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 卡片主题分组 领域服务接口实现
    /// </summary>
    public class CardThemeGroupService : DomainServiceBase<IRepository<CardThemeGroup>, CardThemeGroup, Guid>, ICardThemeGroupService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardThemeGroupService"/> class.
        /// </summary>
        public CardThemeGroupService()
        {
        }

        #region properties

        /// <summary>
        /// 时间段
        /// </summary>
        ICardThemeGroupUseTimeService CardThemeGroupUseTimeService
        {
            get { return ServiceLocator.Instance.GetService<ICardThemeGroupUseTimeService>(); }
        }

        IRepository<CardThemeGroupUseTime> CardThemeGroupUseTimeRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<CardThemeGroupUseTime>>(); }
        }

        /// <summary>
        /// 主题图片
        /// </summary>
        IMemberCardThemeService MemberCardThemeService
        {
            get { return ServiceLocator.Instance.GetService<IMemberCardThemeService>(); }
        }

        IRepository<MemberCardTheme> MemberCardThemeRepo
        {
            get => UnitOfWork.GetRepository<IRepository<MemberCardTheme>>();
        }
        /// <summary>
        /// 卡片关联表
        /// </summary>
        IMemberGiftCardService MemberGiftCarService
        {
            get { return ServiceLocator.Instance.GetService<IMemberGiftCardService>(); }
        }
        IRepository<MemberGiftCard> MemberGiftCarRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<MemberGiftCard>>(); }
        }
        /// <summary>
        /// 礼品卡总表
        /// </summary>
        IMemberCardService MemberCardService
        {
            get { return ServiceLocator.Instance.GetService<IMemberCardService>(); }
        }
        IRepository<MemberCard> MemberCardRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<MemberCard>>(); }
        }



        /// <summary>
        /// 分组
        /// </summary>

        IRepository<CardThemeGroup> CardThemeGroupRepo
        {
            get => UnitOfWork.GetRepository<IRepository<CardThemeGroup>>();
        }




        #endregion

        protected override bool DoValidate(CardThemeGroup entity)
        {
            var exists = Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID);
            if (exists)
                throw new DomainException("编号已存在");
            switch (entity.CardThemeCategoryID.ToString())
            {
                case "00000000-0000-0000-0000-000000000001":
                    var grade1 = Repository.GetQueryable(false).Where(t => t.CardThemeCategory.Grade == 1 && t.ID != entity.ID).Count();
                    if (grade1 > 0)
                        throw new DomainException("推广海报只能添加一个主题分组");
                    break;
                case "00000000-0000-0000-0000-000000000002":
                    var grade2 = Repository.GetQueryable(false).Where(t => t.CardThemeCategory.Grade == 2 && t.ID != entity.ID).Count();
                    if (grade2 > 1)
                        throw new DomainException("1类推荐只能添加两个主题分组");
                    break;
            }
            var existsIndex = Repository.Exists(t => t.CardThemeCategoryID == entity.CardThemeCategoryID && t.Index == entity.Index && t.ID != entity.ID);
            if (existsIndex)
                throw new DomainException("序号已存在");
            return true;
        }

        public override CardThemeGroup Add(CardThemeGroup entity)
        {
            if (entity == null)
                return null;
            if (entity.TimeSlot == false && entity.Week == "")
            {
                throw new DomainException("当选择部分时间段时必须选择星期");
            }

            var RecommendGroupID = entity.RecommendGroupID;
            var IsIsAvailable = CardThemeGroupRepo.GetQueryable(false).Where(t => t.ID == RecommendGroupID).FirstOrDefault();
            if (IsIsAvailable != null)
            {
                if (IsIsAvailable.IsAvailable == false)
                {
                    throw new DomainException("推荐主题不能选择已被禁用的主题");
                }
            }



            entity.ID = Util.NewID();
            entity.IsAvailable = true;
            var TCardThemeGroupIDS = entity.ID;
            entity.Code = System.DateTime.Now.ToString();
            //时间段
            if (entity.TimeSlot == false)
            {
                if (entity.UseTimeList.Count > 0)
                {
                    entity.UseTimeList.ForEach(item =>
                    {
                        item.ID = Util.NewID();
                        item.CardThemeGroupID = TCardThemeGroupIDS;
                    });
                };
            }
            //图片

            if (entity.MemberCardThemeList.Count > 0)
            {
                entity.MemberCardThemeList.ForEach(item =>
                {

                    item.ID = Util.NewID();
                    item.CardThemeGroupID = entity.ID;

                });
            }

            var result = base.Add(entity);


            return result;
        }

        public override bool Update(CardThemeGroup entity)
        {
            if (entity == null)
                return false;
            var group = Repository.GetByKey(entity.ID);
            if (group == null)
                return false;
            var RecommendGroupID = entity.RecommendGroupID;
            var IsIsAvailable = CardThemeGroupRepo.GetQueryable(false).Where(t => t.ID == RecommendGroupID).FirstOrDefault();
            if (IsIsAvailable != null)
            {
                if (IsIsAvailable.IsAvailable == false)
                {
                    throw new DomainException("推荐主题不能选择已被禁用的主题");
                }
            }

            var CheckCardThemeCategoryID = group.CardThemeCategoryID.ToString();
            if (CheckCardThemeCategoryID == "00000000-0000-0000-0000-000000000001" && CheckCardThemeCategoryID != entity.CardThemeCategoryID.ToString())
            {
                throw new DomainException("推荐主题不能修改主题分组");
            }


            if (entity.TimeSlot == false && entity.Week == "")
            {
                throw new DomainException("当选择部分时间段时必须选择星期");
            }
            if (entity.TimeSlot == true)
            {
                group.Week = null;
            }
            else
            {
                group.Week = entity.Week;
            }

            group.CardThemeCategoryID = entity.CardThemeCategoryID;
            group.Name = entity.Name;
            group.Code = System.DateTime.Now.ToString();
            group.Index = entity.Index;
            group.DepartmentCode = entity.DepartmentCode;
            group.RecommendGroupID = entity.RecommendGroupID;
            group.GiftCardStartTime = entity.GiftCardStartTime;
            group.GiftCardEndTime = entity.GiftCardEndTime;
            group.TimeSlot = entity.TimeSlot;
            group.RuleDescription = entity.RuleDescription;
            group.IsNotFixationDate = entity.IsNotFixationDate;
            group.EffectiveDays = entity.EffectiveDays;
            group.EffectiveDaysOfAfterBuy = entity.EffectiveDaysOfAfterBuy;
            //时间段
            if (entity.TimeSlot == true)
            {
                var usetime = CardThemeGroupUseTimeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == group.ID).ToArray();
                usetime.ForEach(item =>
                {
                    CardThemeGroupUseTimeService.Delete(item.ID);
                });
            }
            else
            {
                var usetime = CardThemeGroupUseTimeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == group.ID).ToArray();
                usetime.ForEach(item =>
                {
                    CardThemeGroupUseTimeService.Delete(item.ID);
                });
                if (entity.UseTimeList.Count > 0)
                {
                    entity.UseTimeList.ForEach(item =>
                    {
                        var newUseTimeList = new CardThemeGroupUseTime
                        {
                            ID = Util.NewID(),
                            CardThemeGroupID = entity.ID,
                            BeginTime = item.BeginTime,
                            EndTime = item.EndTime
                        };
                        CardThemeGroupUseTimeService.Add(newUseTimeList);
                    });
                };
            }

            //主题图片
            var MemberCardThemeitem = MemberCardThemeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == group.ID).ToArray();
            MemberCardThemeitem.ForEach(item =>
            {
                var MemberCardThemeitemUp = MemberCardThemeRepo.GetQueryable(false).Where(t => t.ID == item.ID).FirstOrDefault();

                MemberCardThemeitemUpdate(MemberCardThemeitemUp);

            });
            if (entity.MemberCardThemeList.Count > 0)
            {
                entity.MemberCardThemeList.ForEach(item =>
                {
                    var newMemberCardThemeList = new MemberCardTheme
                    {
                        ID = Util.NewID(),
                        CardThemeGroupID = entity.ID,
                        Index = item.Index,
                        CardTypeID = item.CardTypeID,
                        Name = item.Name,
                        ImgUrl = item.ImgUrl,
                    };
                    MemberCardThemeService.Add(newMemberCardThemeList);
                });
            }
            return base.Update(group);
        }

        public bool MemberCardThemeitemUpdate(MemberCardTheme entity)
        {
            var group = MemberCardThemeRepo.GetQueryable(true).Where(t => t.ID == entity.ID).FirstOrDefault();
            group.IsDeleted = true;
            MemberCardThemeService.Update(group);
            return true;
        }


        public override bool Delete(Guid key)
        {
            var group = Repository.GetInclude(t => t.MemberCardThemeList, false).Where(t => t.ID == key).FirstOrDefault();
            if (group != null && group.MemberCardThemeList.Count > 0)
                throw new DomainException("分组包含主题子项，不能删除");
            var groups = Repository.GetInclude(t => t.MemberCardThemeList, false).Where(t => t.RecommendGroupID == group.ID).FirstOrDefault();
            if (groups != null)
            {
                throw new DomainException("该主题被推广主题引用，不能删除！");
            }


            var usetime = CardThemeGroupUseTimeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == group.ID).ToArray();
            usetime.ForEach(item =>
            {
                CardThemeGroupUseTimeService.Delete(item.ID);
            });

            return base.Delete(key);
        }

        public IEnumerable<CardThemeGroupDto> Search(CardThemeGroupFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.CardThemeCategory, false);
            if (filter.CategoryID.HasValue)
                queryable = queryable.Where(t => t.CardThemeCategoryID == filter.CategoryID && t.IsDeleted == false);
            if (filter.ID.HasValue)
                queryable = queryable.Where(t => t.ID == filter.ID && t.IsDeleted == false);
            if (filter.IsNotRecommended)
                queryable = queryable.Where(t => t.CardThemeCategory.Grade != 1);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.OrderBy(t => t.Index).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            var result = queryable.OrderBy(t => t.CardThemeCategory.Grade).OrderBy(t => t.Index).ToArray().MapTo<List<CardThemeGroupDto>>();
            var MemberCardThemeitem = MemberCardThemeRepo.GetQueryable(false).ToArray();
            result.ForEach(item =>
            {
                var CarTEmlist = "";
                var theme = MemberCardThemeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == item.ID && t.IsDefault).FirstOrDefault();
                if (theme == null)
                    theme = MemberCardThemeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == item.ID).OrderBy(t => t.Index).FirstOrDefault();
                if (theme != null)
                    item.ThemeUrl = theme.ImgUrl;

                var themes = MemberCardThemeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == item.ID).ToArray();
                if (themes == null)
                    themes = MemberCardThemeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == item.ID).OrderBy(t => t.Index).ToArray();
                if (themes != null)
                {
                    for (var i = 0; i < themes.Count(); i++)
                    {
                        if (i == 0)
                        {
                            CarTEmlist += themes[i].ImgUrl;
                        }
                        else
                        {
                            CarTEmlist += ";" + themes[i].ImgUrl;
                        }
                    }
                }
                item.ImgUrlDto = CarTEmlist;

                var themeIsAvailable = CardThemeGroupRepo.GetQueryable(false).Where(t => t.ID == item.ID && t.IsDeleted == false).FirstOrDefault();
                if (themeIsAvailable.IsAvailable == true)
                {
                    item.IsAvailableShow = "是";
                }
                else
                {
                    item.IsAvailableShow = "否";
                }
            });
            if (!filter.IsFromPortal)
                result = result.Where(t => !string.IsNullOrEmpty(t.ThemeUrl)).ToList();
            return result;
        }

        /// <summary>
        /// 升序
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool setIndex(CardThemeGroup entity)
        {
            if (entity == null)
                return false;
            var group = Repository.GetByKey(entity.ID);
            if (group.Index == 0 && group.Index < 1)
            {
                throw new DomainException("当前顺序为最小值，不能再降序");
            }
            var tempIndex = group.Index;
            var groups = Repository.GetQueryable(true).Where(t => t.CardThemeCategoryID == entity.CardThemeCategoryID && t.Index < group.Index).OrderBy(t => t.Index).ToArray().LastOrDefault();
            if (groups != null)
            {
                tempIndex = groups.Index;
                groups.Index = group.Index;
                Repository.Update(groups);
            }
            else
            {
                throw new DomainException("没有当前更小的序号，不能再升序");
            }
            group.Index = tempIndex;
            return Repository.Update(group) > 0;
        }


        /// <summary>
        /// 降序
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpIndex(CardThemeGroup entity)
        {
            if (entity == null)
                return false;
            var group = Repository.GetByKey(entity.ID);
            var tempIndex = group.Index;
            var groups = Repository.GetQueryable(true).Where(t => t.CardThemeCategoryID == entity.CardThemeCategoryID && t.Index > group.Index).OrderBy(t => t.Index).ToArray().FirstOrDefault();
            if (groups != null)
            {
                tempIndex = groups.Index;
                groups.Index = group.Index;
                Repository.Update(groups);
            }
            else
            {
                throw new DomainException("没有当前更大的序号，不能再降序");
            }
            group.Index = tempIndex;
            return Repository.Update(group) > 0;
        }


        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool setAvailable(CardThemeGroup entity)
        {
            if (entity == null)
                return false;
            var group = Repository.GetByKey(entity.ID);
            group.IsAvailable = true;
            return Repository.Update(group) > 0;
        }



        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpAvailable(CardThemeGroup entity)
        {
            if (entity == null)
                return false;
            var RecommendGroupID = entity.ID;


            var groupIiem = Repository.GetInclude(t => t.MemberCardThemeList, false).Where(t => t.ID == entity.ID).FirstOrDefault();

            if (groupIiem.CardThemeCategoryID.ToString() == "00000000-0000-0000-0000-000000000001")
            {
                throw new DomainException("推广海报不能禁用！");
            }

            var groups = Repository.GetInclude(t => t.MemberCardThemeList, false).Where(t => t.RecommendGroupID == groupIiem.ID).FirstOrDefault();
            if (groups != null)
            {
                throw new DomainException("该主题被推广主题引用，不能禁用！");
            }

            var group = Repository.GetByKey(entity.ID);
            group.IsAvailable = false;
            return Repository.Update(group) > 0;
        }

        /// <summary>
        /// 查寻时间段
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<CardThemeGroupUseTime> SearchTime(CardThemeGroupUseTime param)
        {
            var usetime = CardThemeGroupUseTimeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == param.ID).ToArray();
            return usetime;
        }

        /// <summary>
        /// 查询图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<MemberCardTheme> SearchImg(MemberCardTheme param)
        {
            var CardTheme = MemberCardThemeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == param.ID).ToArray();
            return CardTheme;
        }


        /// <summary>
        /// 查询星期
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<CardThemeGroupDto> Searchwek(SearchCardThemeGroupFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.CardThemeCategory, false);
            if (filter.ID.HasValue)
                queryable = queryable.Where(t => t.ID == filter.ID);

            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.OrderBy(t => t.Index).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            var result = queryable.OrderBy(t => t.CardThemeCategory.Grade).OrderBy(t => t.Index).ToArray().MapTo<List<CardThemeGroupDto>>();
            var MemberCardThemeitem = MemberCardThemeRepo.GetQueryable(false).ToArray();
            result.ForEach(item =>
            {
                var CarTEmlist = "";
                var theme = MemberCardThemeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == item.ID && t.IsDefault).FirstOrDefault();
                if (theme == null)
                    theme = MemberCardThemeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == item.ID).OrderBy(t => t.Index).FirstOrDefault();
                if (theme != null)
                    item.ThemeUrl = theme.ImgUrl;

                var themes = MemberCardThemeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == item.ID).ToArray();
                if (themes == null)
                    themes = MemberCardThemeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == item.ID).OrderBy(t => t.Index).ToArray();
                if (themes != null)
                {
                    for (var i = 0; i < themes.Count(); i++)
                    {
                        if (i == 0)
                        {
                            CarTEmlist += themes[i].ImgUrl;
                        }
                        else
                        {
                            CarTEmlist += ";" + themes[i].ImgUrl;
                        }
                    }
                }
                item.ImgUrlDto = CarTEmlist;
            });

            return result;
        }

        /// <summary>
        /// 查询适用时间段
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<CardThemeGroupUseTime> SearchUseTime(SearchCardThemeGroupFilter filter, ref int totalCount)
        {
            var usetime = CardThemeGroupUseTimeRepo.GetQueryable(false).Where(t => t.CardThemeGroupID == filter.ID).ToArray();
            return usetime;
            //return queryable;
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteGiftCardTem(CardThemeGroup entity)
        {
            if (entity == null)
                return false;
            var group = Repository.GetByKey(entity.ID);
            var checkG = Repository.GetQueryable(true).Where(t => t.RecommendGroupID == entity.ID).FirstOrDefault();
            if (checkG != null)
            {
                throw new DomainException("当前主题被推广海报引用，不能删除");
            }

            group.IsDeleted = true;
            return base.Update(group);
        }

        /// <summary>
        /// 根据卡号或者手机号码获取卡信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public MemberCardDto GetCardInfoByNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                return null;
            var memberCard = MemberCardRepo.GetQueryable(false).Where(t => t.Code == number).FirstOrDefault();
            var cardInfo = new MemberCardDto
            {
                CardID = memberCard.ID,
                CardNumber = memberCard.Code,
                EffectiveDate = memberCard.EffectiveDate.HasValue ? memberCard.EffectiveDate.Value.ToString("yyyy-MM-dd") : "",
                ExpiredDate = memberCard.ExpiredDate.HasValue ? memberCard.ExpiredDate.Value.ToString("yyyy-MM-dd") : "",
            };
            var FindMemberGiftCard = MemberGiftCarRepo.GetQueryableAllData(false).Where(t => t.CardID == memberCard.ID).FirstOrDefault();
            if (FindMemberGiftCard != null)
            {
                var MemberCardThemeID = FindMemberGiftCard.MemberCardThemeID;
                var FindMemberCardTheme = MemberCardThemeRepo.GetQueryableAllData(true).Where(t => t.ID == MemberCardThemeID).FirstOrDefault();
                if (FindMemberCardTheme != null)
                {
                    var DataTimeList = new List<GiftCardUserTime>();
                    var CardThemeGroupID = FindMemberCardTheme.CardThemeGroupID;
                    var FindCardThemeGroupUseTime = CardThemeGroupUseTimeRepo.GetQueryableAllData(false).Where(t => t.CardThemeGroupID == CardThemeGroupID).ToArray(); //时间
                    if (FindCardThemeGroupUseTime != null)
                    {
                        for (var i = 0; i < FindCardThemeGroupUseTime.Length; i++)
                        {
                            DataTimeList.Add(new GiftCardUserTime
                            {
                                BeginTime = FindCardThemeGroupUseTime[i].BeginTime,
                                EndTime = FindCardThemeGroupUseTime[i].EndTime,
                            });
                        }
                    }
                    var FindCardThemeGroup = CardThemeGroupRepo.GetQueryableAllData(false).Where(t => t.ID == CardThemeGroupID).FirstOrDefault(); //分组信息
                    if (FindCardThemeGroup != null)
                    {
                        cardInfo.UserCode = FindCardThemeGroup.DepartmentCode;
                        cardInfo.UserGiftCardStartTime = FindCardThemeGroup.GiftCardStartTime;
                        cardInfo.RuleDescription = FindCardThemeGroup.RuleDescription;
                        cardInfo.UserGiftCardEndTime = FindCardThemeGroup.GiftCardEndTime;
                        cardInfo.UserIsAvailable = FindCardThemeGroup.IsAvailable;
                        cardInfo.UserTimeSlot = FindCardThemeGroup.TimeSlot;
                        cardInfo.UserWeek = FindCardThemeGroup.Week;
                        cardInfo.EffectiveDaysOfAfterBuy = FindCardThemeGroup.EffectiveDaysOfAfterBuy;
                        cardInfo.EffectiveDays = FindCardThemeGroup.EffectiveDays;
                        cardInfo.IsNotFixationDate = FindCardThemeGroup.IsNotFixationDate;
                    }
                    cardInfo.GiftCardUserTimeList = DataTimeList;
                }
            }
            return cardInfo;
        }

    }
}
