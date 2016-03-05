using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.Commons
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum Status
    {
        [Description("待审核")]
        Pending = 0,

        [Description("已激活")]
        Enable = 1,

        [Description("已停用")]
        Disable = 2
    }
}
