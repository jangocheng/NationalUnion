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
    public class ShareChannelBll : IShareChannelBll
    {
        [Dependency]
        public IShareChannelRepository ShareChannelRepository { get; set; }

        public bool ShareChannelIdExists(Int64 shareChannelId)
        {
            return ShareChannelRepository.ShareChannelIdExists(shareChannelId);
        }

        public ShareChannelInfo GetShareChannelById(Int64 shareChannelId)
        {
            if (ShareChannelIdExists(shareChannelId))
            {
                var entity = ShareChannelRepository.GetShareChannelById(shareChannelId);
                var model = new ShareChannelInfo
                {
                    ShareChannelId = entity.ShareChannelId,
                    ChannelName = entity.ChannelName,
                    ChannelType = entity.ChannelType,
                    Rank = entity.Rank,
                    ParentId = entity.ParentId,
                    CreatedTime = entity.CreatedTime,
                    ModifiedTime = entity.ModifiedTime,
                    IsActive = entity.IsActive
                };

                return model;
            }

            return new ShareChannelInfo();
        }

        public List<ShareChannelInfo> GetShareChannelList(ref GridPager pager)
        {
            var queryData = ShareChannelRepository.GetShareChannelList();

            queryData = LinqHelper.DataSorting(queryData, pager.Sort, pager.Order);

            return CreateModelList(ref pager, ref queryData);
        }

        public List<ShareChannelInfo> GetShareChannelListByQuery(string queryStr, ref GridPager pager)
        {
            var queryData = ShareChannelRepository.GetShareChannelList();

            // *渠道* / 渠道类型 / 渠道等级 / 渠道状态
            string[] queryArr = queryStr.Split(new char[] { '/' });
            string queryByName = queryArr[0];
            string queryByType = queryArr[1];
            int type = Convert.ToInt32(Enum.Parse(typeof(ChannelType), queryByType));
            string queryByRank = queryArr[2];
            int rank = Convert.ToInt32(Enum.Parse(typeof(ChannelRank), queryByRank));
            string queryByStatus = queryArr[3];
            int status = Convert.ToInt32(Enum.Parse(typeof(Status), queryByStatus));

            if (!string.IsNullOrEmpty(queryByName) && !queryByName.Equals("输入*渠道*关键字"))
            {
                queryData = queryData.Where(d => d.ChannelName.Contains(queryByName));
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

            queryData = LinqHelper.DataSorting(queryData, pager.Sort, pager.Order);

            return CreateModelList(ref pager, ref queryData);
        }

        public bool AddShareChannel(ShareChannelInfo model)
        {
            try
            {
                var entity = ShareChannelRepository.GetShareChannelById(model.ShareChannelId);
                if (entity != null)
                {
                    return false;
                }
                entity = new ShareChannel
                {
                    ChannelName = model.ChannelName,
                    Rank = Convert.ToInt32(Enum.Parse(typeof(ChannelRank), model.RankStr)),
                    ChannelType = Convert.ToInt32(Enum.Parse(typeof(ChannelType), model.ChannelTypeStr)),
                    ParentId = model.ParentId,
                    CreatedTime = DateTime.Now,
                    ModifiedTime = null,
                    IsActive = Convert.ToInt32(Enum.Parse(typeof(Status), model.IsActiveStr))
                };

                if (ShareChannelRepository.AddShareChannel(entity) == 1)
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

        public bool EditShareChannel(ShareChannelInfo model)
        {
            try
            {
                var entity = ShareChannelRepository.GetShareChannelById(model.ShareChannelId);
                if (entity == null)
                {
                    return false;
                }
                entity.ChannelName = model.ChannelName;
                entity.Rank = Convert.ToInt32(Enum.Parse(typeof(ChannelRank), model.RankStr));
                entity.ChannelType = Convert.ToInt32(Enum.Parse(typeof(ChannelType), model.ChannelTypeStr));
                entity.ParentId = model.ParentId;
                entity.ModifiedTime = DateTime.Now;
                entity.IsActive = Convert.ToInt32(Enum.Parse(typeof(Status), model.IsActiveStr));

                if (ShareChannelRepository.EditShareChannel(entity) == 1)
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

        public bool DeleteShareChannel(Int64 shareChannelId)
        {
            try
            {
                var entity = ShareChannelRepository.GetShareChannelById(shareChannelId);
                if (ShareChannelRepository.DeleteShareChannel(entity) == 1)
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

        public bool ShareChangeStatus(Int64 shareChannelId)
        {
            try
            {
                var entity = ShareChannelRepository.GetShareChannelById(shareChannelId);
                if (entity == null)
                {
                    return false;
                }
                entity.ModifiedTime = DateTime.Now;
                entity.IsActive = entity.IsActive == 1 ? 2 : 1;

                if (ShareChannelRepository.EditShareChannel(entity) == 1)
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

        private List<ShareChannelInfo> CreateModelList(ref GridPager pager, ref IQueryable<ShareChannel> queryData)
        {
            pager.TotalRows = queryData.Count();
            if (pager.TotalRows > 0)
            {
                queryData = pager.Page <= 1
                    ? queryData.Take(pager.Rows)
                    : queryData.Skip((pager.Page - 1) * pager.Rows).Take(pager.Rows);
            }

            List<ShareChannelInfo> modelList = queryData.Select(d =>
                new ShareChannelInfo
                {
                    ShareChannelId = d.ShareChannelId,
                    ChannelName = d.ChannelName,
                    Rank = d.Rank,
                    ChannelType = d.ChannelType,
                    ParentId = d.ParentId,
                    CreatedTime = d.CreatedTime,
                    ModifiedTime = d.ModifiedTime,
                    IsActive = d.IsActive
                }).ToList();

            return modelList;
        }
    }
}
