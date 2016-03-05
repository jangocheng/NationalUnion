using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.Commons
{
    /// <summary>
    /// 渠道等级
    /// </summary>
    public enum ChannelRank
    {
        [Description("富农")]
        CaoGen = 1,

        [Description("员外")]
        DiaoSi = 2,

        [Description("土豪")]
        TuHao = 3
    }
}
