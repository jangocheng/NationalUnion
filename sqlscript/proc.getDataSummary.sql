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
----------------�˳�������ͳ��PV��UV����������,��Ʒ������,������Ԥ�ƿ���ӵ�𣬿ɽ���ӵ��(��Ͷ),��Ͷ�Ķ�������,��Ͷ�Ķ�����������
----------------ͳ�Ƶ�ά����SharedUserID,PlatformID,ChannelID,SharedManagerID,CreatDate
----------------����@UserIDС��0�Ļ�������ͳ�ƿ��������ݣ���ɺ�Ա���ɾ���������������ͳ��ָ�����û����ݡ�
----------------PV,UV,������ͳ�Ƶ�������ΪSharedLevelΪ1���û��������һ�������ߣ�
-------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------
create proc [dbo].[proc_DataSummary]
(
@UserID bigint--�û�ID,��ָ����Ϊȫ���û�
)
as
begin
	begin try
		begin tran
			declare @Level int,
					@CurrentDatetime datetime
			set @Level=1 --LevelΪ1Ϊ���һ��������
			set @CurrentDatetime=GETDATE();
	    ----------------------------------------PV(ֻͳ�����һ��������)---------------------------------------------------------------------------------------------------------------------------------
			--PV
			merge into dbo.DataSummary as DS
			using (select SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,COUNT(*) as Qty,CONVERT(varchar(100), CreateDate, 111) as CreatDate from [NationalUnionHistory].dbo.PVInfo where SharedLevel = @Level and (@UserID<0 or SharedUserID = @UserID) group by SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,CONVERT(varchar(100), CreateDate, 111)) as P
			on (DS.UserID=P.SharedUserID and DS.PlatformID=P.PlatformID and DS.ChannelID=P.ChannelID and DS.ManagerID=P.SharedManagerID and DS.SharedClientID=P.SharedClientID and DS.SummaryDate=P.CreatDate)
			when matched then
				update set DS.PV=(DS.PV+P.Qty)--����ͳ�Ƽ����ڵ���Ϣ
			when not matched then--ͳ���µ�PV��Ϣ
				insert([UserID],[PlatformID],[SummaryDate],[ChannelID],[ManagerID],[PV],[UV],[OrderQuantity],[OrderAmount],[EstimateCommission],[AvaliableCommission],[SharedClientID],[ProductsQuantity],[AvaliableOrderAmount],[AvaliableOrderQuantity],[SharedQunatity],[CreateDate])
				values(P.SharedUserID,P.PlatformID,P.CreatDate,  P.ChannelID,P.SharedManagerID,P.Qty,0,0,0,0,0,SharedClientID,0,0,0,0,GETDATE());
		----------------------------------------UV(ֻͳ�����һ��������)---------------------------------------------------------------------------------------------------------------------------------
			--UV
			merge into dbo.DataSummary as DS
			using (select SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,COUNT(*) as Qty,CONVERT(varchar(100), CreateDate, 111) as CreatDate from [NationalUnionHistory].dbo.UVInfo where SharedLevel = @Level and (@UserID<0 or SharedUserID = @UserID) group by SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,CONVERT(varchar(100), CreateDate, 111)) as U
			on (DS.UserID=U.SharedUserID and DS.PlatformID=U.PlatformID and DS.ChannelID=U.ChannelID and DS.ManagerID=U.SharedManagerID and DS.SharedClientID=U.SharedClientID and DS.SummaryDate=U.CreatDate)
			when matched then
				update set DS.UV=(DS.UV+U.Qty)--����ͳ�Ƽ����ڵ���Ϣ
			when not matched then--ͳ���µ�PV��Ϣ
				insert([UserID],[PlatformID],[SummaryDate],[ChannelID],[ManagerID],[PV],[UV],[OrderQuantity],[OrderAmount],[EstimateCommission],[AvaliableCommission],[SharedClientID],[ProductsQuantity],[AvaliableOrderAmount],[AvaliableOrderQuantity],[SharedQunatity],[CreateDate])
				values(U.SharedUserID,U.PlatformID,U.CreatDate,U.ChannelID,U.SharedManagerID,0,U.Qty,0,0,0,0,SharedClientID,0,0,0,0,GETDATE());
		----------------------------------------��������,��Ʒ������,������Ԥ�ƿ���ӵ��---------------------------------------------------------------------------------------------------------------------------------				
		    --��������,��Ʒ������,������Ԥ�ƿ���ӵ��
			merge into dbo.DataSummary as DS
			using (
				  select SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,CreatDate,COUNT(*) as orderQty,sum(ProductsQuantity) as ProductsQuantity,SUM(TotalPrice) as TotalPrice,sum(TotalEstimateCommission) as TotalEstimateCommission from (
					  select SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,OrderId,sum(ProductNumber) as ProductsQuantity,SUM(ProductPrice) as TotalPrice,sum(Commission) as TotalEstimateCommission,CONVERT(varchar(100), RecordCreatedTime, 111) as CreatDate from [NationalUnionHistory].[dbo].OrderProductOccur where @UserID<0 or SharedUserID = @UserID group by SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,OrderId,CONVERT(varchar(100), RecordCreatedTime, 111)
				  ) as t group by SharedUserID,PlatformID,ChannelID,SharedManagerID,SharedClientID,CreatDate
			) as O
			on (DS.UserID=O.SharedUserID and DS.PlatformID=O.PlatformID and DS.ChannelID=O.ChannelID and DS.ManagerID=O.SharedManagerID and DS.SharedClientID=O.SharedClientID and DS.SummaryDate=O.CreatDate)
			when matched then--Ԥ�ƿ���ӵ��
				update set ds.OrderQuantity=(ds.OrderQuantity+O.orderQty),ds.ProductsQuantity=(ds.ProductsQuantity+O.ProductsQuantity),DS.[OrderAmount]=(DS.[OrderAmount]+TotalPrice),DS.[EstimateCommission]=(DS.[EstimateCommission]+TotalEstimateCommission)
			when  not matched then
				insert([UserID],[PlatformID],[SummaryDate],[ChannelID],[ManagerID],[PV],[UV],[OrderQuantity],[OrderAmount],[EstimateCommission],[AvaliableCommission],[SharedClientID],[ProductsQuantity],[AvaliableOrderAmount],[AvaliableOrderQuantity],[SharedQunatity],[CreateDate])
				values(O.SharedUserID,O.PlatformID,O.CreatDate,  O.ChannelID,O.SharedManagerID,0,0,O.orderQty,TotalPrice,TotalEstimateCommission,0,SharedClientID,O.ProductsQuantity,0,0,0,GETDATE());
		----------------------------------------�ɽ���ӵ��(��Ͷ),��Ͷ�Ķ�������,��Ͷ�Ķ������---------------------------------------------------------------------------------------------------------------------------------
			--�ɽ���ӵ��(��Ͷ),��Ͷ�Ķ�������,��Ͷ�Ķ������
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
		----------------------------------------������---------------------------------------------------------------------------------------------------------------------------------
			--������
			merge into dbo.DataSummary as DS
			using (select [MemberNo],[PlatformID],[ChannelID],[ManagerID],[ClientType] as SharedClientID,CONVERT(varchar(100),[CreatDate], 111) as CreatDate,COUNT(*) as Qty from [NationalUnionHistory].[dbo].[Share] where @UserID<0 or [MemberNo] = @UserID group by [MemberNo],[PlatformID],[ChannelID],[ManagerID],[ClientType],CONVERT(varchar(100), [CreatDate], 111)) as S
			on (DS.UserID=S.[MemberNo] and DS.PlatformID=S.[PlatformID] and DS.ChannelID=S.[ChannelID] and DS.ManagerID=S.[ManagerID] and DS.SummaryDate=S.CreatDate)
			when matched then
				update set DS.SharedQunatity=(DS.[SharedQunatity]+S.Qty)--���·�����
			when not matched then--ͳ�Ʒ�����
				insert([UserID],[PlatformID],[SummaryDate],[ChannelID],[ManagerID],[PV],[UV],[OrderQuantity],[OrderAmount],[EstimateCommission],[AvaliableCommission],[SharedClientID],[ProductsQuantity],[AvaliableOrderAmount],[AvaliableOrderQuantity],[SharedQunatity],[CreateDate])
				values(S.[MemberNo],S.[PlatformID],S.CreatDate,S.ChannelID,S.[ManagerID],0,0,0,0,0,0,SharedClientID,0,0,0,Qty,GETDATE());	
		------------------------------------------------------------------------------------------------------------------------------------------------------
		-------------Ǩ�����ݲ���ռ�¼��----------------------------------------------------------------------------------------------------------------------------------
		------------------------------------------------------------------------------------------------------------------------------------------------------
			--PV--
			select 'PV��',COUNT(*) from [NationalUnionHistory].dbo.PVInfo where @UserID<0 or SharedUserID = @UserID
			delete from [NationalUnionHistory].dbo.PVInfo where @UserID<0 or SharedUserID = @UserID
			
			--UV--
			select 'UV��',COUNT(*) from [NationalUnionHistory].dbo.UVInfo where @UserID<0 or SharedUserID = @UserID
			delete from [NationalUnionHistory].dbo.UVInfo where @UserID<0 or SharedUserID = @UserID
			
			----------����Ǩ����----------
			select '������',COUNT(*) from [NationalUnionHistory].dbo.OrderProductOccur where @UserID<0 or SharedUserID = @UserID
			delete from [NationalUnionHistory].dbo.OrderProductOccur where @UserID<0 or SharedUserID = @UserID
			
			---------��Ͷ����Ǩ����--------
			select '������Ͷ��',COUNT(*) from [NationalUnionHistory].dbo.OrderProductEffect where @UserID<0 or SharedUserID = @UserID
			delete from [NationalUnionHistory].dbo.OrderProductEffect where @UserID<0 or SharedUserID = @UserID
			
			---------������--------
			select '������',COUNT(*) from [NationalUnionHistory].dbo.[Share] where @UserID<0 or [MemberNo] = @UserID
			delete from [NationalUnionHistory].[dbo].[Share] where @UserID<0 or [MemberNo] = @UserID
			
			---------��ʱ--------
			select '��ʱ',DATEDIFF(SS,@CurrentDatetime,getdate())			
			
			commit tran
	end try
	begin catch
		rollback tran
	    SELECT  ERROR_NUMBER() as ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
	end catch
end
