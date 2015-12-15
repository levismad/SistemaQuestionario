using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SQ.Core.Exceptions
{

    [Serializable]
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException()
            : base() { }

        public ForbiddenAccessException(string message)
            : base(message) { }

        public ForbiddenAccessException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ForbiddenAccessException(string message, Exception innerException)
            : base(message, innerException) { }

        public ForbiddenAccessException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ForbiddenAccessException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}