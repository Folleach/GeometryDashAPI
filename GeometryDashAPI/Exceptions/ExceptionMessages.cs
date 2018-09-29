namespace GeometryDashAPI.Exceptions
{
    internal static class ExceptionMessages
    {
        internal static string PropertyNotSupported(string key, string value = null)
        {
            if (value == null)
                return $"Property '{key}' not supported.";
            return $"Property '{key}' not supported. Value: {value}";
        }

        internal static string BlockTypeNotSupported(string type)
        {
            return $"Type '{type}' not supported";
        }
    }
}
