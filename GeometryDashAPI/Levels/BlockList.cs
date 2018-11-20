using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.GameObjects.Triggers;
using System.Collections.Generic;

namespace GeometryDashAPI.Levels
{
    public class BlockList : List<IBlock>
    {
        public Block[] GetBlocks()
        {
            return (Block[])FindAll(x => x is Block).ToArray();
        }

        public MoveTrigger[] GetMoveTriggers()
        {
            return (MoveTrigger[])FindAll(x => x is MoveTrigger).ToArray();
        }
    }
}
