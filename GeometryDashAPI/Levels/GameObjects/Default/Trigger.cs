using System.Text;

namespace GeometryDashAPI.Levels.GameObjects.Default
{
    public abstract class Trigger : Block, ITrigger
    {
        public bool TouchTrigger { get; set; }
        public bool SpawnTrigger { get; set; }
        public bool MultiTrigger { get; set; }

        public Trigger()
        {
        }

        public Trigger(int id) : base(id)
        {
        }

        public Trigger(string[] data)
        {
            for (int i = 0; i < data.Length; i += 2)
                LoadProperty(byte.Parse(data[i]), data[i + 1]);
        }

        public override void LoadProperty(byte key, string value)
        {
            switch (key)
            {
                case 11:
                    TouchTrigger = GameConvert.StringToBool(value);
                    break;
                case 62:
                    SpawnTrigger = GameConvert.StringToBool(value);
                    break;
                case 87:
                    MultiTrigger = GameConvert.StringToBool(value);
                    break;
                default:
                    base.LoadProperty(key, value);
                    return;
            }
            
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString());
            if (TouchTrigger)
                builder.Append($",11,1");
            if (SpawnTrigger)
                builder.Append($",62,1");
            if (MultiTrigger)
                builder.Append($",87,1");
            return builder.ToString();
        }
    }
}
