using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server
{
    internal class Session
    {
        public bool isEmpty { get; private set; } = true;

        public string UserName { get; private set; }
        public string GeometryJumpPassword { get; private set; }


        private string ConvertPassword(string password)
        {
            return GameConvert.ToBase64(Encoding.ASCII.GetBytes(Crypt.XOR(password, "37526")));
        }

        public Session()
        {
        }

        public Session(string username, string password)
        {
            Load(username, password);
        }

        public void Load(string username, string password)
        {
            UserName = username;
            GeometryJumpPassword = ConvertPassword(password);
            isEmpty = false;
        }

        public List<Property> GetProperties()
        {
            List<Property> properties = new List<Property>();
            properties.Add(new Property("userName", UserName));
            properties.Add(new Property("gjp", GeometryJumpPassword));

            return properties;
        }
    }
}
