using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Application.Commons;
using NationalUnion.Application.ViewModels;

namespace NationalUnion.Application.Interfaces
{
    public interface IChannelRoleBll
    {
        List<RoleInfo> GetRoleList(ref GridPager pager, string queryStr);

        RoleInfo GetRoleById(string roleId);

        bool RoleExistsById(string roleId);

        bool AddRole(RoleInfo model, ref ValidationErrors errors);

        bool EditRole(RoleInfo model, ref ValidationErrors errors);

        bool DeleteRole(string roleId, ref ValidationErrors errors);
    }
}
