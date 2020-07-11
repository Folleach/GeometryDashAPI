using GeometryDashAPI.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examples
{
    public static class MessagesSender
    {
        static public bool SendMessage(string username, string password, string subject, string body, string receiverUsername)
        {
            GameServer gs = new GameServer();

            int accountID = gs.Login(username, password).AccountID;
            int receiverAccountID = gs.GetUserByName(receiverUsername).AccountID;
            return gs.SendMessage(accountID, receiverAccountID, subject, body);
        }
    }
}
