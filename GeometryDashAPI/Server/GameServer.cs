using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Queries;
using System;
using System.Threading.Tasks;
using GeometryDashAPI.Server.Responses;

namespace GeometryDashAPI.Server
{
    public class GameServer
    {
        private Network network = new();

        public async Task<TopResponse> GetTop(TopType type, int count)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(new IdentifierQuery())
                .AddProperty(new Property("type", type.GetAttributeOfSelected<OriginalNameAttribute>().OriginalName))
                .AddProperty(new Property("count", count));
            return await Get<TopResponse>("/database/getGJScores20.php", query);
        }

        public async Task<LevelPageResponse> GetLevels(GetLevelsQuery getLevelsQuery)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(getLevelsQuery);
            return await Get<LevelPageResponse>("/database/getGJLevels21.php", query);
        }

        public async Task<LevelPageResponse> GetFeatureLevels(int page)
        {
            return await GetLevels(new GetLevelsQuery(SearchType.Featured)
            {
                QueryString = "",
                Page = page,
            });
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            var query = new FlexibleQuery()
                .AddProperty(new Property("udid", Guid.NewGuid()))
                .AddProperty(new Property("userName", username))
                .AddProperty(new Property("password", password))
                .AddProperty(new Property("sID", 76561198946149263))
                .AddProperty(new Property("secret", "Wmfv3899gc9"));
            return await Get<LoginResponse>("/database/accounts/loginGJAccount.php", query);
        }

        public async Task<AccountCommentPageResponse> GetAccountComments(int accountId, int page)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddProperty(new Property("accountID", accountId))
                .AddProperty(new Property("page", page))
                .AddProperty(new Property("total", 0));
            return await Get<AccountCommentPageResponse>("/database/getGJAccountComments20.php", query);
        }

        public async Task<UserResponse> GetUserByName(string name)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddProperty(new Property("str", name))
                .AddProperty(new Property("total", 0))
                .AddProperty(new Property("page", 0));
            return await Get<UserResponse>("/database/getGJUsers20.php", query);
        }

        public async Task<LevelResponse> DownloadLevel(int id)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddProperty(new Property("levelID", id))
                .AddProperty(new Property("inc", 0))
                .AddProperty(new Property("extras", 0));
            return await Get<LevelResponse>("/database/downloadGJLevel22.php", query);
        }

        public async Task<AccountInfoResponse> GetAccountInfo(int accountId)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(new IdentifierQuery())
                .AddProperty(new Property("targetAccountID", accountId));
            return await Get<AccountInfoResponse>("/database/getGJUserInfo20.php", query);
        }

        public async Task<LevelPageResponse> GetMyLevels(PasswordQuery account, int userId, int page)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(account)
                .AddToChain(new GetLevelsQuery(SearchType.OnAccaunt)
            {
                QueryString = userId.ToString(),
                Page = page
            });
            return await Get<LevelPageResponse>("/database/getGJLevels21.php", query);
        }

        private async Task<T> Get<T>(string path, IQuery query)
            where T: IServerResponseCode, new()
        {
            var response = await network.GetAsync(path, query);
            if (response.Length < 16 && response[0] == '-')
                return new T() { ResponseCode = int.Parse(response) };
            return (T)GeometryDashApi.GetStringParser(typeof(T))(response);
        }
    }
}
