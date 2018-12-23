using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.GameObjects.Default;
using GeometryDashAPI.Levels.GameObjects.Specific;
using GeometryDashAPI.Levels.GameObjects.Triggers;

namespace GeometryDashAPI.Levels
{
    public class BlockTypeID
    {
        public BindingBlockID BlockBinding { get; set; }

        public BlockTypeID(BindingBlockID blockBinding)
        {
            this.BlockBinding = blockBinding;
        }

        public IBlock InitializeByID(int id, string[] data)
        {
            if (BlockBinding != null && BlockBinding.ContainsKey(id))
                return BlockBinding.Invoke<IBlock>(id, data);

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
                case 36:
                case 141:
                case 1333:
                case 84:
                case 1022:
                case 1330:
                case 1704:
                case 1751:
                    return new JumpSphere(data);
                case 1006:
                    return new PulseTrigger(data);
                case 1329:
                    return new Coin(data);
                case 1586:
                    return new SquareParticle(data);
                case 1700:
                    return new CircleParticle(data);
                case 1611:
                    return new CountTrigger(data);
                case 1811:
                    return new InstantCountTrigger(data);
                default:
                    throw new BlockLoadException(id, data);
            }
        }
    }
}
