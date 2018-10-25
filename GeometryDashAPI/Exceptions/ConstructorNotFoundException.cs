using System;
using System.Runtime.Serialization;

namespace GeometryDashAPI.Exceptions
{
    [Serializable]
    internal class ConstructorNotFoundException : Exception
    {
        public ConstructorNotFoundException(Type type)
            : base($"The class \"{type.FullName}\" does not have a constructor accepting an array of strings.")
        {
        }

        public ConstructorNotFoundException(string message) : base(message)
        {
        }

        public ConstructorNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConstructorNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
