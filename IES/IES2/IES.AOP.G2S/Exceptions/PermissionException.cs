using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.AOP.G2S.Exceptions
{
    public class PermissionException : Exception
    {
        public int UserId { set; get; }
        public int ErrorLevel { set; get; }

        public PermissionException()
            : this(0, null)
        { }
        public PermissionException(int userId, string message)
            : this(userId, 1, message, null)
        { }
        public PermissionException(int userId, int errorLevel, string message)
            : this(userId, errorLevel, message, null)
        { }
        public PermissionException(int userId, int errorLevel, string message, Exception e)
            : base(message, e)
        {
            this.UserId = userId;
            this.ErrorLevel = errorLevel;
        }
    }
}
