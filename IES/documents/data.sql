
INSERT INTO [IES_Resource].[dbo].[Key]([OCID],[CourseID],[OwnerUserID],[CreateUserID],[Name])
     select 1,1,0,0,'��ǩ01' 
     union select 1,1,0,0,'��ǩ02' 
     union select 1,1,0,0,'��ǩ03' 
     union select 1,1,0,0,'��ǩ04' 
     union select 1,1,0,0,'��ǩ04' 
     union select 2,2,0,0,'��ǩ05' 
     union select 2,2,0,0,'��ǩ06' 
     union select 2,2,0,0,'��ǩ07' 
     union select 2,2,0,0,'��ǩ08' 

update ResourceDict set ID=19 where Source='Exercise.ExerciseType' and ID=4
update ResourceDict set ID=10 where Source='Exercise.ExerciseType' and ID=5
insert into ResourceDict(ID, Name, NameEn, [Source],IsDeleted)
select 18, '�����', '�����', 'Exercise.ExerciseType', 0
union select 12, '������', '������', 'Exercise.ExerciseType', 0
union select 10, '�ʴ���', '�ʴ���', 'Exercise.ExerciseType', 0
union select 5, '�����', '�����', 'Exercise.ExerciseType', 0
union select 4, '��տ͹���', '��տ͹���', 'Exercise.ExerciseType', 0
union select 6, '������', '������', 'Exercise.ExerciseType', 0
delete from ResourceDict where Source='Exercise.ExerciseType' and ID  not in (18,19,10,12,1,4,5,6)	 


insert into ResourceKey(ResourceID, Source, KeyID)	
select 2, 'Exercise', 1
union select 2, 'Exercise', 2
union select 3, 'Exercise', 3
union select 4, 'Exercise', 4