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
    public class ChannelManageBll : IChannelManageBll
    {
        [Dependency]
        public IChannelManageRepository ChannelManageRepository { get; set; }

        /// <summary>
        /// 判断渠道ID是否存在
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public bool ChannelIdExists(Int64 channelId)
        {
            return ChannelManageRepository.ChannelIdExists(channelId);
        }

        /// <summary>
        /// 根据渠道ID获取渠道
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public ChannelInfo GetChannelById(Int64 channelId)
        {
            if (ChannelIdExists(channelId))
            {
                var entity = ChannelManageRepository.GetChannelById(channelId);
                var model = new ChannelInfo
                {
                    ChannelId = entity.ChannelId,
                    ChannelName = entity.ChannelName,
                    ChannelType = entity.ChannelType,
                    Rank = entity.Rank,
                    ParentId = entity.ParentId,
                    KeyWord = entity.KeyWord,
                    CreatedTime = entity.CreatedTime,
                    ModifiedTime = entity.ModifiedTime,
                    IsActive = entity.IsActive
                };

                return model;
            }

            return new ChannelInfo();
        }

        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<ChannelInfo> GetChannelList(ref GridPager pager)
        {
            var queryData = ChannelManageRepository.GetChannelList();
            // 排序
            //if (pager.Order == "desc")
            //{
            //    switch (pager.Order)
            //    {
            //        case "ModifiedTime":
            //            queryData = queryData.OrderByDescending(c => c.ModifiedTime);
            //            break;
            //        case "ChannelName":
            //            queryData = queryData.OrderByDescending(c => c.ChannelName);
            //            break;
            //        default:
            //            queryData = queryData.OrderByDescending(c => c.ChannelId);
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
            //        case "ChannelName":
            //            queryData = queryData.OrderBy(c => c.ChannelName);
            //            break;
            //        default:
            //            queryData = queryData.OrderBy(c => c.ChannelId);
            //            break;
            //    }
            //}

            queryData = LinqHelper.DataSorting(queryData, pager.Sort, pager.Order);

            return CreateModelList(ref pager, ref queryData);
        }

        /// <summary>
        /// 根据查询条件获取渠道列表
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<ChannelInfo> GetChannelListByQuery(string queryStr, ref GridPager pager)
        {
            var queryData = ChannelManageRepository.GetChannelList();

            // *渠道* / *归属渠道* / 渠道类型 / 渠道等级 / 渠道状态
            string[] queryArr = queryStr.Split(new char[] { '/' });
            string queryByName = queryArr[0];
            string queryByParentChannel = queryArr[1];
            string queryByType = queryArr[2];
            int type = Convert.ToInt32(Enum.Parse(typeof (ChannelType), queryByType));
            string queryByRank = queryArr[3];
            int rank = Convert.ToInt32(Enum.Parse(typeof (ChannelRank), queryByRank));
            string queryByStatus = queryArr[4];
            int status = Convert.ToInt32(Enum.Parse(typeof (Status), queryByStatus));

            if (!string.IsNullOrEmpty(queryByName) && !queryByName.Equals("输入*渠道*关键字"))
            {
                queryData = queryData.Where(d => d.ChannelName.Contains(queryByName));
            }
            if (!string.IsNullOrEmpty(queryByParentChannel) && !queryByParentChannel.Equals("输入*归属渠道*关键字"))
            {
                var parentChannel = ChannelManageRepository.GetChannelByName(queryByParentChannel);
                if (parentChannel != null)
                {
                    queryData = queryData.Where(d => d.ParentId == parentChannel.ChannelId);
                }
            }
            if (!string.IsNullOrEmpty(queryByType) && !queryByType.Equals("-1"))
            {
                queryData = queryData.Where(d => d.ChannelType == type);
            }
            if (!string.IsNullOrEmpty(queryByRank) && !queryByRank.Equals("-1"))
            {
                queryData = queryData.Where(d => d.Rank == rank);
            }
            if (!string.IsNullOrEmpty(queryByStatus) && !queryByStatus.Equals("-1"))
            {
                queryData = queryData.Where(d => d.IsActive == status);
            }

            // 排序
            //if (pager.Order == "desc")
            //{
            //    switch (pager.Order)
            //    {
            //        case "ModifiedTime":
            //            queryData = queryData.OrderByDescending(c => c.ModifiedTime);
            //            break;
            //        case "ChannelName":
            //            queryData = queryData.OrderByDescending(c => c.ChannelName);
            //            break;
            //        default:
            //            queryData = queryData.OrderByDescending(c => c.ChannelId);
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
            //        case "ChannelName":
            //            queryData = queryData.OrderBy(c => c.ChannelName);
            //            break;
            //        default:
            //            queryData = queryData.OrderBy(c => c.ChannelId);
            //            break;
            //    }
            //}

            queryData = LinqHelper.DataSorting(queryData, pager.Sort, pager.Order);

            return CreateModelList(ref pager, ref queryData);
        }

        /// <summary>
        /// 添加渠道
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddChannel(ChannelInfo model)
        {
            try
            {
                var entity = ChannelManageRepository.GetChannelById(model.ChannelId);
                if (entity != null)
                {
                    return false;
                }
                entity = new Channel
                {
                    ChannelName = model.ChannelName,
                    Rank = Convert.ToInt32(Enum.Parse(typeof (ChannelRank), model.RankStr)),
                    ChannelType = Convert.ToInt32(Enum.Parse(typeof(ChannelType),model.ChannelTypeStr)),
                    ParentId = model.ParentId,
                    KeyWord = model.KeyWord,
                    CreatedTime = DateTime.Now,
                    ModifiedTime = null,
                    IsActive = Convert.ToInt32(Enum.Parse(typeof (Status), model.IsActiveStr))
                };

                if (ChannelManageRepository.AddChannel(entity) == 1)
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
        /// 修改渠道
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditChannel(ChannelInfo model)
        {
            try
            {
                var entity = ChannelManageRepository.GetChannelById(model.ChannelId);
                if (entity == null)
                {
                    return false;
                }
                entity.ChannelName = model.ChannelName;
                entity.Rank = Convert.ToInt32(Enum.Parse(typeof (ChannelRank), model.RankStr));
                entity.ChannelType = Convert.ToInt32(Enum.Parse(typeof (ChannelType), model.ChannelTypeStr));
                entity.ParentId = model.ParentId;
                entity.KeyWord = model.KeyWord;
                entity.ModifiedTime = DateTime.Now;
                entity.IsActive = Convert.ToInt32(Enum.Parse(typeof (Status), model.IsActiveStr));

                if (ChannelManageRepository.EditChannel(entity) == 1)
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
        /// 删除渠道
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public bool DeleteChannel(Int64 channelId)
        {
            try
            {
                var entity = ChannelManageRepository.GetChannelById(channelId);
                if (ChannelManageRepository.DeleteChannel(entity) == 1)
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
        /// 激活或停用
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public bool ChangeStatus(Int64 channelId)
        {
            try
            {
                var entity = ChannelManageRepository.GetChannelById(channelId);
                if (entity == null)
                {
                    return false;
                }
                entity.ModifiedTime = DateTime.Now;
                entity.IsActive = entity.IsActive == 1 ? 2 : 1;

                if (ChannelManageRepository.EditChannel(entity) == 1)
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
        private List<ChannelInfo> CreateModelList(ref GridPager pager, ref IQueryable<Channel> queryData)
        {
            pager.TotalRows = queryData.Count();
            if (pager.TotalRows > 0)
            {
                queryData = pager.Page <= 1
                    ? queryData.Take(pager.Rows)
                    : queryData.Skip((pager.Page - 1)*pager.Rows).Take(pager.Rows);
            }

            List<ChannelInfo> modelList = queryData.Select(d =>
                new ChannelInfo
                {
                    ChannelId = d.ChannelId,
                    ChannelName = d.ChannelName,
                    Rank = d.Rank,
                    ChannelType = d.ChannelType,
                    ParentId = d.ParentId,
                    KeyWord = d.KeyWord,
                    CreatedTime = d.CreatedTime,
                    ModifiedTime = d.ModifiedTime,
                    IsActive = d.IsActive
                }).ToList();

            return modelList;
        }
    }
}
