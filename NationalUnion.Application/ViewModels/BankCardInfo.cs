using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.ViewModels
{
    public class BankCardInfo
    {
        [DisplayName("户主")]
        public string Name { get; set; }

        [DisplayName("卡号")]
        public int CardNo { get; set; }

        [DisplayName("开户行")]
        public string BankName { get; set; }
    }
}
