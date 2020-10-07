using System.Text;

namespace GeometryDashAPI.Server.Queries
{
    public class PasswordQuery : IQuery
    {
        private int accountId;
        private string password;

        public PasswordQuery(int accountId, string password)
        {
            this.accountId = accountId;
            this.password = password;
        }

        public Parameters BuildQuery()
        {
            Parameters result = new Parameters();
            BuildQuery(result);
            return result;
        }

        public void BuildQuery(Parameters parameters)
        {
            parameters.Add(new Property("gdw", 0));
            parameters.Add(new Property("accountID", accountId));
            parameters.Add(new Property("gjp", GameConvert.ToBase64(Encoding.ASCII.GetBytes(Crypt.XOR(password, "37526")))));
        }
    }
}
