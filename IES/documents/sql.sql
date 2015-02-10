update IES_JW.dbo.Menu set URL = 'content.ken.topic' where MenuID='B24';
update IES_JW.dbo.Menu set URL = 'content.ken.chapter' where MenuID='B25';
   
GO
drop proc dbo.ResourceKen_List_OCID
GO
-- =============================================
-- Author:      王胜辉
-- Create date: 20150128
-- Description: 查询所有ocid下的知识点与章节对应关系
-- =============================================
Create proc [dbo].[ResourceKen_List]
  	@OCID int = 1,
  	@Source varchar(50) = ''
as 

	SET NOCOUNT ON;
 
	
	select a.* from ResourceKen a, Ken b 
	where b.OCID=@OCID
	and (@Source = '' or [Source] = @Source)
	and a.KenID=b.KenID
  
  
   
   
  UPDATE ResourceDict SET ID=4 WHERE Name='正式考试' AND NameEn='Exam'
    
	
	
USE [IES_Resource]
GO
/****** Object:  StoredProcedure [dbo].[Exercise_Line_S_Edit]    Script Date: 02/10/2015 11:02:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
--  Author:      王胜辉
--  Create date: 20141128
--  连线题编辑  
-- =============================================
create proc [dbo].[Exercise_Line_S_Edit]
	@ExerciseID int  , 
	@Content nvarchar(max)	
as 
	SET NOCOUNT ON;
	
	
	declare @tb table (rownum int identity(1,1), Choice nvarchar(max) )
	declare @rowscount int 
	
	insert into @tb( Choice )
	select a  from dbo.f_split( @Content , 'wshgkjqbwhfbxlfrh_a' )
	
	select @rowscount = COUNT(*) from @tb
	declare @i int , @Choice nvarchar(max) , @ChoiceID int , @GroupNum int ,  @InnerContent nvarchar(max)
	set  @i = 1 
	
	while ( @i <= @rowscount )
	begin
	   	select @Choice = Choice from @tb where rownum = @i
	   	
	   	select @ChoiceID = IES.dbo.SYS_StringHead( @Choice, 'wshgkjqbwhfbxlfrh_b' )
		select @InnerContent =   IES.dbo.SYS_StringHead( IES.dbo.SYS_StringTail( @Choice, 'wshgkjqbwhfbxlfrh_b' ) ,  'wshgkjqbwhfbxlfrh_c')
		select @GroupNum =  IES.dbo.SYS_StringTail( @Choice, 'wshgkjqbwhfbxlfrh_c' )
	   
		if( @ChoiceID < 1  )	
		begin
			insert into  dbo.ExerciseChoice( ExerciseID, Conten, Grou , IsCorrect    )
			values ( @ExerciseID , @InnerContent , @GroupNum   , 1  )
		end 
		else
		begin
			if( @InnerContent <> char(32) )
				update ExerciseChoice
				set Conten = @InnerContent , Grou = @GroupNum  
				where ChoiceID = @ChoiceID	
			else
				update   ExerciseChoice set IsDeleted = 1
				where ChoiceID = @ChoiceID	 
				
		end 
		set @i = @i + 1 
	end 
	
	
	-----获取正确答案 并保存到主表中
	declare @correctanswer as varchar(500)
	set    @correctanswer = ''
	select @correctanswer = cast(ChoiceID as varchar) +  ',' +  @correctanswer 
	from ExerciseChoice 
	where ExerciseID = @ExerciseID  and IsDeleted = 0 and Grou > 0 
	order by Grou , ChoiceID asc 
	
	
	update dbo.Exercise set Answer = LEFT(@correctanswer,LEN(@correctanswer)-1)
	where  ExerciseID = @ExerciseID 
	
	
	
	
	
-- =============================================    
-- Author:      王胜辉    
-- Create date: 20141128    
-- Description: 排序题   

--- 选项内容为空则表示该选项要删除    
 --- =============================================    
create proc [dbo].[Exercise_Order_S_Edit]    
  @ExerciseID int  ,     
  @Content nvarchar(max)     
as     
 SET NOCOUNT ON;    
     
     
 declare @tb table (rownum int identity(1,1), Choice nvarchar(max) )    
 declare @rowscount int     
     
 insert into @tb( Choice )    
 select a  from dbo.f_split( @Content , 'wshgkjqbwhfbxlfrh_a' )    
     
 select @rowscount = COUNT(*) from @tb    
 declare @i int , @Choice nvarchar(max) , @ChoiceID int , @Order int ,@InnerContent nvarchar(max)    
 set  @i = 1     
     
 while ( @i <= @rowscount )    
 begin    
     select @Choice = Choice from @tb where rownum = @i    
         
     select @ChoiceID = IES.dbo.SYS_StringHead( @Choice, 'wshgkjqbwhfbxlfrh_b' )    
	 select @InnerContent =   IES.dbo.SYS_StringHead( IES.dbo.SYS_StringTail( @Choice, 'wshgkjqbwhfbxlfrh_b' ) ,  'wshgkjqbwhfbxlfrh_C')    
	 select @Order =  IES.dbo.SYS_StringTail( @Choice, 'wshgkjqbwhfbxlfrh_c' )    
        
  if( @ChoiceID < 1  )     
  begin    
	   insert into  dbo.ExerciseChoice( ExerciseID, Conten, IsCorrect ,OrderNum  )    
	   values ( @ExerciseID , @InnerContent, 1 , @Order   )    
  end     
  else    
  begin    
   if( @InnerContent <> char(32) )    
		update ExerciseChoice    
		set Conten = @InnerContent , OrderNum = @Order    
		where ChoiceID = @ChoiceID     
	   else    
		update   ExerciseChoice set IsDeleted = 1    
		where ChoiceID = @ChoiceID      
  end     
  set @i = @i + 1     
 end     
     
     
 ---获取正确答案 并保存到主表中    
 declare @correctanswer as varchar(500)    
 set @correctanswer = ''    
 select @correctanswer = cast(ChoiceID as varchar) +  ',' +  @correctanswer     
 from ExerciseChoice  
 where ExerciseID = @ExerciseID and IsCorrect = 1  and IsDeleted = 0  
 order by OrderNum asc      
     
     
 update dbo.Exercise set Answer = LEFT(@correctanswer,LEN(@correctanswer)-1)    
 where  ExerciseID = @ExerciseID     
     
     
 
 USE IES
go
      
-- =============================================      
-- Author:      王胜辉      
-- Create date: 20141201      
-- Description: 判断文件是否被引用  
-- =============================================      
      
create FUNCTION [dbo].[File_FileIsRef]      
(        
 @FileID int   
)        
RETURNS int                          
 BEGIN        
  DECLARE @Result int     
  set   @Result = 0    
        
 if exists (   
  select * from IES_CC.dbo.OCMoocFile   
  where FileID = @FileID  
  )  
  set @Result = 1    
      
  RETURN @Result        
 END
 
 
 USE IES
go
             
-- =============================================        
-- Author:      王胜辉        
-- Create date:   
-- Description: 获取那些文件列表已经被引用了  
-- =============================================        
create function [dbo].[File_FileIsRef_List]            
(            
 @FileIDS varchar(max)   
)              
returns @tb table(   ID  int    )            
as             
begin            
       
  insert into @tb( ID )    
  select t1.FileID from IES_CC.dbo.OCMoocFile t1  
  inner join   
  (  
   select a from dbo.f_split( @FileIDS, ',' )  
  ) t2 on t1.FileID = t2.a  
  
    return             
end 



USE IES_Resource
go

--- 单选题、多选题详细信息  
---- wsh  
  
alter proc [dbo].[Exercise_MultipleChoice_Get]  
 @ExerciseID int  
as   
 SET NOCOUNT ON;  
   
 -- 基本信息  
 select ExerciseID, OCID, CourseID, OwnerUserID, CreateUserID, CreateUserName, ParentID,   
 ExerciseType , ExerciseTypeName, ChapterID, ChapterName, Diffcult,   
 Scope , ShareRange , Keys, Kens , Conten , Analysis,   
 Score , IsRand , UpdateTime   
 from dbo.Exercise t1  
 where t1.ExerciseID = @ExerciseID and IsDeleted = 0   
   
   
   
 --- 大题的关键字,大题的知识点  
   
 exec [Exercise_Attachment_Key_Ken_List]  @ExerciseID  
   
   
 --- 选项信息  
 select  ChoiceID , ExerciseID , Conten , IsCorrect, OrderNum  ,Grou 
 from dbo.ExerciseChoice  
 where ExerciseID = @ExerciseID and IsDeleted = 0   
   
   
   
   
   
   
USE IES_Resource
go
   -- =============================================  
--  Author:      王胜辉  
--  Create date: 20141128  
--  连线题编辑    
-- =============================================  
alter proc [dbo].[Exercise_Line_S_Edit]  
 @ExerciseID int  ,   
 @Content nvarchar(max)   
as   
 SET NOCOUNT ON;  
   
   
 declare @tb table (rownum int identity(1,1), Choice nvarchar(max) )  
 declare @rowscount int   
   
 insert into @tb( Choice )  
 select a  from dbo.f_split( @Content , 'wshgkjqbwhfbxlfrh_a' )  
   
 select @rowscount = COUNT(*) from @tb  
 declare @i int , @Choice nvarchar(max) , @ChoiceID int , @GroupNum int ,  @InnerContent nvarchar(max)  
 set  @i = 1   
   
 while ( @i <= @rowscount )  
 begin  
     select @Choice = Choice from @tb where rownum = @i  
       
     select @ChoiceID = IES.dbo.SYS_StringHead( @Choice, 'wshgkjqbwhfbxlfrh_b' )  
  select @InnerContent =   IES.dbo.SYS_StringHead( IES.dbo.SYS_StringTail( @Choice, 'wshgkjqbwhfbxlfrh_b' ) ,  'wshgkjqbwhfbxlfrh_c')  
  select @GroupNum =  IES.dbo.SYS_StringTail( @Choice, 'wshgkjqbwhfbxlfrh_c' )  
      
  if( @ChoiceID < 1  )   
  begin  
   insert into  dbo.ExerciseChoice( ExerciseID, Conten, Grou , IsCorrect    )  
   values ( @ExerciseID , @InnerContent , @GroupNum   , 1  )  
  end   
  else  
  begin  
   if( @InnerContent <> char(32) )  
    update ExerciseChoice  
    set Conten = @InnerContent , Grou = @GroupNum    
    where ChoiceID = @ChoiceID   
   else  
   begin
		declare @num int 
		select  @num = Grou from ExerciseChoice where  ChoiceID = @ChoiceID   
		update  ExerciseChoice set IsDeleted = 1  
		where   Grou = @num and ExerciseID = @ExerciseID
   end 
      
  end   
  set @i = @i + 1   
 end   
   
   
 -----获取正确答案 并保存到主表中  
 declare @correctanswer as varchar(500)  
 set    @correctanswer = ''  
 select @correctanswer = cast(ChoiceID as varchar) +  ',' +  @correctanswer   
 from ExerciseChoice   
 where ExerciseID = @ExerciseID  and IsDeleted = 0 and Grou > 0   
 order by Grou , ChoiceID asc   
   
   
 update dbo.Exercise set Answer = LEFT(@correctanswer,LEN(@correctanswer)-1)  
 where  ExerciseID = @ExerciseID   