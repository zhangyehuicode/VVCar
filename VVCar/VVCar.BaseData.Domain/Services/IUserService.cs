using System;
using System.Collections.Generic;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using YEF.Core.Dtos;
using VVCar.BaseData.Domain.Dtos;
using YEF.Core;

namespace VVCar.BaseData.Domain.Services
{
    public partial interface IUserService : IDomainService<IRepository<User>, User, Guid>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号: code</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UserInfoDto Login(string account, string password, string companycode);

        /// <summary>
        /// Sso登录
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UserInfoDto SsoLogin(string userCode, string password, string companycode);

        /// <summary>
        /// App登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        UserInfoDto AppLogin(AppLoginParams param);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="password">新密码</param>
        /// <returns></returns>
        bool ChangePassword(Guid userID, string oldPassword, string password);

        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        PagedResultDto<User> QueryUser(UserFilter filter);

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteUsers(Guid[] ids);

        /// <summary>
        /// 批量重置密码
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool ResetPwds(Guid[] ids);

        /// <summary>
        /// 获取用户功能权限代码列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        IEnumerable<AssignPermissionDto> GetUserFuncPermissionList(Guid userID);

        /// <summary>
        /// RechargeHistoryMemberCmb获取员工姓名
        /// </summary>
        /// <param name="departmentID">部门ID</param>
        /// <returns></returns>
        IEnumerable<IDCodeNameDto> GetUserNameCollect(Guid? departmentID);

        /// <summary>
        /// 是否是管理员
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        bool IsAdmin(Guid userID);

        /// <summary>
        /// 新增人员信息，商户接口
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        User AddMchUser(User newUser);

        /// <summary>
        /// 更新人员信息，商户接口
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        bool UpdateMchUser(MchUserDto userDto);

        /// <summary>
        /// 设置超级管理员
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        bool SetSuperAdmin(MchUserDto userDto);

        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        UserInfoDto WeChatLogin(WeChatLoginParams param);

        /// <summary>
        /// 获取用户信息通过OpenId
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        UserInfoDto GetUserByOpenID(WeChatLoginParams param);

        /// <summary>
        /// 获取商户店员信息
        /// </summary>
        /// <returns></returns>
        List<User> GetManagerUser();

        /// <summary>
        /// 获取销售人员名单
        /// </summary>
        /// <returns></returns>
        List<User> GetSaleUser(UserFilter filter, out int totalCount);

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool BindingMobilePhone(BindingMobilePhoneParam param);

        /// <summary>   
        /// 获取员工信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<UserInfoDto> GetUsers(UserFilter filter, ref int totalCount);

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool ChangeAvailable(Guid userId);
    }
}
