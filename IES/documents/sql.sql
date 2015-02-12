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

