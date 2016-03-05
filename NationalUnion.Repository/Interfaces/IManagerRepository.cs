using System;
using System.Collections.Generic;
using System.Linq;
using NationalUnion.Domain.Mapping;
using NationalUnion.Infrastructure;

namespace NationalUnion.Repository.Interfaces
{
    public interface IManagerRepository : IRepository<Manager>
    {
        #region Methods

        /// <summary>
        /// 判断指定用户名是否存在
        /// </summary>
        /// <param name="managerName"></param>
        /// <returns></returns>
        bool ManagerNameExists(string managerName);

        /// <summary>
        /// 判断指定用户ID是否存在
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        bool ManagerIdExists(Int64 managerId);

        /// <summary>
        /// 根据指定用户名，获取用户实体
        /// </summary>
        /// <param name="managerName"></param>
        /// <returns></returns>
        Manager GetManagerByName(string managerName);

        /// <summary>
        /// 根据指定用户ID，获取用户实体
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        Manager GetManagerById(Int64 managerId);

        /// <summary>
        /// 获取用户实体列表
        /// </summary>
        IQueryable<Manager> GetManagerList();

        /// <summary>
        /// 添加用户实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddManager(Manager entity);

        /// <summary>
        /// 添加用户实体
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddManagerByfk(string modelName, Manager entity);

        /// <summary>
        /// 修改用户实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int EditManager(Manager entity);

        /// <summary>
        /// 根据用户ID，删除用户实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int DeleteManager(Manager entity);

        ///// <summary>
        ///// 登录
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //Manager ManagerLogin(string username, string password);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        Manager ManagerLogin(Int64 managerId);

        #endregion
    }
}
