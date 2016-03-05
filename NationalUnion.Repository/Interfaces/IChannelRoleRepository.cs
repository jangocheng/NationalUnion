using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IChannelRoleRepository
    {
        //IQueryable<ChannelRole> GetRoleList();

        List<ChannelRole> GetRoleList();

        ChannelRole GetRoleById(string roleId);

        bool RoleExistsById(string roleId);

        int AddRole(ChannelRole entity);

        int EditRole(ChannelRole entity);

        int DeleteRole(string roleId);
    }
}
