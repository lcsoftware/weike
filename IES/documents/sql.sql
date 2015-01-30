USE [IES_Resource]
go
-- =============================================    
-- Author:      王胜辉    
-- Create date: 20141127    
-- Description: 获取试卷的列表信息    
-- =============================================    
alter proc [dbo].[Paper_Search]    
 @Searchkey nvarchar(200) = '' , --查询关键字    
 @OCID int = 1 ,     
 @Type int = -1 , -- 试卷类型    
 @Scope int = -1, -- 试卷适用范围   (使用权限)  
 @UpdateTime smalldatetime = '2010-1-1' ,-- 上传日期 ，需要从业务层计算 >= @UploadTime    
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
--更新 url 地址 
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
-- Author:      王胜辉  
-- Create date: 20150110
-- Description: 获取文件的下载地址 
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
-- Author:      王胜辉
-- Create date: 20141208
-- Description: 文件夹的详细信息
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
-- Author:      王胜辉
-- Create date: 20141208
-- Description: 文件的详细信息
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
-- Author:      王胜辉      
-- Create date: 20141231     select * from [dbo].[f_Math_DeliveryValueList]  ( 1, 4096 ) 
-- Description: 获取取模运算结果列表  ，最大支持4096
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
-- Author:      王胜辉  
-- Create date: 20141127  
-- Description: 获取资料文件夹列表信息  
-- =============================================  
ALTER proc [dbo].[Folder_List]  
 @OCID int = 0 , -- 我的资料库OCID=0   
 @ParentID int = 0 , -- 表示根文件夹下  
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
-- Author:      王胜辉  
-- Create date: 20141127  
-- Description: 获取资料的列表信息  
-- =============================================  
ALTER proc [dbo].[File_Search]  
 @Searchkey nvarchar(200) = '' , --查询关键字  
 @OCID int = 0 , -- 我的资料库courseid=-1  ,OCID=0 
 @CourseID int = 1 , --课程编号
 @FolderID int = 0 , -- 表示根文件夹下   
 @FileType int = -1, -- 文件类型  
 @UploadTime smalldatetime ='2011-1-1',-- 上传日期 ，需要从业务层计算 >= @UploadTime  
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
   
 --获取习题列表，满足条件的筛选通过（ f_Exercise_Cacu_GetExeriseList ）,复杂算法必须独立放到外面函数或存储过程中执行  
 
 go 
  
update [IES_Sys].[dbo].[Menu] set URL='content.exercise' where MenuID='B22' 
  go    
  USE [IES_Resource]
-- =============================================  
-- Author:      王胜辉  
-- Create date: 20141127  
-- Description: 章节新增  
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
  --外面统一控制顺序
   --values (  @OCID , @CourseID, @OwnerUserID , @CreateUserID  , @Title , @ParentID  , dbo.[f_Cacu_Chapter_GetOrder]( @OCID,@ParentID  )   )  
     set @ChapterID = @@identity      
 --end   
go     
-- =============================================
-- Author:      王胜辉
-- Create date: 20141127
-- Description: 章节删除
-- =============================================

ALTER proc [dbo].[Chapter_Del]
	@ChapterID int  ,
    @UserID int =0 --执行删除的人员编号
as 
	SET NOCOUNT ON;
	delete from Chapter where ChapterID = @ChapterID or ParentID=@ChapterID
	 
-- =============================================
-- Author:      王胜辉
-- Create date: 20141127
-- Description: 章节更新
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
-- Author:      王胜辉  
-- Create date: 20150126
-- Description: 知识点关联新增  
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
-- Author:      王胜辉
-- Create date: 20150128
-- Description: 查询所有ocid下的知识点与章节对应关系
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
-- Author:      王胜辉  
-- Create date: 20150128
-- Description: 获取标签列表信息  
-- =============================================  
create proc [dbo].[Key_List]  
	 @OCID int 
as 

	SET NOCOUNT ON;  

	select * from dbo.[Key]
	where OCID = @OCID	
	 

go	 
-- =============================================  
-- Author:      王胜辉  
-- Create date: 20150126
-- Description: 知识点关联新增  
-- =============================================    
create proc [dbo].[ResourceKen_Del]   
	@KenID int ,
    @ResourceID int ,       
    @Source nvarchar(30)
as         
  
  delete from [dbo].[ResourceKen] where KenID = @KenID and ResourceID = @ResourceID and [Source] = @Source	 
  
 

USE [IES_Resource]
GO

 
-- =============================================  
-- Author:      王胜辉  
-- Create date: 20141207
-- Description: 获取文件、习题相关有效知识点 
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
/****** Object:  StoredProcedure [dbo].[Exercise_Upd]    Script Date: 01/30/2015 00:18:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      王胜辉
-- Create date: 20141207
-- Description: 习题基本属性修改
-- =============================================

ALTER proc [dbo].[Exercise_Upd]
	 @ExerciseID int  , 
	 @ExerciseType int ,
	 @Diffcult int,
	 @Scope int , 
	 @ShareRange int = 0  ,  
	 @Brief nvarchar(1000)      = '' ,
	 @Conten nvarchar(max)      = '', 
	 @Answer nvarchar(max)      = '' ,
	 @Analysis nvarchar(max)    = '' ,
	 @ScorePoint nvarchar(1000) = '',
	 @Score  decimal(10,1) , 
	 @IsRand bit =  0 
as 

	SET NOCOUNT ON;
	
	update t1
	set   ExerciseType = @ExerciseType, Diffcult = @Diffcult , Scope = @Scope, 
	ShareRange = @ShareRange , Brief = @Brief , Conten = @Conten , Answer =@Answer , 
	Analysis = @Analysis , ScorePoint = @ScorePoint , Score = @Score , IsRand = @IsRand , UpdateTime =GETDATE()
	from Exercise t1
	where ExerciseID = @ExerciseID