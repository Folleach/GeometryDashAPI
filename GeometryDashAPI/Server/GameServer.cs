using GeometryDashAPI.Exceptions;
using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Models;
using GeometryDashAPI.Server.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryDashAPI.Server
{
    public class GameServer
    {
        private Network network = new Network();

        private OnlineQuery defaultOnlineQuery = new OnlineQuery();

        private Session  session = new Session();

        /*keys:
        message: 14251,
        levelpass: 26364,
        accountpass: 37526,
        challenges: 19847,
        rewards: 59182,
        level: 41274,
        comment: 29481,
        like_rate: 58281,
        user_score: 85271,
        levelscore: 39673*/
        /*salts:
        level:      xI25fpAapCQg,
        comment:    xPT6iUrtws0J,
        like_rate:  ysg6pUrtjn0J,
        user_score: xI35fsAapCRg,
        levelscore: yPg6pUrtWn0J   thanks gdpy for those and rs generator*/

        public string GenerateRandomString()
        {
            string characters = "abcdefghijklmnopqrstuvwxyz";
            characters += characters.ToUpper() + "0123456789";

            Random r = new Random();

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                stringBuilder.Append(characters[r.Next(62)]);
            }

            return stringBuilder.ToString();
        }


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

        public string UploadLevel(int accountID, string userName, string name, string desc, int v, OfficialSong song, int songID, bool auto, bool original, bool twoPlayer, bool unlisted, bool friendsOnly, bool ldm, int password, int starsRequested, int coins, string levelString)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("levelName", name));
            query.AddProperty(new Property("levelDesc", GameConvert.ToBase64(Encoding.ASCII.GetBytes(desc))));
            query.AddProperty(new Property("levelVersion", v));
            query.AddProperty(new Property("audioTrack", (int)song));
            query.AddProperty(new Property("songID", songID));
            query.AddProperty(new Property("auto", GameConvert.BoolToString(auto)));
            query.AddProperty(new Property("original", GameConvert.BoolToString(original)));
            query.AddProperty(new Property("twoPlayer", GameConvert.BoolToString(twoPlayer)));
            query.AddProperty(new Property("unlisted", GameConvert.BoolToString(unlisted)));
            query.AddProperty(new Property("unlisted2", GameConvert.BoolToString(friendsOnly)));
            query.AddProperty(new Property("ldm", GameConvert.BoolToString(ldm)));
            query.AddProperty(new Property("password", password));
            query.AddProperty(new Property("objects", 0));
            query.AddProperty(new Property("requestedStars", starsRequested));
            query.AddProperty(new Property("coins", coins));
            query.AddProperty(new Property("levelString", levelString));
            query.AddProperty(new Property("levelLength", 0));
            query.AddProperty(new Property("levelInfo", "H4sIAAAAAAAAC_NIrVQoyUgtStVRCMpPSi0qUbDStwYAsgpl1RUAAAA="));

            string[] values = { Crypt.GenerateSeed2(levelString) };
            string seed2 = Crypt.GenerateCheck(values, "xI25fpAapCQg", 41274);

            string result = network.Get("/database/uploadGJLevel21.php", query);
            return result;
        } //currently doesn't working

        public LevelInfoPage GetLevels(GetLevelsQuery getLevelsQuery)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(getLevelsQuery);
            LevelInfoPage levels = new LevelInfoPage();
            levels.Load(network.Get("/database/getGJLevels21.php", query));
            return levels;
        }

        public LevelInfo GetLevel(int id) {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            GetLevelsQuery getLevelsQuery = new GetLevelsQuery(SearchType.ByString);
            getLevelsQuery.QueryString = id.ToString();
            query.AddToChain(getLevelsQuery);
            LevelInfoPage levels = new LevelInfoPage();
            levels.Load(network.Get("/database/getGJLevels21.php", query));
            return levels[0];
        }

        public LevelLeaderBoardScoreInfoArray GetLevelLeaderBoard(int accountID, int levelID, LeaderBoardGettingType type)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("levelID", levelID));
            query.AddProperty(new Property("type", (int)type));
            PlayerInfoArray players = new PlayerInfoArray();
            string result = network.Get("/database/getGJLevelScores211.php", query);
            return new LevelLeaderBoardScoreInfoArray(result, levelID);
        }

        public Dictionary<int, List<int>> GetGauntlets()
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            string result = network.Get("/database/getGJGauntlets21.php", query);
            Dictionary<int, List<int>> GauntletsInfo = new Dictionary<int, List<int>>();
            result = result.Split('#')[0];
            foreach (string infoString in result.Split('|'))
            {
                List<int> levelIDs = new List<int>(5);
                foreach (string levelID in infoString.Split(':')[3].Split(','))
                    levelIDs.Add(int.Parse(levelID));
                GauntletsInfo.Add(int.Parse(infoString.Split(':')[1]), levelIDs);
            }
            return GauntletsInfo; // i could've make it return all info about levels, but it takes too long to get all of them
        }

        public string DeleteLevel(int accountID, int levelID)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("levelID", levelID));
            string result = network.Get("/database/deleteGJLevelUser20.php", query);
            return result;
        } //currently doesn't work. idk why

        public string GetDailyLevel(int accountID, bool weekly = false)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID)); // idk why it needs it
            query.AddProperty(new Property("weekly", weekly ? 1 : 0));
            LevelInfoPage levels = new LevelInfoPage();
            string result = network.Get("/database/getGJDailyLevel.php", query);
            return result; // idk what does it return
        }

        public string LikeLevel(int accountID, int levelID, bool dislike = false)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();
            string randomString = GenerateRandomString();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddToChain(new IdentifierQuery());
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("rs", randomString));
            query.AddProperty(new Property("type", 1));
            query.AddProperty(new Property("like", dislike ? 0 : 1));
            query.AddProperty(new Property("itemID", levelID));
            query.AddProperty(new Property("special", 0));
            string[] values = { "0", levelID.ToString(), dislike ? "0" : "1", "1" , randomString, accountID.ToString(), "00000000-ffff-dddd-5555-123456789abc", "1" };
            query.AddProperty(new Property("chk", Crypt.GenerateCheck(values, "ysg6pUrtjn0J", 58281)));
            string result = network.Get("/database/likeGJItem211.php", query);
            return result; //probably doesn't work to prevent like/dislike bots
        }

        public string LikeComment(int accountID, int commentID, bool dislike = false)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();
            string randomString = GenerateRandomString();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddToChain(new IdentifierQuery());
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("rs", randomString));
            query.AddProperty(new Property("type", 1));
            query.AddProperty(new Property("like", dislike ? 0 : 1));
            query.AddProperty(new Property("itemID", commentID));
            query.AddProperty(new Property("special", commentID));
            string[] values = { commentID.ToString(), commentID.ToString(), dislike ? "0" : "1", "2", randomString, accountID.ToString(), "00000000-ffff-dddd-5555-123456789abc", "1" };
            query.AddProperty(new Property("chk", Crypt.GenerateCheck(values, "ysg6pUrtjn0J", 58281)));
            string result = network.Get("/database/likeGJItem211.php", query);
            return result; //probably doesn't work to prevent like/dislike bots
        }

        public string LikeAccountComment(int accountID, int commentID, bool dislike = false)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();
            string randomString = GenerateRandomString();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddToChain(new IdentifierQuery());
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("rs", randomString));
            query.AddProperty(new Property("type", 1));
            query.AddProperty(new Property("like", dislike ? 0 : 1));
            query.AddProperty(new Property("itemID", commentID));
            query.AddProperty(new Property("special", commentID));
            string[] values = { commentID.ToString(), commentID.ToString(), dislike ? "0" : "1", "3", randomString, accountID.ToString(), "00000000-ffff-dddd-5555-123456789abc", "1" };
            query.AddProperty(new Property("chk", Crypt.GenerateCheck(values, "ysg6pUrtjn0J", 58281)));
            string result = network.Get("/database/likeGJItem211.php", query);
            return result; //probably doesn't work to prevent like/dislike bots
        }

        public LevelInfoPage GetFeatureLevels(int page)
        {
            return GetLevels(new GetLevelsQuery(SearchType.Featured)
            {
                QueryString = "",
                Page = page,
            });
        }

        public int PostLevelComment(int accountID, string userName, int levelID, string comment, int percentage)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            string commentBase64 = GameConvert.ToBase64(Encoding.ASCII.GetBytes(comment));

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("comment", commentBase64));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("levelID", levelID));
            if (percentage > 0) query.AddProperty(new Property("percent", percentage));
            string[] values = { userName, commentBase64, levelID.ToString(), percentage.ToString(), "0" };
            query.AddProperty(new Property("chk", Crypt.GenerateCheck(values, "xPT6iUrtws0J", 29481)));
            string result = network.Get("/database/uploadGJComment21.php", query);
            int res = 0;
            int.TryParse(result, out res);
            return res; // comment id
        } //it requires username, because game server can return it with wrong letter capitalisation (took me TOO long to figure out)

        public LevelCommentArray GetLevelComments(int levelID, int page, CommentsSortMode sortMode = CommentsSortMode.MostLiked, int count = 20)
        {
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddProperty(new Property("levelID", levelID));
            query.AddProperty(new Property("page", page));
            query.AddProperty(new Property("total", 0));
            query.AddProperty(new Property("count", count));
            query.AddProperty(new Property("mode", (int) sortMode));
            string result = network.Get("/database/getGJComments21.php", query);
            if (result == "-1")
                return null;
            return new LevelCommentArray(result);
        }

        public string DeleteLevelComment(int accountID, int levelID, int commentID)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("levelID", levelID));
            query.AddProperty(new Property("commentID", commentID));
            string result = network.Get("/database/deleteGJComment20.php", query);
            return result;
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

        public string PostAccountComment(int accountID, string userName, string comment)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            string commentBase64 = GameConvert.ToBase64(Encoding.ASCII.GetBytes(comment));

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("comment", commentBase64));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("cType", 1));
            string[] values = { userName, "0", "0", "1" }; //took it from gd.py. idk what it means
            query.AddProperty(new Property("chk", Crypt.GenerateCheck(values, "xPT6iUrtws0J", 29481)));
            string result = network.Get("/database/uploadGJAccComment20.php", query);
            //int res = 0;
            //int.TryParse(result, out res);
            return result; // comment id
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

        public string DeleteAccountComment(int accountID, int commentID)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("cType", 1));
            query.AddProperty(new Property("commentID", commentID));
            string result = network.Get("/database/deleteGJAccComment20.php", query);
            return result;
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

        public MessageArray GetMessages(int accountID, int page, bool sent = false)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("page", page));
            query.AddProperty(new Property("total", 0));
            if (sent) query.AddProperty(new Property("getSent", 1));
            string result = network.Get("/database/getGJMessages20.php", query);
            if (result == "-1") return null;
            return new MessageArray(result, this, accountID); // could've just send session if it wasn't private
        }

        public Message GetMessage(int id, int accountID, bool isSender)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("messageID", id));
            query.AddProperty(new Property("accountID", accountID));
            if (isSender) query.AddProperty(new Property("isSender", 1));
            string result = network.Get("/database/downloadGJMessage20.php", query);
            if (result == "-1") 
                return null;
            return new Message(result);
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

        public string DeleteMessage(int accountID, int messageID, bool outcoming = true)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("isSender", GameConvert.BoolToString(outcoming)));
            query.AddProperty(new Property("messageID", messageID));
            string result = network.Get("/database/deleteGJMessages20.php", query);
            return result;
        }

        public string SendFriendRequest(int accountID, int receiverAccountID, string reason)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new IdentifierQuery());
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("toAccountID", receiverAccountID));
            query.AddProperty(new Property("comment", GameConvert.ToBase64(Encoding.ASCII.GetBytes(reason))));
            string result = network.Get("/database/uploadFriendRequest20.php", query);
            return result;
        }

        public string ReadFriendRequest(int accountID, int id)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new IdentifierQuery());
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("requestID", id));
            string result = network.Get("/database/readGJFriendRequest20.php", query);
            return result; //just mark it as readed
        }

        public string AcceptFriendRequest(int accountID, int targetAccountID, int id)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new IdentifierQuery());
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("targetAccountID", targetAccountID));
            query.AddProperty(new Property("requestID", id));
            string result = network.Get("/database/acceptGJFriendRequest20.php", query);
            if (result == "-1") return null;
            ReadFriendRequest(accountID, id);
            return result;
        }

        public string RemoveFriendRequest(int accountID, int targetAccountID, bool outcoming)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new IdentifierQuery());
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("targetAccountID", targetAccountID));
            if (outcoming) query.AddProperty(new Property("isSender", 1));
            string result = network.Get("/database/deleteGJFriendRequests20.php", query);
            return result;
        }

        public FriendRequestArray GetFriendRequests(int accountID, int page, bool sent = false)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("page", page));
            query.AddProperty(new Property("total", 0));
            if (sent) query.AddProperty(new Property("getSent", 1));
            string result = network.Get("/database/getGJFriendRequests20.php", query);
            if (result == "-1") return null;
            return new FriendRequestArray(result);
        }

        public AccountInfoArray GetFriends(int accountID, int page)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("page", page));
            query.AddProperty(new Property("total", 0));
            query.AddProperty(new Property("type", 0));
            string result = network.Get("/database/getGJUserList20.php", query);
            if (result == "-1") return null;
            return new AccountInfoArray(result);
        }

        public string RemoveFriend(int accountID, int targetAccountID)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new IdentifierQuery());
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("targetAccountID", targetAccountID));
            string result = network.Get("/database/removeGJFriend20.php", query);
            return result;
        }

        public string BlockUser(int accountID, int targetAccountID)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();

            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new IdentifierQuery());
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("targetAccountID", targetAccountID));
            string result = network.Get("/database/blockGJUser20.php", query);
            return result;
        }

        public AccountInfoArray GetBlockedUsers(int accountID, int page)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("page", page));
            query.AddProperty(new Property("total", 0));
            query.AddProperty(new Property("type", 1));
            string result = network.Get("/database/getGJUserList20.php", query);
            if (result == "-1") return null;
            return new AccountInfoArray(result);
        }

        public string UpdatePrivacySettings(int accountID, MessagesPrivacy mp, FriendRequestsPrivacy frp, CommentHistoryShowcasePrivacy csp, string youtubeLink, string twitterLink, string twitchLink)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("mS", (int) mp));
            query.AddProperty(new Property("frS", (int) frp));
            query.AddProperty(new Property("cS", (int) csp));
            query.AddProperty(new Property("yt", youtubeLink));
            query.AddProperty(new Property("twitter", twitterLink));
            query.AddProperty(new Property("twitch", twitchLink));
            string result = network.Get("/database/getGJUserList20.php", query);
            if (result == "-1") return null;
            return result;
        }

        public QuestsInfo GetQuests(int accountID)
        {
            if (session.isEmpty)
                throw new UserNotLoggedInException();
            FlexibleQuery query = new FlexibleQuery();
            query.AddToChain(defaultOnlineQuery);
            query.AddToChain(new AuthentificationQuery(session));
            query.AddToChain(new IdentifierQuery());
            query.AddProperty(new Property("accountID", accountID));
            query.AddProperty(new Property("world", 0));
            string[] values = { new Random().Next(99999).ToString() };
            query.AddProperty(new Property("chk", Crypt.GenerateCheck(values, "", 19847).Remove(12)));
            string result = network.Get("/database/getGJChallenges.php", query);
            return new QuestsInfo(result);
        }
    }
}