using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 预约
    /// </summary>
    [RoutePrefix("api/Appointment")]
    public class AppointmentController : BaseApiController
    {
        public AppointmentController(IAppointmentService appointmentService)
        {
            AppointmentService = appointmentService;
        }

        IAppointmentService AppointmentService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<Appointment> Add(Appointment entity)
        {
            return SafeExecute(() =>
            {
                return AppointmentService.Add(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<Appointment> Search([FromUri]AppointmentFilter filter)
        {
            return SafeGetPagedData<Appointment>((result) =>
            {
                var totalCount = 0;
                var data = AppointmentService.Search(filter, ref totalCount);
                result.Data = data;
            });
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("CancelAppointment")]
        public JsonActionResult<bool> CancelAppointment(Guid id)
        {
            return SafeExecute(() =>
            {
                return AppointmentService.CancelAppointment(id);
            });
        }
    }
}
