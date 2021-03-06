USE [NationalUnion]
GO
/****** Object:  StoredProcedure [dbo].[clearUnusedChannelRightOperate]    Script Date: 8/20/2014 8:05:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[clearUnusedChannelRightOperate]
AS
----清除无用的权限操作码[每次删除角色、模块、权限时都有可能产生]
	delete from [dbo].[ChannelRightOperate] where RightOperateId not in(
		select a.RightId+b.KeyCode from ChannelRight a, ChannelMoudleOperate b
			where a.MoudleId=b.MoudleId)