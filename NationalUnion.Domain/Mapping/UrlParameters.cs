using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Domain.Mapping
{
    [ComplexType]
    public class UrlParameters
    {
        /// <summary>
        /// 分享者[分享此信息的用户的MemberNO]
        /// </summary>
        [Column("SharedUserId")]
        public long SharedUserId { get; set; }

        /// <summary>
        /// 分享所属级别[是第几级分享的倒序，即最后一级分享者的级别为1……]
        /// </summary>
        [Column("SharedLevel")]
        public int SharedLevel { get; set; }

        /// <summary>
        /// 分享者的渠道负责人ID[最后一级分享者的MemberNO]
        /// </summary>
        [Column("SharedManagerId")]
        public long SharedManagerId { get; set; }

        /// <summary>
        /// 分享者的渠道ID[最后一级分享者的渠道ID]
        /// </summary>
        [Column("ChannelId")]
        public long ChannelId { get; set; }

        /// <summary>
        /// 分享平台ID
        /// </summary>
        [Column("PlatformId")]
        public int PlatformId { get; set; }

        /// <summary>
        /// 可能是商品的ID，主题活动的ID，列表页类目的ID
        /// </summary>
        [StringLength(50), Column("ItemId")]
        public string ItemId { get; set; }

        /// <summary>
        /// 类型(商品，活动，类目)
        /// </summary>
        [Column("ItemType")]
        public int ItemType { get; set; }

        /// <summary>
        /// 分享时分享途径[1表示PC端分享，2表示WAP网页分享]
        /// </summary>
        [Column("SharedClientId")]
        public int SharedClientId { get; set; }

        /// <summary>
        /// 分享日期
        /// </summary>
        [Column("SharedDate")]
        public DateTime SharedDate { get; set; }
    }
}
