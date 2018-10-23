namespace GeometryDashAPI.Levels
{
    public class HSV
    {
        const char Separator = 'a';

        public short Hue { get; set; }
        public float Saturation { get; set; } = 1;
        public float Brightness { get; set; } = 1;
        public bool DeltaSaturation { get; set; }
        public bool DeltaBrightness { get; set; }

        public bool IsDefault
        {
            get => Hue == 0 && Saturation == 1f && Brightness == 1f && !DeltaSaturation && !DeltaBrightness;
        }

        public HSV()
        {
        }

        public HSV(string data)
        {
            string[] dataArray = data.Split(Separator);
            Hue = short.Parse(dataArray[0]);
            Saturation = GameConvert.StringToSingle(dataArray[1]);
            Brightness = GameConvert.StringToSingle(dataArray[2]);

            DeltaSaturation = GameConvert.StringToBool(dataArray[3]);
            DeltaBrightness = GameConvert.StringToBool(dataArray[4]);
        }

        public override string ToString()
        {
            return
                $"{Hue}{Separator}" +
                $"{GameConvert.SingleToString(Saturation)}{Separator}" +
                $"{GameConvert.SingleToString(Brightness)}{Separator}" +
                $"{GameConvert.BoolToString(DeltaSaturation)}{Separator}" +
                $"{GameConvert.BoolToString(DeltaBrightness)}";
        }
    }
}
