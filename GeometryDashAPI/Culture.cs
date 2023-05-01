using System;
using System.Globalization;

namespace GeometryDashAPI
{
    public static class Culture
    {
        private static CultureInfo cultureInfo;

        static Culture()
        {
            cultureInfo = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
        }

        public static IFormatProvider FormatProvider
        {
            get => cultureInfo;
        }
    }
}
