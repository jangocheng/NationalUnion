USE [NationalUnion]
GO
/****** Object:  StoredProcedure [dbo].[proc_DataSummary]    Script Date: 07/29/2014 20:42:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
----------------Create Date 2014-07-04 by xu
----------------此程序用于统计PV、UV、订单数量,商品总数量,订单金额，预计可算拥金，可结算拥金(妥投),妥投的订单数量,妥投的订单金额，分享量
----------------统计的维度有SharedUserID,PlatformID,ChannelID,SharedManagerID,CreatDate
----------------参数@UserID小于0的话，就是统计库中所数据，完成后对表做删除操作。否则就是统计指定的用户数据。
----------------PV,UV,订单量统计的限制条为SharedLevel为1的用户（即最后一级分享者）
-------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------
create proc [dbo].[proc_DataSummary]
(
@UserID bigint--用户ID,不指定则为全部用户
)
as
begin
	begin try
		begin tran
			declare @Level int,
					@CurrentDatetime datetime
			set @Level=1 --Level为1为最后一级分享者
			set @CurrentDatetime=GETDATE();
	    ----------------------------------------PV(只统计最后一级分享者)---------------------------------------------------------------------------------------------------------------------------------
			--PV
			merge into dbo.DataSummary as DS
			using (select SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,COUNT(*) as Qty,CONVERT(varchar(100), CreateDate, 111) as CreatDate from [NationalUnionHistory].dbo.PVInfo where SharedLevel = @Level and (@UserID<0 or SharedUserID = @UserID) group by SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,CONVERT(varchar(100), CreateDate, 111)) as P
			on (DS.UserID=P.SharedUserID and DS.PlatformID=P.PlatformID and DS.ChannelID=P.ChannelID and DS.ManagerID=P.SharedManagerID and DS.SharedClientID=P.SharedClientID and DS.SummaryDate=P.CreatDate)
			when matched then
				update set DS.PV=(DS.PV+P.Qty)--更新统计己存在的信息
			when not matched then--统计新的PV信息
				insert([UserID],[PlatformID],[SummaryDate],[ChannelID],[ManagerID],[PV],[UV],[OrderQuantity],[OrderAmount],[EstimateCommission],[AvaliableCommission],[SharedClientID],[ProductsQuantity],[AvaliableOrderAmount],[AvaliableOrderQuantity],[SharedQunatity],[CreateDate])
				values(P.SharedUserID,P.PlatformID,P.CreatDate,  P.ChannelID,P.SharedManagerID,P.Qty,0,0,0,0,0,SharedClientID,0,0,0,0,GETDATE());
		----------------------------------------UV(只统计最后一级分享者)---------------------------------------------------------------------------------------------------------------------------------
			--UV
			merge into dbo.DataSummary as DS
			using (select SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,COUNT(*) as Qty,CONVERT(varchar(100), CreateDate, 111) as CreatDate from [NationalUnionHistory].dbo.UVInfo where SharedLevel = @Level and (@UserID<0 or SharedUserID = @UserID) group by SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,CONVERT(varchar(100), CreateDate, 111)) as U
			on (DS.UserID=U.SharedUserID and DS.PlatformID=U.PlatformID and DS.ChannelID=U.ChannelID and DS.ManagerID=U.SharedManagerID and DS.SharedClientID=U.SharedClientID and DS.SummaryDate=U.CreatDate)
			when matched then
				update set DS.UV=(DS.UV+U.Qty)--更新统计己存在的信息
			when not matched then--统计新的PV信息
				insert([UserID],[PlatformID],[SummaryDate],[ChannelID],[ManagerID],[PV],[UV],[OrderQuantity],[OrderAmount],[EstimateCommission],[AvaliableCommission],[SharedClientID],[ProductsQuantity],[AvaliableOrderAmount],[AvaliableOrderQuantity],[SharedQunatity],[CreateDate])
				values(U.SharedUserID,U.PlatformID,U.CreatDate,U.ChannelID,U.SharedManagerID,0,U.Qty,0,0,0,0,SharedClientID,0,0,0,0,GETDATE());
		----------------------------------------订单数量,商品总数量,订单金额，预计可算拥金---------------------------------------------------------------------------------------------------------------------------------				
		    --订单数量,商品总数量,订单金额，预计可算拥金
			merge into dbo.DataSummary as DS
			using (
				  select SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,CreatDate,COUNT(*) as orderQty,sum(ProductsQuantity) as ProductsQuantity,SUM(TotalPrice) as TotalPrice,sum(TotalEstimateCommission) as TotalEstimateCommission from (
					  select SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,OrderId,sum(ProductNumber) as ProductsQuantity,SUM(ProductPrice) as TotalPrice,sum(Commission) as TotalEstimateCommission,CONVERT(varchar(100), RecordCreatedTime, 111) as CreatDate from [NationalUnionHistory].[dbo].OrderProductOccur where @UserID<0 or SharedUserID = @UserID group by SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,OrderId,CONVERT(varchar(100), RecordCreatedTime, 111)
				  ) as t group by SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,CreatDate
			) as O
			on (DS.UserID=O.SharedUserID and DS.PlatformID=O.PlatformID and DS.ChannelID=O.ChannelID and DS.ManagerID=O.SharedManagerID and DS.SharedClientID=O.SharedClientID and DS.SummaryDate=O.CreatDate)
			when matched then--预计可算拥金
				update set ds.OrderQuantity=(ds.OrderQuantity+O.orderQty),ds.ProductsQuantity=(ds.ProductsQuantity+O.ProductsQuantity),DS.[OrderAmount]=(DS.[OrderAmount]+TotalPrice),DS.[EstimateCommission]=(DS.[EstimateCommission]+TotalEstimateCommission)
			when  not matched then
				insert([UserID],[PlatformID],[SummaryDate],[ChannelID],[ManagerID],[PV],[UV],[OrderQuantity],[OrderAmount],[EstimateCommission],[AvaliableCommission],[SharedClientID],[ProductsQuantity],[AvaliableOrderAmount],[AvaliableOrderQuantity],[SharedQunatity],[CreateDate])
				values(O.SharedUserID,O.PlatformID,O.CreatDate,  O.ChannelID,O.SharedManagerID,0,0,O.orderQty,TotalPrice,TotalEstimateCommission,0,SharedClientID,O.ProductsQuantity,0,0,0,GETDATE());
		----------------------------------------可结算拥金(妥投),妥投的订单数量,妥投的订单金额---------------------------------------------------------------------------------------------------------------------------------
			--可结算拥金(妥投),妥投的订单数量,妥投的订单金额
			merge into dbo.DataSummary as DS
			using (
				  select SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,CreatDate,sum(TotalCommission) as TotalCommission ,COUNT(*) as orderQty,SUM(TotalPrice) as TotalPrice from (
					  select SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,OrderId,SUM(ProductPrice) as TotalPrice,sum(Commission) as TotalCommission,CONVERT(varchar(100), RecordCreatedTime, 111) as CreatDate from [NationalUnionHistory].dbo.OrderProductEffect where @UserID<0 or SharedUserID = @UserID group by SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,OrderId,CONVERT(varchar(100), RecordCreatedTime, 111)
				  ) as t group by SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,CreatDate
			) as E
			on (DS.UserID=E.SharedUserID and DS.PlatformID=E.PlatformID and DS.ChannelID=E.ChannelID and DS.SharedClientID=E.SharedClientID and DS.ManagerID=E.SharedManagerID and DS.SummaryDate=E.CreatDate)
			when matched then
				update set ds.AvaliableCommission=(ds.AvaliableCommission+E.TotalCommission),ds.AvaliableOrderQuantity=(ds.AvaliableOrderQuantity+E.orderQty),ds.AvaliableOrderAmount=(ds.AvaliableOrderAmount+E.TotalPrice)
			when  not matched then
				insert([UserID],[PlatformID],[SummaryDate],[ChannelID],[ManagerID],[PV],[UV],[OrderQuantity],[OrderAmount],[EstimateCommission],[AvaliableCommission],[SharedClientID],[ProductsQuantity],[AvaliableOrderAmount],[AvaliableOrderQuantity],[SharedQunatity],[CreateDate])
				values(E.SharedUserID,E.PlatformID,E.CreatDate,E.ChannelID,E.SharedManagerID,0,0,0,0,0,E.TotalCommission,SharedClientID,0,E.TotalPrice,E.orderQty,0,GETDATE());
		----------------------------------------分享量---------------------------------------------------------------------------------------------------------------------------------
			--分享量
			merge into dbo.DataSummary as DS
			using (select [MemberNo],[PlatformID],[ChannelID],[ManagerID],[ClientType] as SharedClientID,CONVERT(varchar(100),[CreatDate], 111) as CreatDate,COUNT(*) as Qty from [NationalUnionHistory].[dbo].[Share] where @UserID<0 or [MemberNo] = @UserID group by [MemberNo],[PlatformID],[ChannelID],[ManagerID],[ClientType],CONVERT(varchar(100), [CreatDate], 111)) as S
			on (DS.UserID=S.[MemberNo] and DS.PlatformID=S.[PlatformID] and DS.ChannelID=S.[ChannelID] and DS.ManagerID=S.[ManagerID] and DS.SummaryDate=S.CreatDate)
			when matched then
				update set DS.SharedQunatity=(DS.[SharedQunatity]+S.Qty)--更新分享量
			when not matched then--统计分享量
				insert([UserID],[PlatformID],[SummaryDate],[ChannelID],[ManagerID],[PV],[UV],[OrderQuantity],[OrderAmount],[EstimateCommission],[AvaliableCommission],[SharedClientID],[ProductsQuantity],[AvaliableOrderAmount],[AvaliableOrderQuantity],[SharedQunatity],[CreateDate])
				values(S.[MemberNo],S.[PlatformID],S.CreatDate,S.ChannelID,S.[ManagerID],0,0,0,0,0,0,SharedClientID,0,0,0,Qty,GETDATE());	
		------------------------------------------------------------------------------------------------------------------------------------------------------
		-------------迁移数据并清空记录表----------------------------------------------------------------------------------------------------------------------------------
		------------------------------------------------------------------------------------------------------------------------------------------------------
			--PV--
			select 'PV量',COUNT(*) from [NationalUnionHistory].dbo.PVInfo where @UserID<0 or SharedUserID = @UserID
			delete from [NationalUnionHistory].dbo.PVInfo where @UserID<0 or SharedUserID = @UserID
			
			--UV--
			select 'UV量',COUNT(*) from [NationalUnionHistory].dbo.UVInfo where @UserID<0 or SharedUserID = @UserID
			delete from [NationalUnionHistory].dbo.UVInfo where @UserID<0 or SharedUserID = @UserID
			
			----------订单迁移量----------
			select '订单量',COUNT(*) from [NationalUnionHistory].dbo.OrderProductOccur where @UserID<0 or SharedUserID = @UserID
			delete from [NationalUnionHistory].dbo.OrderProductOccur where @UserID<0 or SharedUserID = @UserID
			
			---------妥投订单迁移量--------
			select '订单妥投量',COUNT(*) from [NationalUnionHistory].dbo.OrderProductEffect where @UserID<0 or SharedUserID = @UserID
			delete from [NationalUnionHistory].dbo.OrderProductEffect where @UserID<0 or SharedUserID = @UserID
			
			---------分享量--------
			select '分享量',COUNT(*) from [NationalUnionHistory].dbo.[Share] where @UserID<0 or [MemberNo] = @UserID
			delete from [NationalUnionHistory].[dbo].[Share] where @UserID<0 or [MemberNo] = @UserID
			
			---------耗时--------
			select '耗时',DATEDIFF(SS,@CurrentDatetime,getdate())			
			
			commit tran
	end try
	begin catch
		rollback tran
	    SELECT  ERROR_NUMBER() as ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
	end catch
end
