USE [IES_Resource]
go
-- =============================================    
-- Author:      ��ʤ��    
-- Create date: 20141127    
-- Description: ��ȡ�Ծ����б���Ϣ    
-- =============================================    
alter proc [dbo].[Paper_Search]    
 @Searchkey nvarchar(200) = '' , --��ѯ�ؼ���    
 @OCID int = 1 ,     
 @Type int = -1 , -- �Ծ�����    
 @Scope int = -1, -- �Ծ����÷�Χ   (ʹ��Ȩ��)  
 @UpdateTime smalldatetime = '2010-1-1' ,-- �ϴ����� ����Ҫ��ҵ������ >= @UploadTime    
 @PageSize int = 20 ,                          
 @PageIndex int = 1       
as     
 SET NOCOUNT ON;    
    
	DECLARE @lowerLimit INT                                    
	DECLARE @upperLimit INT   
 
    SET @lowerLimit = @PageSize * ( @PageIndex - 1 )                                    
    SET @upperLimit = @PageSize *   @PageIndex     
    ;
    
    
	with CTE as                                    
    (   
		select Row_Number() OVER( ORDER BY Updatetime Desc ) as rownum , t1.PaperID 
		from dbo.Paper t1
		where OCID = @OCID and t1.UpdateTime > @UpdateTime
		and ( t1.[Type] = @Type or @Type < 1 )
		and ( @Scope < 1 or t1.Scope = @Scope )
		and ( t1.Papername like '%'+@Searchkey + '%' or @Searchkey = '') 
	)
	select t2.PaperID, t2.Papername, t2.[Type], t2.Scope , t2.Num ,t2.Score ,t2.UpdateTime ,
	t3.UserName ,
	( select count(*) from CTE ) as rowscount 
	from CTE t1
	inner join Paper t2 on t1.PaperID = t2.PaperID
	inner join IES.dbo.User_S t3 on t3.UserID = t2.CreateUserID
	where t1.rownum BETWEEN @lowerLimit AND @upperLimit

go  
--���� url ��ַ 
update IES_sys.dbo.Menu set URL='content.knowledge.topic'  where MenuID='B24'   
go
   
  
   

