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
    