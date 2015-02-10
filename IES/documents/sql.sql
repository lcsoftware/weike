update IES_JW.dbo.Menu set URL = 'content.ken.topic' where MenuID='B24';
update IES_JW.dbo.Menu set URL = 'content.ken.chapter' where MenuID='B25';
   
GO
drop proc dbo.ResourceKen_List_OCID
GO
-- =============================================
-- Author:      ��ʤ��
-- Create date: 20150128
-- Description: ��ѯ����ocid�µ�֪ʶ�����½ڶ�Ӧ��ϵ
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
  
  
   
   
  UPDATE ResourceDict SET ID=4 WHERE Name='��ʽ����' AND NameEn='Exam'
    
	
	
USE [IES_Resource]
GO
/****** Object:  StoredProcedure [dbo].[Exercise_Line_S_Edit]    Script Date: 02/10/2015 11:02:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
--  Author:      ��ʤ��
--  Create date: 20141128
--  ������༭  
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
	
	
	-----��ȡ��ȷ�� �����浽������
	declare @correctanswer as varchar(500)
	set    @correctanswer = ''
	select @correctanswer = cast(ChoiceID as varchar) +  ',' +  @correctanswer 
	from ExerciseChoice 
	where ExerciseID = @ExerciseID  and IsDeleted = 0 and Grou > 0 
	order by Grou , ChoiceID asc 
	
	
	update dbo.Exercise set Answer = LEFT(@correctanswer,LEN(@correctanswer)-1)
	where  ExerciseID = @ExerciseID 
	
	
	
	
	
-- =============================================    
-- Author:      ��ʤ��    
-- Create date: 20141128    
-- Description: ������   

--- ѡ������Ϊ�����ʾ��ѡ��Ҫɾ��    
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
     
     
 ---��ȡ��ȷ�� �����浽������    
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
-- Author:      ��ʤ��      
-- Create date: 20141201      
-- Description: �ж��ļ��Ƿ�����  
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
-- Author:      ��ʤ��        
-- Create date:   
-- Description: ��ȡ��Щ�ļ��б��Ѿ���������  
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

--- ��ѡ�⡢��ѡ����ϸ��Ϣ  
---- wsh  
  
alter proc [dbo].[Exercise_MultipleChoice_Get]  
 @ExerciseID int  
as   
 SET NOCOUNT ON;  
   
 -- ������Ϣ  
 select ExerciseID, OCID, CourseID, OwnerUserID, CreateUserID, CreateUserName, ParentID,   
 ExerciseType , ExerciseTypeName, ChapterID, ChapterName, Diffcult,   
 Scope , ShareRange , Keys, Kens , Conten , Analysis,   
 Score , IsRand , UpdateTime   
 from dbo.Exercise t1  
 where t1.ExerciseID = @ExerciseID and IsDeleted = 0   
   
   
   
 --- ����Ĺؼ���,�����֪ʶ��  
   
 exec [Exercise_Attachment_Key_Ken_List]  @ExerciseID  
   
   
 --- ѡ����Ϣ  
 select  ChoiceID , ExerciseID , Conten , IsCorrect, OrderNum  ,Grou 
 from dbo.ExerciseChoice  
 where ExerciseID = @ExerciseID and IsDeleted = 0   
   
   
   
   
   
   
USE IES_Resource
go
   -- =============================================  
--  Author:      ��ʤ��  
--  Create date: 20141128  
--  ������༭    
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
   
   
 -----��ȡ��ȷ�� �����浽������  
 declare @correctanswer as varchar(500)  
 set    @correctanswer = ''  
 select @correctanswer = cast(ChoiceID as varchar) +  ',' +  @correctanswer   
 from ExerciseChoice   
 where ExerciseID = @ExerciseID  and IsDeleted = 0 and Grou > 0   
 order by Grou , ChoiceID asc   
   
   
 update dbo.Exercise set Answer = LEFT(@correctanswer,LEN(@correctanswer)-1)  
 where  ExerciseID = @ExerciseID   