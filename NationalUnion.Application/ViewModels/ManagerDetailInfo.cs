using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.ViewModels
{
    public class ManagerDetailInfo
    {
        [Display(Name = "历史明细编号")]
        public int ManagerDetailId { get; set; }

        [Display(Name = "会员编号")]
        public Int64 ManagerId { get; set; }

        [Display(Name = "会员姓名")]
        public string ManagerName { get; set; }

        [Display(Name = "当前渠道")]
        public Int64 CurrentChannelId { get; set; }

        [Display(Name = "当前渠道")]
        public string CurrentChannel { get; set; }

        [Display(Name = "历史渠道")]
        public Int64 OldChannelId { get; set; }

        [Display(Name = "历史渠道")]
        public string OldChannel { get; set; }

        [Display(Name = "当前渠道等级")]
        public int CurrentRank { get; set; }

        [Display(Name = "当前渠道等级")]
        public string CurrentRankStr { get; set; }

        [Display(Name = "当前渠道等级")]
        public string CurrentRankDesc { get; set; }

        [Display(Name = "历史渠道等级")]
        public int OldRank { get; set; }

        [Display(Name = "历史渠道等级")]
        public string OldRankStr { get; set; }

        [Display(Name = "历史渠道等级")]
        public string OldRankDesc { get; set; }

        [Display(Name = "渠道等级状态")]
        public int RankStatus { get; set; }

        [Display(Name = "渠道等级状态")]
        public string RankStatusStr { get; set; }

        [Display(Name = "渠道等级状态")]
        public string RankStatusDesc { get; set; }

        [Display(Name = "记录创建时间")]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "最后修改时间")]
        public DateTime? ModifiedTime { get; set; }
    }
}
