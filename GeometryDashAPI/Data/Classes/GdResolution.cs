using System;

namespace GeometryDashAPI.Data.Classes
{
    public sealed class GdResolution
    {
        public int Width { get; }
        public int Height { get; }

        public GdResolution(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static GdResolution ToGdResolution(int gdResFormat)
        {
            return gdResFormat switch
            {
                25 => new(2560, 1440),
                24 => new(2560, 1200), // NOT TESTED. 2560x1200 IS A GUESSING. TESTER HAD NO VALUE AT THIS SPOT. RECHECK IF YOU HAVE ABILITY TO!
                23 => new(1920, 1440),
                22 => new(1920, 1200),
                21 => new(1920, 1080),
                20 => new(1768, 1050),
                19 => new(1680, 992),
                18 => new(1600, 1200),
                17 => new(1600, 1024),
                16 => new(1600, 900),
                15 => new(1440, 900),
                14 => new(1366, 768),
                13 => new(1360, 768),
                12 => new(1280, 1024),
                11 => new(1280, 960),
                10 => new(1280, 800),
                9 => new(1280, 768),
                8 => new(1280, 720),
                7 => new(1176, 664),
                6 => new(1152, 864),
                5 => new(1024, 768),
                4 => new(800, 600),
                3 => new(720, 576),
                2 => new(720, 480),
                1 => new(640, 480),
                _ => throw new InvalidOperationException(nameof(gdResFormat))
            };
        }

        public static int FromGdResolution(GdResolution res)
        {
            return (res.Width, res.Height) switch
            {
                (2560, 1440) => 25,
                (2560, 1200) => 24,
                (1920, 1440) => 23,
                (1920, 1200) => 22,
                (1920, 1080) => 21,
                (1768, 1050) => 20,
                (1680, 992) => 19,
                (1600, 1200) => 18,
                (1600, 1024) => 17,
                (1600, 900) => 16,
                (1440, 900) => 15,
                (1366, 768) => 14,
                (1360, 768) => 13,
                (1280, 1024) => 12,
                (1280, 960) => 11,
                (1280, 800) => 10,
                (1280, 768) => 9,
                (1280, 720) => 8,
                (1176, 664) => 7,
                (1152, 864) => 6,
                (1024, 768) => 5,
                (800, 600) => 4,
                (720, 576) => 3,
                (720, 480) => 2,
                (640, 480) => 1,
                _ => throw new InvalidOperationException("Invalid resolutions' values")
            };
        }
    }
}
