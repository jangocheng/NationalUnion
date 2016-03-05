using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Application.Commons;
using NationalUnion.Application.ViewModels;

namespace NationalUnion.Application.Interfaces
{
    /// <summary>
    /// 账户管理模块账户历史明细接口
    /// </summary>
    public interface IManagerDetailBll
    {
        /// <summary>
        /// 判断历史明细ID是否存在
        /// </summary>
        /// <param name="managerDetailId"></param>
        /// <returns></returns>
        bool ManagerDetailIdExists(int managerDetailId);

        /// <summary>
        /// 根据历史明细ID获取历史明细
        /// </summary>
        /// <param name="managerDetailId"></param>
        /// <returns></returns>
        ManagerDetailInfo GetManagerDetailById(int managerDetailId);

        /// <summary>
        /// 获取历史明细列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<ManagerDetailInfo> GetManagerDetailList(ref GridPager pager);

        /// <summary>
        /// 根据查询条件获取历史明细列表
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<ManagerDetailInfo> GetManagerDetailListByQuery(string queryStr, ref GridPager pager);

        /// <summary>
        /// 添加历史明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddManagerDetail(ManagerDetailInfo model);

        /// <summary>
        /// 修改历史明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditManagerDetail(ManagerDetailInfo model);

        /// <summary>
        /// 删除历史明细
        /// </summary>
        /// <param name="managerDetailId"></param>
        /// <returns></returns>
        bool DeleteManagerDetail(int managerDetailId);
    }
}
