using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NationalUnion.Application.Implements;
using NationalUnion.Application.Interfaces;
using NationalUnion.Repository.Interfaces;
using NationalUnion.Repository.Repositories;

namespace NationalUnion.Application
{
    /// <summary>
    /// 依赖注入代码注册
    /// </summary>
    /// Created by jiangew 2014.06.10
    public static class UnityDependencyRegister
    {
        public static void ContainerRegister(ref UnityContainer container)
        {
            container.RegisterType<IManagerAccountBll, ManagerAccountBll>();
            container.RegisterType<IManagerRepository, ManagerRepository>();

            container.RegisterType<IChannelMenuBll, ChannelMenuBll>();
            container.RegisterType<IChannelMenuRepository, ChannelMenuRepository>();

            container.RegisterType<IChannelManageBll, ChannelManageBll>();
            container.RegisterType<IChannelManageRepository, ChannelManageRepository>();

            container.RegisterType<IShareChannelBll, ShareChannelBll>();
            container.RegisterType<IShareChannelRepository, ShareChannelRepository>();

            container.RegisterType<IManagerDetailBll, ManagerDetailBll>();
            container.RegisterType<IManagerDetailRepository, ManagerDetailRepository>();

            container.RegisterType<IChannelPermissionBll, ChannelPermissionBll>();
            container.RegisterType<IChannelPermissionRepository, ChannelPermissionRepository>();

            container.RegisterType<IChannelMoudleBll, ChannelMoudleBll>();
            container.RegisterType<IChannelMoudleRepository, ChannelMoudleRepository>();

            container.RegisterType<IChannelMoudleOperateBll, ChannelMoudleOperateBll>();
            container.RegisterType<IChannelMoudleOperateRepository, ChannelMoudleOperateRepository>();

            container.RegisterType<IChannelRoleBll, ChannelRoleBll>();
            container.RegisterType<IChannelRoleRepository, ChannelRoleRepository>();

            container.RegisterType<IRegionProvinceCityRepository, RegionProvinceCityRepository>();

            container.RegisterType<IManagerStatisticsRepository, ManagerStatisticsRepository>();

            container.RegisterType<IShareStatisticsBll, ShareStatisticsBll>();
            container.RegisterType<IShareStatisticsRepository, ShareStatisticsRepository>();

            container.RegisterType<IShareDetailBll, ShareDetailBll>();
            container.RegisterType<IShareDetailRepository, ShareDetailRepository>();
        }
    }
}