Use IES
GO
/****** Object:  UserDefinedFunction [dbo].[f_AttachmentListGet]    Script Date: 01/10/2015 08:59:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:      ��ʤ��  
-- Create date: 20150110
-- Description: ��ȡ�ļ������ص�ַ 
-- =============================================   
create function [dbo].[f_Resource_URL_Get]      
(   
   @FileID int 
)        
returns @temp table( 
	FileID int ,  DownURL nvarchar(500) ,  ViewURL nvarchar(500) )      
as       
begin      
        
    declare @serverID int ,@FileName nvarchar(200)
    select @serverID =  ServerID ,  @FileName = [FileName] from  IES_Resource.dbo.[File] t1   
    where FileID = @FileID
    
    
      
     insert into @temp( FileID , DownURL , ViewURL )
	 select @FileID as FileID , 
	 'http://'+t1.Host+':'+t1.IISPort+ '/'+t1.IISFolder+'/'+ @FileName as DownURL,
	 'http://'+t1.Host+':'+t1.NginxPort+ '/'+t1.NginxFolder+'/'+ @FileName as ViewURL
	  from  IES.dbo.ResourceServer t1
	inner join ( select ID from dbo.f_Math_DeliveryValueList( @serverID, 4096 ) )t2
	on t1.ServerID = t2.ID 
      
    return       
end      
go  
  
USE [IES_Resource]
GO
/****** Object:  StoredProcedure [dbo].[Folder_Get]    Script Date: 01/10/2015 09:25:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      ��ʤ��
-- Create date: 20141208
-- Description: �ļ��е���ϸ��Ϣ
-- =============================================

create proc [dbo].[Folder_Get]
	@FolderID int
as 
	select FolderID, OCID, CourseID , CreateUserID, OwnerUserID, ParentID,
	FolderName, Brief, Orde , CreateTime
	from dbo.Folder
	where FolderID = @FolderID and  IsDeleted = 0 
    

USE [IES_Resource]
GO
/****** Object:  StoredProcedure [dbo].[File_Get]    Script Date: 01/10/2015 09:19:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      ��ʤ��
-- Create date: 20141208
-- Description: �ļ�����ϸ��Ϣ
-- =============================================

create proc [dbo].[File_Get]
	@FileID int = 1
as 
	select t1.FileID, t1.OCID, t1.CourseID, t1.FolderID, t1.SubjectID1, t1.SubjectID2,
	t1.CreateUserID, t1.CreateUserName, t1.OwnerUserID, t1.FileTitle, t1.[FileName], t1.Ext, t1.FileType,
	t1.Brief, t1.Keys, t1.FileSize, t1.pingyin, t1.TimeLength, t1.RarIndexPage, t1.UploadTime, t1.Orde, 
	t1.ShareRange, t1.AllowDownload, 
	t1.ServerID, t1.Clicks, t1.Downloads, t1.IsTransfer , t2.DownURL, t2.ViewURL
	from dbo.[File] t1
	inner join ( select top 1 * from  IES.dbo.f_Resource_URL_Get(@FileID ) ) t2
	on t1.FileID = t2.FileID 
	
	where t1.FileID = @FileID and IsDeleted = 0 
    
go
USE [IES]
GO
/****** Object:  UserDefinedFunction [dbo].[f_Math_DeliveryValueList]    Script Date: 01/10/2015 09:26:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
-- =============================================      
-- Author:      ��ʤ��      
-- Create date: 20141231     select * from [dbo].[f_Math_DeliveryValueList]  ( 1, 4096 ) 
-- Description: ��ȡȡģ�������б�  �����֧��4096
-- =============================================      
ALTER function [dbo].[f_Math_DeliveryValueList]          
(          
   @i int  = 1     ,
   @modevalue int  
)            
returns @tb table(   ID  int    )          
as           
begin          
     
 
	 while( @i > 0  )
	 begin
		if( @i >= @modevalue )
			insert into @tb values ( @modevalue )
		set  @i =  @i % @modevalue
		set  @modevalue = @modevalue / 2
	 end 

    return           
end 
go    
USE [IES_Resource]
GO
/****** Object:  StoredProcedure [dbo].[Folder_List]    Script Date: 01/13/2015 17:47:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:      ��ʤ��  
-- Create date: 20141127  
-- Description: ��ȡ�����ļ����б���Ϣ  
-- =============================================  
ALTER proc [dbo].[Folder_List]  
 @OCID int = 0 , -- �ҵ����Ͽ�OCID=0   
 @ParentID int = 0 , -- ��ʾ���ļ�����  
 @UserID int = 0   
as   
 SET NOCOUNT ON;  
  
  
 select FolderID, OCID, CourseID, CreateUserID, OwnerUserID, ParentID, FolderName, Brief, Orde, CreateTime  
 from dbo.Folder  
 where IsDeleted = 0 AND (@ParentID = -1 or ParentID = @ParentID) and OCID = @OCID   
 and ( (  CreateUserID = @UserID or OwnerUserID = @UserID  ) or @UserID = 0 )  
    
   
     
 go 
  
  USE [IES_Resource]
GO
/****** Object:  StoredProcedure [dbo].[File_Search]    Script Date: 01/21/2015 22:18:58 ******/

