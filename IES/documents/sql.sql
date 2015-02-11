update IES_JW.dbo.Menu set URL = 'content.ken.topic' where MenuID='B24';
update IES_JW.dbo.Menu set URL = 'content.ken.chapter' where MenuID='B25';
 
insert into ResourceDict(ID, Name, NameEn, Source, IsDeleted) select 18, '¼ò´ğÌâ', '¼ò´ğÌâ','Exercise.ExerciseType',0  