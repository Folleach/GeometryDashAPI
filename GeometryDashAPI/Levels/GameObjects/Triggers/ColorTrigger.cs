﻿using GeometryDashAPI.Attributes;
using GeometryDashAPI.Levels.GameObjects.Default;

namespace GeometryDashAPI.Levels.GameObjects.Triggers
{
    [GameBlock(899)]
    public class ColorTrigger : Trigger
    {
        [GameProperty("7", (byte)255, true, Order = OrderTriggerBase + 1)] public byte Red { get; set; } = 255;
        [GameProperty("8", (byte)255, true, Order = OrderTriggerBase + 2)] public byte Green { get; set; } = 255;
        [GameProperty("9", (byte)255, true, Order = OrderTriggerBase + 3)] public byte Blue { get; set; } = 255;

        [GameProperty("10", 0.5f, true, Order = OrderTriggerBase + 4)] public float FadeTime { get; set; } = 0.5f;
        [GameProperty("35", 1f, true, Order = OrderTriggerBase + 5)] public float Opacity { get; set; } = 1f;

        [GameProperty("17", false, Order = OrderTriggerBase + 7)] public bool Blending { get; set; } = false;

        [GameProperty("23", 1, Order = OrderTriggerBase + 9)] public int ColorId { get; set; } = 1;

        [GameProperty("15", false, Order = OrderTriggerBase + 6)] public bool PlayerColor1 { get; set; }
        [GameProperty("16", false, Order = OrderTriggerBase + 6)] public bool PlayerColor2 { get; set; }
        [GameProperty("60", false, Order = OrderTriggerBase + 8)] public bool CopyOpacity { get; set; }

        [GameProperty("49", Order = OrderTriggerBase + 11)] public Hsv ColorHsv { get; set; }
        [GameProperty("50", 0, Order = OrderTriggerBase + 10)] public int TargetChannelId { get; set; }

        public ColorTrigger() : base(899)
        {
        }
    }
}
