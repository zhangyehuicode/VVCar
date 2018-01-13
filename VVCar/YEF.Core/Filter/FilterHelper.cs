using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace YEF.Core.Filter
{
    /// <summary>
    /// 查询表达式辅助操作类
    /// </summary>
    public static class FilterHelper
    {
        #region fields

        private static readonly Dictionary<FilterOperate, Func<Expression, Expression, Expression>> ExpressionDict;
        #endregion

        #region ctor.

        static FilterHelper()
        {
            ExpressionDict = new Dictionary<FilterOperate, Func<Expression, Expression, Expression>>
            {
                {FilterOperate.Equal, Expression.Equal},
                {FilterOperate.NotEqual, Expression.NotEqual},
                {FilterOperate.LessThan, Expression.LessThan},
                {FilterOperate.LessThanOrEqual, Expression.LessThanOrEqual},
                {FilterOperate.GreaterThan, Expression.GreaterThan},
                {FilterOperate.GreaterThanOrEqual, Expression.GreaterThanOrEqual},
                {FilterOperate.StartsWith, (left, right) =>
                    {
                        if (left.Type != typeof(string))
                        {
                            throw new NotSupportedException("“StartsWith”比较方式只支持字符串类型的数据");
                        }
                        return Expression.Call(left, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), right);
                    }
                },
                {FilterOperate.EndsWith, (left, right) =>
                    {
                        if (left.Type != typeof(string))
                        {
                            throw new NotSupportedException("“EndsWith”比较方式只支持字符串类型的数据");
                        }
                        return Expression.Call(left, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), right);
                    }
                },
                {FilterOperate.Contains, (left, right) =>
                    {
                        if (left.Type != typeof(string))
                        {
                            throw new NotSupportedException("“Contains”比较方式只支持字符串类型的数据");
                        }
                        return Expression.Call(left, typeof(string).GetMethod("Contains", new[] { typeof(string) }), right);
                    }
                },
            };
        }
        #endregion

        #region methods
        /// <summary>
        /// 获取指定查询条件组的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="group">查询条件组，如果为null，则直接返回 true 表达式</param>
        public static Expression<Func<T, bool>> GetExpression<T>(FilterGroup group)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "m");
            Expression body = GetExpressionBody(param, group);
            Expression<Func<T, bool>> expression = Expression.Lambda<Func<T, bool>>(body, param);
            return expression;
        }

        /// <summary>
        /// 获取指定查询条件的查询表达式
        /// </summary>
        /// <typeparam name="T">表达式实体类型</typeparam>
        /// <param name="rule">查询条件，如果为null，则直接返回 true 表达式</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GetExpression<T>(FilterRule rule = null)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "m");
            Expression body = GetExpressionBody(param, rule);
            Expression<Func<T, bool>> expression = Expression.Lambda<Func<T, bool>>(body, param);
            return expression;
        }

        private static Expression GetExpressionBody(ParameterExpression param, FilterGroup group)
        {
            Contract.Requires(param != null);

            //如果无条件或条件为空，直接返回 true表达式
            if (group == null || (group.Rules.Count == 0 && group.Groups.Count == 0))
            {
                return Expression.Constant(true);
            }
            List<Expression> bodys = new List<Expression>();
            bodys.AddRange(group.Rules.Select(rule => GetExpressionBody(param, rule)));
            bodys.AddRange(group.Groups.Select(subGroup => GetExpressionBody(param, subGroup)));

            return group.IsAnd ? bodys.Aggregate(Expression.AndAlso) : bodys.Aggregate(Expression.OrElse);
        }

        private static Expression GetExpressionBody(ParameterExpression param, FilterRule rule)
        {
            if (rule == null || rule.Value == null || string.IsNullOrEmpty(rule.Value.ToString()))
            {
                return Expression.Constant(true);
            }
            LambdaExpression expression = GetPropertyLambdaExpression(param, rule);
            Expression constant = ChangeTypeToExpression(rule, expression.Body.Type);
            return ExpressionDict[rule.Operate](expression.Body, constant);
        }

        private static LambdaExpression GetPropertyLambdaExpression(ParameterExpression param, FilterRule rule)
        {
            string[] propertyNames = rule.Field.Split('.');
            Expression propertyAccess = param;
            Type type = param.Type;
            foreach (string propertyName in propertyNames)
            {
                PropertyInfo property = type.GetProperty(propertyName);
                Contract.Assume(property != null);
                type = property.PropertyType;
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            }
            return Expression.Lambda(propertyAccess, param);
        }

        private static Expression ChangeTypeToExpression(FilterRule rule, Type conversionType)
        {
            Type elementType = conversionType.GetUnNullableType();
            object value = Convert.ChangeType(rule.Value, elementType);
            return Expression.Constant(value, conversionType);
        }

        #endregion
    }
}
