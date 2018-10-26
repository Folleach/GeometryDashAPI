using System;
using System.Runtime.Serialization;

namespace GeometryDashAPI.Exceptions
{
    [Serializable]
    internal class BlockLoadException : Exception
    {
        public string[] BlockData;

        public BlockLoadException(int id, string[] data)
            : base($"Load block with ID {id} not support in this version API")
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
