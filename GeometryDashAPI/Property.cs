namespace GeometryDashAPI
{
    public struct Property
    {
        public object Key { get; set; }
        public object Value { get; set; }

        public Property(object key, object value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Key} - {Value}";
        }
    }
}
