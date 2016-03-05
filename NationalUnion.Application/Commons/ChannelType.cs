using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.Commons
{
    /// <summary>
    /// 渠道类型
    /// </summary>
    public enum ChannelType
    {
        [Description("高校")]
        University = 1,

        //[Description("商圈")]
        //TradeArea = 2,

        //[Description("社区")]
        //Community = 3,

        [Description("分享宝")]
        ShareTool = 4
    }
}
