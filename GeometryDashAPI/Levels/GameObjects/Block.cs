using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.Interfaces;
using System.Text;

namespace GeometryDashAPI.Levels.GameObjects
{
    public abstract class Block : IBlock
    {
        const Layer Default_ZLayer = Layer.T1;
        const short Default_ZOrder = 2;

        public int ID { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public bool HorizontalReflection { get; set; }
        public bool VerticalReflection { get; set; }
        public short Rotation { get; set; }
        public bool Glow { get; set; } = true; //Reverse (0 = true; false = 1)
        public int LinkControl { get; set; }
        public short EditorL { get; set; }
        public short EditorL2 { get; set; }
        public bool HighDetal { get; set; }
        public BlockGroup Group { get; set; }
        public bool DontFade { get; set; }
        public bool DontEnter { get; set; }
        public short ZOrder { get; set; } = 2;
        public Layer ZLayer { get; set; } = Layer.T1;
        public float Scale { get; set; } = 1f;
        public bool GroupParent { get; set; }
        public bool IsTrigger { get; set; }

        public Block(int id)
        {
            this.ID = id;
            Group = new BlockGroup();
        }

        public Block(string[] data)
        {
            Group = new BlockGroup();
            for (int i = 0; i < data.Length; i += 2)
            {
                switch (data[i])
                {
                    case "1":
                        ID = int.Parse(data[i + 1]);
                        continue;
                    case "2":
                        PositionX = GameConvert.StringToSingle(data[i + 1]);
                        continue;
                    case "3":
                        PositionY = GameConvert.StringToSingle(data[i + 1]);
                        continue;
                    case "4":
                        HorizontalReflection = GameConvert.StringToBool(data[i + 1]);
                        continue;
                    case "5":
                        VerticalReflection = GameConvert.StringToBool(data[i + 1]);
                        continue;
                    case "6":
                        Rotation = short.Parse(data[i + 1]);
                        continue;
                    case "96":
                        Glow = GameConvert.StringToBool(data[i + 1], true);
                        continue;
                    case "108":
                        LinkControl = int.Parse(data[i + 1]);
                        continue;
                    case "20":
                        EditorL = short.Parse(data[i + 1]);
                        continue;
                    case "61":
                        EditorL2 = short.Parse(data[i + 1]);
                        continue;
                    case "103":
                        HighDetal = GameConvert.StringToBool(data[i + 1]);
                        continue;
                    case "57":
                        Group = new BlockGroup(data[i + 1]);
                        continue;
                    case "64":
                        DontFade = GameConvert.StringToBool(data[i + 1]);
                        continue;
                    case "67":
                        DontEnter = GameConvert.StringToBool(data[i + 1]);
                        continue;
                    case "25":
                        ZOrder = short.Parse(data[i + 1]);
                        continue;
                    case "24":
                        ZLayer = (Layer)short.Parse(data[i + 1]);
                        continue;
                    case "32":
                        Scale = GameConvert.StringToSingle(data[i + 1]);
                        continue;
                    case "34":
                        GroupParent = GameConvert.StringToBool(data[i + 1]);
                        continue;
                    case "36":
                        IsTrigger = GameConvert.StringToBool(data[i + 1]);
                        continue;
                    default:
                        continue;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"1,{ID},2,{GameConvert.SingleToString(PositionX)},3,{GameConvert.SingleToString(PositionY)}");
            if (HorizontalReflection)
                builder.Append($",4,1");
            if (VerticalReflection)
                builder.Append($",5,1");
            if (Rotation != 0)
                builder.Append($",6,{Rotation}");
            if (!Glow)
                builder.Append($",96,1");
            if (LinkControl != 0)
                builder.Append($",108,{LinkControl}");
            if (EditorL != 0)
                builder.Append($",20,{EditorL}");
            if (EditorL2 != 0)
                builder.Append($",61,{EditorL2}");
            if (HighDetal)
                builder.Append($",103,1");
            if (Group != null && Group.Count > 0)
                builder.Append($",57,{Group.ToString()}");
            if (DontFade)
                builder.Append($",64,1");
            if (DontEnter)
                builder.Append($",67,1");
            if (ZOrder != Default_ZOrder)
                builder.Append($",25,{ZOrder}");
            if (ZLayer != Default_ZLayer)
                builder.Append($",24,{(short)ZLayer}");
            if (Scale != 1f)
                builder.Append($",32,{GameConvert.SingleToString(Scale)}");
            if (GroupParent)
                builder.Append($",34,1");
            return builder.ToString();
        }
    }
}
