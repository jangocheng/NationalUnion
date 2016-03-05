using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.Commons
{
    /// <summary>
    /// 渠道角色
    /// </summary>
    public enum ManagerRole
    {
        [Description("渠道主管")]
        ChannelManager = 1,

        [Description("渠道经理")]
        CityManager = 2,

        [Description("渠道总监")]
        ChannelDirector= 3,

        [Description("运营人员")]
        Admin = 4,

        [Description("管理员")]
        SuperAdmin = 5
    }
}
