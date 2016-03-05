using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.ViewModels
{
    public class MoudleOperateInfo
    {
        [Display(Name = "操作编号")]
        public string MoudleOperateId { get; set; }

        [Display(Name = "操作名称")]
        public string Name { get; set; }

        [Display(Name = "所属模块")]
        public string MoudleId { get; set; }

        [Display(Name = "操作码")]
        public string KeyCode { get; set; }

        [Display(Name = "是否验证")]
        public int IsValid { get; set; }

        [Display(Name = "排序号"), Required(ErrorMessage = "{0}必须填写")]
        public int Sort { get; set; }
    }
}
