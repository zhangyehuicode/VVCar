using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YEF.Core.Dtos;
using YEF.Core.Filter;

namespace VVCar.Controllers
{
    /// <summary>
    /// ApiController 基类
    /// </summary>
    [ApiAuthorize(NeedLogin = true)]
    [ApiAuthorize(NeedCompanyCode = true)]
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// 为 基础操作 提供统一的返回格式，包含try / catch操作。
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="execAction"></param>
        /// <returns></returns>
        protected JsonActionResult<TResult> SafeExecute<TResult>(Func<TResult> execAction)
        {
            var result = new JsonActionResult<TResult>();
            try
            {
                result.Data = execAction();
            }
            catch (Exception ex)
            {
                YEF.Core.Logging.LoggerManager.GetLogger().Error(ex.ToString());
                result.IsSuccessful = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }

        /// <summary>
        /// 为取 分页数据 提供统一的返回格式，包含try / catch操作。
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="execAction"></param>
        /// <returns></returns>
        protected PagedActionResult<TResult> SafeGetPagedData<TResult>(Action<PagedActionResult<TResult>> execAction)
        {
            var result = new PagedActionResult<TResult>();
            try
            {
                execAction(result);
            }
            catch (Exception ex)
            {
                YEF.Core.Logging.LoggerManager.GetLogger().Error(ex.ToString());
                result.IsSuccessful = false;
                result.TotalCount = 0;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 为取 树结构数据 提供统一的返回格式，包含try / catch操作。
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="execAction"></param>
        /// <returns></returns>
        protected TreeActionResult<TResult> SafeGetTreeData<TResult>(Func<IEnumerable<TResult>> execAction)
        {
            var result = new TreeActionResult<TResult>();
            try
            {
                result.Children = execAction();
            }
            catch (Exception ex)
            {
                YEF.Core.Logging.LoggerManager.GetLogger().Error(ex.ToString());
                result.IsSuccessful = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据Request中的QueryString获得Linq表达式
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <returns></returns>
        protected Expression<Func<TEntity, bool>> GetExpressionFromQueryString<TEntity>()
        {
            Expression<Func<TEntity, bool>> expr = null;
            var queryPairs = Request.GetQueryNameValuePairs().Where(q => q.Key.Equals("$format") == false);
            if (queryPairs.Count() > 0)
            {
                var filterGroup = new FilterGroup();
                foreach (var pairItem in queryPairs)
                {
                    filterGroup.Rules.Add(new FilterRule(pairItem.Key, pairItem.Value));
                }
                expr = filterGroup.BuildExpression<TEntity>();
            }
            return expr;
        }
    }
}
