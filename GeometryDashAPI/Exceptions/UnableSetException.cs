using System;
using System.Runtime.Serialization;

namespace GeometryDashAPI.Exceptions
{
    [Serializable]
    internal class UnableSetException : Exception
    {
        public UnableSetException()
        {
        }

        public UnableSetException(string message) : base(message)
        {
        }

        public UnableSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnableSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
