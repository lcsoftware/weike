using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;
using IES.AOP.G2S.Exceptions;

namespace IES.AOP.G2S
{
    public class ExceptionCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn result = input.CreateMethodReturn(null);
            try
            {
                result = getNext()(input, getNext);
            }
            catch (Exception e)
            {
                //处理异常 1直接将异常抛出
                result = input.CreateExceptionMethodReturn(new Exception(e.Message));

                //处理异常 2根据业务需要将异常处理
                //code:
                //code:
                //返回空值
                //result = input.CreateMethodReturn(null);
            }
            return result;
        }

        public int Order
        {
            set;
            get;
        }
    }
    public class ExceptionCallHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(Microsoft.Practices.Unity.IUnityContainer container)
        {
            return new ExceptionCallHandler() { Order = this.Order };
        }
    }
}
