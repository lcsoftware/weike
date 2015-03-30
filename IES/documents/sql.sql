USE [IES]
GO
/****** Object:  StoredProcedure [dbo].[OC_ALLRole_Get]    Script Date: 03/30/2015 08:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
-- 获取用户的在线课程的角色      
create proc [dbo].[OC_ALLRole_Get]     
 @UserID int ,
 @OCID int = 0       
as       
      
  -- 0 课程创建人、 1 课程负责人 ;   不受控制      
  -- 2 只能删自己创建      
  ---3 根据授权来判断       
            
 select [Role] ,OCID               
 from   IES.dbo.OCTeam               
 where  [Status] = 2  and UserID = @UserID  and ( OCID =  @OCID or  @OCID = 0 )      
       
go
USE [IES_Resource]
GO

/****** Object:  StoredProcedure [dbo].[Paper_Search]    Script Date: 03/30/2015 08:35:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Paper_Search]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Paper_Search]
GO

/****** Object:  StoredProcedure [dbo].[Exercise_KenID_ChapterID_List]    Script Date: 03/30/2015 08:35:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Exercise_KenID_ChapterID_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Exercise_KenID_ChapterID_List]
GO

/****** Object:  StoredProcedure [dbo].[Exercise_ChapterID_KenID_List]    Script Date: 03/30/2015 08:35:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Exercise_ChapterID_KenID_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Exercise_ChapterID_KenID_List]
GO

/****** Object:  StoredProcedure [dbo].[Exercise_Search]    Script Date: 03/30/2015 08:35:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Exercise_Search]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Exercise_Search]
GO

/****** Object:  StoredProcedure [dbo].[Chapter_ADD]    Script Date: 03/30/2015 08:35:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Chapter_ADD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Chapter_ADD]
GO

/****** Object:  StoredProcedure [dbo].[FileSummary_Get]    Script Date: 03/30/2015 08:35:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FileSummary_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FileSummary_Get]
GO

/****** Object:  StoredProcedure [dbo].[File_Search]    Script Date: 03/30/2015 08:35:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[File_Search]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[File_Search]
GO

USE [IES_Resource]
GO

/****** Object:  StoredProcedure [dbo].[Paper_Search]    Script Date: 03/30/2015 08:35:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================            
-- Author:      王胜辉            
-- Create date: 20141127            
-- Description: 获取试卷的列表信息            
-- =============================================            
CREATE proc [dbo].[Paper_Search]            
 @Searchkey nvarchar(200) = '' , --查询关键字            
 @OCID int = 1 ,             
 @Type int = -1 , -- 试卷类型            
 @Scope int = -1, -- 试卷适用范围   (使用权限)          
 @UpdateTime smalldatetime = '2010-1-1' ,-- 上传日期 ，需要从业务层计算 >= @UploadTime            
 @PageSize int = 20 ,                                  
 @PageIndex int = 1               
as             
 SET NOCOUNT ON;   
   
 declare @exercisecount int   
 set @exercisecount = 0   
   
 if( @PageSize >100 )     
 set  @exercisecount = 1      
            
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
  and t1.Num >=  @exercisecount      
  AND ShareScope>0 
 )        
 select t2.PaperID, t2.Papername, t2.[Type], t2.Scope , t2.Num ,t2.Score ,t2.UpdateTime ,  t2.CreateUserID,      
 t3.UserName , t2.ShareScope,     
 ( select count(*) from CTE ) as rowscount         
 from CTE t1        
 inner join Paper t2 on t1.PaperID = t2.PaperID        
 inner join IES.dbo.User_S t3 on t3.UserID = t2.CreateUserID        
 where t1.rownum BETWEEN @lowerLimit AND @upperLimit   
 
 

GO

/****** Object:  StoredProcedure [dbo].[Exercise_KenID_ChapterID_List]    Script Date: 03/30/2015 08:35:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================    
-- Author:      王胜辉    
-- Create date: 20141206    
-- Description: 根据知识点编号、章节编号 获取关联的习题列表    
-- =============================================    
CREATE proc [dbo].[Exercise_KenID_ChapterID_List]    
  @KenID int = 0 ,  
  @ChapterID int = 0,  
  @UserID int ,  
  @OCID int   
as   
  
 SET NOCOUNT ON;    
    
    -- 获取和知识点、章节关联的文件  
 if( @ChapterID > 0  )  
 begin  
    
    select t1.ExerciseID, t1.Conten, t1.ExerciseType  
 from dbo.Exercise t1  
 inner join dbo.ResourceKen t2 on t1.ExerciseID = t2.ResourceID   
 inner join   
 (  
  select ExerciseID from dbo.Exercise   
  where ChapterID = @ChapterID and OCID = @OCID  
 )  t3 on t3.ExerciseID = t1.ExerciseID   
 where t2.[Source] = 'Exercise' and t1.IsDeleted = 0   
 and ( t2.KenID = @KenID or @KenID = 0 )  
   
   
 end   
    else  
    begin   
      
  -- 获取和知识点关联的习题  
     select t1.ExerciseID, t1.Conten, t1.ExerciseType  ,t1.CreateUserName , t1.Diffcult,t1.ShareRange,
     t1.CreateUserID , t1.CreateUserName 
  from dbo.Exercise t1  
  inner join dbo.ResourceKen t2 on t1.ExerciseID = t2.ResourceID   
  where t2.[Source] = 'Exercise' and t1.IsDeleted = 0 and ( t2.KenID = @KenID or @KenID = 0 )  
  and t1.OCID = @OCID   
   
 end   
     
     
    
     
    
GO

/****** Object:  StoredProcedure [dbo].[Exercise_ChapterID_KenID_List]    Script Date: 03/30/2015 08:35:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:      王胜辉  
-- Create date: 20141206  
-- Description: 根据知识点编号、章节编号 获取关联的习题列表  
-- =============================================  
CREATE proc [dbo].[Exercise_ChapterID_KenID_List]  
	 @KenID int = 0 ,
	 @ChapterID int = 0,
	 @UserID int ,
	 @OCID int 
as 

	SET NOCOUNT ON;  
  
    -- 获取章节下的习题， 节点递归
    if( @KenID = 0  )
    begin
		with
		cte as 
		(
			select ChapterID, ParentID   from dbo.Chapter  
			where ChapterID = @ChapterID and OCID = @OCID and IsDeleted = 0 
			union all
			select t2.ChapterID, t2.ParentID 
			from Chapter t2 , cte t1
			where t2.ParentID = t2.ChapterID and t2.OCID = @OCID
		)
		select  t1.ExerciseID, t1.Conten, t1.ExerciseType   from cte 
		inner join dbo.Exercise t1 on t1.ChapterID = cte.ChapterID 
		where t1.OCID = @OCID
	end 
	
	if( @KenID > 0  )
	begin
		with
		cte as 
		(
			select ChapterID, ParentID   from dbo.Chapter  
			where ChapterID = @ChapterID and OCID = @OCID and IsDeleted = 0 
			union all
			select t2.ChapterID, t2.ParentID 
			from Chapter t2 , cte t1
			where t2.ParentID = t2.ChapterID and t2.OCID = @OCID
		)
		select  t1.ExerciseID, t1.Conten, t1.ExerciseType  , t1.CreateUserID,t1.CreateUserName  from cte 
		inner join dbo.Exercise t1 on t1.ChapterID = cte.ChapterID 
		inner join 
		(
		   	select t1.ExerciseID
			from dbo.Exercise t1
			inner join dbo.ResourceKen t2 on t1.ExerciseID = t2.ResourceID 
			where t2.[Source] = 'Exercise' and t1.IsDeleted = 0 and ( t2.KenID = @KenID )
			and t1.OCID = @OCID 
		) t2 on t1.ExerciseID = t2.ExerciseID 
		where t1.OCID = @OCID
	end 
    
    

  
   
  
  
GO

/****** Object:  StoredProcedure [dbo].[Exercise_Search]    Script Date: 03/30/2015 08:35:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================                      
-- Author:      王胜辉                      
-- Create date: 20141127                      
-- Description: 获取习题的列表信息                      
-- =============================================                      
CREATE proc [dbo].[Exercise_Search]                      
  @Searchkey nvarchar(200) = '' , --查询关键字                      
  @OCID int = 1 , -- 在线课程编号                      
  @CourseID int = 1 , -- 课程编号                      
  @UserID int =-1 , -- 当前用户身份编号                      
  @ExerciseType int = -1, -- 习题题型                      
  @Diffcult int = -1, -- 难度系统                      
  @Scope int = -1 , -- 适用范围                      
  @ShareRange int = -1 ,                      
  @KeyID int  =  -1  ,-- 标签编号                   
  @Kens nvarchar(200) = '', -- 知识点名称                     
  @Keys nvarchar(200) = '', -- 标签名称                     
  @PageSize int = 20 ,                                            
  @PageIndex int = 1                         
as                       
 SET NOCOUNT ON;                      
                      
   --if( @OCID = 0  )              
   --set @OCID = 1         
                 
DECLARE @lowerLimit INT                      
DECLARE @upperLimit INT                      
                   
SET @lowerLimit = @PageSize * ( @PageIndex - 1 ) + 1                                                                 
SET @upperLimit = @PageSize *   @PageIndex                                   
;                    
                      
                       
 --获取习题列表，满足条件的筛选通过（ f_Cacu_Exercise_GetExeriseList ）,复杂算法必须独立放到外面函数或存储过程中执行                      
 ;                      
 with CTE as                                                          
    (                         
  select Row_Number() OVER( ORDER BY Updatetime Desc ) as rownum , t1.ExerciseID from dbo.Exercise t1                                    
  where t1.ParentID = 0  and         
     ( t1.Conten like '%'+@Searchkey+'%'  or @Searchkey=''  )                      
  and   ( t1.CourseID = @CourseID  or @CourseID < 1   )                       
  and   ( t1.ExerciseType = @ExerciseType or @ExerciseType< 1  )                        
  and   ( t1.Diffcult = @Diffcult or @Diffcult < 1  )                        
  and   ( t1.Scope = @Scope  or @Scope< 1   )                       
  and   ( t1.ShareRange = @ShareRange or @ShareRange < 1  )                      
  --and   ( t1.CreateUserID = @UserID or t1.OwnerUserID = @UserID  )          
  and t1.OCID =  @OCID  and t1.IsDeleted = 0          
  and t1.ParentID = 0          
  and  ( charindex( 'wshgkjqbwhfbxlfrh'+@Kens+'wshgkjqbwhfbxlfrh' , 'wshgkjqbwhfbxlfrh' +t1.Kens + 'wshgkjqbwhfbxlfrh'   )  > 0  or  @Kens = '')              
  and  ( charindex( 'wshgkjqbwhfbxlfrh'+@Keys+'wshgkjqbwhfbxlfrh' , 'wshgkjqbwhfbxlfrh' +t1.Keys + 'wshgkjqbwhfbxlfrh'   )  > 0  or  @Keys = '')              
     
      
 )                      
 select tt1.ExerciseID, tt1.OCID , tt1.CourseID ,tt1.OwnerUserID , tt1.CreateUserID,tt1.CreateUserName , tt1.ParentID ,                      
 tt1.ExerciseType, tt1.ExerciseTypeName as ExerciseTypeName  , tt1.Diffcult,                       
  tt1.Scope, tt1.ShareRange ,  tt1.Keys as keys  , tt1.Kens as Kens , tt1.Conten , tt1.UpdateTime,                      
 ( select count(*) from CTE ) as rowscount  ,  ( select count(*) from CTE ) as RowsCount                     
 from CTE                       
 inner join Exercise tt1 on tt1.ExerciseID = CTE.ExerciseID                      
 WHERE  RowNum between @lowerLimit AND @upperLimit              
             
GO

/****** Object:  StoredProcedure [dbo].[Chapter_ADD]    Script Date: 03/30/2015 08:35:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

        
-- =============================================          
-- Author:      王胜辉,qubo(20150129)          
-- Create date: 20141127          
-- Description: 章节新增          
-- =============================================          
          
CREATE proc [dbo].[Chapter_ADD]          
 @ChapterID int output ,    
 @Orde int output,         
 @OCID int ,        
 @CourseID int,          
 @OwnerUserID int ,        
 @CreateUserID int ,           
 @Title nvarchar(500) ,           
 @ParentID int          
as           
          
SET NOCOUNT ON;   

set @Orde = dbo.[f_Cacu_Chapter_GetOrder]( @OCID,@ParentID  )     
    
insert into dbo.Chapter( OCID ,  CourseID, OwnerUserID , CreateUserID , Title, ParentID, Orde  )          
values (  @OCID , @CourseID, @OwnerUserID , @CreateUserID  , @Title , @ParentID  , @orde )          
      
set @ChapterID = @@identity              
      
          
          
          
           
           
           
GO

/****** Object:  StoredProcedure [dbo].[FileSummary_Get]    Script Date: 03/30/2015 08:35:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

              
--qubo---20150312---add--              
--在线课程资源汇总    
-- FileSummary_Get 1,'2015-02-23','2015-03-07'      
              
CREATE proc [dbo].[FileSummary_Get]    
 @OCID int,    
 @StartDate date,    
 @EndDate date    
as    
    
declare @tb table(FileType int)    
insert into @tb    
select FileType    
from [File]    
where OCID=@OCID and IsDeleted=0    
and CreateDate between @StartDate and @EndDate    

--1 视频 ； 2  word； 3excel；4 ppt ； 5pdf ；6图片；7 音频片；8压缩包；9其他

declare @AllCount int    
select @AllCount=COUNT(*) from @tb 
declare @VideoCount int    
select @VideoCount=COUNT(*) from @tb where FileType=1      
declare @WordCount int    
select @WordCount=COUNT(*) from @tb where FileType=2    
declare @ExcelCount int    
select @ExcelCount=COUNT(*) from @tb where FileType=3  
declare @PPTCount int    
select @PPTCount=COUNT(*) from @tb where FileType=4      
declare @PDFCount int    
select @PDFCount=COUNT(*) from @tb where FileType=5    
declare @PicCount int    
select @PicCount=COUNT(*) from @tb where FileType=6 
declare @AudioCount int    
select @AudioCount=COUNT(*) from @tb where FileType=7      
declare @RarCount int    
select @RarCount=COUNT(*) from @tb where FileType=8    
declare @ElseCount int    
select @ElseCount=COUNT(*) from @tb where FileType=9      
    
select @AllCount as AllCount,    
@VideoCount as VideoCount,    
@AudioCount as AudioCount,    
@WordCount as WordCount,    
@PPTCount as PPTCount,    
@ExcelCount as ExcelCount,    
@PDFCount as PDFCount,    
@PicCount as PicCount,    
@RarCount as RarCount,    
@ElseCount as ElseCount
GO

/****** Object:  StoredProcedure [dbo].[File_Search]    Script Date: 03/30/2015 08:35:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================                    
-- Author:      王胜辉,qubo(20150124)                    
-- Create date: 20141127                    
-- Description: 获取资料的列表信息                    
--- MOD： 20150113  --- 新增参数 @OwnerUserID                  
-- =============================================                    
CREATE proc [dbo].[File_Search]                    
  @Searchkey nvarchar(200) = '' , --查询关键字                    
  @OCID int = 0 , -- 我的资料库courseid=-1  ,OCID=0                   
  @CourseID int = 1 , --课程编号                  
  @FolderID int = 0 , -- 0表示根文件夹下,-1表示全部                    
  @FileType int = -1, -- 文件类型                    
  @UploadTime smalldatetime ='2011-1-1',-- 上传日期 ，需要从业务层计算 >= @UploadTime                    
  @ShareRange int = -1  ,                    
  @UserID int = 1 ,                   
  @OwnerUserID  int = 1 ,  --- 新增参数                  
  @PageSize int = 20 ,                                          
  @PageIndex int = 1                       
as                     
SET NOCOUNT ON;                    
                
DECLARE @iLowerLimit INT                                                                              
DECLARE @iUpperLimit INT                                                                          
                
SET @iLowerLimit = @PageSize*(@PageIndex-1)+1                                                                              
SET @iUpperLimit = @PageSize*@PageIndex                        
;                                  
WITH tb                            
AS (                
 SELECT ROW_NUMBER() OVER (ORDER BY t1.FileID DESC) AS RowNum ,                    
 t1.FileID, t1.OCID, t1.CourseID, t1.FolderID, t1.SubjectID1, t1.SubjectID2,                   
 t1.CreateUserID, t1.CreateUserName, t1.OwnerUserID, t1.FileTitle, t1.FileName, t1.Ext,                   
 t1.FileType, t1.Brief, t1.Keys, t1.FileSize, t1.pingyin, t1.TimeLength, t1.RarIndexPage,                   
 t1.UploadTime, t1.Orde, t1.ShareRange, t1.AllowDownload, t1.ServerID, t1.Clicks,                   
 t1.Downloads, t1.IsTransfer  ,   
 'Redir/filedown.aspx?fid='+cast(FileID as varchar) as  DownURL ,   
 'Redir/fileview.aspx?fid='+cast(FileID as varchar) as  ViewURL  ,  t1.Kens            
 from  dbo.[File] t1                  
 where IsDeleted = 0  and t1.OCID = @OCID                  
 and (t1.FolderID = @FolderID or @FolderID = -1)                   
 and ( t1.FileType = @FileType or @FileType < 1  )                   
 and t1.UploadTime >= @UploadTime                  
 and ( t1.ShareRange = @ShareRange or @ShareRange < 1  )                  
-- and ( t1.CreateUserID = @UserID or ( t1.OwnerUserID = @OwnerUserID and @OCID  >0  ) )               
 and (@Searchkey='' or t1.FileTitle like '%'+@Searchkey+'%')                 
)                
SELECT (SELECT COUNT(1) FROM tb) AS rowscount,*                          
FROM tb                
WHERE RowNum between @iLowerLimit and @iUpperLimit                
              
--文件夹目录导航表              
declare @FolderNavigation table(FolderID int, FolderName nvarchar(200), ParentID int)               
declare @ID int              
set @ID=@FolderID              
while (@ID>0)              
begin              
 insert into @FolderNavigation              
 select FolderID,FolderName,ParentID from Folder where FolderID=@ID and IsDeleted=0            
               
 select @ID=ParentID from Folder where FolderID=@ID              
end              
select * from @FolderNavigation
GO


       