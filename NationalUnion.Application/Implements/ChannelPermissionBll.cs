using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Interfaces;
using NationalUnion.Application.ViewModels;
using NationalUnion.Domain.Mapping;
using NationalUnion.Domain.NotMapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Application.Implements
{
    public sealed class ChannelPermissionBll : IChannelPermissionBll, IDisposable
    {
        [Dependency]
        public IChannelPermissionRepository ChannelPermissionRepository { get; set; }

        private bool _isReadyDisposed = false;

        public ChannelPermissionBll()
        {
            // 析构函数调用时不释放托管资源，因为交由GC进行释放
            Dispose(false);
        }

        /// <summary>
        /// 获取操作权限
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public List<GetPermissionCode> GetPermission(Int64 managerId, string controller)
        {
            return ChannelPermissionRepository.GetPermission(managerId, controller);
        }

        /// <summary>
        /// 根据角色和模块，获取权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moudleId"></param>
        /// <returns></returns>
        public List<GetPermissionByRoleAndMoudle> GetPermissionByRoleAndMoudle(string roleId, string moudleId)
        {
            return ChannelPermissionRepository.GetPermissionByRoleAndMoudle(roleId, moudleId);
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdatePermission(ChannelRightOperate entity)
        {
            return ChannelPermissionRepository.UpdatePermission(entity);
        }

        #region 垃圾回收
        public void Dispose()
        {
            // 强制回收
            //GC.Collect();

            // 用户手动释放托管资源和非托管资源
            Dispose(true);
            // 用户已经释放了托管和非托管资源，所以不需要再调用析构函数
            GC.SuppressFinalize(this);

            // 如果子类继承此类时，需要按照如下写法进行
            //try
            //{
            //    Dispose(true);
            //}
            //finally
            //{
            //    base.Dispose();
            //}
        }

        private void Dispose(bool isDispose)
        {
            // isReadyDisposed是控制只有第一次调用Dispose才有效才需要释放托管和非托管资源
            if (_isReadyDisposed)
                return;
            if (isDispose)
            {
                // 析构函数调用时不释放托管资源，因为交由GC进行释放
                // 如果析构函数释放托管资源可能之前GC释放过，就会导致出现异常
                // 托管资源释放
            }
            //非托管资源释放
            _isReadyDisposed = true;
        }
        #endregion
    }
}
