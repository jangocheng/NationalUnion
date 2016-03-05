USE [NationalUnion]
GO
/****** Object:  StoredProcedure [dbo].[assignRightInsertChannelRight]    Script Date: 8/20/2014 8:04:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[assignRightInsertChannelRight]
AS
----分配功能模块到角色组
	insert into [dbo].[ChannelRight] (RightId,MoudleId,RoleId,RightFlag)
		select distinct b.RoleId+a.MoudleId, a.MoudleId, b.RoleId, 0 from ChannelMoudle a,ChannelRole b
			where b.RoleId+a.MoudleId not in(select RightId from ChannelRight)