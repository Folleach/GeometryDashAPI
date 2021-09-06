namespace GeometryDashAPI
{
    public readonly struct Property
    {
        public readonly string Key;
        public readonly string Value;

        public Property(object key, object value)
        {
            Key = key.ToString();
            Value = value.ToString();
        }

        public override string ToString()
        {
            return $"{Key} - {Value}";
        }
    }
}
