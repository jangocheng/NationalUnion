using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.ViewModels
{
    public class ShareChannelInfo
    {
        [Display(Name = "渠道编号")]
        public Int64 ShareChannelId { get; set; }

        [Display(Name = "渠道")]
        public string ChannelName { get; set; }

        [Display(Name = "渠道等级")]
        public int Rank { get; set; }

        [Display(Name = "渠道等级")]
        public string RankStr { get; set; }

        [Display(Name = "渠道等级")]
        public string RankDesc { get; set; }

        [Display(Name = "渠道类型")]
        public int ChannelType { get; set; }

        [Display(Name = "渠道类型")]
        public string ChannelTypeStr { get; set; }

        [Display(Name = "渠道类型")]
        public string ChannelTypeDesc { get; set; }

        [Display(Name = "归属渠道")]
        public Int64 ParentId { get; set; }

        [Display(Name = "归属渠道")]
        public string ParentChannel { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "最后修改时间")]
        public DateTime? ModifiedTime { get; set; }

        [Display(Name = "渠道状态")]
        public int IsActive { get; set; }

        [Display(Name = "渠道状态")]
        public string IsActiveStr { get; set; }

        [Display(Name = "渠道状态")]
        public string IsActiveDesc { get; set; }
    }
}
