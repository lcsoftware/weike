
INSERT INTO [IES_Resource].[dbo].[Key]([OCID],[CourseID],[OwnerUserID],[CreateUserID],[Name])
     select 1,1,0,0,'标签01' 
     union select 1,1,0,0,'标签02' 
     union select 1,1,0,0,'标签03' 
     union select 1,1,0,0,'标签04' 
     union select 1,1,0,0,'标签04' 
     union select 2,2,0,0,'标签05' 
     union select 2,2,0,0,'标签06' 
     union select 2,2,0,0,'标签07' 
     union select 2,2,0,0,'标签08' 

update ResourceDict set ID=19 where Source='Exercise.ExerciseType' and ID=4
update ResourceDict set ID=10 where Source='Exercise.ExerciseType' and ID=5
insert into ResourceDict(ID, Name, NameEn, [Source],IsDeleted)
select 18, '简答题', '简答题', 'Exercise.ExerciseType', 0
union select 12, '听力题', '听力题', 'Exercise.ExerciseType', 0
union select 10, '问答题', '问答题', 'Exercise.ExerciseType', 0
union select 5, '填空题', '填空题', 'Exercise.ExerciseType', 0
union select 4, '填空客观题', '填空客观题', 'Exercise.ExerciseType', 0
union select 6, '连线题', '连线题', 'Exercise.ExerciseType', 0
delete from ResourceDict where Source='Exercise.ExerciseType' and ID  not in (18,19,10,12,1,4,5,6)	 


insert into ResourceKey(ResourceID, Source, KeyID)	
select 2, 'Exercise', 1
union select 2, 'Exercise', 2
union select 3, 'Exercise', 3
union select 4, 'Exercise', 4