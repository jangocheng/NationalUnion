using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.ViewModels
{
    public class OrderProductOccurInfo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 商品唯一标识
        /// </summary>
        public string ProductSkuId { get; set; }

        /// <summary>
        /// 三级类别编号
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 三级类别
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 二级类别
        /// </summary>
        public string CategoryParentId { get; set; }

        /// <summary>
        /// 二级类别
        /// </summary>
        public string CategoryParentName { get; set; }

        /// <summary>
        /// 一级类别
        /// </summary>
        public string CategoryFinalId { get; set; }

        /// <summary>
        /// 一级类别
        /// </summary>
        public string CategoryFinalName { get; set; }

        /// <summary>
        /// 单品价格
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductNumber { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品总额
        /// </summary>
        public decimal ProductPriceAmount { get; set; }

        /// <summary>
        /// 下单类型[团购TG/抢购QG/普通PT]
        /// </summary>
        public string ProductSite { get; set; }

        /// <summary>
        /// 是否使用优惠券
        /// </summary>
        public string IsCoupon { get; set; }

        /// <summary>
        /// 分享用户编号
        /// </summary>
        public long SharedUserId { get; set; }

        /// <summary>
        /// 分享用户
        /// </summary>
        public string SharedUserName { get; set; }

        /// <summary>
        /// 分享等级
        /// </summary>
        public int SharedLevel { get; set; }

        /// <summary>
        /// 分享等级
        /// </summary>
        public string ShareLevel { get; set; }

        /// <summary>
        /// 预计佣金
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// 佣金比例
        /// </summary>
        public decimal CommissionRatio { get; set; }

        /// <summary>
        /// 分享平台ID
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// 分享平台
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// 分享时分享途径[1表示PC端分享，2表示WAP网页分享]
        /// </summary>
        public int SharedClientId { get; set; }

        /// <summary>
        /// 分享途径
        /// </summary>
        public string SharedClient { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderOccurTime { get; set; }
    }
}
