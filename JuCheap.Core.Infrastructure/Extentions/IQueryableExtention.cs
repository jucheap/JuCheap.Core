using System;
using System.Linq;
using System.Linq.Expressions;

namespace JuCheap.Core.Infrastructure.Extentions
{
    /// <summary>
    /// Queryable扩展
    /// </summary>
    public static class QueryableExtention
    {
        /// <summary>
        /// WhereIf[在condition为true的情况下应用Where表达式]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> expression)
        {
            return condition ? source.Where(expression) : source;
        }
    }
}
