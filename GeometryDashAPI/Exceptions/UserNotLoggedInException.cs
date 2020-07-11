using System;
using System.Runtime.Serialization;

namespace GeometryDashAPI.Exceptions
{
    [Serializable]
    internal class UserNotLoggedInException : Exception
    {
        public UserNotLoggedInException()
        {
        }

        public UserNotLoggedInException(string message) : base(message)
        {
        }

        public UserNotLoggedInException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotLoggedInException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
