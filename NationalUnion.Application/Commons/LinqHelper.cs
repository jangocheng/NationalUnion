using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.Commons
{
    /// <summary>
    /// Linq动态排序
    /// </summary>
    public class LinqHelper
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static IQueryable<T> DataSorting<T>(IQueryable<T> source, string sortExpression, string sortDirection)
        {
            string sortingDir = string.Empty;
            if (sortDirection.ToUpper().Trim() == "ASC")
            {
                sortingDir = "OrderBy";
            }
            else if (sortDirection.ToUpper().Trim() == "DESC")
            {
                sortingDir = "OrderByDescending";
            }
            // 从传入的类型中找出需要进行排序的字段
            ParameterExpression param = Expression.Parameter(typeof (T), sortExpression);
            // 取出要排序字段的相关属性
            PropertyInfo proinfo = typeof (T).GetProperty(sortExpression);
            var types = new Type[2];
            // 要进行排序的数据集类型
            types[0] = typeof (T);
            // 要进行排序的字段类型[传入参数值类型]
            types[1] = proinfo.PropertyType;
            // 生成排序表达式
            Expression expr = Expression.Call(typeof (Queryable), sortingDir, types, source.Expression,
                Expression.Lambda(Expression.Property(param, sortExpression), param));
            IQueryable<T> query = source.AsQueryable().Provider.CreateQuery<T>(expr);

            return query;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<T> DataPaging<T>(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber <= 1)
            {
                return source.Take(pageSize);
            }

            return source.Skip((pageNumber - 1)*pageSize).Take(pageSize);
        }

        /// <summary>
        /// 排序并分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<T> DataSortingAndPaging<T>(IQueryable<T> source, string sortExpression, string sortDirection, int pageNumber, int pageSize)
        {
            IQueryable<T> query = DataSorting<T>(source, sortExpression, sortDirection);

            return DataPaging<T>(query, pageNumber, pageSize);
        }
    }
}
