namespace GeometryDashAPI.Server.Models
{
    public class UserInfo
    {
        public string AccountName { get; set; }
        public int AccountID { get; set; }

        public UserInfo(string data)
        {
            string[] arr = data.Split(':');
            for (int i = 0; i < arr.Length; i += 2)
            {
                switch (arr[i])
                {
                    case "1":
                        AccountName = arr[i + 1];
                        break;
                    case "16":
                        AccountID = int.Parse(arr[i + 1]);
                        break;
                }
            }
        }
    }
}
