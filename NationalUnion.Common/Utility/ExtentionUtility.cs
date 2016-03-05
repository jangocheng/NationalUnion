using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NationalUnion.Common.Utility
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtentionUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="argObj"></param>
        /// <returns></returns>
        public static string AsString(this object argObj)
        {
            if (argObj == null)
            {
                return string.Empty;
            }
            return argObj.ToString();
        }

        /// <summary>
        /// 转换成Int，否则为0
        /// </summary>
        /// <param name="argObj"></param>
        /// <returns></returns>
        public static int AsInt(this string argObj)
        {
            if (NumberUtility.IsIntNumber(argObj))
            {
                return int.Parse(argObj.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 转化成DateTime，否则为当前时间
        /// </summary>
        /// <param name="argValue"></param>
        /// <returns></returns>
        public static DateTime AsDateTime(this string argValue)
        {
            DateTime vResult;
            DateTime.TryParse(argValue, out vResult);
            return vResult;
        }

        /// <summary>
        /// 转化成DateTime，否则为当前时间
        /// </summary>
        /// <param name="argValue"></param>
        /// <returns></returns>
        public static DateTime AsDateTime(this object argValue)
        {
            DateTime vResult;
            DateTime.TryParse(argValue.AsString(), out vResult);
            return vResult;
        }

        /// <summary>
        /// 转换成Long，否则为0
        /// </summary>
        /// <param name="argValue"></param>
        /// <returns></returns>
        public static long AsLong(this string argValue)
        {
            long vResult = 0;
            if (NumberUtility.IsIntNumber(argValue))
            {
                vResult = long.Parse(argValue);
            }
            return vResult;
        }

        /// <summary>
        /// 转换成decimal，否则为0
        /// </summary>
        /// <param name="argValue"></param>
        /// <returns></returns>
        public static decimal AsDecimal(this string argValue)
        {
            decimal vResult = 0;
            decimal.TryParse(argValue, out vResult);
            return vResult;
        }
    }
}
