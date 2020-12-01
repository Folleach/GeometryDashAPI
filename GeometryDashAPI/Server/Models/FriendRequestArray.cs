using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server.Models
{
    public class FriendRequestArray : List<FriendRequest>
    {
        public FriendRequestArray(string data)
        {
            foreach (string frData in data.Split('|')) Add(new FriendRequest(frData));
        }
    }
}
