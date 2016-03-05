using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.ViewModels
{
    public class RoleInfo
    {
        [Display(Name = "角色编号")]
        public string RoleId { get; set; }

        [Display(Name = "角色名称")]
        public string Name { get; set; }

        [Display(Name = "说明")]
        public string Description { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "创建人")]
        public string CreatePerson { get; set; }

        [Display(Name = "拥有用户")]
        public string ManagerName { get; set; }

        [Display(Name = "用户分配角色")]
        public string Flag { get; set; }
    }
}
