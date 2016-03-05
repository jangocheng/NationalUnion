using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NationalUnion.Application.Commons;

namespace NationalUnion.Application.ViewModels
{
    public class ManagerAccount
    {
        [DisplayName("会员编号")]
        public Int64 ManagerId { get; set; }

        [DisplayName("会员姓名")]
        public string ManagerName { get; set; }

        [DisplayName("邮箱"), RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", ErrorMessage = "邮箱格式不正确")]
        public string ManagerEmail { get; set; }

        [DisplayName("手机号"), RegularExpression(@"^1[3|4|5|8][0-9]\d{4,8}$", ErrorMessage = "手机号格式不正确")]
        public string MobilePhone { get; set; }

        [DisplayName("身份证号"), RegularExpression(@"(^\d{15}$)|(^\d{17}([0-9]|X)$)", ErrorMessage = "身份证号必须为15位或18位")]
        public string IdCardNo { get; set; }

        [DisplayName("渠道")]
        public Int64 ChannelId { get; set; }

        [DisplayName("分享宝渠道")]
        public Int64 ShareChannelId { get; set; }

        [DisplayName("归属渠道")]
        public Int64 ParentId { get; set; }

        [DisplayName("渠道")]
        public string ChannelName { get; set; }

        [DisplayName("分享宝渠道")]
        public string ShareChannelName { get; set; }

        [DisplayName("归属渠道")]
        public string ParentChannel { get; set; }

        [DisplayName("渠道类型")]
        public int ChannelType { get; set; }

        [DisplayName("渠道类型")]
        public string ChannelTypeStr { get; set; }

        [DisplayName("渠道类型")]
        public string ChannelTypeDesc { get; set; }

        [DisplayName("渠道级别")]
        public int ChannelRank { get; set; }

        [DisplayName("渠道级别")]
        public string ChannelRankStr { get; set; }

        [DisplayName("渠道级别")]
        public string ChannelRankDesc { get; set; }

        [DisplayName("省份")]
        public int ProvinceId { get; set; }

        [DisplayName("省份")]
        public string Province { get; set; }

        [DisplayName("省份")]
        public string ProvinceStr { get; set; }

        [DisplayName("城市")]
        public int CityId { get; set; }

        [DisplayName("城市")]
        public string City { get; set; }

        [DisplayName("城市")]
        public string CityStr { get; set; }

        [DisplayName("创建时间")]
        public DateTime CreatedTime { get; set; }

        [DisplayName("最后修改时间")]
        public DateTime? ModifiedTime { get; set; }

        [DisplayName("账户状态")]
        public int IsActive { get; set; }

        [DisplayName("账户状态")]
        public string IsActiveStr { get; set; }

        [DisplayName("账户状态")]
        public string IsActiveDesc { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("输入旧密码")]
        [DataType(DataType.Password)]
        public string OldPwd { get; set; }

        [DisplayName("输入新密码")]
        [DataType(DataType.Password)]
        public string ResetPwd { get; set; }

        [DisplayName("确认新密码")]
        [DataType(DataType.Password)]
        [Compare("ResetPwd", ErrorMessage = "两次输入密码不一致")]
        public string ResetPwdSec { get; set; }

        [DisplayName("详细信息")]
        public ManagerInfo ManagerInfo { get; set; }
    }
}
