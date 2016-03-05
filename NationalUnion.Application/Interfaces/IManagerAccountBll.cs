using System;
using System.Collections.Generic;
using NationalUnion.Application.Commons;
using NationalUnion.Application.ViewModels;
using NationalUnion.Domain.Mapping;
using NationalUnion.Domain.NotMapping;

namespace NationalUnion.Application.Interfaces
{
    /// <summary>
    /// 渠道负责人账户管理模块接口
    /// </summary>
    public interface IManagerAccountBll
    {
        /// <summary>
        /// 获取负责人列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<ManagerAccount> GetManagerList(ref GridPager pager);

        /// <summary>
        /// 根据查询条件获取负责人列表
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<ManagerAccount> GetManagerListByQuery(string queryStr, ref GridPager pager); 

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="managerName"></param>
        /// <returns></returns>
        bool ManagerNameExists(string managerName);

        /// <summary>
        /// 判断用户ID是否存在
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        bool ManagerIdExists(Int64 managerId);

        /// <summary>
        /// 根据名称获取负责人
        /// </summary>
        /// <param name="managerName"></param>
        /// <returns></returns>
        ManagerAccount GetManagerByName(string managerName);

        /// <summary>
        /// 根据ID获取负责人
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        ManagerAccount GetManagerById(Int64 managerId);

        /// <summary>
        /// 添加负责人
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        bool AddManager(ManagerAccount model, ref ValidationErrors errors);

        /// <summary>
        /// 修改负责人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditManager(ManagerAccount model);

        /// <summary>
        /// 删除负责人
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        bool DeleteManager(Int64 managerId);

        /// <summary>
        /// 激活或停用
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        bool ChangeAccountStatus(Int64 managerId);

        ///// <summary>
        ///// 重置密码
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //bool ResetPassword(ManagerAccount model);

        ///// <summary>
        ///// 修改密码
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //bool ModifyPassword(ManagerAccount model);

        ///// <summary>
        ///// 登录
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //Manager Login(string username, string password);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        Manager Login(Int64 managerId);

        /// <summary>
        /// 按天数据统计
        /// </summary>
        /// <returns></returns>
        List<GetStatistics> GetDataStatisticsByDay(Int64 managerId);

        /// <summary>
        /// 按周数据统计
        /// </summary>
        /// <returns></returns>
        List<GetStatistics> GetDataStatisticsByWeek(Int64 managerId);

        /// <summary>
        /// 按月数据统计
        /// </summary>
        /// <returns></returns>
        List<GetStatistics> GetDataStatisticsByMonth(Int64 managerId);
    }
}
