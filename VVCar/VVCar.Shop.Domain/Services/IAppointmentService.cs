﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    public interface IAppointmentService : IDomainService<IRepository<Appointment>, Appointment, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<Appointment> Search(AppointmentFilter filter, ref int totalCount);

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CancelAppointment(Guid id);
    }
}
