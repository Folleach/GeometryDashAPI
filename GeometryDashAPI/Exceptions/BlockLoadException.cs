using System;
using System.Runtime.Serialization;

namespace GeometryDashAPI.Exceptions
{
    [Serializable]
    internal class BlockLoadException : Exception
    {
        public string[] BlockData;

        public BlockLoadException(string id, string[] data)
            : base($"Type '{id}' not supported.")
        {
            this.BlockData = data;
        }

        public BlockLoadException(string message) : base(message)
        {
        }

        public BlockLoadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlockLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
