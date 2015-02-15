update IES_JW.dbo.Menu set URL = 'content.ken.topic' where MenuID='B24';
update IES_JW.dbo.Menu set URL = 'content.ken.chapter' where MenuID='B25';
 
insert into ResourceDict(ID, Name, NameEn, Source, IsDeleted) select 18, '简答题', '简答题','Exercise.ExerciseType',0  



USE [IES_Resource]
GO

/****** Object:  StoredProcedure [dbo].[Exercise_Search]    Script Date: 02/13/2015 01:42:16 ******/
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
	 @OCID int = -1 , -- 在线课程编号    
	 @CourseID int = -1 , -- 课程编号    
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
    
 DECLARE @lowerLimit INT                                        
 DECLARE @upperLimit INT       
     
    SET @lowerLimit = @PageSize * ( @PageIndex - 1 )                                        
    SET @upperLimit = @PageSize *   @PageIndex         
    
     
 --获取习题列表，满足条件的筛选通过（ f_Cacu_Exercise_GetExeriseList ）,复杂算法必须独立放到外面函数或存储过程中执行    
 ;    
 with CTE as                                        
    (       
  select Row_Number() OVER( ORDER BY Updatetime Desc ) as rownum , t1.ExerciseID from dbo.Exercise t1    
  inner join     
       ( select ExerciseID from dbo.f_Cacu_Exercise_GetExeriseList( @Searchkey ,@OCID  ,@CourseID, @UserID  ,  
        @ExerciseType ,@Diffcult ,@Scope ,@ShareRange ,@KeyID  ) )     
   t2    
  on t1.ExerciseID = t2.ExerciseID    
 )    
 select tt1.ExerciseID, tt1.OCID , tt1.CourseID ,tt1.OwnerUserID , tt1.CreateUserID , tt1.ParentID ,    
 tt1.ExerciseType, tt1.ExerciseTypeName as ExerciseTypeName  , tt1.Diffcult,     
  tt1.Scope, tt1.ShareRange ,  tt1.Keys as keys  , tt1.Kens as Kens , tt1.Conten , tt1.UpdateTime,    
 ( select count(*) from CTE ) as rowscount     
 from CTE     
 inner join Exercise tt1 on tt1.ExerciseID = CTE.ExerciseID    
 where CTE.rownum >= @lowerLimit AND CTE.rownum  <= @upperLimit and tt1.IsDeleted=0  
     
     
     
    
     
    
     
    
GO

/****** Object:  StoredProcedure [dbo].[Exercise_Batch_Del]    Script Date: 02/13/2015 01:42:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:      王胜辉
-- Create date: 20141205
-- Description: 习题批量删除
-- =============================================

CREATE proc  [dbo].[Exercise_Batch_Del]
	 @ExerciseIDS  varchar(max)
as 


	SET NOCOUNT ON;
	
	update  t1  set IsDeleted = 1 from Exercise  t1
	inner join ( select a from dbo.f_split(@ExerciseIDS , ',') ) t2 
	on t1.ExerciseID = t2.a
	    


GO




USE [IES_Resource]
GO

/****** Object:  StoredProcedure [dbo].[File_Search]    Script Date: 02/15/2015 08:48:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[File_Search]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[File_Search]
GO

/****** Object:  StoredProcedure [dbo].[Folder_List]    Script Date: 02/15/2015 08:48:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Folder_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Folder_List]
GO

USE [IES_Resource]
GO

/****** Object:  StoredProcedure [dbo].[File_Search]    Script Date: 02/15/2015 08:48:55 ******/
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
 t1.Downloads, t1.IsTransfer  ,'' as DownURL , '' as ViewURL  , '测试界面用知识点' as Kens  
 from  dbo.[File] t1        
 where IsDeleted = 0  and t1.OCID = @OCID        
 and (t1.FolderID = @FolderID or @FolderID = -1)         
 and ( t1.FileType = @FileType or @FileType < 1  )         
 and t1.UploadTime >= @UploadTime        
 and ( t1.ShareRange = @ShareRange or @ShareRange < 1  )        
 and ( t1.CreateUserID = @UserID or ( t1.OwnerUserID = @OwnerUserID and @OCID  >0  ) )     
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

/****** Object:  StoredProcedure [dbo].[Folder_List]    Script Date: 02/15/2015 08:48:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:      王胜辉  
-- Create date: 20141127  
-- Description: 获取资料文件夹列表信息  
-- =============================================  
CREATE proc [dbo].[Folder_List]  
 @OCID int = 0 , -- 我的资料库OCID=0   
 @ParentID int = 0 , -- 表示根文件夹下  
 @UserID int = 0   ,
 @ShareRange int = 0 
as   
 SET NOCOUNT ON;  
  
  
 select FolderID, OCID, CourseID, CreateUserID, ShareRange , OwnerUserID, ParentID, FolderName, Brief, Orde, CreateTime  
 from dbo.Folder  
 where IsDeleted = 0 AND (@ParentID = -1 or ParentID = @ParentID) and OCID = @OCID   
 and ( (  CreateUserID = @UserID or OwnerUserID = @UserID  ) or @UserID = 0 )
 and ( ShareRange = @ShareRange or @ShareRange < 1 )

GO


