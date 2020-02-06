namespace GeometryDashAPI.Realtime.Structures
{
    public struct RealtimeColor
    {
        public int ID;
        public byte Red;
        public byte Green;
        public byte Blue;

        public override string ToString()
        {
            return $"{Red.ToString("X2")}{Green.ToString("X2")}{Blue.ToString("X2")}";
        }
    }
}
