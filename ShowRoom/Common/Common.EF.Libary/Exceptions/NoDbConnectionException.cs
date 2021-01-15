using System;
using System.Runtime.Serialization;

namespace Common.EF.Library.Exceptions
{
    [Serializable]
    public class NoDbConnectionException
        : Exception
    {


        public NoDbConnectionException(string message) : base(message)
        {
        }

        public NoDbConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoDbConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}