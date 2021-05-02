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

        public async Task<PlayerInfoArray> GetTop(TopType type, int count)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(new IdentifierQuery())
                .AddProperty(new Property("type", type.GetAttributeOfSelected<OriginalNameAttribute>().OriginalName))
                .AddProperty(new Property("count", count));
            
            var players = new PlayerInfoArray();
            players.Load(await network.GetAsync("/database/getGJScores20.php", query));
            return players;
        }

        public async Task<LevelInfoPage> GetLevels(GetLevelsQuery getLevelsQuery)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(getLevelsQuery);
            var levels = new LevelInfoPage();
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
            var query = new FlexibleQuery()
                .AddProperty(new Property("udid", Guid.NewGuid()))
                .AddProperty(new Property("userName", username))
                .AddProperty(new Property("password", password))
                .AddProperty(new Property("sID", 76561198946149263))
                .AddProperty(new Property("secret", "Wmfv3899gc9"));
            return LoginInfo.FromResponse(await network.GetAsync("/database/accounts/loginGJAccount.php", query));
        }

        public async Task<AccountCommentArray> GetAccountComment(int accountId, int page)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddProperty(new Property("accountID", accountId))
                .AddProperty(new Property("page", page))
                .AddProperty(new Property("total", 0));
            var result = await network.GetAsync("/database/getGJAccountComments20.php", query);
            if (result == "-1")
                return null;
            return new AccountCommentArray(result);
        }

        public async Task<UserInfo> GetUserByName(string name)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddProperty(new Property("str", name))
                .AddProperty(new Property("total", 0))
                .AddProperty(new Property("page", 0));
            var result = await network.GetAsync("/database/getGJUsers20.php", query);
            if (result == "-1")
                return null;
            return new UserInfo(result.Split('#')[0]);
        }

        public async Task<LevelInfo> DownloadLevel(int id)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddProperty(new Property("levelID", id))
                .AddProperty(new Property("inc", 0))
                .AddProperty(new Property("extras", 0));
            var result = await network.GetAsync("/database/downloadGJLevel22.php", query);
            if (result == "-1")
                return null;
            return new LevelInfo(result);
        }

        public async Task<AccountInfo> GetAccountInfo(int accountID)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(new IdentifierQuery())
                .AddProperty(new Property("targetAccountID", accountID));
            var result = await network.GetAsync("/database/getGJUserInfo20.php", query);
            if (result == "-1")
                return null;
            return new AccountInfo(result);
        }

        public async Task<LevelInfoPage> GetMyLevels(PasswordQuery account, int userId, int page)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(account)
                .AddToChain(new GetLevelsQuery(SearchType.OnAccaunt)
            {
                QueryString = userId.ToString(),
                Page = page
            });
            var levels = new LevelInfoPage();
            levels.Load(await network.GetAsync("/database/getGJLevels21.php", query));
            return levels;
        }
    }
}
