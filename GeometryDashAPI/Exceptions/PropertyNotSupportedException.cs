using System;
using System.Runtime.Serialization;

namespace GeometryDashAPI.Exceptions
{
    [Serializable]
    internal class PropertyNotSupportedException : Exception
    {
        public PropertyNotSupportedException()
        {
        }

        internal PropertyNotSupportedException(string message) : base(message)
        {
        }

        public PropertyNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PropertyNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
