USE [NationalUnion]
GO
/****** Object:  StoredProcedure [dbo].[updateChannelRightFlag]    Script Date: 8/20/2014 8:07:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[updateChannelRightFlag]
@MoudleId nvarchar(50), @RoleId nvarchar(50)
AS
begin
----计算上级模块的RightFlag标识
declare @Count int
----根据操作权限项计算模块权限
select @Count=COUNT(*) from ChannelRightOperate where RightId=@RoleId+@MoudleId and IsValid=1

if(@Count>0)
begin
	update ChannelRight set RightFlag=1 where MoudleId=@MoudleId and RoleId=@RoleId
end
else
begin
	update ChannelRight set RightFlag=0 where MoudleId=@MoudleId and RoleId=@RoleId
end

declare @ParentId nvarchar(50)
set @ParentId=@MoudleId

while(@ParentId<>'0')
begin
	select @ParentId=ParentId from ChannelMoudle where MoudleId=@ParentId
	if(@ParentId is null)
	begin
		return
	end

	select @Count=COUNT(*) from ChannelRight
		where MoudleId in(select MoudleId from ChannelMoudle where ParentId=@ParentId)
		and RoleId=@RoleId
		and RightFlag=1

	if(@count>0)
	begin
		update ChannelRight set RightFlag=1 where MoudleId=@MoudleId and RoleId=@RoleId
	end
	else
	begin
		update ChannelRight set RightFlag=0 where MoudleId=@MoudleId and RoleId=@RoleId
	end
end
end