using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Domain.NotMapping
{
    public class GetPermissionCode
    {
        /// <summary>
        /// 操作码
        /// </summary>
        public string KeyCode { get; set; }

        /// <summary>
        /// 是否验证
        /// </summary>
        public int IsValid { get; set; }
    }
}
