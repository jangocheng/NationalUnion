using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Domain.NotMapping
{
    public class GetPermissionByRoleAndMoudle
    {
        /// <summary>
        /// 操作码名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作码
        /// </summary>
        public string KeyCode { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        public string MoudleId { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        public string RightId { get; set; }

        /// <summary>
        /// 是否验证
        /// </summary>
        public int IsValid { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }
    }
}
