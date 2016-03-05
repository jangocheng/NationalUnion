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
    public class ChannelMoudleBll : IChannelMoudleBll
    {
        [Dependency]
        public IChannelMoudleRepository ChannelMoudleRepository { get; set; }

        /// <summary>
        /// 获取功能模块列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<MoudleInfo> GetMoudleList(string parentId)
        {
            //IQueryable<ChannelMoudle>  queryData = ChannelMoudleRepository.GetMoudleList().Where(m => m.ParentId == parentId).OrderBy(a => a.Sort);
            //IQueryable<ChannelMoudle> queryData = ChannelMoudleRepository.GetMoudleByParent(parentId).OrderBy(a => a.Sort);
            List<ChannelMoudle> queryData = ChannelMoudleRepository.GetMoudleByParent(parentId);

            return CreateModelList(ref queryData);
        }

        /// <summary>
        /// 转换成View层展现的数据列表
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public List<MoudleInfo> CreateModelList(ref List<ChannelMoudle> queryData)
        {
            List<MoudleInfo> modelList = queryData.Select(d =>
                new MoudleInfo
                {
                    MoudleId = d.MoudleId,
                    Name = d.Name,
                    EnglishName = d.EnglishName,
                    ParentId = d.ParentId,
                    Url = d.Url,
                    Iconic = d.Iconic,
                    Sort = d.Sort,
                    Remark = d.Remark,
                    MoudleState = d.MoudleState,
                    CreatePerson = d.CreatePerson,
                    CreateTime = d.CreateTime,
                    IsLast = d.IsLast
                }).ToList();

            return modelList;
        }

        /// <summary>
        /// 根据上级Id，获取功能模块列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<ChannelMoudle> GetMoudleByParent(string parentId)
        {
            return ChannelMoudleRepository.GetMoudleByParent(parentId);
        }

        /// <summary>
        /// 根据功能模块Id，获取功能模块
        /// </summary>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        public MoudleInfo GetMoudleById(string moudleId)
        {
            if (MoudleExistsById(moudleId))
            {
                ChannelMoudle entity = ChannelMoudleRepository.GetMoudleById(moudleId);
                var model = new MoudleInfo
                {
                    MoudleId = entity.MoudleId,
                    Name = entity.Name,
                    EnglishName = entity.EnglishName,
                    ParentId = entity.ParentId,
                    Url = entity.Url,
                    Iconic = entity.Iconic,
                    Sort = entity.Sort,
                    Remark = entity.Remark,
                    MoudleState = entity.MoudleState,
                    CreatePerson = entity.CreatePerson,
                    CreateTime = entity.CreateTime,
                    IsLast = entity.IsLast
                };

                return model;
            }

            return null;
        }

        /// <summary>
        /// 判断功能模块是否存在
        /// </summary>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        public bool MoudleExistsById(string moudleId)
        {
            return ChannelMoudleRepository.MoudleExistsById(moudleId);
        }

        /// <summary>
        /// 添加功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool AddMoudle(MoudleInfo model, ref ValidationErrors errors)
        {
            try
            {
                ChannelMoudle entity = ChannelMoudleRepository.GetMoudleById(model.MoudleId);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new ChannelMoudle
                {
                    MoudleId = model.MoudleId,
                    Name = model.Name,
                    EnglishName = model.EnglishName,
                    ParentId = model.ParentId,
                    Url = model.Url,
                    Iconic = model.Iconic,
                    Sort = model.Sort,
                    Remark = model.Remark,
                    MoudleState = model.MoudleState,
                    CreatePerson = model.CreatePerson,
                    CreateTime = model.CreateTime,
                    IsLast = model.IsLast
                };

                if (ChannelMoudleRepository.AddMoudle(entity) == 1)
                {
                    // 分配给角色 存储过程
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

        /// <summary>
        /// 修改功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool EditMoudle(MoudleInfo model, ref ValidationErrors errors)
        {
            try
            {
                ChannelMoudle entity = ChannelMoudleRepository.GetMoudleById(model.MoudleId);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.MoudleId = model.MoudleId;
                entity.Name = model.Name;
                entity.EnglishName = model.EnglishName;
                entity.ParentId = model.ParentId;
                entity.Url = model.Url;
                entity.Iconic = model.Iconic;
                entity.Sort = model.Sort;
                entity.Remark = model.Remark;
                entity.MoudleState = model.MoudleState;
                entity.IsLast = model.IsLast;

                if (ChannelMoudleRepository.EditMoudle(entity) == 1)
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

        /// <summary>
        /// 删除功能模块
        /// </summary>
        /// <param name="moudleId"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool DeleteMoudle(string moudleId, ref ValidationErrors errors)
        {
            try
            {
                // 检查是否有下级
                var count = ChannelMoudleRepository.GetMoudleByParent(moudleId).Count();
                if (count > 0)
                {
                    // 有下属关联，请先删除下属模块
                    errors.Add(Suggestion.HasChildRelated);
                    return false;
                }

                if (ChannelMoudleRepository.DeleteMoudle(moudleId) > 0)
                {
                    // 清理无用操作权限 存储过程
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
