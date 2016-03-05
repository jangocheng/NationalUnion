using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.Commons
{
    /// <summary>
    /// 分享宝渠道
    /// </summary>
    public enum ChannelShare
    {
        [Description("主管")]
        ZhuGuan = 1,

        [Description("经理")]
        JingLi = 2,

        [Description("总监")]
        ZongJian = 3
    }
}
