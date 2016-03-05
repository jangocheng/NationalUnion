using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Commons;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Application.Implements
{
    public class ChannelRoleBll : IChannelRoleBll
    {
        [Dependency]
        public IChannelRoleRepository ChannelRoleRepository { get; set; }

        public List<RoleInfo> GetRoleList(ref GridPager pager, string queryStr)
        {
            List<ChannelRole> queryData = null;
            queryData = (!string.IsNullOrWhiteSpace(queryStr)
                ? ChannelRoleRepository.GetRoleList().Where(a => a.Name.Contains(queryStr))
                : ChannelRoleRepository.GetRoleList()) as List<ChannelRole>;

            if (queryData != null)
            {
                pager.TotalRows = queryData.Count();
                //queryData = LinqHelper.DataSortingAndPaging(queryData, pager.Sort, pager.Order, pager.Page, pager.Rows);

                return CreateModelList(ref queryData);
            }

            return null;
        }

        private List<RoleInfo> CreateModelList(ref List<ChannelRole> queryData)
        {
            List<RoleInfo> modelList = queryData.Select(r =>
                new RoleInfo
                {
                    RoleId = r.RoleId,
                    Name = r.Name,
                    Description = r.Description,
                    CreateTime = r.CreateTime,
                    CreatePerson = r.CreatePerson,
                    ManagerName = ""
                }).ToList();

            return modelList;
        }

        public RoleInfo GetRoleById(string roleId)
        {
            if (RoleExistsById(roleId))
            {
                ChannelRole entity = ChannelRoleRepository.GetRoleById(roleId);
                var model = new RoleInfo
                {
                    RoleId = entity.RoleId,
                    Name = entity.Name,
                    Description = entity.Description,
                    CreateTime = entity.CreateTime,
                    CreatePerson = entity.CreatePerson
                };

                return model;
            }

            return null;
        }

        public bool RoleExistsById(string roleId)
        {
            return ChannelRoleRepository.RoleExistsById(roleId);
        }

        public bool AddRole(RoleInfo model, ref ValidationErrors errors)
        {
            try
            {
                ChannelRole entity = ChannelRoleRepository.GetRoleById(model.RoleId);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new ChannelRole
                {
                    RoleId = model.RoleId,
                    Name = model.Name,
                    Description = model.Description,
                    CreateTime = model.CreateTime,
                    CreatePerson = model.CreatePerson
                };

                if (ChannelRoleRepository.AddRole(entity) == 1)
                {
                    // 分配给角色 存储过程
                    // 清理无用项 存储过程
                    return true;
                }

                errors.Add(Suggestion.InsertFail);
                return false;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return false;
            }
        }

        public bool EditRole(RoleInfo model, ref ValidationErrors errors)
        {
            try
            {
                ChannelRole entity = ChannelRoleRepository.GetRoleById(model.RoleId);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.RoleId = model.RoleId;
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.CreateTime = model.CreateTime;
                entity.CreatePerson = model.CreatePerson;

                if (ChannelRoleRepository.EditRole(entity) == 1)
                {
                    return true;
                }

                errors.Add(Suggestion.EditFail);
                return false;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return false;
            }
        }

        public bool DeleteRole(string roleId, ref ValidationErrors errors)
        {
            try
            {
                if (ChannelRoleRepository.DeleteRole(roleId) > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return false;
            }
        }
    }
}
