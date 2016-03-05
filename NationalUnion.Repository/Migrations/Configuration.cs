namespace NationalUnion.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using NationalUnion.Domain.Mapping;
    using NationalUnion.Repository;

    internal sealed class Configuration : DbMigrationsConfiguration<NationalUnionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NationalUnionContext context)
        {
            var managers = new List<Manager>
            {
                new Manager
                {
                    ManagerId = 55518732429,
                    ChannelType = 1,
                    ChannelId = 66,
                    ShareChannelId = 1,
                    ManagerName = "姜以翔",
                    ManagerEmail = "jiangerwei2008@163.com",
                    CId = 340,
                    ManagerInfo = "",
                    CreatedTime = DateTime.Now,
                    IsActive = 1
                }
            };
            managers.ForEach(m => context.Managers.AddOrUpdate(p => p.ManagerId, m));
            context.SaveChanges();

            var managerDetails = new List<ManagerDetail>
            {
                new ManagerDetail
                {
                    ManagerId = 55518732429,
                    CurrentChannelId = 66,
                    OldChannelId = 1,
                    CurrentRank = 2,
                    OldRank = 1,
                    RankStatus = 4,
                    CreatedTime = DateTime.Now
                }
            };
            managerDetails.ForEach(md => context.ManagerDetails.AddOrUpdate(p => p.ManagerId, md));
            context.SaveChanges();

            var channelMenus = new List<ChannelMoudle>
            {
                new ChannelMoudle
                {
                    MoudleId = "0",
                    Name = "全民联盟后台",
                    EnglishName = "NationalUnion",
                    ParentId = "0",
                    Url = "",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 0,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "PersonalCenter",
                    Name = "个人中心",
                    EnglishName = "PersonalCenter",
                    ParentId = "0",
                    Url = "",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 0,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "PersonalInfo",
                    Name = "我的主页",
                    EnglishName = "PersonalInfo",
                    ParentId = "PersonalCenter",
                    Url = "Home/AccountInfo",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                
                new ChannelMoudle
                {
                    MoudleId = "PromotionManage",
                    Name = "推广管理",
                    EnglishName = "PromotionManage",
                    ParentId = "0",
                    Url = "",
                    Iconic = "",
                    Sort = 1,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 0,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "HotProductPromotion",
                    Name = "热销单品",
                    EnglishName = "HotProductPromotion",
                    ParentId = "PromotionManage",
                    Url = "",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "ActivityPromotion",
                    Name = "活动专题",
                    EnglishName = "ActivityPromotion",
                    ParentId = "PromotionManage",
                    Url = "",
                    Iconic = "",
                    Sort = 1,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "AccountManage",
                    Name = "会员管理",
                    EnglishName = "AccountManage",
                    ParentId = "0",
                    Url = "",
                    Iconic = "",
                    Sort = 2,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 0,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "AccountInfo",
                    Name = "会员管理",
                    EnglishName = "AccountInfo",
                    ParentId = "AccountManage",
                    Url = "ManagerAccount/ManagerList",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "AccountDetail",
                    Name = "会员明细",
                    EnglishName = "AccountDetail",
                    ParentId = "AccountManage",
                    Url = "ManagerDetail/ManagerDetailList",
                    Iconic = "",
                    Sort = 1,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "ChannelManage",
                    Name = "渠道管理",
                    EnglishName = "ChannelManage",
                    ParentId = "0",
                    Url = "",
                    Iconic = "",
                    Sort = 3,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 0,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "ChannelInfo",
                    Name = "渠道管理",
                    EnglishName = "ChannelInfo",
                    ParentId = "ChannelManage",
                    Url = "ChannelManage/ChannelList",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "DataStatistics",
                    Name = "数据统计",
                    EnglishName = "DataStatistics",
                    ParentId = "0",
                    Url = "",
                    Iconic = "",
                    Sort = 4,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 0,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "ShareStatistics",
                    Name = "分享宝数据统计",
                    EnglishName = "ShareStatistics",
                    ParentId = "DataStatistics",
                    Url = "ShareStatistics/StatisticsList",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "ShareDetail",
                    Name = "分享宝分享明细",
                    EnglishName = "ShareDetail",
                    ParentId = "DataStatistics",
                    Url = "ShareDetail/ShareDetailList",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "EvaluationManage",
                    Name = "考核管理",
                    EnglishName = "EvaluationManage",
                    ParentId = "0",
                    Url = "",
                    Iconic = "",
                    Sort = 5,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 0,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "KPIConfig",
                    Name = "考核指标设置",
                    EnglishName = "KPIConfig",
                    ParentId = "EvaluationManage",
                    Url = "",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "KPIScore",
                    Name = "考核指标得分",
                    EnglishName = "KPIScore",
                    ParentId = "EvaluationManage",
                    Url = "",
                    Iconic = "",
                    Sort = 1,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "PerformanceManage",
                    Name = "业绩管理",
                    EnglishName = "PerformanceManage",
                    ParentId = "0",
                    Url = "",
                    Iconic = "",
                    Sort = 6,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 0,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "PersonalPerformance",
                    Name = "我的业绩",
                    EnglishName = "PersonalPerformance",
                    ParentId = "PerformanceManage",
                    Url = "",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "TeamPerformance",
                    Name = "团队业绩",
                    EnglishName = "TeamPerformance",
                    ParentId = "PerformanceManage",
                    Url = "",
                    Iconic = "",
                    Sort = 1,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "AssistPerformance",
                    Name = "辅助业绩",
                    EnglishName = "AssistPerformance",
                    ParentId = "PerformanceManage",
                    Url = "",
                    Iconic = "",
                    Sort = 2,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "RevenueManage",
                    Name = "收益管理",
                    EnglishName = "RevenueManage",
                    ParentId = "0",
                    Url = "",
                    Iconic = "",
                    Sort = 7,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 0,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "PromotionRevenue",
                    Name = "推广收益",
                    EnglishName = "PromotionRevenue",
                    ParentId = "RevenueManage",
                    Url = "",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "TotalRevenue",
                    Name = "我的收益",
                    EnglishName = "TotalRevenue",
                    ParentId = "RevenueManage",
                    Url = "",
                    Iconic = "",
                    Sort = 1,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "PermissonManage",
                    Name = "权限管理",
                    EnglishName = "PermissonManage",
                    ParentId = "0",
                    Url = "",
                    Iconic = "",
                    Sort = 8,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 0,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "MoudleManage",
                    Name = "模块管理",
                    EnglishName = "MoudleManage",
                    ParentId = "PermissonManage",
                    Url = "ChannelMoudle/MoudleList",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "RoleManage",
                    Name = "角色管理",
                    EnglishName = "RoleManage",
                    ParentId = "PermissonManage",
                    Url = "ChannelRole/RoleList",
                    Iconic = "",
                    Sort = 1,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "PermissionInfo",
                    Name = "授权管理",
                    EnglishName = "PermissionInfo",
                    ParentId = "PermissonManage",
                    Url = "ChannelPermission/GetList",
                    Iconic = "",
                    Sort = 2,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "SystemManage",
                    Name = "系统管理",
                    EnglishName = "SystemManage",
                    ParentId = "0",
                    Url = "",
                    Iconic = "",
                    Sort = 9,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 0,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "SystemLog",
                    Name = "系统日志",
                    EnglishName = "SystemLog",
                    ParentId = "SystemManage",
                    Url = "",
                    Iconic = "",
                    Sort = 0,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                },
                new ChannelMoudle
                {
                    MoudleId = "SystemException",
                    Name = "异常日志",
                    EnglishName = "SystemException",
                    ParentId = "SystemManage",
                    Url = "",
                    Iconic = "",
                    Sort = 1,
                    Remark = "",
                    MoudleState = 1,
                    CreatePerson = "Administrator",
                    CreateTime = DateTime.Now,
                    IsLast = 1,
                    Version = null
                }
            };
            channelMenus.ForEach(cm => context.ChannelMoudles.AddOrUpdate(p => p.MoudleId, cm));
            context.SaveChanges();

            var channelMenuOperates = new List<ChannelMoudleOperate>
            {
                new ChannelMoudleOperate
                {
                    MoudleOperateId = "ChannelInfoCreate",
                    Name = "创建",
                    KeyCode = "Create",
                    MoudleId = "ChannelInfo",
                    IsValid = 1,
                    Sort = 0
                },
                new ChannelMoudleOperate
                {
                    MoudleOperateId = "ChannelInfoEdit",
                    Name = "修改",
                    KeyCode = "Edit",
                    MoudleId = "ChannelInfo",
                    IsValid = 1,
                    Sort = 0
                },
                new ChannelMoudleOperate
                {
                    MoudleOperateId = "ChannelInfoDetail",
                    Name = "详细",
                    KeyCode = "Detail",
                    MoudleId = "ChannelInfo",
                    IsValid = 1,
                    Sort = 0
                },
                new ChannelMoudleOperate
                {
                    MoudleOperateId = "ChannelInfoDelete",
                    Name = "删除",
                    KeyCode = "Delete",
                    MoudleId = "ChannelInfo",
                    IsValid = 1,
                    Sort = 0
                },
                new ChannelMoudleOperate
                {
                    MoudleOperateId = "ChannelInfoQuery",
                    Name = "查询",
                    KeyCode = "Query",
                    MoudleId = "ChannelInfo",
                    IsValid = 1,
                    Sort = 0
                },
                new ChannelMoudleOperate
                {
                    MoudleOperateId = "ChannelInfoActive",
                    Name = "激活停用",
                    KeyCode = "Active",
                    MoudleId = "ChannelInfo",
                    IsValid = 1,
                    Sort = 0
                },
                new ChannelMoudleOperate
                {
                    MoudleOperateId = "ChannelInfoSave",
                    Name = "保存",
                    KeyCode = "Save",
                    MoudleId = "ChannelInfo",
                    IsValid = 1,
                    Sort = 0
                }
            };
            channelMenuOperates.ForEach(cmo => context.ChannelMoudleOperates.AddOrUpdate(p => p.MoudleOperateId, cmo));
            context.SaveChanges();

            var channelRoles = new List<ChannelRole>
            {
                new ChannelRole
                {
                    RoleId = "administrator",
                    Name = "超级管理员",
                    Description = "全部授权",
                    CreateTime = DateTime.Now,
                    CreatePerson = "Administrator"
                }
            };
            channelRoles.ForEach(cr => context.ChannelRoles.AddOrUpdate(p => p.RoleId, cr));
            context.SaveChanges();

            var channelRights = new List<ChannelRight>
            {
                new ChannelRight
                {
                    RightId = "administratorChannelManage",
                    MoudleId = "ChannelManage",
                    RoleId = "administrator",
                    RightFlag = 1
                },
                new ChannelRight
                {
                    RightId = "administratorChannelInfo",
                    MoudleId = "ChannelInfo",
                    RoleId = "administrator",
                    RightFlag = 1
                }
            };
            channelRights.ForEach(cr => context.ChannelRights.AddOrUpdate(p => p.RightId, cr));
            context.SaveChanges();

            var channelRightOperates = new List<ChannelRightOperate>
            {
                new ChannelRightOperate
                {
                    RightOperateId = "administratorChannelInfoCreate",
                    RightId = "administratorChannelInfo",
                    KeyCode = "Create",
                    IsValid = 1
                },
                new ChannelRightOperate
                {
                    RightOperateId = "administratorChannelInfoEdit",
                    RightId = "administratorChannelInfo",
                    KeyCode = "Edit",
                    IsValid = 1
                },
                new ChannelRightOperate
                {
                    RightOperateId = "administratorChannelInfoDetail",
                    RightId = "administratorChannelInfo",
                    KeyCode = "Detail",
                    IsValid = 1
                },
                new ChannelRightOperate
                {
                    RightOperateId = "administratorChannelInfoDelete",
                    RightId = "administratorChannelInfo",
                    KeyCode = "Delete",
                    IsValid = 1
                },
                new ChannelRightOperate
                {
                    RightOperateId = "administratorChannelInfoQuery",
                    RightId = "administratorChannelInfo",
                    KeyCode = "Query",
                    IsValid = 1
                },
                new ChannelRightOperate
                {
                    RightOperateId = "administratorChannelInfoActive",
                    RightId = "administratorChannelInfo",
                    KeyCode = "Active",
                    IsValid = 1
                },
                new ChannelRightOperate
                {
                    RightOperateId = "administratorChannelInfoSave",
                    RightId = "administratorChannelInfo",
                    KeyCode = "Save",
                    IsValid = 1
                }
            };
            channelRightOperates.ForEach(cro => context.ChannelRightOperates.AddOrUpdate(p => p.RightOperateId, cro));
            context.SaveChanges();

            var channelRoleManagers = new List<ChannelRoleManager>
            {
                new ChannelRoleManager
                {
                    ManagerId = 55518732429,
                    RoleId = "administrator"
                }
            };
            channelRoleManagers.ForEach(crm => context.ChannelRoleManagers.AddOrUpdate(p => p.ManagerId, crm));
            context.SaveChanges();

        }
    }
}
