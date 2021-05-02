using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Models;
using GeometryDashAPI.Server.Queries;
using System;
using System.Threading.Tasks;

namespace GeometryDashAPI.Server
{
    public class GameServer
    {
        private Network network = new Network();

        private OnlineQuery defaultOnlineQuery = new OnlineQuery();

        public async Task<PlayerInfoArray> GetTop(TopType type, int count)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new IdentifierQuery());
            query.AddProperty(new Property("type", type.GetAttributeOfSelected<OriginalNameAttribute>().OriginalName));
            query.AddProperty(new Property("count", count));
            PlayerInfoArray players = new PlayerInfoArray();
            players.Load(await network.GetAsync("/database/getGJScores20.php", query));
            return players;
        }

        public async Task<LevelInfoPage> GetLevels(GetLevelsQuery getLevelsQuery)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(getLevelsQuery);
            LevelInfoPage levels = new LevelInfoPage();
            levels.Load(await network.GetAsync("/database/getGJLevels21.php", query));
            return levels;
        }

        public async Task<LevelInfoPage> GetFeatureLevels(int page)
        {
            return await GetLevels(new GetLevelsQuery(SearchType.Featured)
            {
                QueryString = "",
                Page = page,
            });
        }

        public async Task<LoginInfo> Login(string username, string password)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddProperty(new Property("udid", Guid.NewGuid()));
            query.AddProperty(new Property("userName", username));
            query.AddProperty(new Property("password", password));
            query.AddProperty(new Property("sID", 76561198946149263));
            query.AddProperty(new Property("secret", "Wmfv3899gc9"));
            return LoginInfo.FromResponse(await network.GetAsync("/database/accounts/loginGJAccount.php", query));
        }

        public async Task<AccountCommentArray> GetAccountComment(int accountID, int page)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("page", page));
            query.AddProperty(new Property("total", 0));
            string result = await network.GetAsync("/database/getGJAccountComments20.php", query);
            if (result == "-1")
                return null;
            return new AccountCommentArray(result);
        }

        public async Task<UserInfo> GetUserByName(string name)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddProperty(new Property("str", name));
            query.AddProperty(new Property("total", 0));
            query.AddProperty(new Property("page", 0));
            string result = await network.GetAsync("/database/getGJUsers20.php", query);
            if (result == "-1")
                return null;
            return new UserInfo(result.Split('#')[0]);
        }

        public async Task<LevelInfo> DownloadLevel(int id)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddProperty(new Property("levelID", id));
            query.AddProperty(new Property("inc", 0));
            query.AddProperty(new Property("extras", 0));
            string result = await network.GetAsync("/database/downloadGJLevel22.php", query);
            if (result == "-1")
                return null;
            return new LevelInfo(result);
        }

        public async Task<AccountInfo> GetAccountInfo(int accountID)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new IdentifierQuery());
            query.AddProperty(new Property("targetAccountID", accountID));
            string result = await network.GetAsync("/database/getGJUserInfo20.php", query);
            if (result == "-1")
                return null;
            return new AccountInfo(result);
        }

        public async Task<LevelInfoPage> GetMyLevels(PasswordQuery account, int userId, int page)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(account);
            query.AddToChain(new GetLevelsQuery(SearchType.OnAccaunt)
            {
                QueryString = userId.ToString(),
                Page = page
            });
            LevelInfoPage levels = new LevelInfoPage();
            levels.Load(await network.GetAsync("/database/getGJLevels21.php", query));
            return levels;
        }
    }
}
