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
    public class ManagerDetailBll : IManagerDetailBll
    {
        [Dependency]
        public IManagerDetailRepository ManagerDetailRepository { get; set; }
        [Dependency]
        public IChannelManageBll ChannelManageBll { get; set; }

        /// <summary>
        /// 判断历史明细ID是否存在
        /// </summary>
        /// <param name="managerDetailId"></param>
        /// <returns></returns>
        public bool ManagerDetailIdExists(int managerDetailId)
        {
            return ManagerDetailRepository.ManagerDetailIdExists(managerDetailId);
        }

        /// <summary>
        /// 根据历史明细ID获取历史明细
        /// </summary>
        /// <param name="managerDetailId"></param>
        /// <returns></returns>
        public ManagerDetailInfo GetManagerDetailById(int managerDetailId)
        {
            if (ManagerDetailIdExists(managerDetailId))
            {
                var entity = ManagerDetailRepository.GetManagerDetailById(managerDetailId);
                var model = new ManagerDetailInfo
                {
                    ManagerDetailId = entity.ManagerDetailId,
                    ManagerId = entity.ManagerId,
                    ManagerName = entity.Manager.ManagerName,
                    CurrentChannelId = entity.CurrentChannelId,
                    CurrentChannel = ChannelManageBll.GetChannelById(entity.CurrentChannelId).ChannelName,
                    OldChannelId = entity.OldChannelId,
                    OldChannel = ChannelManageBll.GetChannelById(entity.OldChannelId).ChannelName,
                    CurrentRank = entity.CurrentRank,
                    OldRank = entity.OldRank,
                    RankStatus = entity.RankStatus,
                    CreatedTime = entity.CreatedTime,
                    ModifiedTime = entity.ModifiedTime
                };

                return model;
            }

            return new ManagerDetailInfo();
        }

        /// <summary>
        /// 获取历史明细列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<ManagerDetailInfo> GetManagerDetailList(ref GridPager pager)
        {
            var queryData = ManagerDetailRepository.GetManagerDetailList();
            // 排序
            //if (pager.Order == "desc")
            //{
            //    switch (pager.Order)
            //    {
            //        case "ModifiedTime":
            //            queryData = queryData.OrderByDescending(c => c.ModifiedTime);
            //            break;
            //        case "ManagerId":
            //            queryData = queryData.OrderByDescending(c => c.ManagerId);
            //            break;
            //        default:
            //            queryData = queryData.OrderByDescending(c => c.ManagerDetailId);
            //            break;
            //    }
            //}
            //else
            //{
            //    switch (pager.Order)
            //    {
            //        case "ModifiedTime":
            //            queryData = queryData.OrderBy(c => c.ModifiedTime);
            //            break;
            //        case "ManagerId":
            //            queryData = queryData.OrderBy(c => c.ManagerId);
            //            break;
            //        default:
            //            queryData = queryData.OrderBy(c => c.ManagerDetailId);
            //            break;
            //    }
            //}

            queryData = LinqHelper.DataSorting(queryData, pager.Sort, pager.Order);

            return CreateModelList(ref pager, ref queryData);
        }

        public List<ManagerDetailInfo> GetManagerDetailListByQuery(string queryStr, ref GridPager pager)
        {
            var queryData = ManagerDetailRepository.GetManagerDetailList();

            // *负责人* / 当前渠道级别 / 历史渠道级别 / 渠道等级状态
            string[] queryArr = queryStr.Split(new char[] {'/'});
            string queryByName = queryArr[0];
            string queryByCurRank = queryArr[1];
            int curRank = Convert.ToInt32(Enum.Parse(typeof (ChannelRank), queryByCurRank));
            string queryByOldRank = queryArr[2];
            int oldRank = Convert.ToInt32(Enum.Parse(typeof (ChannelRank), queryByOldRank));
            string queryByRankStatus = queryArr[3];
            int rankStatus = Convert.ToInt32(Enum.Parse(typeof (ChannelRankChangeStatus), queryByRankStatus));

            if (!string.IsNullOrEmpty(queryByName) && !queryByName.Equals("输入*负责人*关键字"))
            {
                queryData = queryData.Where(d => d.Manager.ManagerName.Contains(queryByName));
            }
            if (!string.IsNullOrEmpty(queryByCurRank) && !queryByCurRank.Equals("-1"))
            {
                queryData = queryData.Where(d => d.CurrentRank == curRank);
            }
            if (!string.IsNullOrEmpty(queryByOldRank) && !queryByOldRank.Equals("-1"))
            {
                queryData = queryData.Where(d => d.OldRank == oldRank);
            }
            if (!string.IsNullOrEmpty(queryByRankStatus) && !queryByRankStatus.Equals("-1"))
            {
                queryData = queryData.Where(d => d.RankStatus == rankStatus);
            }

            // 排序
            //if (pager.Order == "desc")
            //{
            //    switch (pager.Order)
            //    {
            //        case "ModifiedTime":
            //            queryData = queryData.OrderByDescending(c => c.ModifiedTime);
            //            break;
            //        case "ManagerId":
            //            queryData = queryData.OrderByDescending(c => c.ManagerId);
            //            break;
            //        default:
            //            queryData = queryData.OrderByDescending(c => c.ManagerDetailId);
            //            break;
            //    }
            //}
            //else
            //{
            //    switch (pager.Order)
            //    {
            //        case "ModifiedTime":
            //            queryData = queryData.OrderBy(c => c.ModifiedTime);
            //            break;
            //        case "ManagerId":
            //            queryData = queryData.OrderBy(c => c.ManagerId);
            //            break;
            //        default:
            //            queryData = queryData.OrderBy(c => c.ManagerDetailId);
            //            break;
            //    }
            //}

            queryData = LinqHelper.DataSorting(queryData, pager.Sort, pager.Order);

            return CreateModelList(ref pager, ref queryData);
        }

        public bool AddManagerDetail(ManagerDetailInfo model)
        {
            throw new NotImplementedException();
        }

        public bool EditManagerDetail(ManagerDetailInfo model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除历史明细
        /// </summary>
        /// <param name="managerDetailId"></param>
        /// <returns></returns>
        public bool DeleteManagerDetail(int managerDetailId)
        {
            try
            {
                var entity = ManagerDetailRepository.GetManagerDetailById(managerDetailId);
                if (ManagerDetailRepository.DeleteManagerDetail(entity) == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 转换成View层展现的数据列表
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        private List<ManagerDetailInfo> CreateModelList(ref GridPager pager, ref IQueryable<ManagerDetail> queryData)
        {
            pager.TotalRows = queryData.Count();
            if (pager.TotalRows > 0)
            {
                queryData = pager.Page <= 1
                    ? queryData.Take(pager.Rows)
                    : queryData.Skip((pager.Page - 1) * pager.Rows).Take(pager.Rows);
            }

            List<ManagerDetailInfo> modelList = queryData.Select(d =>
                new ManagerDetailInfo
                {
                    ManagerDetailId = d.ManagerDetailId,
                    ManagerId = d.ManagerId,
                    ManagerName = d.Manager.ManagerName,
                    CurrentChannelId = d.CurrentChannelId,
                    OldChannelId = d.OldChannelId,
                    CurrentRank = d.CurrentRank,
                    OldRank = d.OldRank,
                    RankStatus = d.RankStatus,
                    CreatedTime = d.CreatedTime,
                    ModifiedTime = d.ModifiedTime
                }).ToList();

            return modelList;
        }
    }
}
