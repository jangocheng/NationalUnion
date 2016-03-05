using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Repository.Repositories
{
    public class ChannelRoleRepository : IChannelRoleRepository
    {
        //public IQueryable<ChannelRole> GetRoleList()
        //{
        //    using (var db = new NationalUnionContext())
        //    {
        //        var list = db.ChannelRoles.AsQueryable();

        //        return list;
        //    }
        //}

        public List<ChannelRole> GetRoleList()
        {
            using (var db = new NationalUnionContext())
            {
                var list = db.ChannelRoles.ToList();

                return list;
            }
        }

        public ChannelRole GetRoleById(string roleId)
        {
            using (var db = new NationalUnionContext())
            {
                var role = db.ChannelRoles.SingleOrDefault(r => r.RoleId == roleId);

                return role;
            }
        }

        public bool RoleExistsById(string roleId)
        {
            using (var db = new NationalUnionContext())
            {
                var count = db.ChannelRoles.Count(r => r.RoleId == roleId);

                return count > 0;
            }
        }

        public int AddRole(ChannelRole entity)
        {
            using (var db = new NationalUnionContext())
            {
                db.ChannelRoles.Add(entity);

                return db.SaveChanges();
            }
        }

        public int EditRole(ChannelRole entity)
        {
            using (var db = new NationalUnionContext())
            {
                db.ChannelRoles.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;

                return db.SaveChanges();
            }
        }

        public int DeleteRole(string roleId)
        {
            using (var db = new NationalUnionContext())
            {
                var entity = db.ChannelRoles.SingleOrDefault(r => r.RoleId == roleId);
                if (entity != null)
                {
                    db.ChannelRoles.Remove(entity);
                }

                return db.SaveChanges();
            }
        }
    }
}
