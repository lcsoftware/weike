-- =============================================    
-- Author:      王胜辉    
-- Create date: 20141206    
-- Description: 根据知识点编号、章节编号 获取关联的习题列表    
-- =============================================    
alter proc [dbo].[Exercise_ChapterID_KenID_List]    
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
  select  t1.ExerciseID, t1.Conten, t1.ExerciseType , t1.Diffcult  from cte   
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
  select  t1.ExerciseID, t1.Conten, t1.ExerciseType  , t1.CreateUserID,t1.CreateUserName , t1.Diffcult from cte   
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
      
      
  
    
     
go
USE [IES_Resource]
GO
/****** Object:  StoredProcedure [dbo].[Exercise_KenID_ChapterID_List]    Script Date: 04/01/2015 16:09:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================    
-- Author:      王胜辉    
-- Create date: 20141206    
-- Description: 根据知识点编号、章节编号 获取关联的习题列表    
-- =============================================    
ALTER proc [dbo].[Exercise_KenID_ChapterID_List]    
  @KenID int = 0 ,  
  @ChapterID int = 0,  
  @UserID int ,  
  @OCID int   
as   
  
 SET NOCOUNT ON;    
    
    -- 获取和知识点、章节关联的文件  
 if( @ChapterID > 0  )  
 begin  
    
    select t1.ExerciseID, t1.Conten, t1.ExerciseType  ,t1.Diffcult 
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
     
     
    
     
        