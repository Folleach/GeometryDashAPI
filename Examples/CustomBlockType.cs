using GeometryDashAPI.Data;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace Examples
{
    //An example of creating your own classes for blocks.
    class MyShakeTrigger : Trigger
    {
        public override Layer Default_ZLayer { get; protected set; } = Layer.T2;
        public override short Default_ZOrder { get; protected set; } = 99;

        public MyShakeTrigger() : base(1520)
        {
        }

        public MyShakeTrigger(string[] data) : base(data)
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    // TODO: Update this example
    class CustomBlockType
    {
        public static void Invoke()
        {
            LocalLevels levels = new LocalLevels();
            BindingBlockID binding = new BindingBlockID();
            binding.Bind(1520, typeof(MyShakeTrigger));
            // Level level = new Level(levels.GetLevelByName("Temp"), binding);
        }
    }
}
