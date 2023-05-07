using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(1006)]
    public class PulseTrigger : Trigger
    {
        [GameProperty("7", (byte)255, Order = OrderTriggerBase + 9)] public byte Red { get; set; }
        [GameProperty("8", (byte)255, Order = OrderTriggerBase + 10)] public byte Green { get; set; }
        [GameProperty("9", (byte)255, Order = OrderTriggerBase + 11)] public byte Blue { get; set; }

        [GameProperty("45", 0f, Order = OrderTriggerBase + 2)] public float FadeIn { get; set; }
        [GameProperty("46", 0f, Order = OrderTriggerBase + 3)] public float Hold { get; set; }
        [GameProperty("47", 0f, Order = OrderTriggerBase + 4)] public float FadeOut { get; set; }

        [GameProperty("48", PulseModeType.Color, Order = OrderTriggerBase + 5)] public PulseModeType PulseMode { get; set; }
        [GameProperty("49", Order = OrderTriggerBase + 9)] public Hsv HsvValue { get; set; }

        [GameProperty("50", Order = OrderTriggerBase + 12)] public int ColorID { get; set; }
        [GameProperty("51", 0, true, Order = OrderTriggerBase + 1)] public int TargetID { get; set; } = 0;

        [GameProperty("52", TargetType.Channel, Order = OrderTriggerBase + 6)] public TargetType TargetType { get; set; }

        [GameProperty("65", false, Order = OrderTriggerBase + 7)] public bool MainOnly { get; set; }
        [GameProperty("66", false, Order = OrderTriggerBase + 7)] public bool DetailOnly { get; set; }

        [GameProperty("86", false, Order = OrderTriggerBase + 8)] public bool Exclusive { get; set; }

        public PulseTrigger() : base(1006)
        {
            IsTrigger = true;
        }
        public override string ToString() => "PulseTrigger";
    }
}
