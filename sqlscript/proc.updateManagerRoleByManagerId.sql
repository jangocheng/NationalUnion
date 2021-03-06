USE [NationalUnion]
GO
/****** Object:  StoredProcedure [dbo].[updateManagerRoleByManagerId]    Script Date: 8/20/2014 8:07:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[updateManagerRoleByManagerId]
@ManagerId bigint, @RoleId nvarchar(50)
AS
----更新角色用户中间表前删除关联
begin
	delete from ChannelRoleManager where ManagerId=@ManagerId
end
----更新角色用户中间表
begin
	insert into ChannelRoleManager (RoleId, ManagerId) values (@RoleId, @ManagerId)
end