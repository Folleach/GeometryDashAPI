using GeometryDashAPI.Exceptions;
using System;

namespace GeometryDashAPI.Levels
{
    public static class BlockTypeID
    {
        public static int GetTypeByID(string id)
        {
            switch (id)
            {
                case "1":
                case "8":
                    return 1; //BaseBlock
                case "1658":
                case "1888":
                    return 2; //DetailBlock
                default:
                    throw new Exception(ExceptionMessages.BlockTypeNotSupported(id));
            }
        }
    }
}
