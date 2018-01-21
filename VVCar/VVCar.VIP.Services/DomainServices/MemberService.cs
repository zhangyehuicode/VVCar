using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Domain;
using YEF.Core.Data;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Dtos;
using YEF.Utility;
using YEF.Core.Dtos;
using VVCar.VIP.Domain.Filters;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 会员领域服务
    /// </summary>
    public class MemberService : DomainServiceBase<IRepository<Member>, Member, Guid>, IMemberService
    {
        public MemberService()
        {
        }

        #region properties

        IMemberCardService _memberCardService;

        /// <summary>
        /// 会员卡
        /// </summary>
        private IMemberCardService MemberCardService
        {
            get
            {
                if (_memberCardService == null)
                {
                    _memberCardService = ServiceLocator.Instance.GetService<IMemberCardService>();
                }
                return _memberCardService;
            }
        }

        IRechargeHistoryService _rechargeHistoryService;

        /// <summary>
        /// 储值记录
        /// </summary>
        IRechargeHistoryService RechargeHistoryService
        {
            get
            {
                if (_rechargeHistoryService == null)
                {
                    _rechargeHistoryService = ServiceLocator.Instance.GetService<IRechargeHistoryService>();
                }
                return _rechargeHistoryService;
            }
        }

        #endregion

        #region propertiesTemp

        //IRechargeHistoryService _rechargeHistoryService;

        ///// <summary>
        ///// 储值记录
        ///// </summary>
        //IRechargeHistoryService RechargeHistoryService
        //{
        //    get
        //    {
        //        if (_rechargeHistoryService == null)
        //        {
        //            _rechargeHistoryService = ServiceLocator.Instance.GetService<IRechargeHistoryService>();
        //        }
        //        return _rechargeHistoryService;
        //    }
        //}

        //ITradeHistoryService _tradeHistoryService;

        ///// <summary>
        ///// 消费记录
        ///// </summary>
        //ITradeHistoryService TradeHistoryService
        //{
        //    get
        //    {
        //        if (_tradeHistoryService == null)
        //        {
        //            _tradeHistoryService = ServiceLocator.Instance.GetService<ITradeHistoryService>();
        //        }
        //        return _tradeHistoryService;
        //    }
        //}

        //IRepository<TradeHistory> TradeHistoryRepo
        //{
        //    get { return UnitOfWork.GetRepository<IRepository<TradeHistory>>(); }
        //}

        //IMemberGroupService MemberGroupService
        //{
        //    get { return UnitOfWork.GetRepository<IMemberGroupService>(); }
        //}

        //IMemberGradeService MemberGradeService
        //{
        //    get { return UnitOfWork.GetRepository<IMemberGradeService>(); }
        //}

        //IRepository<MemberPoint> MemberPointRepo
        //{
        //    get { return UnitOfWork.GetRepository<IRepository<MemberPoint>>(); }
        //}

        //IRepository<MemberPointHistory> MemberPointHistoryRepo
        //{
        //    get { return UnitOfWork.GetRepository<IRepository<MemberPointHistory>>(); }
        //}

        //IMemberGradeHistoryService MemberGradeHistoryService { get => ServiceLocator.Instance.GetService<IMemberGradeHistoryService>(); }

        //IRepository<MemberSignIn> MemberSignInRepo
        //{
        //    get { return UnitOfWork.GetRepository<IRepository<MemberSignIn>>(); }
        //}

        //IRepository<MemberGrade> MemberGradeRepo { get => UnitOfWork.GetRepository<IRepository<MemberGrade>>(); }

        #endregion

        #region methods

        private Guid GetCardID(string cardCode)
        {
            var card = MemberCardService.Query(p => p.Code == cardCode).FirstOrDefault();
            if (card == null)
                throw new DomainException("会员卡号无效");
            return card.ID;
        }

        protected override bool DoValidate(Member entity)
        {
            bool exists = true;
            if (!string.IsNullOrEmpty(entity.CardNumber))
            {
                exists = this.Repository.Exists(t => t.CardNumber == entity.CardNumber && t.ID != entity.ID);
                if (exists)
                    throw new DomainException(String.Format("卡号 {0} 已绑定。", entity.CardNumber));
            }
            if (!string.IsNullOrEmpty(entity.MobilePhoneNo))
            {
                exists = this.Repository.Exists(t => t.MobilePhoneNo == entity.MobilePhoneNo && t.ID != entity.ID);
                if (exists)
                    throw new DomainException(String.Format("手机号码 {0} 已绑定。", entity.MobilePhoneNo));
            }
            return true;
        }

        public override Member Add(Member entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.Password = Util.EncryptPassword(entity.CardNumber, entity.Password);
            entity.Card = null;
            if (entity.CardID == Guid.Empty)
            {
                entity.CardID = GetCardID(entity.CardNumber);
            }
            entity.PhoneLocation = GetPhoneLoaction(entity.MobilePhoneNo);
            //if (!entity.MemberGroupID.HasValue)
            //{
            //    entity.MemberGroupID = MemberGroupService.GetDefaultMemberGroupID();
            //}
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            var member = base.Add(entity);
            //if (!member.MemberGradeID.HasValue)
            //{
            //    var defaultGrade = MemberGradeService.GetDefaultMemberGrade();
            //    if (defaultGrade != null)
            //        SetMemberGrade(member, defaultGrade);
            //}
            return member;
        }

        public override bool Update(Member entity)
        {
            if (entity == null)
                return false;
            var member = this.Repository.GetByKey(entity.ID);
            if (member == null)
                return false;
            if (entity.Password != member.Password)
            {
                member.Password = Util.EncryptPassword(member.CardNumber, entity.Password);
            }
            member.Name = entity.Name;
            member.Sex = entity.Sex;
            member.Birthday = entity.Birthday;
            if (member.MobilePhoneNo != entity.MobilePhoneNo)
            {
                member.MobilePhoneNo = entity.MobilePhoneNo;
                member.PhoneLocation = GetPhoneLoaction(member.MobilePhoneNo);
            }
            member.CardID = entity.CardID;
            member.CardNumber = entity.CardNumber;
            member.WeChatOpenID = entity.WeChatOpenID;
            if (entity.OwnerDepartmentID.HasValue)
            {
                member.OwnerDepartmentID = entity.OwnerDepartmentID;
            }
            //member.MemberGroupID = entity.MemberGroupID;
            member.LastUpdateUserID = AppContext.CurrentSession.UserID;
            member.LastUpdateUser = AppContext.CurrentSession.UserName;
            member.LastUpdateDate = DateTime.Now;
            return base.Update(member);
        }

        public PagedResultDto<MemberDto> Search(MemberFilter filter)
        {
            var result = new PagedResultDto<MemberDto>();
            var queryable = Repository.GetQueryable(false);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.MobilePhoneNo))
                    queryable = queryable.Where(p => p.MobilePhoneNo.Contains(filter.MobilePhoneNo));
                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    queryable = queryable.Where(p => p.CardNumber.Contains(filter.Keyword)
                    || p.MobilePhoneNo.Contains(filter.Keyword) || p.Name.Contains(filter.Keyword));
                }
                if (filter.Status.HasValue)
                    queryable = queryable.Where(p => p.Card.Status == filter.Status.Value);
                if (filter.CardTypeID.HasValue)
                    queryable = queryable.Where(p => p.Card.CardTypeID == filter.CardTypeID.Value);
                //if (filter.MemberGroupID.HasValue && filter.MemberGroupID != Guid.Empty)
                //{
                //    queryable = queryable.Where(t => t.MemberGroupID == filter.MemberGroupID);
                //}
                //if (filter.MemberGradeID.HasValue)
                //{
                //    queryable = queryable.Where(t => t.MemberGradeID == filter.MemberGradeID);
                //}
            }
            queryable = queryable.OrderByDescending(t => t.CreatedDate);
            if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
            {
                result.TotalCount = queryable.Count();
                result.Items = queryable.Skip(filter.Start.Value).Take(filter.Limit.Value)
                    .MapTo<MemberDto>().ToArray();
            }
            else
            {
                result.Items = queryable.MapTo<MemberDto>().ToArray();
                result.TotalCount = result.Items.Count();
            }
            result.Items.ForEach(t =>
            {
                t.CardTypeDesc = t.CardType != null ? t.CardType.Name : string.Empty;
            });
            return result;
        }

        /// <summary>
        /// 会员注册，微信渠道
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        public string Register(MemberRegisterDto registerDto)
        {
            if (registerDto == null)
                return string.Empty;
            var exist = this.Repository.Exists(t => t.WeChatOpenID == registerDto.WeChatOpenID);
            if (exist)
                throw new DomainException("微信已绑定会员卡");
            var member = this.Repository.GetQueryable().Where(t => t.MobilePhoneNo == registerDto.MobilePhoneNo).FirstOrDefault();
            //if (exist)
            //    throw new DomainException("手机号码已绑定会员卡");
            if (member != null)
            {
                member.WeChatOpenID = registerDto.WeChatOpenID;
                member.Password = Util.EncryptPassword(member.CardNumber, registerDto.Password);
                //member.MemberGradeID = registerDto.MemberGradeID;
                Repository.Update(member);
                return member.CardNumber;
            }
            else
            {
                this.UnitOfWork.BeginTransaction();
                try
                {
                    var memberCard = MemberCardService.GenerateVirtualCard();
                    var newMember = registerDto.MapTo<Member>();
                    newMember.Name = string.Empty;
                    newMember.CardID = memberCard.ID;
                    newMember.CardNumber = memberCard.Code;
                    newMember.Password = registerDto.Password;
                    newMember.Source = EMemberSource.WeChat;
                    //newMember.MemberGradeID = registerDto.MemberGradeID;
                    Add(newMember);
                    this.UnitOfWork.CommitTransaction();

                    //try
                    //{
                    //    var setResult = AdjustMemberPoint(newMember.ID, EMemberPointType.Register, 0);
                    //    if (!setResult)
                    //        AppContext.Logger.Error("会员注册送积分操作失败");
                    //}
                    //catch (Exception ex)
                    //{
                    //    AppContext.Logger.Error($"会员注册送积分操作失败,{ex.Message}");
                    //}

                    return newMember.CardNumber;
                }
                catch (Exception ex)
                {
                    AppContext.Logger.Error("会员注册失败", ex);
                    this.UnitOfWork.RollbackTransaction();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 获取号码归属地
        /// </summary>
        /// <param name="phoneNumber">手机号码</param>
        /// <returns></returns>
        public string GetPhoneLoaction(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return string.Empty;
            var location = "未知";
            //拍拍网获取归属地API地址
            var requestUrl = string.Format("http://cx.shouji.360.cn/phonearea.php?number={0}", phoneNumber);
            try
            {
                var result = HttpHelper.GetString(requestUrl);
                //返回结果:{"code":0,"data":{"province":"\u798f\u5efa","city":"\u53a6\u95e8","sp":"\u79fb\u52a8"}}
                if (string.IsNullOrEmpty(result))
                    return location;
                var phoneLocation = JsonHelper.FromJson<dynamic>(result);
                if (phoneLocation == null)
                    return location;
                var province = phoneLocation.data.province;
                var city = phoneLocation.data.city;
                location = string.Concat(province, " ", city);
            }
            catch (Exception ex)
            {
                AppContext.Logger.Error("获取号码归属地失败，", ex);
            }
            return location;
        }

        /// <summary>
        /// 根据会员卡号获取会员信息
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public Member GetMemberByCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                return null;
            return Repository.GetIncludes(false)//"OwnerGroup", "MemberGrade", "MemberGrade.GradeRights"
                .FirstOrDefault(t => t.CardNumber == cardNumber);
        }

        /// <summary>
        /// 根据手机号码获取会员信息
        /// </summary>
        /// <param name="mobilePhoneNo"></param>
        /// <returns></returns>
        public Member GetMemberByMobilePhone(string mobilePhoneNo)
        {
            if (string.IsNullOrEmpty(mobilePhoneNo))
                return null;
            return this.Repository.GetIncludes(false)//"Card", "OwnerGroup", "MemberGrade", "MemberGrade.GradeRights"
                .FirstOrDefault(t => t.MobilePhoneNo == mobilePhoneNo);
        }

        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public bool ReportLoss(string cardNumber)
        {
            //var cardService = this.MemberCardService;
            //var card = cardService.Query(p => p.Code == cardNumber).FirstOrDefault();
            //if (card == null)
            //    throw new DomainException("未找到对应的会员卡信息");
            //card.Status = ECardStatus.Lost;
            //cardService.Update(card);
            //return true;
            return MemberCardService.ReportOrCancelLoss(cardNumber, ECardStatus.Lost);
        }

        /// <summary>
        /// 解挂
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public bool CancelLoss(string cardNumber)
        {
            //var cardService = this.MemberCardService;
            //var card = cardService.Query(p => p.Code == cardNumber).FirstOrDefault();
            //if (card == null)
            //    throw new DomainException("未找到对应的会员卡信息");
            //card.Status = ECardStatus.Activated;
            //cardService.Update(card);
            return MemberCardService.ReportOrCancelLoss(cardNumber, ECardStatus.Activated);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="changeDto"></param>
        /// <returns></returns>
        public bool ChangePassword(ChangePasswordDto changeDto)
        {
            if (changeDto == null)
                return false;

            if (string.IsNullOrEmpty(changeDto.OldPassword) || string.IsNullOrEmpty(changeDto.NewPassword))
                throw new DomainException("密码不能为空");
            var member = this.Repository.GetByKey(changeDto.MemberID);
            if (member == null)
                throw new DomainException("会员不存在");
            var oldPassword = Util.EncryptPassword(member.CardNumber, changeDto.OldPassword);
            if (!oldPassword.Equals(member.Password))
                throw new DomainException("原密码不正确");
            member.Password = Util.EncryptPassword(member.CardNumber, changeDto.NewPassword);
            this.Repository.Update(member);
            return true;
        }

        /// <summary>
        /// 重置密码，将密码重置为123456
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public bool ResetPassword(Guid memberID)
        {
            var member = this.Repository.GetByKey(memberID);
            if (member == null)
                throw new DomainException("会员不存在");
            member.Password = Util.EncryptPassword(member.CardNumber, "123456");
            this.Repository.Update(member);
            return true;
        }

        /// <summary>
        /// 更新会员信息
        /// </summary>
        /// <param name="updateMember"></param>
        /// <returns></returns>
        public bool UpdateMemberInfoForWeChat(UpdateMemberDto updateMember)
        {
            if (updateMember == null)
                return false;
            var member = Repository.GetByKey(updateMember.MemberID);
            if (member == null)
                throw new DomainException("未找到对应的会员信息");
            member.Name = updateMember.Name;
            if (!updateMember.Birthday.HasValue)
                member.Birthday = updateMember.Birthday;
            member.Sex = updateMember.Sex;
            Repository.Update(member);
            return true;
        }

        /// <summary>
        /// 获取会员基本信息
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public MemberBaseInfoDto GetBaseInfo(Guid memberId)
        {
            var member = Repository.GetInclude(p => p.Card).FirstOrDefault(p => p.ID == memberId);
            if (member == null)
                throw new DomainException("未找到相关信息");

            var memberInfo = new MemberBaseInfoDto
            {
                Status = member.Card.Status.GetDescription(),
                EffectiveDate = member.Card.EffectiveDate.HasValue ? member.Card.EffectiveDate.Value.ToDateString() : string.Empty,
                ExpiredDate = member.Card.ExpiredDate.HasValue ? member.Card.ExpiredDate.Value.ToDateString() : string.Empty,
                LastRechargeMoney = RechargeHistoryService.LastRecharge(p => p.CardID == member.CardID).ToString("f1"),
                CardBalance = member.Card.CardBalance.ToString("f1"),
                TotalConsume = member.Card.TotalConsume.ToString("f1"),
                TotalRecharge = member.Card.TotalRecharge.ToString("f1")
            };
            return memberInfo;
        }

        /// <summary>
        /// 获取顾客会员信息
        /// </summary>
        /// <param name="mobilePhoneNo">手机号码</param>
        /// <param name="wechatOpenID">微信openID</param>
        /// <returns></returns>
        public MemberDto GetCustomerMemberInfo(string mobilePhoneNo, string wechatOpenID)
        {
            if (string.IsNullOrEmpty(mobilePhoneNo) && string.IsNullOrEmpty(wechatOpenID))
                throw new DomainException("参数错误");
            var queryable = Repository.GetQueryable(false);
            if (!string.IsNullOrEmpty(mobilePhoneNo))
            {
                queryable = queryable.Where(p => p.MobilePhoneNo == mobilePhoneNo);
            }
            if (!string.IsNullOrEmpty(wechatOpenID))
            {
                queryable = queryable.Where(p => p.WeChatOpenID == wechatOpenID);
            }
            return queryable.MapTo<MemberDto>().FirstOrDefault();
        }

        /// <summary>
        /// 根据微信OpenID获取会员信息
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        public MemberCardDto GetMemberInfoByWeChat(string openID)
        {
            if (string.IsNullOrEmpty(openID))
                return null;
            var member = this.Repository.GetIncludes(false, "Card")//, "OwnerGroup", "MemberGrade"
                .FirstOrDefault(t => t.WeChatOpenID == openID);

            if (member == null)
                return null;
            //MemberGrade highGrade = null;
            //var limitDate = DateTime.Now;
            //if (member.MemberGrade != null)
            //{
            //    highGrade = MemberGradeRepo.GetQueryable(false)
            //                .Where(t => !t.IsNotOpen && t.Status == EMemberGradeStatus.Enabled && t.Level > member.MemberGrade.Level && t.IsQualifyByConsume).OrderBy(t => t.Level).FirstOrDefault();
            //    if (highGrade != null)
            //        limitDate = DateTime.Today.AddMonths(-1 * highGrade.QualifyByConsumeLimitedMonths.Value);
            //}
            //var consumeAmountOfReachNextGrade = highGrade?.QualifyByConsumeTotalAmount;
            //var consumeCountOfReachNextGrade = highGrade?.QualifyByConsumeTotalCount;
            //var consumeAmountOfCurrentGrade = TradeHistoryRepo.GetQueryable(false).Where(t => t.MemberID == member.ID && t.MemberGradeID == member.MemberGradeID).GroupBy(t => 1).Select(t => t.Sum(s => s.TradeAmount)).FirstOrDefault();
            //var consumeCountOfCurrentGrade = TradeHistoryRepo.Count(t => t.MemberID == member.ID && t.MemberGradeID == member.MemberGradeID && t.CreatedDate > limitDate);
            var cardInfo = new MemberCardDto
            {
                MemberID = member.ID,
                MemberName = member.Name,
                MemberPoint = member.Point,
                MobilePhoneNo = member.MobilePhoneNo,
                Birthday = member.Birthday.HasValue ? member.Birthday.Value.ToDateString() : string.Empty,
                Sex = ((int)member.Sex).ToString(),
                MemberGroup = "普通会员",//member.OwnerGroup == null ? "普通会员" : member.OwnerGroup.Name,
                CardID = member.Card.ID,
                CardNumber = member.Card.Code,
                CardType = member.Card.CardType != null ? member.Card.CardType.Name : String.Empty,
                AllowDiscount = member.Card.CardType != null ? member.Card.CardType.AllowDiscount : true,
                AllowRecharge = member.Card.CardType != null ? member.Card.CardType.AllowRecharge : true,
                CardStatus = member.Card.Status.GetDescription(),
                EffectiveDate = member.Card.EffectiveDate.HasValue ? member.Card.EffectiveDate.Value.ToDateString() : string.Empty,
                ExpiredDate = member.Card.ExpiredDate.HasValue ? member.Card.ExpiredDate.Value.ToDateString() : string.Empty,
                CardBalance = member.Card.CardBalance,
                MemberGrade = "",//member.MemberGrade != null ? member.MemberGrade.Name : string.Empty,
                IsNotOpen = false,//member.MemberGrade != null ? member.MemberGrade.IsNotOpen : false,
                //ConsumeAmountOfReachNextGrade = consumeAmountOfReachNextGrade,
                //ConsumeCountOfReachNextGrade = consumeCountOfReachNextGrade,
                //ConsumeAmountOfCurrentGrade = consumeAmountOfCurrentGrade,
                //ConsumeCountOfCurrentGrade = consumeCountOfCurrentGrade,
                //CurrentConsumeAmountRate = consumeAmountOfReachNextGrade != null ? consumeAmountOfCurrentGrade / consumeAmountOfReachNextGrade.Value : 0,
                //CurrentConsumeCountRate = consumeCountOfReachNextGrade != null ? (decimal)consumeCountOfCurrentGrade / consumeCountOfReachNextGrade.Value : 0,
            };
            return cardInfo;
        }

        #endregion

        #region methodsTemp

        ///// <summary>
        ///// 获取会员分析-用户明细分页数据
        ///// </summary>
        ///// <param name="filter">过滤参数</param>
        ///// <returns></returns>
        //public PagedResultDto<MemberDetailDto> GetMemberDetail(MemberDetailFilter filter)
        //{
        //    var queryable = Repository.GetQueryable(false);

        //    //时间过滤
        //    if (filter != null && filter.StartDate != null && filter.EndDate != null)
        //    {
        //        var endDate = filter.EndDate.Value.AddDays(1);
        //        queryable = queryable.Where(w => w.CreatedDate >= filter.StartDate && w.CreatedDate < endDate);
        //    }

        //    //端口过滤
        //    if (filter.Source != null && (int)filter.Source != -1)
        //    {
        //        var source = filter.Source.Value;
        //        queryable = queryable.Where(w => w.Source == source);
        //    }
        //    var totalCount = queryable.Count();
        //    //分页
        //    if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
        //    {
        //        queryable = queryable.OrderByDescending(o => o.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
        //    }

        //    var tempData = queryable.Select(s => new
        //    {
        //        Id = s.WeChatOpenID,
        //        CreatedDate = s.CreatedDate,
        //        Name = s.Name,
        //        Sex = s.Sex,
        //        MobilePhoneNo = s.MobilePhoneNo,
        //        PhoneLocation = s.PhoneLocation
        //    }).ToList();
        //    return new PagedResultDto<MemberDetailDto>(totalCount, tempData.Select(s => new MemberDetailDto
        //    {
        //        ID = s.Id,
        //        CreatedDate = s.CreatedDate,
        //        Name = s.Name,
        //        Sex = s.Sex,
        //        MobilePhoneNo = s.MobilePhoneNo,
        //        Province = s.PhoneLocation.Split(' ').FirstOrDefault(),
        //        City = s.PhoneLocation.Split(' ').LastOrDefault()
        //    }));
        //}

        ///// <summary>
        /////会员分析->人数分析->获取用户明细
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <returns></returns>
        //public IEnumerable<MemberDetailDto> GetMemberDetailForConsumeAnalysis(MemberDetailFilter filter, ref int totalCount)
        //{
        //    var memberqueryable = Repository.GetQueryable(false);
        //    var tradehistoryQueryable = TradeHistoryRepo.GetQueryable(false);
        //    var enddate = ((DateTime)filter.EndDate).AddDays(1);
        //    if (filter.StartDate.HasValue && filter.EndDate.HasValue)
        //    {
        //        tradehistoryQueryable = tradehistoryQueryable.Where(t => t.CreatedDate >= filter.StartDate && t.CreatedDate < enddate);
        //    }
        //    var membercardids = tradehistoryQueryable.ToArray().Select(t => t.CardID).ToArray();
        //    if (filter.IsConsume)
        //    {
        //        memberqueryable = memberqueryable.Where(m => membercardids.Contains(m.CardID));
        //    }
        //    else
        //    {
        //        memberqueryable = memberqueryable.Where(m => !membercardids.Contains(m.CardID));
        //    }
        //    totalCount = memberqueryable.Count();
        //    if (filter.Start.HasValue && filter.Limit.HasValue)
        //    {
        //        memberqueryable = memberqueryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
        //    }
        //    var result = memberqueryable.ToArray().Select(t => new MemberDetailDto
        //    {
        //        ID = t.WeChatOpenID,
        //        CreatedDate = t.CreatedDate,
        //        Name = t.Name,
        //        Sex = t.Sex,
        //        MobilePhoneNo = t.MobilePhoneNo,
        //        Province = t.PhoneLocation != null ? t.PhoneLocation.Split(' ').FirstOrDefault() : "",
        //        City = t.PhoneLocation != null ? t.PhoneLocation.Split(' ').LastOrDefault() : "",
        //    }).ToArray();
        //    return result;
        //}

        ///// <summary>
        ///// 获取会员分析-新增用户
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <returns></returns>
        //public IEnumerable<NewMemberCountOfDateRange> GetNewMemberCount(NewMemberFilter filter)
        //{
        //    var queryable = Repository.GetQueryable(false);
        //    if (filter != null)
        //    {
        //        var endDate = filter.EndDate.AddDays(1);
        //        queryable = queryable.Where(w => w.CreatedDate >= filter.StartDate && w.CreatedDate < endDate);
        //        if ((int)filter.Source != -1)
        //        {
        //            queryable = queryable.Where(w => w.Source == filter.Source);
        //        }
        //    }
        //    var tempData = queryable.GroupBy(q => DbFunctions.TruncateTime(q.CreatedDate)).Select(s => new
        //    {
        //        Date = s.Key,
        //        Count = s.Count()
        //    }).ToList();
        //    List<NewMemberCountOfDateRange> resultData = new List<NewMemberCountOfDateRange>();
        //    for (DateTime i = filter.StartDate.Date; i <= filter.EndDate.Date; i = i.AddDays(1))
        //    {
        //        var item = tempData.FirstOrDefault(f => f.Date == i);
        //        var newMemberCountOfDateRange = new NewMemberCountOfDateRange
        //        {
        //            Date = i.ToShortDateString(),
        //            Count = item == null ? 0 : item.Count
        //        };
        //        resultData.Add(newMemberCountOfDateRange);
        //    }
        //    return resultData;
        //}

        ///// <summary>
        ///// 获取会员Lite信息
        ///// </summary>
        ///// <returns></returns>
        //public MemberLiteInfoDto GetMemberLiteInfo(EMemberFindType findType, string keyword)
        //{
        //    var memberQueryable = Repository.GetQueryable(false);
        //    switch (findType)
        //    {
        //        case EMemberFindType.Number:
        //            memberQueryable = memberQueryable.Where(t => t.CardNumber == keyword || t.MobilePhoneNo == keyword);
        //            break;
        //        case EMemberFindType.WeChatOpenID:
        //            memberQueryable = memberQueryable.Where(t => t.WeChatOpenID == keyword);
        //            break;
        //        default:
        //            return null;
        //    }
        //    return memberQueryable.MapTo<MemberLiteInfoDto>().FirstOrDefault();
        //}

        ///// <summary>
        ///// 获取会员Nano信息
        ///// </summary>
        ///// <returns></returns>
        //public MemberNanoInfoDto GetMemberNanoInfo(EMemberFindType findType, string keyword)
        //{
        //    var memberQueryable = Repository.GetQueryable(false);
        //    switch (findType)
        //    {
        //        case EMemberFindType.Number:
        //            memberQueryable = memberQueryable.Where(t => t.CardNumber == keyword);
        //            break;
        //        case EMemberFindType.WeChatOpenID:
        //            memberQueryable = memberQueryable.Where(t => t.WeChatOpenID == keyword);
        //            break;
        //        default:
        //            return null;
        //    }
        //    return memberQueryable.MapTo<MemberNanoInfoDto>().FirstOrDefault();
        //}

        ///// <summary>
        ///// 根据账号密码获取会员Nano信息
        ///// </summary>
        ///// <param name="account"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //public MemberNanoInfoDto GetMemberNanoInfoByPassword(string account, string password)
        //{
        //    var memberInfo = Repository.GetQueryable(false)
        //        .Where(t => t.MobilePhoneNo == account || t.CardNumber == account)
        //        .Select(t => new
        //        {
        //            t.ID,
        //            t.CardNumber,
        //            t.Name,
        //            t.Password,
        //            t.WeChatOpenID
        //        }).FirstOrDefault();
        //    if (memberInfo == null)
        //        throw new DomainException("账号或密码不正确");
        //    password = Util.EncryptPassword(memberInfo.CardNumber, password);
        //    if (!password.Equals(memberInfo.Password))
        //        throw new DomainException("账号或密码不正确");
        //    return new MemberNanoInfoDto
        //    {
        //        ID = memberInfo.ID,
        //        CardNumber = memberInfo.CardNumber,
        //        Name = memberInfo.Name,
        //        WeChatOpenID = memberInfo.WeChatOpenID,
        //    };
        //}

        ///// <summary>
        ///// 换卡
        ///// </summary>
        ///// <param name="changeCardDto"></param>
        ///// <returns></returns>
        //public bool ChangeCard(ChangeCardDto changeCardDto)
        //{
        //    if (changeCardDto == null)
        //        throw new DomainException("参数错误");
        //    var member = this.Repository.GetByKey(changeCardDto.MemberID);
        //    if (member == null)
        //        throw new DomainException("会员不存在");
        //    var oldPassword = Util.EncryptPassword(member.CardNumber, changeCardDto.OldPassword);
        //    if (!oldPassword.Equals(member.Password))
        //        throw new DomainException("原密码不正确");
        //    this.UnitOfWork.BeginTransaction();
        //    try
        //    {
        //        var newCard = MemberCardService.ChangeCard(member.CardID, changeCardDto.CardNumber, changeCardDto.VerifyCode);
        //        member.CardID = newCard.ID;
        //        member.CardNumber = newCard.Code;
        //        member.Password = changeCardDto.NewPassword;
        //        member.LastUpdateUserID = AppContext.CurrentSession.UserID;
        //        member.LastUpdateUser = AppContext.CurrentSession.UserName;
        //        member.LastUpdateDate = DateTime.Now;
        //        this.Update(member);
        //        this.UnitOfWork.CommitTransaction();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.UnitOfWork.RollbackTransaction();
        //        AppContext.Logger.Error("会员换卡失败", ex);
        //        throw ex;
        //    }
        //}

        //public StatisticDto Statistic()
        //{
        //    return Repository.GetQueryable(false).GroupBy(p => 0).Select(g => new StatisticDto
        //    {
        //        Recharge = g.Sum(t => t.Card.TotalRecharge),
        //        Consume = g.Sum(t => t.Card.TotalConsume),
        //        MemberCount = g.Count(t => !t.IsDeleted),
        //        RechargeMemberCount = g.Count(t => t.Card.CardTypeID == MemberCardTypes.PrePaidCard),
        //        DiscountMemberCount = g.Count(t => t.Card.CardTypeID == MemberCardTypes.DiscountCard),
        //        Fund = g.Sum(t => t.Card.CardBalance),
        //        Give = g.Sum(t => t.Card.TotalGive),
        //        RechargeAndGive = g.Sum(t => t.Card.TotalRecharge + t.Card.TotalGive)
        //    }).FirstOrDefault() ?? new StatisticDto();
        //}

        //public bool ChangeMemberGroup(ChangeMemberGroupDto changeDto)
        //{
        //    if (changeDto == null || changeDto.MemberIDList == null || changeDto.MemberIDList.Count() < 1)
        //        throw new DomainException("参数错误");
        //    var memberGroupID = changeDto.MemberGroupID;
        //    if (!memberGroupID.HasValue && memberGroupID == Guid.Empty)
        //    {
        //        memberGroupID = MemberGroupService.GetDefaultMemberGroupID();
        //    }
        //    else
        //    {
        //        var exists = MemberGroupService.Exists(t => t.ID == memberGroupID);
        //        if (!exists)
        //            throw new DomainException("会员分组不存在");
        //    }
        //    var memberList = Repository.GetQueryable()
        //        .Where(t => changeDto.MemberIDList.Contains(t.ID))
        //        .ToList();
        //    memberList.ForEach(t => t.MemberGroupID = memberGroupID);
        //    Repository.Update(memberList);
        //    return true;
        //}

        //public bool AdjustMemberPoint(string openId, EMemberPointType pointType, int adjustPoints = 0)
        //{
        //    var member = Repository.GetQueryable(false)
        //        .Where(t => t.WeChatOpenID == openId)
        //        .FirstOrDefault();
        //    if (member == null)
        //    {
        //        throw new DomainException("您还不是会员，请先注册");
        //    }
        //    if (member.Point + adjustPoints < 0)
        //    {
        //        throw new DomainException("您的积分不足！");
        //    }
        //    return AdjustMemberPoint(member.ID, pointType, adjustPoints);
        //}

        //public bool AdjustMemberPoint(Guid memberID, EMemberPointType pointType, int adjustPoints, string outTradeNo = "")
        //{
        //    var result = false;
        //    var remark = string.Empty;
        //    var memberPoint = MemberPointRepo.GetInclude(t => t.AdditionalRules, false).Where(t => t.Type == pointType).FirstOrDefault();
        //    if (memberPoint != null)
        //    {
        //        if (!memberPoint.IsAvailable)
        //        {
        //            AppContext.Logger.Debug($"设置会员积分失败，对应参数type为{pointType}类型的积分规则未启用");
        //            return result;
        //        }
        //        var historyQuery = MemberPointHistoryRepo.GetQueryable(false).Where(t => t.Source == pointType && t.MemberID == memberID);
        //        var historyCount = historyQuery.Count();
        //        var todayCount = historyQuery.Where(t => t.CreatedDate >= DateTime.Today).Count();
        //        adjustPoints = memberPoint.Point;
        //        var additionalPoints = 0;
        //        var additionalRules = memberPoint.AdditionalRules.OrderBy(t => t.Count);
        //        if (pointType == EMemberPointType.SignIn)
        //        {
        //            foreach (var additional in additionalRules)
        //            {
        //                var oriNum = (historyCount + 1) / (decimal)additional.Count;
        //                if (Math.Ceiling(oriNum) == oriNum)
        //                {
        //                    additionalPoints = additional.Point;
        //                }
        //            }
        //            adjustPoints += additionalPoints;
        //        }
        //        else if (pointType == EMemberPointType.Share || pointType == EMemberPointType.Appraise)
        //        {
        //            if (todayCount >= memberPoint.Limit)
        //            {
        //                AppContext.Logger.Error("增加会员积分超过限制次数");
        //                return false;
        //            }
        //            foreach (var additional in additionalRules)
        //            {
        //                if (additional.Count > 0)
        //                {
        //                    var oriNum = (historyCount + 1) / (decimal)additional.Count;
        //                    if (Math.Ceiling(oriNum) == oriNum)
        //                    {
        //                        additionalPoints = additional.Point;
        //                    }
        //                }
        //            }
        //            adjustPoints += additionalPoints;
        //        }
        //    }
        //    var member = Repository.GetByKey(memberID);
        //    if (member == null)
        //    {
        //        AppContext.Logger.Debug($"调整会员积分失败，会员信息不存在");
        //        return result;
        //    }
        //    member.Point += adjustPoints;
        //    result = Repository.Update(member) > 0;
        //    if (result)
        //    {
        //        MemberPointHistoryRepo.Add(new MemberPointHistory
        //        {
        //            ID = Util.NewID(),
        //            MemberID = member.ID,
        //            Point = adjustPoints,
        //            Source = pointType,
        //            Remark = pointType.GetDescription(),
        //            CreatedDate = DateTime.Now,
        //            OutTradeNo = outTradeNo,
        //        });
        //    }
        //    else
        //    {
        //        AppContext.Logger.Error("更新会员失败导致设置会员积分失败");
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 设置会员等级
        ///// </summary>
        ///// <param name="memberID">会员ID</param>
        ///// <param name="memberGradeID">会员等级ID</param>
        ///// <returns></returns>
        //public bool SetMemberGrade(Guid memberID, Guid memberGradeID)
        //{
        //    var memberGrade = MemberGradeService
        //        .Query(grade => grade.ID == memberGradeID)
        //        .FirstOrDefault();
        //    if (memberGrade == null)
        //        throw new DomainException("会员等级参数错误");
        //    return SetMemberGrade(memberID, memberGrade);
        //}

        ///// <summary>
        ///// 设置会员等级
        ///// </summary>
        ///// <param name="memberID"></param>
        ///// <param name="memberGrade"></param>
        ///// <returns></returns>
        //public bool SetMemberGrade(Guid memberID, MemberGrade memberGrade)
        //{
        //    var member = Repository.GetByKey(memberID);
        //    return SetMemberGrade(member, memberGrade);
        //}

        ///// <summary>
        ///// 设置会员等级
        ///// </summary>
        ///// <param name="member"></param>
        ///// <param name="memberGrade"></param>
        ///// <returns></returns>
        //bool SetMemberGrade(Member member, MemberGrade memberGrade)
        //{
        //    if (member == null)
        //    {
        //        AppContext.Logger.Debug("设置会员等级失败，会员不存在");
        //        return false;
        //    }
        //    if (member.MemberGradeID == memberGrade.ID)
        //        return true;
        //    MemberGradeHistoryService.Add(new MemberGradeHistory
        //    {
        //        MemberID = member.ID,
        //        BeforeMemberGradeID = member.MemberGradeID.GetValueOrDefault(),
        //        AfterMemberGradeID = memberGrade.ID,
        //    });
        //    member.MemberGradeID = memberGrade.ID;
        //    Repository.Update(member);
        //    if (memberGrade.GradePoint > 0)
        //    {
        //        AdjustMemberPoint(member.ID, EMemberPointType.MemberGradeUpgrade, memberGrade.GradePoint);
        //    }
        //    return true;
        //}

        ///// <summary>
        ///// 校验会员是否享有POS权益
        ///// </summary>
        ///// <param name="memberCardNumber">会员卡号</param>
        ///// <param name="posRightID">POS权益ID</param>
        ///// <returns></returns>
        //public bool CheckMemberPosRight(string memberCardNumber, Guid posRightID)
        //{
        //    var hasRight = Repository.Exists(t => t.CardNumber == memberCardNumber
        //            && t.MemberGrade.GradeRights.Any(right => right.RightType == EGradeRightType.Product && right.PosRightID == posRightID));
        //    if (!hasRight)
        //    {
        //        IRepository<MemberGrade> gradeRepo = ServiceLocator.Instance.GetService<IRepository<MemberGrade>>();
        //        var gradeName = gradeRepo.GetQueryable(false)
        //            .Where(t => t.GradeRights.Any(right => right.RightType == EGradeRightType.Product && right.PosRightID == posRightID))
        //            .OrderBy(t => t.Level).Select(t => t.Name)
        //            .FirstOrDefault();
        //        throw new DomainException($"尊敬的客户，该菜品是{gradeName}专享菜品");
        //    }
        //    return hasRight;
        //}

        //public IEnumerable<MemberSignInDto> GetMemberSignInOfContinuous(MemberSignInFilter filter)
        //{
        //    var result = new List<MemberSignInDto>();
        //    var resultItem = new List<MemberSignInDtoItem>();
        //    var resultItemTemp = new List<MemberSignIn>();
        //    if (filter == null || string.IsNullOrEmpty(filter.OpenID))
        //    {
        //        throw new DomainException("参数错误");
        //    }
        //    var member = Repository.GetInclude(t => t.MemberSignIn, false).Where(t => t.WeChatOpenID == filter.OpenID).FirstOrDefault();
        //    if (member == null)
        //    {
        //        throw new DomainException("找不到会员信息");
        //    }

        //    var point = 0;
        //    var todayPoint = 0;
        //    var additionalRuleCount = new List<int>();
        //    var pointRule = MemberPointRepo.GetInclude(t => t.AdditionalRules, false).Where(t => t.Type == EMemberPointType.SignIn).FirstOrDefault();
        //    if (pointRule != null)
        //    {
        //        if (pointRule.AdditionalRules != null)
        //        {
        //            additionalRuleCount = pointRule.AdditionalRules.Select(t => t.Count).ToList();
        //        }
        //        point = pointRule.Point;
        //        todayPoint = pointRule.Point;
        //    }
        //    var memberSignIns = member.MemberSignIn.Where(t => t.IsDeleted == false).OrderBy(t => t.CreatedDate).ToList();
        //    var signindate = memberSignIns.Select(t => t.CreatedDate.Date).ToArray();

        //    var todaySigninPoint = point;
        //    var todayIsSigned = false;
        //    if (memberSignIns.Count() > 0)
        //    {
        //        var todaySignin = memberSignIns.Where(t => t.CreatedDate.Date == DateTime.Now.Date).FirstOrDefault();
        //        if (todaySignin != null)
        //        {
        //            todayIsSigned = true;
        //            todaySigninPoint = todaySignin.Point;
        //        }
        //    }

        //    if (member.MemberSignIn == null || member.MemberSignIn.Count < 1 || !signindate.Contains(DateTime.Now.Date.AddDays(-1)))
        //    {
        //        if (pointRule != null && pointRule.AdditionalRules != null && pointRule.AdditionalRules.Count > 0)
        //        {
        //            var rule = pointRule.AdditionalRules.Where(t => t.Count == 1).FirstOrDefault();
        //            if (rule != null)
        //            {
        //                todayPoint += rule.Point;
        //            }
        //        }
        //        if (filter.Limit.HasValue)
        //        {
        //            resultItem.Add(new MemberSignInDtoItem
        //            {
        //                Index = -1,
        //                IsLastPage = true,
        //                TodayPoint = todayPoint,
        //                SignedDays = signindate.Contains(DateTime.Now.Date) ? 1 : 0,
        //                TodayIsSigned = todayIsSigned,
        //                IsFirstPage = true,
        //            });
        //            for (var i = 0; i < filter.Limit.Value; i++)
        //            {
        //                resultItem.Add(new MemberSignInDtoItem
        //                {
        //                    Day = i + 1,
        //                    Index = i + 1,
        //                    SignedDays = signindate.Contains(DateTime.Now.Date) ? 1 : 0,
        //                    Point = i == 0 ? todaySigninPoint : point,
        //                    TodayPoint = todayPoint,
        //                    IsToday = i == 0,
        //                    IsGifDay = additionalRuleCount.Contains(i + 1),
        //                    IsSignedDay = signindate.Contains(DateTime.Now.Date) && i == 0,
        //                    IsLastPage = true,
        //                });
        //            }
        //            resultItem.Add(new MemberSignInDtoItem
        //            {
        //                Index = -2,
        //                IsLastPage = true,
        //            });
        //        }
        //    }
        //    else
        //    {
        //        var uncontinuousDate = signindate.Where(t => !signindate.Contains(t.AddDays(1))).ToList();
        //        var uncontinuous = uncontinuousDate.Count() > 1;

        //        if (uncontinuous)
        //        {
        //            uncontinuousDate.Remove(uncontinuousDate.Last());
        //            var continuousBeginDate = uncontinuousDate.Last();
        //            resultItemTemp = memberSignIns.Where(t => t.CreatedDate > continuousBeginDate.Date).ToList();
        //        }
        //        else
        //        {
        //            resultItemTemp = memberSignIns;
        //        }

        //        var signeddays = resultItemTemp.Count;
        //        var daystart = 1;
        //        if (filter.Start.HasValue && filter.Limit.HasValue)
        //        {
        //            var startIndex = filter.Start.Value;
        //            if (filter.RowLimit.HasValue && filter.RowCount.HasValue)
        //            {
        //                var totalCountPerPage = filter.RowLimit.Value * filter.RowCount.Value;
        //                if (resultItemTemp.Count > totalCountPerPage)
        //                {
        //                    var totalPage = Math.Ceiling((decimal)resultItemTemp.Count / totalCountPerPage);
        //                    var page = filter.Limit.Value > 0 ? filter.Start.Value / filter.Limit.Value : 0;
        //                    var startPage = totalPage - page - 1;
        //                    if (page >= 0 && startPage >= 0)
        //                    {
        //                        startIndex = (int)startPage * filter.Limit.Value;
        //                    }
        //                }
        //            }
        //            resultItemTemp = resultItemTemp.OrderBy(t => t.CreatedDate).Skip(startIndex).Take(filter.Limit.Value).ToList();
        //            daystart += startIndex;
        //        }

        //        var index = 0;
        //        if (pointRule != null)
        //        {
        //            var signeddaystemp = signindate.Contains(DateTime.Now.Date) ? resultItemTemp.Count : resultItemTemp.Count + 1;
        //            foreach (var additionalRule in pointRule.AdditionalRules)
        //            {
        //                if (additionalRule.Count == signeddaystemp)
        //                {
        //                    todayPoint += additionalRule.Point;
        //                    break;
        //                }
        //            }
        //        }

        //        var totalCountOfPage = -1;
        //        if (filter.RowCount.HasValue && filter.RowLimit.HasValue)
        //        {
        //            totalCountOfPage = filter.RowCount.Value * filter.RowLimit.Value;
        //        }
        //        resultItem.Add(new MemberSignInDtoItem
        //        {
        //            Index = -1,
        //            TodayPoint = todayPoint,
        //            TodayIsSigned = todayIsSigned,
        //            SignedDays = signeddays,
        //            IsFirstPage = daystart == 1,
        //        });
        //        var signedDays = resultItemTemp.Select(t => new MemberSignInDtoItem
        //        {
        //            Day = daystart + index,
        //            Index = index++,
        //            SignedDays = signeddays,
        //            Point = t.Point,
        //            TodayPoint = todayPoint,
        //            IsToday = t.CreatedDate.Date == DateTime.Now.Date,
        //            IsGifDay = false,
        //            IsSignedDay = true,
        //        }).ToList();
        //        resultItem.AddRange(signedDays);

        //        var isLastPage = false;
        //        if (filter.Limit.HasValue && filter.Limit.Value > signedDays.Count)
        //        {
        //            isLastPage = true;
        //            var signedLastDate = signindate != null && signindate.Count() > 0 ? signindate.Last() : DateTime.Now;
        //            for (var i = 0; i < filter.Limit.Value - signedDays.Count; i++)
        //            {
        //                resultItem.Add(new MemberSignInDtoItem
        //                {
        //                    Day = daystart + index,
        //                    SignedDays = signeddays,
        //                    Point = point,
        //                    TodayPoint = todayPoint,
        //                    IsToday = (signedLastDate.Date == DateTime.Now.AddDays(-1).Date) && i == 0,
        //                    IsGifDay = additionalRuleCount.Contains(daystart + index),//signedDays.Count + i + 1,
        //                    IsSignedDay = false,
        //                    Index = index++,
        //                });
        //            }
        //        }

        //        resultItem.Add(new MemberSignInDtoItem
        //        {
        //            Index = -2,
        //        });

        //        if (isLastPage)//|| filter.Limit.HasValue && filter.Limit.Value == signedDays.Count
        //        {
        //            resultItem.ForEach(t =>
        //            {
        //                t.IsLastPage = true;
        //            });
        //        }

        //        var todaySignin = resultItem.Where(t => t.IsToday).FirstOrDefault();
        //        var firstSignin = resultItem.Where(t => t.Index == -1).FirstOrDefault();
        //        if (todaySignin != null && firstSignin != null)
        //        {
        //            firstSignin.TodayPoint = todaySignin.Point;
        //        }

        //    }

        //    if (filter.RowLimit.HasValue && filter.RowCount.HasValue)
        //    {
        //        var rowLimit = filter.RowLimit.Value;
        //        var start = 0;
        //        var rowCount = filter.RowCount.Value;
        //        for (var i = 0; i < rowLimit; i++)
        //        {
        //            var resItem = new MemberSignInDto();
        //            var item = resultItem.Skip(start).Take(rowCount).ToList();
        //            resItem.Items.AddRange(item);
        //            result.Add(resItem);
        //            start += rowCount;
        //        }
        //    }

        //    return result;
        //}

        //public int SignIn(string openId)
        //{
        //    if (string.IsNullOrEmpty(openId))
        //    {
        //        throw new DomainException("参数错误");
        //    }
        //    var member = Repository.GetInclude(t => t.MemberSignIn).Where(t => t.WeChatOpenID == openId).FirstOrDefault();
        //    if (member == null)
        //    {
        //        throw new DomainException("找不到会员信息");
        //    }
        //    var memberSignInHistory = member.MemberSignIn.Where(t => t.IsDeleted == false).OrderBy(t => t.CreatedDate).ToArray();
        //    var signindate = memberSignInHistory.Select(t => t.CreatedDate.Date).ToArray();
        //    if (signindate.Contains(DateTime.Now.Date))
        //    {
        //        throw new DomainException("今天已签到");
        //    }

        //    var uncontinuousDate = signindate.Where(t => !signindate.Contains(t.AddDays(1))).ToList();
        //    var uncontinuous = uncontinuousDate.Count() > 1;

        //    var signInPointRule = MemberPointRepo.GetInclude(t => t.AdditionalRules, false).Where(t => t.Type == EMemberPointType.SignIn).FirstOrDefault();
        //    var totalPoint = signInPointRule.Point;

        //    var containuesCount = 0;
        //    if (signindate.Contains(DateTime.Now.Date.AddDays(-1)))
        //    {
        //        if (uncontinuous)
        //        {
        //            uncontinuousDate.Remove(uncontinuousDate.Last());
        //            var continuousBeginDate = uncontinuousDate.Last();
        //            containuesCount = memberSignInHistory.Where(t => t.CreatedDate > continuousBeginDate.Date).Count();
        //        }
        //        else
        //        {
        //            containuesCount = memberSignInHistory.Count();
        //        }
        //    }
        //    var additionalPoints = 0;
        //    var additionalRules = signInPointRule.AdditionalRules.OrderBy(t => t.Count);
        //    foreach (var additionalRule in additionalRules)
        //    {
        //        if (additionalRule.Count > 0)
        //        {
        //            var oriNum = (containuesCount + 1) / (decimal)additionalRule.Count;
        //            if (Math.Ceiling(oriNum) == oriNum)
        //            {
        //                additionalPoints = additionalRule.Point;
        //            }
        //        }
        //    }
        //    totalPoint += additionalPoints;

        //    try
        //    {
        //        UnitOfWork.BeginTransaction();
        //        member.Point += totalPoint;
        //        var updateRes = base.Update(member);
        //        if (updateRes)
        //        {
        //            MemberSignInRepo.Add(new MemberSignIn
        //            {
        //                ID = Util.NewID(),
        //                MemberID = member.ID,
        //                Point = totalPoint,
        //                CreatedDate = DateTime.Now,
        //            });
        //        }
        //        UnitOfWork.CommitTransaction();
        //    }
        //    catch (Exception ex)
        //    {
        //        UnitOfWork.RollbackTransaction();
        //        throw new DomainException(ex.Message);
        //    }

        //    return totalPoint;
        //}

        ///// <summary>
        ///// 预览积分抵扣pos金额
        ///// </summary>
        ///// <param name="cardNumber">The card number.</param>
        ///// <param name="paymentAmount">The order amount.</param>
        ///// <returns></returns>
        //public PreviewUsePointPaymentResultDto PreviewUsePointPayment(string cardNumber, decimal paymentAmount)
        //{
        //    var result = new PreviewUsePointPaymentResultDto
        //    {
        //        CardNumber = cardNumber,
        //        Remark = "积分不可用"
        //    };
        //    var member = Repository.GetQueryable(false)
        //        .Where(t => t.CardNumber == cardNumber)
        //        .Select(t => new
        //        {
        //            t.Card.Status,
        //            t.Point,
        //            t.MemberGrade.IsAllowPointPayment,
        //            t.MemberGrade.PonitExchangeValue
        //        }).FirstOrDefault();
        //    if (member == null)
        //    {
        //        AppContext.Logger.Debug("积分抵扣pos金额, 会员卡不存在");
        //        return result;
        //    }
        //    if (member.Status != ECardStatus.Activated)
        //    {
        //        AppContext.Logger.Debug($"积分抵扣pos金额, 会员状态{member.Status.GetDescription()}, 不允许积分支付");
        //        return result;
        //    }
        //    if (!member.IsAllowPointPayment || !member.PonitExchangeValue.HasValue)
        //    {
        //        AppContext.Logger.Debug("积分抵扣pos金额, 不允许使用积分支付");
        //        return result;
        //    }
        //    var usedPoint = member.Point;
        //    var exchangeMoney = usedPoint * member.PonitExchangeValue.Value;
        //    if (exchangeMoney > paymentAmount)
        //    {
        //        exchangeMoney = paymentAmount;
        //        usedPoint = Convert.ToInt32(Math.Floor(exchangeMoney / member.PonitExchangeValue.Value));
        //        exchangeMoney = usedPoint * member.PonitExchangeValue.Value;
        //    }
        //    return new PreviewUsePointPaymentResultDto
        //    {
        //        CardNumber = cardNumber,
        //        UsedPoint = usedPoint,
        //        ExchangeMoney = exchangeMoney,
        //        Remark = $"{usedPoint}积分抵{exchangeMoney.ToString("0.##")}元"
        //    };
        //}

        ///// <summary>
        ///// 获取会员可购买的会员等级
        ///// </summary>
        ///// <param name="memberID">会员ID</param>
        ///// <returns></returns>
        //public IEnumerable<MemberGradeIntroDto> GetMemberCanPurchaseGradeList(Guid? memberID)
        //{
        //    Guid? memberGradeID = null;
        //    if (memberID.HasValue)
        //    {
        //        memberGradeID = Repository.GetQueryable(false)
        //            .Where(t => t.ID == memberID.Value)
        //            .Select(t => t.MemberGradeID).FirstOrDefault();
        //    }
        //    return MemberGradeService.GetCanPurchaseGradeList(memberGradeID);
        //}

        #endregion public
    }
}
