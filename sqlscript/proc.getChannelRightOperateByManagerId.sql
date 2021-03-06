USE [NationalUnion]
GO
/****** Object:  StoredProcedure [dbo].[getChannelRightOperateByManagerId]    Script Date: 8/20/2014 8:06:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[getChannelRightOperateByManagerId]
@ManagerId bigint, @Url nvarchar(200)
AS
----获取模块的当前用户的操作权限
select distinct KeyCode, IsValid from ChannelRightOperate where RightId in(
	select a.RightId from ChannelRight a, ChannelMoudle b where a.RoleId in(
		select RoleId from ChannelRoleManager where ManagerId=@ManagerId)
		and a.MoudleId=b.MoudleId
		and b.Url=@Url
	and IsValid=1)

