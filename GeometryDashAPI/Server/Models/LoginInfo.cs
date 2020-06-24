namespace GeometryDashAPI.Server.Models
{
    public class LoginInfo
    {
        public int AccountID;
        public int UserID;

        public static LoginInfo FromResponse(string response)
        {
            LoginInfo result = new LoginInfo();
            string[] data = response.Split(',');
            if (data.Length != 2)
                return null;
            if (!int.TryParse(data[0], out result.AccountID))
                return null;
            if (!int.TryParse(data[1], out result.UserID))
                return null;
            return result;
        }
    }
}
