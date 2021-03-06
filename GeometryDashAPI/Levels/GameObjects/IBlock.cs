﻿using GeometryDashAPI.Levels.Enums;

namespace GeometryDashAPI.Levels.GameObjects
{
    public interface IBlock
    {
        int Id { get; set; }
        float PositionX { get; set; }
        float PositionY { get; set; }
        bool HorizontalReflection { get; set; }
        bool VerticalReflection { get; set; }
        short Rotation { get; set; }
        bool Glow { get; set; }
        int LinkControl { get; set; }
        short EditorL { get; set; }
        short EditorL2 { get; set; }
        bool HighDetail { get; set; }
        BlockGroup Group { get; set; }
        bool DontFade { get; set; }
        bool DontEnter { get; set; }
        short ZOrder { get; set; }
        Layer ZLayer { get; set; }
        float Scale { get; set; }
        bool GroupParent { get; set; }
        bool IsTrigger { get; set; }
    }
}