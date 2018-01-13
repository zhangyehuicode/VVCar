using System;
using System.Linq.Expressions;

namespace YEF.Core
{
    /// <summary>
    /// 后台任务 Provider
    /// </summary>
    public interface IBackgroundJobProvider : IDependency
    {
        /// <summary>
        /// 创建后台任务
        /// </summary>
        /// <param name="methodCall">新的后台任务</param>
        /// <returns></returns>
        string Enqueue(Expression<Action> methodCall);

        /// <summary>
        /// 创建后台任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodCall">The method call.</param>
        /// <returns></returns>
        string Enqueue<T>(Expression<Action<T>> methodCall);

        /// <summary>
        /// 等待上一个任务完成后创建后台任务
        /// </summary>
        /// <param name="parentJobId">上一个任务Id</param>
        /// <param name="methodCall">后台任务</param>
        /// <returns></returns>
        string ContinueWith(string parentJobId, Expression<Action> methodCall);

        /// <summary>
        /// 等待上一个任务完成后创建后台任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="methodCall">The method call.</param>
        /// <returns></returns>
        string ContinueWith<T>(string parentId, Expression<Action<T>> methodCall);

        /// <summary>
        /// 创建一个延迟执行的后台任务
        /// </summary>
        /// <param name="methodCall">后台任务</param>
        /// <param name="delay">延迟时间</param>
        /// <returns></returns>
        string Schedule(Expression<Action> methodCall, TimeSpan delay);

        /// <summary>
        /// 创建一个延迟执行的后台任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodCall">The method call.</param>
        /// <param name="delay">The delay.</param>
        /// <returns></returns>
        string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay);

        /// <summary>
        /// 新增或更新一个可循环的任务
        /// </summary>
        /// <param name="recurringJobId">任务Id</param>
        /// <param name="methodCall">后台任务</param>
        /// <param name="cronExpression">cron表达式</param>
        void Recurring(string recurringJobId, Expression<Action> methodCall, string cronExpression);

        /// <summary>
        /// 新增或更新一个可循环的任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recurringJobId">The recurring job identifier.</param>
        /// <param name="methodCall">The method call.</param>
        /// <param name="cronExpression">The cron expression.</param>
        void Recurring<T>(string recurringJobId, Expression<Action<T>> methodCall, string cronExpression);
    }
}
