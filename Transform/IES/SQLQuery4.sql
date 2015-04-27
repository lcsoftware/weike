        
-- 获取用户的在线课程的角色        
CREATE proc [dbo].[OC_ALLRole_Get]       
 @UserID int ,  
 @OCID int = 0         
as         
        
  -- 0 课程创建人、 1 课程负责人 ;   不受控制        
  -- 2 只能删自己创建        
  ---3 根据授权来判断         
              
 select [Role] ,OCID                 
 from   IES.dbo.OCTeam                 
 where  [Status] = 2  and UserID = @UserID  and ( OCID =  @OCID or  @OCID = 0 )        
         