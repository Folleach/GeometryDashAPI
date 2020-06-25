using System;
using System.Globalization;

namespace GeometryDashAPI
{
    public static class Culture
    {
        private static CultureInfo cultureInfo;
        private static bool initialized = false;

        private static void Initialize()
        {
            cultureInfo = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
        }

        public static IFormatProvider FormatProvider
        {
            get
            {
                if (!initialized)
                {
                    Initialize();
                    initialized = true;
                }

                return cultureInfo;
            }
        }
    }
}
