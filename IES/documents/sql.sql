update IES_JW.dbo.Menu set URL = 'content.ken.topic' where MenuID='B24';
update IES_JW.dbo.Menu set URL = 'content.ken.chapter' where MenuID='B25';
 
insert into ResourceDict(ID, Name, NameEn, Source, IsDeleted) select 18, '�����', '�����','Exercise.ExerciseType',0  



USE [IES_Resource]
GO

/****** Object:  StoredProcedure [dbo].[Exercise_Search]    Script Date: 02/13/2015 01:42:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================    
-- Author:      ��ʤ��    
-- Create date: 20141127    
-- Description: ��ȡϰ����б���Ϣ    
-- =============================================    
CREATE proc [dbo].[Exercise_Search]    
	 @Searchkey nvarchar(200) = '' , --��ѯ�ؼ���    
	 @OCID int = -1 , -- ���߿γ̱��    
	 @CourseID int = -1 , -- �γ̱��    
	 @UserID int =-1 , -- ��ǰ�û���ݱ��    
	 @ExerciseType int = -1, -- ϰ������    
	 @Diffcult int = -1, -- �Ѷ�ϵͳ    
	 @Scope int = -1 , -- ���÷�Χ    
	 @ShareRange int = -1 ,    
	 @KeyID int  =  -1  ,-- ��ǩ��� 
	 @Kens nvarchar(200) = '', -- ֪ʶ������   
	 @Keys nvarchar(200) = '', -- ��ǩ����   
	 @PageSize int = 20 ,                          
	 @PageIndex int = 1       
as     
 SET NOCOUNT ON;    
    
 DECLARE @lowerLimit INT                                        
 DECLARE @upperLimit INT       
     
    SET @lowerLimit = @PageSize * ( @PageIndex - 1 )                                        
    SET @upperLimit = @PageSize *   @PageIndex         
    
     
 --��ȡϰ���б�����������ɸѡͨ���� f_Cacu_Exercise_GetExeriseList ��,�����㷨��������ŵ����溯����洢������ִ��    
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
-- Author:      ��ʤ��
-- Create date: 20141205
-- Description: ϰ������ɾ��
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
-- Author:      ��ʤ��,qubo(20150124)          
-- Create date: 20141127          
-- Description: ��ȡ���ϵ��б���Ϣ          
--- MOD�� 20150113  --- �������� @OwnerUserID        
-- =============================================          
CREATE proc [dbo].[File_Search]          
  @Searchkey nvarchar(200) = '' , --��ѯ�ؼ���          
  @OCID int = 0 , -- �ҵ����Ͽ�courseid=-1  ,OCID=0         
  @CourseID int = 1 , --�γ̱��        
  @FolderID int = 0 , -- 0��ʾ���ļ�����,-1��ʾȫ��          
  @FileType int = -1, -- �ļ�����          
  @UploadTime smalldatetime ='2011-1-1',-- �ϴ����� ����Ҫ��ҵ������ >= @UploadTime          
  @ShareRange int = -1  ,          
  @UserID int = 1 ,         
  @OwnerUserID  int = 1 ,  --- ��������        
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
 t1.Downloads, t1.IsTransfer  ,'' as DownURL , '' as ViewURL  , '���Խ�����֪ʶ��' as Kens  
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
    
--�ļ���Ŀ¼������    
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
-- Author:      ��ʤ��  
-- Create date: 20141127  
-- Description: ��ȡ�����ļ����б���Ϣ  
-- =============================================  
CREATE proc [dbo].[Folder_List]  
 @OCID int = 0 , -- �ҵ����Ͽ�OCID=0   
 @ParentID int = 0 , -- ��ʾ���ļ�����  
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


