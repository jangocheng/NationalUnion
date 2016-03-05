using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.ViewModels
{
    public class ManagerInfo
    {
        [DisplayName("手机号")]
        public int PhoneNo { get; set; }

        [DisplayName("身份证号")]
        public int IdCardNo { get; set; }

        [DisplayName("邮箱")]
        public string Email { get; set; }

        [DisplayName("QQ")]
        public int QQ { get; set; }

        [DisplayName("微信")]
        public string Weixin { get; set; }

        [DisplayName("银行卡信息")]
        public BankCardInfo BankCardInfo { get; set; }
    }
}
