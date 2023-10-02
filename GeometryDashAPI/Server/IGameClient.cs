using System.Threading.Tasks;
using GeometryDashAPI.Server.Dtos;
using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Queries;
using GeometryDashAPI.Server.Responses;

namespace GeometryDashAPI.Server;

public interface IGameClient
{
    Task<ServerResponse<TopResponse>> GetTopAsync(TopType type, int count);
    Task<ServerResponse<LevelPageResponse>> SearchLevelsAsync(GetLevelsQuery getLevelsQuery);
    Task<ServerResponse<LevelPageResponse>> GetFeaturedLevelsAsync(int page);
    Task<ServerResponse<LoginResponse>> LoginAsync(string username, string password);
    Task<ServerResponse<AccountCommentPageResponse>> GetAccountCommentsAsync(int accountId, int page);
    Task<ServerResponse<UserResponse>> SearchUserAsync(string name);
    Task<ServerResponse<LevelResponse>> DownloadLevelAsync(int id);
    Task<ServerResponse<AccountInfoResponse>> GetAccountAsync(int accountId);
    Task<ServerResponse<LevelPageResponse>> GetMyLevelsAsync(PasswordQuery account, int userId, int page);
    Task<ServerResponse<NoneResponse>> SendMessageAsync(PasswordQuery fromAccount, int toAccountId, Message message);
    Task<ServerResponse<MessagesPageResponse>> GetMessagesAsync(PasswordQuery account, int page);
    Task<ServerResponse<MessageContent>> ReadMessageAsync(PasswordQuery account, int messageId);
}
