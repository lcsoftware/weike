        
-- ��ȡ�û������߿γ̵Ľ�ɫ        
CREATE proc [dbo].[OC_ALLRole_Get]       
 @UserID int ,  
 @OCID int = 0         
as         
        
  -- 0 �γ̴����ˡ� 1 �γ̸����� ;   ���ܿ���        
  -- 2 ֻ��ɾ�Լ�����        
  ---3 ������Ȩ���ж�         
              
 select [Role] ,OCID                 
 from   IES.dbo.OCTeam                 
 where  [Status] = 2  and UserID = @UserID  and ( OCID =  @OCID or  @OCID = 0 )        
         