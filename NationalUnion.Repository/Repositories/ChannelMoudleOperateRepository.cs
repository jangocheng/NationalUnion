using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Repository.Repositories
{
    public class ChannelMoudleOperateRepository : IChannelMoudleOperateRepository
    {
        //public IQueryable<ChannelMoudleOperate> GetMoudleOperateList(string moudleId)
        //{
        //    using (var db = new NationalUnionContext())
        //    {
        //        return db.ChannelMoudleOperates.Where(cmo => cmo.MoudleId == moudleId).AsQueryable();
        //    }
        //}

        public List<ChannelMoudleOperate> GetMoudleOperateList(string moudleId)
        {
            using (var db = new NationalUnionContext())
            {
                return db.ChannelMoudleOperates.Where(cmo => cmo.MoudleId == moudleId).Distinct().OrderBy(a => a.Sort).ToList();
            }
        }

        public ChannelMoudleOperate GetMoudleOperateById(string moudleOperateId)
        {
            using (var db = new NationalUnionContext())
            {
                return db.ChannelMoudleOperates.SingleOrDefault(cmo => cmo.MoudleOperateId == moudleOperateId);
            }
        }

        public bool MoudleOperateExistsById(string moudleOperateId)
        {
            using (var db = new NationalUnionContext())
            {
                var count = db.ChannelMoudleOperates.Count(cmo => cmo.MoudleOperateId == moudleOperateId);

                return count > 0;
            }
        }

        public int AddMoudleOperate(ChannelMoudleOperate entity)
        {
            using (var db = new NationalUnionContext())
            {
                db.ChannelMoudleOperates.Add(entity);

                return db.SaveChanges();
            }
        }

        public int EditMoudleOperate(ChannelMoudleOperate entity)
        {
            using (var db = new NationalUnionContext())
            {
                db.ChannelMoudleOperates.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;

                return db.SaveChanges();
            }
        }

        public int DeleteMoudleOperate(string moudleOperateId)
        {
            using (var db = new NationalUnionContext())
            {
                ChannelMoudleOperate entity = db.ChannelMoudleOperates.SingleOrDefault(cmo => cmo.MoudleOperateId == moudleOperateId);
                if (entity != null)
                {
                    db.ChannelMoudleOperates.Remove(entity);
                }

                return db.SaveChanges();
            }
        }
    }
}
