using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Repository.Repositories
{
    public class ChannelMoudleRepository : IChannelMoudleRepository
    {
        //public IQueryable<ChannelMoudle> GetMoudleList()
        //{
        //    using (var db = new NationalUnionContext())
        //    {
        //        return db.ChannelMoudles.AsQueryable();
        //    }
        //}

        public List<ChannelMoudle> GetMoudleList()
        {
            using (var db = new NationalUnionContext())
            {
                return db.ChannelMoudles.Distinct().OrderBy(a => a.Sort).ToList();
            }
        }

        //public IQueryable<ChannelMoudle> GetMoudleByParent(string parentId)
        //{
        //    using (var db = new NationalUnionContext())
        //    {
        //        //return db.ChannelMoudles.Where(cm => cm.ParentId == parentId).AsQueryable();

        //        var menus = (from m in db.ChannelMoudles
        //            where m.ParentId == parentId
        //            select m).Distinct();

        //        return menus;
        //    }
        //}

        public List<ChannelMoudle> GetMoudleByParent(string parentId)
        {
            using (var db = new NationalUnionContext())
            {
                var menus = db.ChannelMoudles.Where(cm => cm.ParentId == parentId && cm.MoudleId != "0")
                    .Distinct()
                    .OrderBy(a => a.Sort)
                    .ToList();

                return menus;

                //var menus = (from m in db.ChannelMoudles
                //    where m.ParentId == parentId
                //    select m).Distinct().OrderBy(a => a.Sort).ToList();

                //return menus;
            }
        }

        public ChannelMoudle GetMoudleById(string moudleId)
        {
            using (var db = new NationalUnionContext())
            {
                return db.ChannelMoudles.SingleOrDefault(cm => cm.MoudleId == moudleId);
            }
        }

        public bool MoudleExistsById(string moudleId)
        {
            using (var db = new NationalUnionContext())
            {
                var count = db.ChannelMoudles.Count(cm => cm.MoudleId == moudleId);

                return count > 0;
            }
        }

        public int AddMoudle(ChannelMoudle entity)
        {
            using (var db = new NationalUnionContext())
            {
                db.ChannelMoudles.Add(entity);

                return db.SaveChanges();
            }
        }

        public int EditMoudle(ChannelMoudle entity)
        {
            using (var db = new NationalUnionContext())
            {
                db.ChannelMoudles.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;

                return db.SaveChanges();
            }
        }

        public int DeleteMoudle(string moudleId)
        {
            using (var db = new NationalUnionContext())
            {
                var entity = db.ChannelMoudles.SingleOrDefault(cm => cm.MoudleId == moudleId);
                if (entity != null)
                {
                    // 删除ChannelRight表数据
                    var rights = db.ChannelRights.Where(cr => cr.MoudleId == moudleId).AsQueryable();
                    foreach (var right in rights)
                    {
                        // 删除ChannelRightOperate表数据
                        var operates = db.ChannelRightOperates.Where(cro => cro.RightId == right.RightId).AsQueryable();
                        foreach (var operate in operates)
                        {
                            db.ChannelRightOperates.Remove(operate);
                        }
                        db.ChannelRights.Remove(right);
                    }

                    // 删除ChannelMoudleOperate表数据
                    var menuOperates = db.ChannelMoudleOperates.Where(cmo => cmo.MoudleId == moudleId).AsQueryable();
                    foreach (var menuOperate in menuOperates)
                    {
                        db.ChannelMoudleOperates.Remove(menuOperate);
                    }

                    db.ChannelMoudles.Remove(entity);

                    return db.SaveChanges();
                }

                return 0;
            }
        }
    }
}
