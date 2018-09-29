using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.Interfaces;
using System.Collections.Generic;

namespace GeometryDashAPI.Levels
{
    public class BlockList : List<IBlock>
    {
        public Block[] GetBlocks()
        {
            return (Block[])FindAll(x => x is Block).ToArray();
        }
    }
}
