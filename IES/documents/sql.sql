
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
   
  
  