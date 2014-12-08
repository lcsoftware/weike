using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.AOP.G2S.Factorys
{
    internal class IESLogServerFactory : LogServerFactory<ILogServer<IESLogConent>, IESLogServer>
    {
        public IESLogServerFactory(IServerFactoryCache cache)
            : base(cache)
        { }
    }
}
