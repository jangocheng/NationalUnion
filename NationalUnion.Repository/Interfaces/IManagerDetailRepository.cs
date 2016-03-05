using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Infrastructure;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Repository.Interfaces
{
    public interface IManagerDetailRepository : IRepository<ManagerDetail>
    {
        #region Methods

        /// <summary>
        /// 判断指定历史明细是否存在
        /// </summary>
        /// <param name="managerDetailId"></param>
        /// <returns></returns>
        bool ManagerDetailIdExists(int managerDetailId);

        /// <summary>
        /// 根据指定历史明细ID，获取历史明细
        /// </summary>
        /// <param name="managerDetailId"></param>
        /// <returns></returns>
        ManagerDetail GetManagerDetailById(int managerDetailId);

        /// <summary>
        /// 获取历史明细列表
        /// </summary>
        /// <returns></returns>
        IQueryable<ManagerDetail> GetManagerDetailList();

        /// <summary>
        /// 添加历史明细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddManagerDetail(ManagerDetail entity);

        /// <summary>
        /// 修改历史明细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int EditManagerDetail(ManagerDetail entity);

        /// <summary>
        /// 根据历史明细ID，删除历史明细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int DeleteManagerDetail(ManagerDetail entity);

        #endregion
    }
}
