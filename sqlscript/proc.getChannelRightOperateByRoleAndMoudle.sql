USE [NationalUnion]
GO
/****** Object:  StoredProcedure [dbo].[getChannelRightOperateByRoleAndMoudle]    Script Date: 8/20/2014 8:06:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[getChannelRightOperateByRoleAndMoudle]
@RoleId nvarchar(50), @MoudleId nvarchar(50)
AS
----根据选择的角色及模块加载模块的权限项
begin
select a.MoudleOperateId, a.Name, a.MoudleId, a.KeyCode, ISNULL(b.IsValid,0) as IsValid, a.Sort, @RoleId+@MoudleId as RightId
	from ChannelMoudleOperate a
	left join(select c.MoudleOperateId, a.IsValid from ChannelRightOperate a, ChannelRight b, ChannelMoudleOperate c
				where b.RightId in(
					select RightId from ChannelRight where RoleId=@RoleId and MoudleId=@MoudleId)
				and a.RightId=b.RightId
				and b.MoudleId=c.MoudleId
				and a.KeyCode=c.KeyCode) b
	on a.MoudleOperateId=b.MoudleOperateId
	where a.MoudleId=@MoudleId
end