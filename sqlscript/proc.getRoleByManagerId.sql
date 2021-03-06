USE [NationalUnion]
GO
/****** Object:  StoredProcedure [dbo].[getRoleByManagerId]    Script Date: 8/20/2014 8:06:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[getRoleByManagerId]
@ManagerId bigint
AS
begin
----读取用户所包含的角色
select a.*, ISNULL(b.ManagerId,0) as flag from ChannelRole a
	left join ChannelRoleManager b
	on a.RoleId=b.RoleId
	and b.ManagerId=@ManagerId
	order by b.ManagerId desc
end