using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryDashAPI.Levels.GameObjects.Specific
{
    public class Coin : Block
    {
        public override short Default_ZOrder { get; protected set; } = 9;

        public Coin() : base(1329)
        {
        }

        public Coin(string[] data) : base(data)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
