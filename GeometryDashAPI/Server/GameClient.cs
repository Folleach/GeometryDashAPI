using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Queries;
using System;
using System.Net;
using System.Threading.Tasks;
using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;
using GeometryDashAPI.Server.Responses;

namespace GeometryDashAPI.Server
{
    public class GameClient
    {
        private readonly IdentifierQuery identifierQuery;
        private readonly OnlineQuery onlineQuery;
        private readonly Network network;

        public GameClient() : this(null)
        {
        }
        
        public GameClient(IdentifierQuery identifierQuery = null, OnlineQuery onlineQuery = null, Network network = null)
        {
            this.identifierQuery = identifierQuery;
            this.onlineQuery = onlineQuery;
            this.network = network ?? new Network();
        }

        public async Task<ServerResponse<TopResponse>> GetTopAsync(TopType type, int count)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(GetIdentifier())
                .AddProperty(new Property("type", type.GetAttributeOfSelected<OriginalNameAttribute>().OriginalName))
                .AddProperty(new Property("count", count));
            return await Get<TopResponse>("/getGJScores20.php", query);
        }

        public async Task<ServerResponse<LevelPageResponse>> SearchLevelsAsync(GetLevelsQuery getLevelsQuery)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(getLevelsQuery);
            return await Get<LevelPageResponse>("/getGJLevels21.php", query);
        }

        public async Task<ServerResponse<LevelPageResponse>> GetFeaturedLevelsAsync(int page)
        {
            return await SearchLevelsAsync(new GetLevelsQuery(SearchType.Featured)
            {
                QueryString = "",
                Page = page,
            });
        }

        public async Task<ServerResponse<LoginResponse>> LoginAsync(string username, string password)
        {
            var query = new FlexibleQuery()
                .AddProperty(new Property("udid", Guid.NewGuid()))
                .AddProperty(new Property("userName", username))
                .AddProperty(new Property("password", password))
                .AddProperty(new Property("sID", 76561198946149263))
                .AddProperty(new Property("secret", "Wmfv3899gc9"));
            return await Get<LoginResponse>("/accounts/loginGJAccount.php", query);
        }

        public async Task<ServerResponse<AccountCommentPageResponse>> GetAccountCommentsAsync(int accountId, int page)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddProperty(new Property("accountID", accountId))
                .AddProperty(new Property("page", page))
                .AddProperty(new Property("total", 0));
            return await Get<AccountCommentPageResponse>("/getGJAccountComments20.php", query);
        }

        public async Task<ServerResponse<UserResponse>> SearchUserAsync(string name)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddProperty(new Property("str", name))
                .AddProperty(new Property("total", 0))
                .AddProperty(new Property("page", 0));
            return await Get<UserResponse>("/getGJUsers20.php", query);
        }

        public async Task<ServerResponse<LevelResponse>> DownloadLevelAsync(int id)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddProperty(new Property("levelID", id))
                .AddProperty(new Property("inc", 0))
                .AddProperty(new Property("extras", 0));
            return await Get<LevelResponse>("/downloadGJLevel22.php", query);
        }

        public async Task<ServerResponse<AccountInfoResponse>> GetAccountAsync(int accountId)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(GetIdentifier())
                .AddProperty(new Property("targetAccountID", accountId));
            return await Get<AccountInfoResponse>("/getGJUserInfo20.php", query);
        }

        public async Task<ServerResponse<LevelPageResponse>> GetMyLevelsAsync(PasswordQuery account, int userId, int page)
        {
            var query = new FlexibleQuery()
                .AddToChain(OnlineQuery.Default)
                .AddToChain(account)
                .AddToChain(new GetLevelsQuery(SearchType.OnAccaunt)
            {
                QueryString = userId.ToString(),
                Page = page
            });
            return await Get<LevelPageResponse>("/getGJLevels21.php", query);
        }

        public async Task<ServerResponse<NoneResponse>> SendMessageAsync(PasswordQuery fromAccount, int toAccountId, Message message)
        {
            var query = new FlexibleQuery()
                .AddToChain(GetOnlineQuery())
                .AddToChain(fromAccount)
                .AddProperty(new Property("toAccountID", toAccountId))
                .AddToChain(message);

            return await Get<NoneResponse>("/uploadGJMessage20.php", query);
        }

        public Task<ServerResponse<MessagesPageResponse>> GetMessagesAsync(PasswordQuery account, int page)
        {
            var query = new FlexibleQuery()
                .AddToChain(GetOnlineQuery())
                .AddToChain(account)
                .AddProperty(new Property("page", page))
                .AddProperty(new Property("total", 0));

            return Get<MessagesPageResponse>("/getGJMessages20.php", query);
        }

        private async Task<ServerResponse<T>> Get<T>(string path, IQuery query) where T : IGameObject
        {
            var (statusCode, body) = await network.GetAsync(path, query);
            return new ServerResponse<T>(statusCode, body);
        }

        private IdentifierQuery GetIdentifier() => identifierQuery ?? IdentifierQuery.Default;

        private OnlineQuery GetOnlineQuery() => onlineQuery ?? OnlineQuery.Default;
    }
}