/*
exec File_Search @OCID=-1
*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:      ��ʤ��  
-- Create date: 20141127  
-- Description: ��ȡ���ϵ��б���Ϣ  
-- =============================================  
ALTER proc [dbo].[File_Search]  
 @Searchkey nvarchar(200) = '' , --��ѯ�ؼ���  
 @OCID int = 0 , -- �ҵ����Ͽ�courseid=-1  ,OCID=0 
 @CourseID int = 1 , --�γ̱��
 @FolderID int = 0 , -- ��ʾ���ļ�����   
 @FileType int = -1, -- �ļ�����  
 @UploadTime smalldatetime ='2011-1-1',-- �ϴ����� ����Ҫ��ҵ������ >= @UploadTime  
 @ShareRange int = -1  ,  
 @UserID int =1    
as   
 SET NOCOUNT ON;  
    
  
 
 select FileID, OCID, CourseID, FolderID, SubjectID1, SubjectID2, 
 CreateUserID, CreateUserName, OwnerUserID, FileTitle, t1.FileName, Ext, 
 FileType, Brief, Keys, FileSize, pingyin, TimeLength, RarIndexPage, 
 UploadTime, Orde, ShareRange, AllowDownload, ServerID, Clicks, Downloads, IsTransfer
 from  dbo.[File] t1
 where IsDeleted = 0  and t1.OCID = @OCID 
 and CreateUserID = @UserID and FolderID = @FolderID 
 AND (@ShareRange = -1 or ShareRange = @ShareRange)
 AND (@FileType = -1 or FileType = @FileType)
   
 --��ȡϰ���б�������������ɸѡͨ���� f_Exercise_Cacu_GetExeriseList ��,�����㷨��������ŵ����溯����洢������ִ��  
 
 go 
  
update [IES_Sys].[dbo].[Menu] set URL='content.exercise' where MenuID='B22' 
  go    
  USE [IES_Resource]
-- =============================================  
-- Author:      ��ʤ��  
-- Create date: 20141127  
-- Description: �½�����  
-- =============================================  
  
ALTER proc [dbo].[Chapter_ADD]  
	@ChapterID int output , 
	@OCID int ,
    @CourseID int ,  
    @OwnerUserID int ,
    @CreateUserID int ,   
    @Title nvarchar(500) ,   
    @Orde int,
    @ParentID int  
as   
  
 SET NOCOUNT ON;  
   
  
 set @ChapterID = 0   
   
/* select   @ChapterID = ChapterID 
 from  dbo.Chapter  
 where  OCID = @OCID*/
   
 --if( @ChapterID = 0 )  
 --begin  
  insert into dbo.Chapter( OCID ,  CourseID, OwnerUserID , CreateUserID , Title, ParentID, Orde  )  
  values (  @OCID , @CourseID, @OwnerUserID , @CreateUserID  , @Title , @ParentID  , @Orde)  
  --����ͳһ����˳��
   --values (  @OCID , @CourseID, @OwnerUserID , @CreateUserID  , @Title , @ParentID  , dbo.[f_Cacu_Chapter_GetOrder]( @OCID,@ParentID  )   )  
     set @ChapterID = @@identity      
 --end   
go     
-- =============================================
-- Author:      ��ʤ��
-- Create date: 20141127
-- Description: �½�ɾ��
-- =============================================

ALTER proc [dbo].[Chapter_Del]
	@ChapterID int  ,
    @UserID int =0 --ִ��ɾ������Ա���
as 
	SET NOCOUNT ON;
	delete from Chapter where ChapterID = @ChapterID or ParentID=@ChapterID
	 
-- =============================================
-- Author:      ��ʤ��
-- Create date: 20141127
-- Description: �½ڸ���
-- =============================================
go
ALTER proc [dbo].[Chapter_Upd]
	@ChapterID int  ,
    @Title nvarchar(500),
    @Orde int,
    @parentID int	
as 

	SET NOCOUNT ON;
	
    Update Chapter set Title = @Title, Orde = @Orde, ParentID=@parentID
    where ChapterID = @ChapterID    
go
-- =============================================  
-- Author:      ��ʤ��  
-- Create date: 20150126
-- Description: ֪ʶ���������  
-- =============================================    
create proc [dbo].[ResourceKen_ADD]  
	@ID int output , 
	@KenID int ,
    @ResourceID int ,       
    @Source nvarchar(30)
as     
  set @ID = 0        
  
  INSERT INTO [dbo].[ResourceKen]([ResourceID],[Source],[KenID])
  SELECT @ResourceID, @Source, @KenID
  set @ID = @@identity      
