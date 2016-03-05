using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Commons;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Domain.Mapping;
using NationalUnion.Domain.NotMapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Application.Implements
{
    public class ManagerAccountBll : IManagerAccountBll
    {
        [Dependency]
        public IManagerRepository ManagerRepository { get; set; }
        [Dependency]
        public IRegionProvinceCityRepository RegionProvinceCityRepository { get; set; }
        [Dependency]
        public IManagerStatisticsRepository ManagerStatisticsRepository { get; set; }
        [Dependency]
        public IChannelManageRepository ChannelManageRepository { get; set; }
        [Dependency]
        public IShareChannelRepository ShareChannelRepository { get; set; }

        /// <summary>
        /// 获取负责人列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public List<ManagerAccount> GetManagerList(ref GridPager pager)
        {
            var queryData = ManagerRepository.GetManagerList();
            // 排序
            //if (pager.Order == "desc")
            //{
            //    switch (pager.Order)
            //    {
            //        case "ModifiedTime":
            //            queryData = queryData.OrderByDescending(m => m.ModifiedTime);
            //            break;
            //        case "ManagerName":
            //            queryData = queryData.OrderByDescending(m => m.ManagerName);
            //            break;
            //        default:
            //            queryData = queryData.OrderByDescending(m => m.ManagerId);
            //            break;
            //    }
            //}
            //else
            //{
            //    switch (pager.Order)
            //    {
            //        case "ModifiedTime":
            //            queryData = queryData.OrderBy(m => m.ModifiedTime);
            //            break;
            //        case "ManagerName":
            //            queryData = queryData.OrderBy(m => m.ManagerName);
            //            break;
            //        default:
            //            queryData = queryData.OrderBy(m => m.ManagerId);
            //            break;
            //    }
            //}

            queryData = LinqHelper.DataSorting(queryData, pager.Sort, pager.Order);

            return CreateModelList(ref pager, ref queryData);
        }

        public List<ManagerAccount> GetManagerListByQuery(string queryStr, ref GridPager pager)
        {
            var queryData = ManagerRepository.GetManagerList();

            // *负责人* / *归属渠道* / 渠道类型 / 渠道等级 / 账号状态
            string[] queryArr = queryStr.Split(new char[] { '/' });
            string queryByName = queryArr[0];
            string queryByParentChannel = queryArr[1];
            string queryByType = queryArr[2];
            int type = Convert.ToInt32(Enum.Parse(typeof(ChannelType), queryByType));
            string queryByRank = queryArr[3];
            int rank = Convert.ToInt32(Enum.Parse(typeof(ChannelRank), queryByRank));
            string queryByStatus = queryArr[4];
            int status = Convert.ToInt32(Enum.Parse(typeof(Status), queryByStatus));

            if (!string.IsNullOrEmpty(queryByName) && !queryByName.Equals("输入*负责人*关键字"))
            {
                queryData = queryData.Where(d => d.ManagerName.Contains(queryByName));
            }
            if (!string.IsNullOrEmpty(queryByParentChannel) && !queryByParentChannel.Equals("输入*归属渠道*关键字"))
            {
                var parentChannel = ChannelManageRepository.GetChannelByName(queryByParentChannel);
                if (parentChannel != null)
                {
                    queryData = queryData.Where(d => d.Channel.ParentId == parentChannel.ChannelId);
                }
            }
            if (!string.IsNullOrEmpty(queryByType) && !queryByType.Equals("-1"))
            {
                queryData = queryData.Where(d => d.Channel.ChannelType == type);
            }
            if (!string.IsNullOrEmpty(queryByRank) && !queryByRank.Equals("-1"))
            {
                queryData = queryData.Where(d => d.Channel.Rank == rank);
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
            //            queryData = queryData.OrderByDescending(m => m.ModifiedTime);
            //            break;
            //        case "ManagerName":
            //            queryData = queryData.OrderByDescending(m => m.ManagerName);
            //            break;
            //        default:
            //            queryData = queryData.OrderByDescending(m => m.ManagerId);
            //            break;
            //    }
            //}
            //else
            //{
            //    switch (pager.Order)
            //    {
            //        case "ModifiedTime":
            //            queryData = queryData.OrderBy(m => m.ModifiedTime);
            //            break;
            //        case "ManagerName":
            //            queryData = queryData.OrderBy(m => m.ManagerName);
            //            break;
            //        default:
            //            queryData = queryData.OrderBy(m => m.ManagerId);
            //            break;
            //    }
            //}

            queryData = LinqHelper.DataSorting(queryData, pager.Sort, pager.Order);

            return CreateModelList(ref pager, ref queryData);
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="managerName"></param>
        /// <returns></returns>
        public bool ManagerNameExists(string managerName)
        {
            return ManagerRepository.ManagerNameExists(managerName);
        }

        /// <summary>
        /// 判断用户ID是否存在
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public bool ManagerIdExists(Int64 managerId)
        {
            return ManagerRepository.ManagerIdExists(managerId);
        }

        /// <summary>
        /// 根据名称获取负责人
        /// </summary>
        /// <param name="managerName"></param>
        /// <returns></returns>
        public ManagerAccount GetManagerByName(string managerName)
        {
            if (ManagerNameExists(managerName))
            {
                Manager entity = ManagerRepository.GetManagerByName(managerName);
                var model = new ManagerAccount
                {
                    ManagerId = entity.ManagerId,
                    ManagerName = entity.ManagerName,
                    ManagerEmail = entity.ManagerEmail,
                    MobilePhone = entity.MobilePhone,
                    IdCardNo = entity.IdCardNo,
                    ChannelId = entity.ChannelId,
                    ChannelName = entity.Channel.ChannelName,
                    CityId = entity.CId,
                    City = entity.City.Name,
                    ProvinceId = entity.City.PId,
                    CreatedTime = entity.CreatedTime,
                    ModifiedTime = entity.ModifiedTime,
                    IsActive = entity.IsActive
                };

                return model;
            }

            return new ManagerAccount();
        }

        /// <summary>
        /// 根据用户ID获取负责人
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ManagerAccount GetManagerById(Int64 managerId)
        {
            if (ManagerIdExists(managerId))
            {
                Manager entity = ManagerRepository.GetManagerById(managerId);
                var model = new ManagerAccount
                {
                    ManagerId = entity.ManagerId,
                    ManagerName = entity.ManagerName,
                    ManagerEmail = entity.ManagerEmail,
                    MobilePhone = entity.MobilePhone,
                    IdCardNo = entity.IdCardNo,
                    ChannelId = entity.ChannelId,
                    ChannelName = entity.Channel.ChannelName,
                    ChannelType = entity.Channel.ChannelType,
                    ChannelTypeDesc = EnumHelper.GetDescription((ChannelType)(entity.Channel.ChannelType)),
                    ChannelRank = entity.Channel.Rank,
                    ChannelRankDesc = EnumHelper.GetDescription((ChannelRank)(entity.Channel.Rank)),
                    CityId = entity.CId,
                    City = entity.City.Name,
                    ProvinceId = entity.City.PId,
                    Province = RegionProvinceCityRepository.GetProvinceById(entity.City.PId).Name,
                    CreatedTime = entity.CreatedTime,
                    ModifiedTime = entity.ModifiedTime,
                    IsActive = entity.IsActive,
                    IsActiveDesc = EnumHelper.GetDescription((Status)(entity.IsActive))
                };

                return model;
            }

            return new ManagerAccount();
        }

        /// <summary>
        /// 添加负责人
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool AddManager(ManagerAccount model, ref ValidationErrors errors)
        {
            try
            {
                Manager entity = ManagerRepository.GetManagerById(model.ManagerId);
                int channelType = Convert.ToInt32(Enum.Parse(typeof(ChannelType), model.ChannelTypeStr));
                if (entity != null)
                {
                    errors.Add(Suggestion.ExistUser);
                    return false;
                }
                if (channelType != 4)
                {
                    entity = new Manager
                    {
                        ManagerId = model.ManagerId,
                        ManagerName = model.ManagerName,
                        ManagerEmail = model.ManagerEmail,
                        MobilePhone = model.MobilePhone,
                        IdCardNo = model.IdCardNo,
                        ChannelType = Convert.ToInt32(Enum.Parse(typeof(ChannelType), model.ChannelTypeStr)),
                        ChannelId = model.ChannelId,
                        ShareChannelId = 1,
                        CId = model.CityId,
                        CreatedTime = DateTime.Now,
                        ModifiedTime = null,
                        IsActive = Convert.ToInt32(Enum.Parse(typeof(Status), model.IsActiveStr))
                    };

                    if (ManagerRepository.AddManager(entity) == 1)
                    {
                        return true;
                    }
                    errors.Add(Suggestion.AddNotShareUserFailed);
                    return false;
                }
                else
                {
                    var shareChannel = new ShareChannel
                    {
                        ShareChannelId = model.ManagerId,
                        ChannelName = model.ShareChannelName + "-" + model.ManagerId,
                        Rank = 1,
                        ChannelType = Convert.ToInt32(Enum.Parse(typeof(ChannelType), model.ChannelTypeStr)),
                        ParentId = 1,
                        Sort = 1,
                        CreatedTime = DateTime.Now,
                        ModifiedTime = null,
                        IsActive = 1
                    };

                    //var flag = ShareChannelRepository.AddShareChannel(shareChannel);
                    //var flag = ShareChannelRepository.AddShareChannelByfk("ShareChannel", shareChannel);

                    //if (flag > 0)
                    //{
                    entity = new Manager
                    {
                        ManagerId = model.ManagerId,
                        ManagerName = model.ManagerName,
                        ManagerEmail = model.ManagerEmail,
                        MobilePhone = model.MobilePhone,
                        IdCardNo = model.IdCardNo,
                        ChannelType = Convert.ToInt32(Enum.Parse(typeof(ChannelType), model.ChannelTypeStr)),
                        ChannelId = 1,
                        ShareChannel = shareChannel,
                        CId = model.CityId,
                        CreatedTime = DateTime.Now,
                        ModifiedTime = null,
                        IsActive = Convert.ToInt32(Enum.Parse(typeof(Status), model.IsActiveStr))
                    };

                    var flag = ManagerRepository.AddManager(entity);
                    //var flag = ManagerRepository.AddManagerByfk("Managers", entity);

                    if (flag > 0)
                    {
                        return true;
                    }
                    errors.Add(Suggestion.AddShareUserFailed);
                    return false;
                    //}
                    //errors.Add(Suggestion.AddShareUserChannelFailed);
                    //return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 修改负责人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditManager(ManagerAccount model)
        {
            try
            {
                Manager entity = ManagerRepository.GetManagerById(model.ManagerId);
                if (entity == null)
                {
                    return false;
                }
                entity.ManagerName = model.ManagerName;
                entity.ManagerEmail = model.ManagerEmail;
                entity.MobilePhone = model.MobilePhone;
                entity.IdCardNo = model.IdCardNo;
                entity.ChannelId = model.ChannelId;
                entity.CId = model.CityId;
                entity.ModifiedTime = DateTime.Now;
                entity.IsActive = Convert.ToInt32(Enum.Parse(typeof(Status), model.IsActiveStr));

                if (ManagerRepository.EditManager(entity) == 1)
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
        /// 删除负责人
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public bool DeleteManager(Int64 managerId)
        {
            try
            {
                Manager entity = ManagerRepository.GetManagerById(managerId);
                if (ManagerRepository.DeleteManager(entity) == 1)
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
        /// <param name="managerId"></param>
        /// <returns></returns>
        public bool ChangeAccountStatus(Int64 managerId)
        {
            try
            {
                Manager entity = ManagerRepository.GetManagerById(managerId);
                if (entity == null)
                {
                    return false;
                }
                entity.ModifiedTime = DateTime.Now;
                entity.IsActive = entity.IsActive == 1 ? 2 : 1;

                if (ManagerRepository.EditManager(entity) == 1)
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

        ///// <summary>
        ///// 重置密码
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public bool ResetPassword(ManagerAccount model)
        //{
        //    try
        //    {
        //        Manager entity = ManagerRepository.GetManagerById(model.ManagerId);
        //        if (entity == null)
        //        {
        //            return false;
        //        }
        //        // 加密
        //        entity.Password = (model.ResetPwdSec).Encrypt();
        //        entity.ModifiedTime = DateTime.Now;

        //        if (ManagerRepository.EditManager(entity) == 1)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 修改密码
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public bool ModifyPassword(ManagerAccount model)
        //{
        //    try
        //    {
        //        Manager entity = ManagerRepository.GetManagerById(model.ManagerId);
        //        if (entity == null)
        //        {
        //            return false;
        //        }
        //        var oldPwd = entity.Password;
        //        var inputPwd = (model.OldPwd).Encrypt();
        //        if (!inputPwd.Equals(oldPwd))
        //        {
        //            return false;
        //        }
        //        // 加密
        //        entity.Password = (model.ResetPwdSec).Encrypt();
        //        entity.ModifiedTime = DateTime.Now;

        //        if (ManagerRepository.EditManager(entity) == 1)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 登录
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //public Manager Login(string username, string password)
        //{
        //    //return ManagerRepository.ManagerLogin(username, password.Encrypt());

        //    return ManagerRepository.ManagerLogin(username, password);
        //}

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public Manager Login(Int64 managerId)
        {
            return ManagerRepository.ManagerLogin(managerId);
        }

        /// <summary>
        /// 获取操作权限
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public List<GetPermissionCode> GetPermission(long managerId, string controller)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 转换成View层展现的数据列表
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        private List<ManagerAccount> CreateModelList(ref GridPager pager, ref IQueryable<Manager> queryData)
        {
            pager.TotalRows = queryData.Count();
            if (pager.TotalRows > 0)
            {
                queryData = pager.Page <= 1
                    ? queryData.Take(pager.Rows)
                    : queryData.Skip((pager.Page - 1) * pager.Rows).Take(pager.Rows);
            }

            List<ManagerAccount> modelList = queryData.Select(d =>
                new ManagerAccount
                {
                    ManagerId = d.ManagerId,
                    ManagerName = d.ManagerName,
                    ManagerEmail = d.ManagerEmail,
                    MobilePhone = d.MobilePhone,
                    IdCardNo = d.IdCardNo,
                    ChannelId = d.ChannelId,
                    ChannelType = d.Channel.ChannelType,
                    ChannelName = d.Channel.ChannelName,
                    ChannelRank = d.Channel.Rank,
                    ParentId = d.Channel.ParentId,
                    CityId = d.CId,
                    City = d.City.Name,
                    ProvinceId = d.City.PId,
                    CreatedTime = d.CreatedTime,
                    ModifiedTime = d.ModifiedTime,
                    IsActive = d.IsActive
                }).ToList();

            return modelList;
        }

        public List<GetStatistics> GetDataStatisticsByDay(Int64 managerId)
        {
            return ManagerStatisticsRepository.GetDataStatisticsByDay(managerId);
        }

        public List<GetStatistics> GetDataStatisticsByWeek(Int64 managerId)
        {
            return ManagerStatisticsRepository.GetDataStatisticsByWeek(managerId);
        }

        public List<GetStatistics> GetDataStatisticsByMonth(Int64 managerId)
        {
            return ManagerStatisticsRepository.GetDataStatisticsByMonth(managerId);
        }
    }
}
