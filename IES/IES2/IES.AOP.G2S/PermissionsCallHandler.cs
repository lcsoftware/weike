using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;
using IES.AOP.G2S.Exceptions;

namespace IES.AOP.G2S
{
    /// <summary>
    /// 提供对用户操作权限的处理类
    /// </summary>
    public class PermissionsCallHandler : ICallHandler
    {
        public int Order
        {
            set;
            get;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            System.Web.HttpContext.Current.Session["userid"].ToString() ;


            string a = input.MethodBase.ToString();

            if (input.Inputs.Count == 0)
                throw new PermissionException(0, "检测用户权限时,参数异常！！！");


            if (System.Web.HttpContext.Current.Session["userid"] == null )
                throw new PermissionException( 1  , "检测权限时错误,用户ID异常！！！");

            IMethodReturn result = null;
            //开始判断权限  权限判断逻辑code:
            //
            //
            //如果权限通过 
            if (System.Web.HttpContext.Current.Session["userid"].ToString() == "1")
            {
                result = getNext()(input, getNext);
            }
            else
            //否则没有权限
            {
                throw new PermissionException(1, "该用户没有权限！！！");
            }
            return result;
        }
    }


    public class PermissionsCallHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(Microsoft.Practices.Unity.IUnityContainer container)
        {
            return new PermissionsCallHandler() { Order = this.Order };
        }
    }
}