go  
 -- =============================================
-- Author:      ��ʤ��
-- Create date: 20150128
-- Description: ��ѯ����ocid�µ�֪ʶ�����½ڶ�Ӧ��ϵ
-- =============================================
create proc [dbo].[ResourceKen_List_OCID]
  	@OCID int
as 

	SET NOCOUNT ON;
 
	
	select a.* from ResourceKen a, Ken b 
	where b.OCID=@OCID
	and a.KenID=b.KenID
go	
-- =============================================  
-- Author:      ��ʤ��  
-- Create date: 20150128
-- Description: ��ȡ��ǩ�б���Ϣ  
-- =============================================  
create proc [dbo].[Key_List]  
	 @OCID int 
as 

	SET NOCOUNT ON;  

	select * from dbo.[Key]
	where OCID = @OCID	
	 

go	 
-- =============================================  
-- Author:      ��ʤ��  
-- Create date: 20150126
-- Description: ֪ʶ���������  
-- =============================================    
create proc [dbo].[ResourceKen_Del]   
	@KenID int ,
    @ResourceID int ,       
    @Source nvarchar(30)
as         
  
  delete from [dbo].[ResourceKen] where KenID = @KenID and ResourceID = @ResourceID and [Source] = @Source	 
  
go  
-- =============================================  
-- Author:      ��ʤ��  
-- Create date: 20141207
-- Description: ��ȡ�ļ���ϰ�������Ч֪ʶ�� 
-- =============================================  
ALTER proc [dbo].[Resource_Ken_List]  
	@SearchKey nvarchar(50) = '' ,
	@Source nvarchar(50) = 'File',
	@UserID int = 0 ,
	@TopNum int = 20 
as   
 SET NOCOUNT ON;  
  
  
	if ( @Source = 'File' )
	begin
		select distinct top(@TopNum) t3.KenID , t3.Name As [Source]  
		from dbo.ResourceKen t1 
		inner join dbo.[File] t2 on t1.ResourceID = t2.FileID
		inner join dbo.Ken t3 on t3.KenID = t1.KenID 
		inner join ( select FileID from  [dbo].[f_Cacu_GetUserAuFileList](@UserID)) t4 on t4.FileID = t2.FileID
		where t1.Source =  'File' and  ( t3.Name  like '%'+ @SearchKey + '%' or @SearchKey = '')
	end
	
	
	
	if ( @Source = 'Exercise' )
	begin
		select distinct top(@TopNum) t3.KenID , t3.Name As [Source]    
		from dbo.ResourceKen t1 
		inner join dbo.Exercise  t2 on t1.ResourceID = t2.ExerciseID
		inner join dbo.Ken t3 on t3.KenID = t1.KenID 
		inner join ( select ExerciseID from  [dbo].[f_Cacu_GetUserAuExerciseList](@UserID)) t4 on t4.ExerciseID = t2.ExerciseID
		where t1.Source = 'Exercise' and  ( t3.Name  like '%'+ @SearchKey + '%' or @SearchKey = '')
	end
go
USE [IES_Resource]
GO

/****** Object:  Table [dbo].[ResourceKey]    Script Date: 01/29/2015 20:00:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResourceKey]') AND type in (N'U'))
DROP TABLE [dbo].[ResourceKey]
GO

USE [IES_Resource]
GO

/****** Object:  Table [dbo].[ResourceKey]    Script Date: 01/29/2015 20:00:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ResourceKey](
	[ID] [int] identity(1,1) NOT NULL,
	[ResourceID] [int] NOT NULL,
	[Source] [varchar](30) NOT NULL,
	[KeyID] [int] NOT NULL,
 CONSTRAINT [PK_ResourceKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Դ��ţ�ϰ���š��ļ���ţ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ResourceKey', @level2type=N'COLUMN',@level2name=N'ResourceID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Դ����Exercise, File' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ResourceKey', @level2type=N'COLUMN',@level2name=N'Source'
GO


	