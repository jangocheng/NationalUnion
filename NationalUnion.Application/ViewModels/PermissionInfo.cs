using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Application.ViewModels
{
    public class PermissionInfo
    {
        // RightId = RoleId + MoudleId
        // MoudleOperateId = MoudleId + KeyCode
        // RightId + KeyCode = RoleId + MoudleOperateId
        public string RightOperateId { get; set; }

        public string Name { get; set; }

        public string RightId { get; set; }

        public string KeyCode { get; set; }

        public int IsValid { get; set; }
    }
}
