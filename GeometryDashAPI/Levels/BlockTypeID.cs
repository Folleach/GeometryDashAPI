using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.Interfaces;
using System;

namespace GeometryDashAPI.Levels
{
    public static class BlockTypeID
    {
        public static IBlock InitializeByID(int id, string[] data)
        {
            switch (id)
            {
                case 1:
                case 8:
                    return new BaseBlock(data);
                case 1658:
                case 1888:
                    return new DetailBlock(data);
                case 914:
                    return new TextBlock(data);
                default:
                    throw new Exception(ExceptionMessages.BlockTypeNotSupported(id.ToString()));
            }
        }
    }
}
