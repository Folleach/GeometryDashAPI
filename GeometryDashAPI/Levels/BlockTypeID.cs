using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.GameObjects.Specific;
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
                case 200:
                case 201:
                case 202:
                case 203:
                case 1334:
                    return new SpeedBlock(data);
                case 35:
                case 67:
                case 140:
                case 1332:
                    return new JumpPlate(data);
                default:
                    throw new Exception(ExceptionMessages.BlockTypeNotSupported(id.ToString()));
            }
        }
    }
}
