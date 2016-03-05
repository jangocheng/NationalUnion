using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace NationalUnion.Common.Utility
{
    public static class NumberUtility
    {
        /// <summary>
        /// 判断是否是整数(可以是负数)
        /// </summary>
        /// <param name="argStr"></param>
        /// <returns></returns>
        public static bool IsIntNumber(string argStr)
        {
            var reg = new Regex(@"^-?\d+$");
            if (reg.IsMatch(argStr))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 1900-01-01
        /// </summary>
        public static DateTime DefaultDate
        {
            get
            {
                return new DateTime(1900, 1, 1);
            }
        }
    }
}
