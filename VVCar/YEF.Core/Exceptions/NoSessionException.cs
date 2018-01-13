using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core
{
    [Serializable]
    public class NoSessionException: Exception
    {
         public NoSessionException()
        { }

        public NoSessionException(string message)
            : base(message)
        { }

        public NoSessionException(string message, Exception inner)
            : base(message, inner)
        { }

        protected NoSessionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
