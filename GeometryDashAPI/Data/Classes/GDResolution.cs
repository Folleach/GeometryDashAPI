using System;

namespace GeometryDashAPI.Data.Classes
{
    public sealed class GDResolution
    {
        public int Width { get; }
        public int Height { get; }

        public GDResolution(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static GDResolution ToGDResolution(int gdResFormat)
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

        public static int FromGDResolution(GDResolution res)
        {
            if (res.Width.Equals(2560) && res.Height.Equals(1440)) return 25;
            if (res.Width.Equals(2560) && res.Height.Equals(1200)) return 24; // NOT TESTED. 2560x1200 IS A GUESSING. TESTER HAD NO VALUE AT THIS SPOT. RECHECK IF YOU HAVE ABILITY TO!
            if (res.Width.Equals(1920) && res.Height.Equals(1440)) return 23;
            if (res.Width.Equals(1920) && res.Height.Equals(1200)) return 22;
            if (res.Width.Equals(1920) && res.Height.Equals(1080)) return 21;
            if (res.Width.Equals(1768) && res.Height.Equals(1050)) return 20;
            if (res.Width.Equals(1680) && res.Height.Equals(992)) return 19;
            if (res.Width.Equals(1600) && res.Height.Equals(1200)) return 18;
            if (res.Width.Equals(1600) && res.Height.Equals(1024)) return 17;
            if (res.Width.Equals(1600) && res.Height.Equals(900)) return 16;
            if (res.Width.Equals(1440) && res.Height.Equals(900)) return 15;
            if (res.Width.Equals(1366) && res.Height.Equals(768)) return 14;
            if (res.Width.Equals(1360) && res.Height.Equals(768)) return 13;
            if (res.Width.Equals(1280) && res.Height.Equals(1024)) return 12;
            if (res.Width.Equals(1280) && res.Height.Equals(960)) return 11;
            if (res.Width.Equals(1280) && res.Height.Equals(800)) return 10;
            if (res.Width.Equals(1280) && res.Height.Equals(768)) return 9;
            if (res.Width.Equals(1280) && res.Height.Equals(720)) return 8;
            if (res.Width.Equals(1176) && res.Height.Equals(664)) return 7;
            if (res.Width.Equals(1152) && res.Height.Equals(864)) return 6;
            if (res.Width.Equals(1024) && res.Height.Equals(768)) return 5;
            if (res.Width.Equals(800) && res.Height.Equals(600)) return 4;
            if (res.Width.Equals(720) && res.Height.Equals(576)) return 3;
            if (res.Width.Equals(720) && res.Height.Equals(480)) return 2;
            if (res.Width.Equals(640) && res.Height.Equals(480)) return 1;

            throw new InvalidOperationException("Invalid resolutions' values");
        }
    }
}
