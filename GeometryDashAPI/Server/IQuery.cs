using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server
{
    public interface IQuery
    {
        Parameters BuildQuery();
        void BuildQuery(Parameters parameters);
    }
}
