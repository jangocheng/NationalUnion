using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.Commons
{
    public class GridPager
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get { return (int)Math.Ceiling((float)TotalRows / (float)Rows); }
        }
    }
}
