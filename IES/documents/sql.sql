
-- =============================================
-- Author:      ��ʤ��
-- Create date: 20150314
-- Description: �ļ���Ӧ���½���֪ʶ��
-- =============================================
create proc File_Chapter_Ken
@FileId int
as 
declare @KenID int
declare @ChapterID int

select @KenID = 0, @ChapterID = 0
select @KenID=KenID from ResourceKen where ResourceID=@FileId and Source='File'
select @ChapterID=ChapterID from FileChapter where FileID=@FileId
select @KenID as KenID, @ChapterID as ChapterID