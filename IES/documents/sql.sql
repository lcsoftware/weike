
-- =============================================  
-- Author:      王胜辉  
-- Create date: 20141206  
-- Description: 获取知识点的章节列表信息  
-- =============================================  
create proc IES_RESOURCE.[dbo].[Ken_Chapter_List]  
	 @KenID int = 0 
as 
 
	select distinct t2.*  
	from dbo.ResourceKen t1
	inner join dbo.[Chapter] t2 on t1.ResourceID = t2.ChapterID 
	where [Source] = 'Chapter' and KenID = @KenID and t2.IsDeleted = 0 
   
   
GO

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
  
  
  
go
USE [IES_Resource]
GO
/****** Object:  StoredProcedure [dbo].[Chapter_Ken_List]    Script Date: 02/04/2015 23:22:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:      王胜辉  
-- Create date: 20141206  
-- Description: 获取章节的知识点列表信息  
-- =============================================  
ALTER proc [dbo].[Chapter_Ken_List]  
	 @ChapterID int = 0 ,
	 @UserID int 
as 

	SET NOCOUNT ON;  
  
	--select t2.KenID , t2.Name  , Requirement
	--from Chapter t1
	--inner join dbo.Ken t2 on t1.ChapterID = t2.ChapterID 
	--where t1.ChapterID = @ChapterID
   
    select t2.KenID , t2.Name, Requirement
    from dbo.Ken t2, dbo.ResourceKen t1
    where t1.KenID=t2.KenID 
		and t1.[Source]= 'Chapter' 
		and t1.ResourceID = @ChapterID
		and t2.CreateUserID = @UserID
  
   
  
    