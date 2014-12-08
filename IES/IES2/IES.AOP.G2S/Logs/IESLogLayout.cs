using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Layout;

namespace IES.AOP.G2S.Logs
{
    public class IESLogLayout : PatternLayout
    {
        public IESLogLayout()
        {
            this.AddConverter("property", typeof(IESLogPatternConverter));
        }
    }
}
