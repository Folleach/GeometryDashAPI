using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class AccountInfoArray : List<AccountInfo>
    {
        public AccountInfoArray(string data)
        {
            foreach (string accData in data.Split('|')) Add(new AccountInfo(accData));
        }
    }
}
