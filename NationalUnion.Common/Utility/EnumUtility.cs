using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Common.Utility
{
    public static class EnumUtility
    {
        /// <summary>
        /// 系统中缺省的ChannelID，
        /// </summary>
        public readonly static int Systemchannelid = 1;

        /// <summary>
        /// 系统中缺省的ManagerID，
        /// </summary>
        public readonly static long SystemManagerId = 0;

        /// <summary>
        /// 分享平台
        /// </summary>
        public enum SharedPlatform
        {
            [Description("百度分享")]
            BaiduShare = -1,

            [Description("未标识")]
            NoDistinction = 0,

            [Description("新浪微博")]
            WeiBo = 1,

            [Description("腾讯微博")]
            TecentWeiBo = 2,

            [Description("人人网")]
            RenRen = 3,

            [Description("开心网")]
            KaiXin = 4,

            [Description("豆瓣")]
            DouBan = 5,

            [Description("易信")]
            YiXin = 6
        }

        public enum OrderType
        {
            [Description("普通")]
            PT = 1,

            [Description("团购")]
            TG = 2,

            [Description("抢购")]
            QG = 3
        }

        public enum ShareLevel
        {
            [Description("一级")]
            First = 1,

            [Description("二级")]
            Second = 2,

            [Description("三级")]
            Third = 3
        }

        /// <summary>
        /// 页面类型
        /// </summary>
        public enum PageType
        {
            /// <summary>
            /// 单品
            /// </summary>
            SingleProduct = 1,
            /// <summary>
            /// 活动
            /// </summary>
            Activity = 2,
            /// <summary>
            /// 类目、列表页
            /// </summary>
            ProductList = 3
        }

        /// <summary>
        /// 分享途径
        /// </summary>
        public enum SharedClientType
        {
            [Description("未标识")]
            NoDistinction = 0,

            [Description("PC")]
            PC = 1,

            [Description("WAP")]
            WAP = 2
        }

        /// <summary>
        /// 访客客户端
        /// </summary>
        public enum ClientType
        {
            PC = 1,
            WAP = 2,
            PAD = 3
        }

        /// <summary>
        /// 用户类别
        /// </summary>
        public enum UserType
        {
            /// <summary>
            /// 联盟用户
            /// </summary>
            NationalUnion = 1,

            /// <summary>
            /// 国美在线内部用户
            /// </summary>
            Gome = 2
        }

        /// <summary>
        /// 赞助方类别
        /// </summary>
        public enum SponsorType
        {
            /// <summary>
            /// 国美
            /// </summary>
            Gome = 1,

            /// <summary>
            /// 国美第三方商家
            /// </summary>
            Cooperator = 2,

            /// <summary>
            /// 会员
            /// </summary>
            User = 3
        }
    }
}
