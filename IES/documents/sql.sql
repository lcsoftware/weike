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

