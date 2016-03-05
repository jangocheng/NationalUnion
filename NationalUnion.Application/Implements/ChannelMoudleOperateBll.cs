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
    public class ChannelMoudleOperateBll : IChannelMoudleOperateBll
    {
        [Dependency]
        public IChannelMoudleOperateRepository ChannelMoudleOperateRepository { get; set; }

        /// <summary>
        /// 获取模块操作列表
        /// </summary>
        /// <param name="moudleId"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<MoudleOperateInfo> GetMoudleOperateList(string moudleId, ref GridPager pager)
        {
            var queryData = ChannelMoudleOperateRepository.GetMoudleOperateList(moudleId);
            pager.TotalRows = queryData.Count();
            //queryData = LinqHelper.DataSortingAndPaging(queryData, pager.Sort, pager.Order, pager.Page, pager.Rows);

            return CreateModelList(ref queryData);
        }

        /// <summary>
        /// 转换成View层展现的数据列表
        /// </summary>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public List<MoudleOperateInfo> CreateModelList(ref List<ChannelMoudleOperate> queryData)
        {
            List<MoudleOperateInfo> modelList = queryData.Select(d =>
                new MoudleOperateInfo
                {
                    MoudleOperateId = d.MoudleOperateId,
                    Name = d.Name,
                    MoudleId = d.MoudleId,
                    KeyCode = d.KeyCode,
                    IsValid = d.IsValid,
                    Sort = d.Sort
                }).ToList();

            return modelList;
        }

        /// <summary>
        /// 根据模块操作Id，获取模块操作
        /// </summary>
        /// <param name="moudleOperateId"></param>
        /// <returns></returns>
        public MoudleOperateInfo GetMoudleOperateById(string moudleOperateId)
        {
            if (MoudleOperateExistsById(moudleOperateId))
            {
                ChannelMoudleOperate entity = ChannelMoudleOperateRepository.GetMoudleOperateById(moudleOperateId);
                var model = new MoudleOperateInfo
                {
                    MoudleOperateId = entity.MoudleOperateId,
                    Name = entity.Name,
                    MoudleId = entity.MoudleId,
                    KeyCode = entity.KeyCode,
                    IsValid = entity.IsValid,
                    Sort = entity.Sort
                };

                return model;
            }

            return null;
        }

        /// <summary>
        /// 判断模块操作是否存在
        /// </summary>
        /// <param name="moudleOperateId"></param>
        /// <returns></returns>
        public bool MoudleOperateExistsById(string moudleOperateId)
        {
            return ChannelMoudleOperateRepository.MoudleOperateExistsById(moudleOperateId);
        }

        /// <summary>
        /// 添加模块操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool AddMoudleOperate(MoudleOperateInfo model, ref ValidationErrors errors)
        {
            try
            {
                ChannelMoudleOperate entity = ChannelMoudleOperateRepository.GetMoudleOperateById(model.MoudleOperateId);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new ChannelMoudleOperate
                {
                    MoudleOperateId = model.MoudleOperateId,
                    Name = model.Name,
                    MoudleId = model.MoudleId,
                    KeyCode = model.KeyCode,
                    IsValid = model.IsValid,
                    Sort = model.Sort
                };

                if (ChannelMoudleOperateRepository.AddMoudleOperate(entity) == 1)
                {
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
        /// 修改模块操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool EditMoudleOperate(MoudleOperateInfo model, ref ValidationErrors errors)
        {
            try
            {
                ChannelMoudleOperate entity = ChannelMoudleOperateRepository.GetMoudleOperateById(model.MoudleOperateId);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.MoudleOperateId = model.MoudleOperateId;
                entity.Name = model.Name;
                entity.MoudleId = model.MoudleId;
                entity.KeyCode = model.KeyCode;
                entity.IsValid = model.IsValid;
                entity.Sort = model.Sort;

                if (ChannelMoudleOperateRepository.EditMoudleOperate(entity) == 1)
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
        /// 删除模块操作
        /// </summary>
        /// <param name="moudleOperateId"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool DeleteMoudleOperate(string moudleOperateId, ref ValidationErrors errors)
        {
            try
            {
                if (ChannelMoudleOperateRepository.DeleteMoudleOperate(moudleOperateId) > 0)
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
