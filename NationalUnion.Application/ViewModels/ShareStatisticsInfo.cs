using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.ViewModels
{
    public class ShareStatisticsInfo
    {
        [Display(Name = "用户名")]
        public long UserId { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "统计日期")]
        public DateTime SummaryDate { get; set; }

        [Display(Name = "渠道编号")]
        public long ChannelId { get; set; }

        [Display(Name = "渠道")]
        public string ChannelName { get; set; }

        [Display(Name = "分享平台编号")]
        public int PlatformId { get; set; }

        [Display(Name = "分享平台")]
        public string PlatformName { get; set; }

        [Display(Name = "分享途径编号")]
        public int SharedClientId { get; set; }

        [Display(Name = "分享途径")]
        public string SharedClientName { get; set; }

        [Display(Name = "分享量")]
        public int SharedQunatity { get; set; }

        [Display(Name = "PV")]
        public int PV { get; set; }

        [Display(Name = "UV")]
        public int UV { get; set; }

        [Display(Name = "订单数量")]
        public int OrderQuantity { get; set; }

        [Display(Name = "商品数量")]
        public int ProductQuantity { get; set; }

        [Display(Name = "订单金额")]
        public decimal OrderAmount { get; set; }

        [Display(Name = "预计佣金")]
        public decimal EstimateCommission { get; set; }
    }
}
