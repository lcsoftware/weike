USE [IES_Resource]
GO
/****** Object:  StoredProcedure [dbo].[Folder_ParentID_Upd]    Script Date: 05/20/2015 10:06:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:      王胜辉  
-- Create date: 20141215    [Folder_ParentID_Upd] 25,20  
-- Description: 文件夹移动  
-- =============================================  
  
ALTER proc [dbo].[Folder_ParentID_Upd]  
  @FolderID int ,  
  @OCID int ,   
  @ParentID  int  
as   
 SET NOCOUNT ON;  
 
 
  declare @sourceOCID int 
  select  @sourceOCID = OCID from Folder where FolderID = @FolderID
 


 
 declare @subfolder table ( FolderID int , ParentID int  )  
   
 declare @notinchildfolder int   
 set @notinchildfolder = 0   
 ;  
     
 with    
 cte as     
 (    
		 -- 当前节点的所有子节点  
	  select FolderID,  ParentID   from dbo.Folder       
	  where  ( ParentID = @FolderID  ) and IsDeleted = 0  and OCID = @sourceOCID
	  union all    
	  select t2.FolderID,  t2.ParentID   
	  from Folder t2,cte t1    
	  where t1.ParentID = t2.FolderID and OCID = @sourceOCID
 )  
 insert into  @subfolder ( FolderID , ParentID   ) 
 select FolderID , ParentID  from cte  
   
 
   
   
   
if not exists (  select * from @subfolder where FolderID = @ParentID )
begin  
	  declare @orde int   
	  select @orde = ISNULL( MAX( orde ) , 1 )  
	  from Folder  
	  where  ParentID = @ParentID and IsDeleted = 0 and OCID =  @OCID
	    
	  update Folder set ParentID = @ParentID, OCID=@OCID  , Orde = @orde  
	  where FolderID = @FolderID  
	  
	  
	  update t1  set t1.OCID = @OCID from Folder t1 
	  inner join @subfolder t2 on t1.FolderID = t2.FolderID 
	  
	  update t1  set t1.OCID = @OCID  from [File] t1
	  inner join @subfolder t2 on t1.FolderID = t2.FolderID 
	  
 end  
