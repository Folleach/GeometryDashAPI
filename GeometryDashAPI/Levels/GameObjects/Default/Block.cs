using GeometryDashAPI.Levels.Enums;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public class Block : IBlock
    {
        public virtual Layer Default_ZLayer { get; protected set; } = Layer.T1;
        public virtual short Default_ZOrder { get; protected set; } = 2;

        public int ID { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public bool HorizontalReflection { get; set; }
        public bool VerticalReflection { get; set; }
        public short Rotation { get; set; }
        public bool Glow { get; set; } = true; //Reverse (0 = true; 1 = false)
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

        public Dictionary<byte, string> OtherProperties;

        public Block()
        {
        }
        
        public Block(int id)
        {
            this.SetDefault();
            this.ID = id;
            Group = new BlockGroup();
        }

        public Block(string[] data)
        {
            for (int i = 0; i < data.Length; i += 2)
                LoadProperty(byte.Parse(data[i]), data[i + 1]);
        }

        public virtual void LoadProperty(byte key, string value)
        {
            switch (key)
            {
                case 1:
                    ID = int.Parse(value);
                    return;
                case 2:
                    PositionX = GameConvert.StringToSingle(value);
                    return;
                case 3:
                    PositionY = GameConvert.StringToSingle(value);
                    return;
                case 4:
                    HorizontalReflection = GameConvert.StringToBool(value);
                    return;
                case 5:
                    VerticalReflection = GameConvert.StringToBool(value);
                    return;
                case 6:
                    Rotation = short.Parse(value);
                    return;
                case 96:
                    Glow = GameConvert.StringToBool(value, true);
                    return;
                case 108:
                    LinkControl = int.Parse(value);
                    return;
                case 20:
                    EditorL = short.Parse(value);
                    return;
                case 61:
                    EditorL2 = short.Parse(value);
                    return;
                case 103:
                    HighDetal = GameConvert.StringToBool(value);
                    return;
                case 57:
                    Group = new BlockGroup(value);
                    return;
                case 64:
                    DontFade = GameConvert.StringToBool(value);
                    return;
                case 67:
                    DontEnter = GameConvert.StringToBool(value);
                    return;
                case 25:
                    ZOrder = short.Parse(value);
                    return;
                case 24:
                    ZLayer = (Layer)short.Parse(value);
                    return;
                case 32:
                    Scale = GameConvert.StringToSingle(value);
                    return;
                case 34:
                    GroupParent = GameConvert.StringToBool(value);
                    return;
                case 36:
                    IsTrigger = GameConvert.StringToBool(value);
                    return;
                default:
                    if (OtherProperties == null)
                        OtherProperties = new Dictionary<byte, string>();
                    if (!OtherProperties.ContainsKey(key))
                        OtherProperties.Add(key, value);
                    return;
            }
        }

        private void SetDefault()
        {
            this.ZLayer = Default_ZLayer;
            this.ZOrder = Default_ZOrder;
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
            if (IsTrigger)
                builder.Append(",36,1");

            if (OtherProperties != null)
                foreach (KeyValuePair<byte, string> element in OtherProperties)
                    builder.Append($",{element.Key},{element.Value}");

            return builder.ToString();
        }
    }
}
