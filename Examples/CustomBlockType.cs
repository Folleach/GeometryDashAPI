using GeometryDashAPI;
using GeometryDashAPI.Data;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace Examples
{
    //An example of creating your own classes for blocks.
    [GameBlock(1520)]
    class MyShakeTrigger : Trigger
    {
        [GameProperty("24", (short)Layer.T2)] protected override short zLayer { get; set; } = (short)Layer.T2;
        [GameProperty("25", (short)99)] public override short ZOrder { get; set; } = 99;

        [GameProperty("9999", 2)] public int MySuperVelocity { get; set; } = 2;
    }

    class CustomBlockType
    {
        public static void Invoke()
        {
            LocalLevels levels = new LocalLevels();
            BindingBlockID binding = new BindingBlockID();
            binding.Bind(1520, typeof(MyShakeTrigger));
            Level level = new Level(levels.GetLevelByName("Temp"), binding);
        }
    }
}
