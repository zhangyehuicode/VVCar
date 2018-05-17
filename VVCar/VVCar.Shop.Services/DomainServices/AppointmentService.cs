using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
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

        public override Appointment Add(Appointment entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        public IEnumerable<Appointment> Search(AppointmentFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name == filter.Name);
            if (!string.IsNullOrEmpty(filter.MobilePhoneNo))
                queryable = queryable.Where(t => t.MobilePhoneNo == filter.MobilePhoneNo);
            if (!string.IsNullOrEmpty(filter.OpenID))
                queryable = queryable.Where(t => t.OpenID == filter.OpenID);
            if (!string.IsNullOrEmpty(filter.ServiceName))
                queryable = queryable.Where(t => t.ServiceName == filter.ServiceName);
            if (filter.StartDate.HasValue)
                queryable = queryable.Where(t => t.Time >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                queryable = queryable.Where(t => t.Time <= filter.EndDate.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
