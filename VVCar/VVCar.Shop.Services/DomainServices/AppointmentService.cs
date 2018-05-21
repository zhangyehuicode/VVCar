using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Services;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    public class AppointmentService : DomainServiceBase<IRepository<Appointment>, Appointment, Guid>, IAppointmentService
    {
        public AppointmentService()
        {
        }

        #region properties

        IUserService UserService { get => ServiceLocator.Instance.GetService<IUserService>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        #endregion

        public override Appointment Add(Appointment entity)
        {
            if (entity == null)
                return null;

            var count = Repository.GetQueryable(false).Where(t => t.OpenID == entity.OpenID).ToList().Count(t => t.CreatedDate.Date == DateTime.Now.Date);
            if (count >= 3)
                throw new DomainException("每天最多可预约三次");

            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            if (!string.IsNullOrEmpty(entity.AppointmentDate) && !string.IsNullOrEmpty(entity.AppointmentTime))
                entity.Date = Convert.ToDateTime($"{entity.AppointmentDate} {entity.AppointmentTime}");
            else
                throw new DomainException("预约时间不能为空");
            var result = base.Add(entity);
            try
            {
                SenWeChatNotifyToCustomer(result);
                SenWeChatNotifyToManager(result);
            }
            catch (Exception e)
            {
                AppContext.Logger.Error($"预约发送微信通知出现异常,{e.Message}");
            }
            return result;
        }

        private void SenWeChatNotifyToManager(Appointment appointment)
        {
            if (appointment == null)
                return;
            var useropenids = new List<string>();
            var user = UserService.GetManagerUser();
            if (user != null && user.Count > 0)
                user.ForEach(t =>
                {
                    if (!string.IsNullOrEmpty(t.OpenID) && !useropenids.Contains(t.OpenID))
                    {
                        useropenids.Add(t.OpenID);
                        var message = new WeChatTemplateMessageDto
                        {
                            touser = t.OpenID,
                            template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_AppointmentRemind),
                            url = $"{AppContext.Settings.SiteDomain}/Mobile/Merchant/MyAppointment?mch={AppContext.CurrentSession.CompanyCode}",
                            data = new System.Dynamic.ExpandoObject(),
                        };
                        message.data.first = new WeChatTemplateMessageDto.MessageData("您有一个新的预约");
                        message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(appointment.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                        message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(appointment.ServiceName);
                        message.data.remark = new WeChatTemplateMessageDto.MessageData($"预约信息：{appointment.Name}({appointment.MobilePhoneNo})请知悉！");
                        WeChatService.SendWeChatNotifyAsync(message);
                    }
                });
        }

        private void SenWeChatNotifyToCustomer(Appointment appointment)
        {
            if (appointment == null)
                return;
            if (!string.IsNullOrEmpty(appointment.OpenID))
            {
                var message = new WeChatTemplateMessageDto
                {
                    touser = appointment.OpenID,
                    template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_AppointmentSuccess),
                    url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MyAppointment?mch={AppContext.CurrentSession.CompanyCode}",
                    data = new System.Dynamic.ExpandoObject(),
                };
                message.data.first = new WeChatTemplateMessageDto.MessageData("您已成功预约");
                message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(appointment.ServiceName);
                message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(appointment.Date.ToString("yyyy-MM-dd HH:mm:ss"));
                message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(appointment.Name);
                message.data.keyword4 = new WeChatTemplateMessageDto.MessageData(appointment.MobilePhoneNo);
                message.data.keyword5 = new WeChatTemplateMessageDto.MessageData("");
                WeChatService.SendWeChatNotifyAsync(message);
            }
        }

        public bool CancelAppointment(Guid id)
        {
            var appointment = Repository.GetByKey(id);
            if (appointment == null)
                return false;
            appointment.Status = EAppointmentStatus.Cancel;
            return Repository.Update(appointment) > 0;
        }

        public IEnumerable<Appointment> Search(AppointmentFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name == filter.Name);
            if (!string.IsNullOrEmpty(filter.MobilePhoneNo))
                queryable = queryable.Where(t => t.MobilePhoneNo == filter.MobilePhoneNo);
            if (!string.IsNullOrEmpty(filter.OpenID))
                queryable = queryable.Where(t => t.OpenID == filter.OpenID);
            if (!string.IsNullOrEmpty(filter.ServiceName))
                queryable = queryable.Where(t => t.ServiceName == filter.ServiceName);
            if (filter.StartDate.HasValue)
                queryable = queryable.Where(t => t.Date >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                queryable = queryable.Where(t => t.Date <= filter.EndDate.Value);
            if (filter.Status.HasValue)
                queryable = queryable.Where(t => t.Status == filter.Status.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).ToArray();
        }
    }
}
