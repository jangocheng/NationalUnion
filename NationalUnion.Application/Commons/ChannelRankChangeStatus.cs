using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.Commons
{
    /// <summary>
    /// 渠道等级状态
    /// </summary>
    public enum ChannelRankChangeStatus
    {
        [Description("离职")]
        Leave = 1,

        [Description("降职")]
        Degrade = 2,

        [Description("平调")]
        Original = 3,

        [Description("升职")]
        Promotion = 4
    }
}
