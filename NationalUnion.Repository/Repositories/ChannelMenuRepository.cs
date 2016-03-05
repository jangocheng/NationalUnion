using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Repository.Repositories
{
    public class ChannelMenuRepository : IChannelMenuRepository
    {
        public List<ChannelMoudle> GetMenuByParentId(string moudleId)
        {
            //var menus =
            //    this.FindBy(m => m.ParentId == moudleId && m.MoudleId != "0")
            //        .Distinct()
            //        .OrderBy(a => a.Sort)
            //        .ToList();

            //return menus;

            using (var db = new NationalUnionContext())
            {
                var menus = (from m in db.ChannelMoudles
                    where m.ParentId == moudleId
                    where m.MoudleId != "0"
                    select m).Distinct().OrderBy(a => a.Sort).ToList();

                return menus;
            }
        }

        public List<ChannelMoudle> GetTempMenuByParentId(string moudleId)
        {
            var permArr = ConfigurationManager.AppSettings["Permission"].Split(new char[] { '|' });
            var moudleFirst = permArr[0];
            var moudleSecond = permArr[1];
            using (var db = new NationalUnionContext())
            {
                var menus = (from m in db.ChannelMoudles
                             where m.ParentId == moudleId
                             where m.MoudleId != "0"
                             select m).Distinct().OrderBy(a => a.Sort).ToList();

                menus = menus.Where(m =>
                            (m.MoudleId == moudleFirst || m.ParentId == moudleFirst) ||
                            (m.MoudleId == moudleSecond || m.ParentId == moudleSecond)).ToList();

                return menus;
            }

            //var permArr = ConfigurationManager.AppSettings["Permission"].Split(new char[] { '|' });
            //using (var db = new NationalUnionContext())
            //{
            //    var menus = (from m in db.ChannelMoudles
            //                 where m.ParentId == moudleId
            //                 where m.MoudleId != "0"
            //                 select m).Distinct().OrderBy(a => a.Sort).ToList();

            //    menus = permArr.Aggregate(menus, (current, permisMenu) =>
            //            current.Where(m => m.MoudleId == permisMenu || m.ParentId == permisMenu).ToList());

            //    return menus;
            //}
        }

        public List<ChannelMoudle> GetMenuByPersonId(Int64 personId, string moudleId)
        {
            using (var db = new NationalUnionContext())
            {
                //var menus = (from menu in db.ChannelMoudles
                //    join right in db.ChannelRights
                //        on menu.MoudleId equals right.MoudleId
                //    join rm in db.ChannelRoleManagers
                //        on right.RoleId equals rm.RoleId
                //    where rm.ManagerId == personId
                //    where right.RightFlag == 1
                //    where menu.ParentId == moudleId
                //    where menu.MoudleId != "0"
                //    select menu).Distinct().OrderBy(a => a.Sort).ToList();

                var menus = (from rm in db.ChannelRoleManagers
                    where rm.Manager.ManagerId == personId
                    from right in db.ChannelRights
                    where right.RoleId == rm.RoleId
                    where right.RightFlag == 1
                    from m in db.ChannelMoudles
                    where m.MoudleId == right.MoudleId
                    where m.ParentId == moudleId
                    where m.MoudleId != "0"
                    select m).Distinct().OrderBy(a => a.Sort).ToList();

                return menus;
            }
        }

        public List<ChannelMoudle> GetMenuByPersonName(string personName, string moudleId)
        {
            using (var db = new NationalUnionContext())
            {
                var menus = (from menu in db.ChannelMoudles
                    join right in db.ChannelRights
                        on menu.MoudleId equals right.MoudleId
                    join rm in db.ChannelRoleManagers
                        on right.RoleId equals rm.RoleId
                    where rm.Manager.ManagerName == personName
                    where right.RightFlag == 1
                    where menu.ParentId == moudleId
                    where menu.MoudleId != "0"
                    select menu).Distinct().OrderBy(a => a.Sort).ToList();

                return menus;
            }
        }
    }
}
