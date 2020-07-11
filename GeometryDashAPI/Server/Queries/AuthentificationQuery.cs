using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Queries
{
    class AuthentificationQuery : IQuery //i hope i spelled it right lol
    {
        private Session session = new Session();

        public AuthentificationQuery()
        {
        }

        public AuthentificationQuery(Session session)
        {
            this.session = session;
        }
        public Parameters BuildQuery()
        {
            Parameters result = new Parameters();
            BuildQuery(result);
            return result;
        }

        public void BuildQuery(Parameters parameters)
        {
            parameters.AddRange(session.GetProperties());
        }
    }
}
