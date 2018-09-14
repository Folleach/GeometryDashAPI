namespace GeometryDashAPI
{
    public class GameConvert
    {
        public static string BoolToString(bool value, bool isReverse)
        {
            if (isReverse)
                return value ? "0" : "1";
            return value ? "1" : "0";
        }

        public static bool StringToBool(string value, bool isReverse)
        {
            if (isReverse)
                return value == "1" ? false : true;
            return value == "1" ? true : false;
        }
    }
}
