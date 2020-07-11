using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Models;
using GeometryDashAPI.Server.Queries;
using System;
using System.Text;

namespace GeometryDashAPI.Server
{
    public class GameServer
    {
        private Network network = new Network();

        private OnlineQuery defaultOnlineQuery = new OnlineQuery();

        private Session  session = new Session();
        public PlayerInfoArray GetTop(TopType type, int count)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new IdentifierQuery());
            query.AddProperty(new Property("type", type.GetAttributeOfSelected<OriginalNameAttribute>().OriginalName));
            query.AddProperty(new Property("count", count));
            PlayerInfoArray players = new PlayerInfoArray();
            players.Load(network.Get("/database/getGJScores20.php", query));
            return players;
        }

        public LevelInfoPage GetLevels(GetLevelsQuery getLevelsQuery)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(getLevelsQuery);
            LevelInfoPage levels = new LevelInfoPage();
            levels.Load(network.Get("/database/getGJLevels21.php", query));
            return levels;
        }

        public LevelInfoPage GetFeatureLevels(int page)
        {
            return GetLevels(new GetLevelsQuery(SearchType.Featured)
            {
                QueryString = "",
                Page = page,
            });
        }

        public LoginInfo Login(string username, string password)
        {
            session.Load(username, password);

            FlexibleQuery query = new FlexibleQuery();
            query.AddProperty(new Property("udid", Guid.NewGuid()));
            query.AddProperty(new Property("userName", username));
            query.AddProperty(new Property("password", password));
            query.AddProperty(new Property("sID", 76561198946149263));
            query.AddProperty(new Property("secret", "Wmfv3899gc9"));
            return LoginInfo.FromResponse(network.Get("/database/accounts/loginGJAccount.php", query));
        }

        public AccountCommentArray GetAccountComment(int accountID, int page)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("page", page));
            query.AddProperty(new Property("total", 0));
            string result = network.Get("/database/getGJAccountComments20.php", query);
            if (result == "-1")
                return null;
            return new AccountCommentArray(result);
        }

        public UserInfo GetUserByName(string name)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddProperty(new Property("str", name));
            query.AddProperty(new Property("total", 0));
            query.AddProperty(new Property("page", 0));
            string result = network.Get("/database/getGJUsers20.php", query);
            if (result == "-1")
                return null;
            return new UserInfo(result.Split('#')[0]);
        }

        public AccountInfo GetAccountInfo(int accountID)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new IdentifierQuery());
            query.AddProperty(new Property("targetAccountID", accountID));
            string result = network.Get("/database/getGJUserInfo20.php", query);
            if (result == "-1")
                return null;
            return new AccountInfo(result);
        }

        public bool SendMessage(int accountID, int receiverAccountID, string subject, string body)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new IdentifierQuery());
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("toAccountID", receiverAccountID));
            query.AddProperty(new Property("subject", GameConvert.ToBase64(Encoding.ASCII.GetBytes(subject))));
            query.AddProperty(new Property("body", GameConvert.ToBase64(Encoding.ASCII.GetBytes(Crypt.XOR(body, "14251")))));
            string result = network.Get("/database/uploadGJMessage20.php", query);
            return result == "1";
        }
    }
}
