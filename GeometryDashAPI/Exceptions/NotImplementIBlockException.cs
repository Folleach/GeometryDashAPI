using System;
using System.Runtime.Serialization;

namespace GeometryDashAPI.Exceptions
{
    [Serializable]
    internal class NotImplementIBlockException : Exception
    {
        public NotImplementIBlockException(Type type)
            : base($"Class \"{type.FullName}\" does not implement IBlock interface.")
        {
        }

        internal NotImplementIBlockException(string message) : base(message)
        {
        }

        public NotImplementIBlockException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotImplementIBlockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
