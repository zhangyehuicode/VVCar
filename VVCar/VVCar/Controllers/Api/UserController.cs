using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 用户
    /// </summary>
    [RoutePrefix("api/User")]
    public class UserController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            this.UserService = userService;
        }

        #endregion

        #region properties
        IUserService UserService { get; set; }
        #endregion

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="newUser">用户</param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<User> PostUser(User newUser)
        {
            return SafeExecute(() =>
            {
                return this.UserService.Add(newUser);
            });
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> DeleteUser(Guid id)
        {
            return SafeExecute(() =>
            {
                return this.UserService.Delete(id);
            });
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> PutUser(User user)
        {
            return SafeExecute(() =>
            {
                return this.UserService.Update(user);
            });
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonActionResult<User> GetUser(Guid id)
        {
            return SafeExecute(() =>
            {
                return this.UserService.Get(id);
            });
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<User> Query([FromUri]UserFilter filter)
        {
            return SafeGetPagedData<User>((result) =>
            {
                if (!ModelState.IsValid)//表示没有过滤参数成功匹配，判定为错误请求。
                {
                    throw new DomainException("查询参数错误。");
                }
                var pagedData = this.UserService.QueryUser(filter);
                result.Data = pagedData.Items;
                result.TotalCount = pagedData.TotalCount;
            });
        }

        /// <summary>
        /// 批量重置密码
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("resetPwds")]
        public JsonActionResult<bool> ResetPwds(BatchOperationDto parameter)
        {
            return SafeExecute(() => UserService.ResetPwds(parameter.IdList.ToArray()));
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteUsers")]
        public JsonActionResult<bool> DeleteUsers(BatchOperationDto parameter)
        {
            return SafeExecute(() => UserService.DeleteUsers(parameter.IdList.ToArray()));
        }

        /// <summary>
        /// RechargeHistoryMemberCmb获取员工姓名
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        [HttpGet, Route("getUserNameCollect")]
        public JsonActionResult<IEnumerable<IDCodeNameDto>> GetUserNameCollect(Guid? departmentID)
        {
            return SafeExecute(() =>
            {
                return this.UserService.GetUserNameCollect(departmentID);
            });
        }

        /// <summary>
        /// 获取商户店员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetManagerUser")]
        public PagedActionResult<User> GetManagerUser()
        {
            return SafeGetPagedData<User>((result) =>
            {
                result.Data = UserService.GetManagerUser();
            });
        }

        /// <summary>
        /// 获取销售人员名单
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSaleUser")]
        public PagedActionResult<User> GetSaleUser([FromUri]UserFilter filter)
        {
            return SafeGetPagedData<User>((result) =>
            {
                var totalCount = 0;
                result.Data = UserService.GetSaleUser(filter, out totalCount);
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 获取用户功能权限代码列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("PermissionList")]
        public JsonActionResult<IEnumerable<string>> GetUserPermissionList()
        {
            return SafeExecute(() =>
            {
                var userFuncPermissions = UserService.GetUserFuncPermissionList(AppContext.CurrentSession.UserID);
                IEnumerable<string> actionPermissions = new List<string>();
                if (userFuncPermissions != null && userFuncPermissions.Count() > 0)
                {
                    actionPermissions = userFuncPermissions.Where(p => p.PermissionType == BaseData.Domain.Enums.EPermissionType.PortalAction)
                        .Select(t => t.PermissionCode).ToList();
                }
                return actionPermissions;
            });
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        [HttpPost, Route("ChangePassword")]
        public JsonActionResult<bool> ChangePassword(string oldPassword, string newPassword)
        {
            return SafeExecute(() =>
            {
                return UserService.ChangePassword(AppContext.CurrentSession.UserID, oldPassword, newPassword);
            });
        }

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        [HttpGet, Route("WeChatLogin"), AllowAnonymous]
        public JsonActionResult<UserInfoDto> WeChatLogin([FromUri]WeChatLoginParams param)
        {
            return SafeExecute(() =>
            {
                if (param == null)
                    throw new DomainException("参数为空");
                return UserService.WeChatLogin(param);
            });
        }

        /// <summary>
        /// 获取用户信息通过OpenId
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet, Route("GetUserByOpenID"), AllowAnonymous]
        public JsonActionResult<UserInfoDto> GetUserByOpenID([FromUri]WeChatLoginParams param)
        {
            return SafeExecute(() =>
            {
                return UserService.GetUserByOpenID(param);
            });
        }

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet, Route("BindingMobilePhone"), AllowAnonymous]
        public JsonActionResult<bool> BindingMobilePhone([FromUri]BindingMobilePhoneParam param)
        {
            return SafeExecute(() =>
            {
                return UserService.BindingMobilePhone(param);
            });
        }

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("GetUsers"), AllowAnonymous]
        public PagedActionResult<UserInfoDto> GetUsers([FromUri]UserFilter filter)
        {
            return SafeGetPagedData<UserInfoDto>((result) =>
            {
                var totalCount = 0;
                var data = UserService.GetUsers(filter, ref totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet, Route("ChangeAvailable"), AllowAnonymous]
        public JsonActionResult<bool> ChangeAvailable(Guid userId)
        {
            return SafeExecute(() =>
            {
                return UserService.ChangeAvailable(userId);
            });
        }
    }
}
