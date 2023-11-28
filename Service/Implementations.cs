using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using DataAccess;
using System.ServiceModel;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Security.Principal;
using log4net;
using System.Data.Entity.Core;

namespace Service
{
    public partial class Implementations : IPlayerServices, IAccountServices
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<User> GetPlayersList()
        {
            var list = new List<User>();
            foreach (var player in players)
            {
                var newPlayer = new User()
                {
                    accountNickname = player.Key
                };

                list.Add(newPlayer);
            }
            return list;
        }

        public Account Login(Account account)
        {
            if (account == null)
            {
                return null;
            }
            else
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var foundAccount = context.AccountSet.Where(accountSet => accountSet.Email == account.email).FirstOrDefault();
                    if (foundAccount == default)
                    {
                        return null;
                    }
                    else
                    {
                        if (account.password == foundAccount.Password)
                        {
                            Account newAccount = new Account()
                            {
                                email = foundAccount.Email,
                                emailVerify = foundAccount.EmailVerify,
                                nickname = foundAccount.Nickname
                            };
                            return newAccount;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public bool RegisterAccount(Account account)
        {
            if (account == null)
            {
                return false;
            }
            else
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var newAccount = new DataAccess.AccountSet
                    {
                        Nickname = account.nickname,
                        Email = account.email,
                        EmailVerify = account.emailVerify,
                        Password = account.password
                    };
                    try
                    {
                        context.AccountSet.Add(newAccount);
                        context.SaveChanges();
                        return true;
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationError in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationError.ValidationErrors)
                            {
                                log.Error(validationError);
                            }
                        }
                        return true;
                    }
                }
            }
        }

        public bool RegisterPlayer(User user)
        {
            if (user == null)
            {
                return false;
            }
            else
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var newUser = new DataAccess.UserSet()
                    {
                        Name = user.name,
                        UserId = user.idUser,
                        Account_Nickname = user.accountNickname,
                        BirthDay = user.birthDay
                    };
                    try
                    {
                        context.UserSet.Add(newUser);
                        context.SaveChanges();
                        return true;

                    }
                    catch (DbUpdateException ex)
                    {
                        log.Debug(ex);
                        return false;
                    }
                };
            }
        }

        public bool SendToken(string email, string title, string message, int code)
        {
            string smtpServer = ConfigurationSettings.AppSettings["SMTP_SERVER"];
            int port = int.Parse(ConfigurationSettings.AppSettings["PORT"]);
            string emailAddress = ConfigurationSettings.AppSettings["EMAIL_ADDRESS"];
            string password = ConfigurationSettings.AppSettings["PASSWORD"];
            string addressee = email;
            bool success = false;
            try
            {

                var mailMessage = new MailMessage(emailAddress, addressee, title, (message + " " + code + "."))
                {
                    IsBodyHtml = true
                };
                var smtpClient = new SmtpClient(smtpServer)
                {
                    Port = port,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailAddress, password),
                    EnableSsl = true
                };
                smtpClient.Send(mailMessage);
                success = true;
            }
            catch (SmtpException ex)
            {
                log.Error(ex);
            }
            catch (InvalidOperationException ex)
            {
                log.Error(ex);
            }
            catch (FormatException ex)
            {
                log.Error(ex);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return success;
        }

        public bool VerifyEmail(Account account)
        {
            bool success = false;
            
            try
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var foundAccount = context.AccountSet.Where(accountSet => accountSet.Email == account.email).FirstOrDefault();
                    if (foundAccount == default)
                    {
                        success = false;
                    }
                    else
                    {
                        foundAccount.EmailVerify = true;
                        context.SaveChanges();
                        success = true;
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                log.Error(ex);
                success = false;
            }
            return success;
        }
        public bool IsCurrentPassword(Account account, string passwordIngresed)
        {
            bool success = false;

            using (DeCryptoEntities context = new DeCryptoEntities()){
                var foundAccount = context.AccountSet.Where(accountSet => accountSet.Email == account.email).FirstOrDefault();
                if (foundAccount == default)
                {
                    success = false;
                }
                else
                {
                     if (foundAccount.Password == passwordIngresed)
                     {
                        success = true;
                     }
                     else
                        {
                        success = false;
                     }
                }
            }
            return success;
        }

        public bool ChangePassword(Account account, string newPassword)
        {
            bool success = false;

            try
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var foundAccount = context.AccountSet.Where(accountSet => accountSet.Email == account.email).FirstOrDefault();
                    if (foundAccount == default)
                    {
                        success = false;
                    }
                    else
                    {
                        foundAccount.Password = newPassword;
                        context.SaveChanges();
                        success = true;
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                log.Error(ex);
                success = false;
            }
            return success;
        }

        public bool ExistAccount(string email)
        {
            bool success = false;

            using (DeCryptoEntities context = new DeCryptoEntities())
            {
                var foundAccount = context.AccountSet.Where(accountSet => accountSet.Email == email).FirstOrDefault();
                if (foundAccount == default)
                {
                    success = false;
                }
                else
                {
                    success = true;
                }
            }
            return success;
        }

        public List<string> GetSimilarsNickNames(string nickname)
        {
            List<string> nickNameList = new List<string>();
            try
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var foundPlayers = (from Account in context.AccountSet 
                                        where Account.Nickname.StartsWith(nickname)
                                        select Account).ToList();
                    if (foundPlayers.Any())
                    {
                        foreach (var player in foundPlayers)
                        {
                            nickNameList.Add(player.Nickname);
                        }
                    }
                    return nickNameList;
                }
            }
            catch
            {
                return  nickNameList;
            }
        }

        public bool ExistNickname(string nickName)
        {
            bool exist = false;
            
            try
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var foundPlayer = context.AccountSet.Where(accountSet => accountSet.Nickname == nickName).FirstOrDefault();
                    if(foundPlayer != default)
                    {
                        exist = true;
                    }
                    else
                    {
                        exist = false;
                    }
                }
            }
            catch (EntityException ex)
            {
                log.Error(ex);
                exist = false;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                exist = false;
            }
            return exist;
        }
    }



    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public partial class Implementations : IJoinToGame, IChatMessage, IFriendsServices
    {
        private static readonly List<int> rooms = new List<int>();
        private static readonly Dictionary<string, int> roomPlayers = new Dictionary<string, int>();
        private static readonly Dictionary<int, BlueTeam> bluePlayers = new Dictionary<int, BlueTeam>();
        private static readonly Dictionary<int, RedTeam> redPlayers = new Dictionary<int, RedTeam>();

        private static readonly Dictionary<string, Player> players = new Dictionary<string, Player>();

        private static readonly Dictionary<string, byte[]> profilePictures = new Dictionary<string, byte[]>();
        private static readonly Dictionary<int, List<ChatMessage>> roomMessages = new Dictionary<int, List<ChatMessage>>();
        private static readonly Dictionary<string, IChatMessageCallback> chatPlayers = new Dictionary<string, IChatMessageCallback>();

        public int CreateRoom()
        {
            var random = new Random();
            var lowerBound = 1000;
            var upperBound = 9999;
            int code = random.Next(lowerBound, upperBound);
            while (rooms.Contains(code))
            {
                code = random.Next(lowerBound, upperBound);
            };
            rooms.Add(code);
            bluePlayers.Add(code, new BlueTeam());
            redPlayers.Add(code, new RedTeam());
            return code;
        }

        public void JoinToRoom(int code, string nickname)
        {
            if (!roomPlayers.ContainsKey(nickname))
            {
                roomPlayers.Add(nickname, code);
            }
            SetPlayers(code);
            if (redPlayers.ContainsKey(code) && bluePlayers.ContainsKey(code))
            {
                SetBlueteam(code, bluePlayers[code]);
                SetRedteam(code, redPlayers[code]);
            }
        }

        private void SetPlayers(int code)
        {
            var playersList = roomPlayers.Where(player => player.Value.Equals(code)).Select(player => player.Key).ToList();
            Dictionary<string, byte[]> profiles = profilePictures;

            foreach (var player in playersList)
            {
                if (!profiles.ContainsKey(player))
                {
                    profiles.Remove(player);
                }
            }
            foreach (var player in playersList)
            {
                players[player].joinToGameCallback.RecivePlayers(profiles);
            }
        }

        public void LeaveRoom(string nickname, int code, BlueTeam blueTeam, RedTeam redTeam)
        {
            roomPlayers.Remove(nickname);
            bluePlayers[code] = blueTeam;
            redPlayers[code] = redTeam;
            if (!roomPlayers.ContainsValue(code))
            {
                rooms.Remove(code);
                bluePlayers.Remove(code);
                redPlayers.Remove(code);
            }
            else
            {
                SetPlayers(code);
            }
        }

        public bool AllreadyExistRoom(int code)
        {
            bool existRoom;
            if (rooms.Contains(code))
            {
                existRoom = true;
            }
            else
            {
                existRoom = false;
            }
            return existRoom;
        }

        public void JoinToBlueTeam(BlueTeam blueTeam, int code)
        {
            if (bluePlayers.ContainsKey(code))
            {
                bluePlayers[code] = blueTeam;
            }
            else
            {
                bluePlayers.Add(code, blueTeam);
            }
            SetBlueteam(code, blueTeam);
        }

        public void JoinToRedTeam(RedTeam redTeam, int code)
        {
            if (redPlayers.ContainsKey(code))
            {
                redPlayers[code] = redTeam;
            }
            else
            {
                redPlayers.Add(code, redTeam);
            }
            SetRedteam(code, redTeam);
        }

        private void SetBlueteam(int code, BlueTeam blueTeam)
        {
            var playersList = roomPlayers.Where(player => player.Value.Equals(code)).Select(player => player.Key).ToList();
            foreach (var player in playersList)
            {
                players[player].joinToGameCallback.ReciveBlueTeam(blueTeam);               
            }
        }

        private void SetRedteam(int code, RedTeam redTeam)
        {
            var playersList = roomPlayers.Where(player => player.Value.Equals(code)).Select(player => player.Key).ToList();
            foreach (var player in playersList)
            {
                players[player].joinToGameCallback.ReciveRedTeam(redTeam);
            }
        }

        public void JoinToGame(string nickname, byte[] profilePicture)
        {
            Player player = new Player
            {
                friendsServicesCallback = OperationContext.Current.GetCallbackChannel<IFriendsServicesCallback>(),
                joinToGameCallback = OperationContext.Current.GetCallbackChannel<IJoinToGameCallback>()
            };

            if (!players.ContainsKey(nickname))
            {
                players.Add(nickname, player);
                profilePictures.Add(nickname, profilePicture);
            }
            else
            {
                players[nickname] = player;
            }
        }

        public void LeaveGame(string nickname)
        {
            if (players.ContainsKey(nickname))
            {
                players.Remove(nickname);
                profilePictures.Remove(nickname);
            }
        }

        public bool IsFullRoom(int code)
        {
            bool isFullRoom = false;

            int amountPlayers = roomPlayers.Where(player => player.Value.Equals(code)).Select(player => player.Key).Count();
            if (amountPlayers >= 4)
            {
                isFullRoom = true;
            }
            else
            {
                isFullRoom = false;
            }
            return isFullRoom;
        }

        public void SendMessage(ChatMessage chatMessage, int code)
        {
            var chatMessagesList = GetChatMessages(code);

            if (chatMessagesList != null)
            {
                roomMessages[code].Add(chatMessage);
                SetMessage(code);
            }
        }

        public void JoinChat(string nickname, int code)
        {
            if (!chatPlayers.ContainsKey(nickname))
            {
                chatPlayers.Add(nickname, OperationContext.Current.GetCallbackChannel<IChatMessageCallback>());
            }

            var chatMessagesList = GetChatMessages(code);

            if (chatMessagesList != null)
            {
                chatPlayers[nickname].ReceiveChatMessages(chatMessagesList);
            }
        }

        private void SetMessage(int code)
        {
            var playersInChat = roomPlayers.Where(player => player.Value.Equals(code)).Select(player => player.Key).ToList();

            foreach (var player in playersInChat)
            {
                if (chatPlayers.ContainsKey(player))
                {
                    var chatMessagesList = GetChatMessages(code);
                    chatPlayers[player].ReceiveChatMessages(chatMessagesList);
                }
            }
        }

        public List<ChatMessage> GetChatMessages(int code)
        {
            if (!roomMessages.ContainsKey(code))
            {
                roomMessages.Add(code, new List<ChatMessage>());
            }
            return roomMessages[code];
        }

        public void LeaveChat(string nickname, int code)
        {
            if (chatPlayers.ContainsKey(nickname))
            {
                chatPlayers.Remove(nickname);
            }

            if (!roomPlayers.ContainsValue(code))
            {
                roomMessages.Remove(code);
            }
        }

        public void SendFriendRequest(string senderNickname, string recipientNickname)
        {
            try
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var newRequest = new FriendList()
                    {
                        Account2_Nickname = senderNickname,
                        Account1_Nickname = recipientNickname,
                        IsBlocked = false,
                        StartDate = DateTime.Now,
                        RequestAccepted = false
                    };
                    context.FriendList.Add(newRequest);
                    context.SaveChanges();
                    var friendRequests = context.FriendList.Where(friendList => friendList.Account2_Nickname.Equals(recipientNickname) 
                    || friendList.Account1_Nickname.Equals(recipientNickname) && friendList.RequestAccepted == false).ToList();


                    if (players.ContainsKey(recipientNickname))
                    {
                        players[recipientNickname].friendsServicesCallback.ReciveFriendRequest(senderNickname, new List<string>());
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                log.Error(ex);
            }
        }

        public Dictionary<string, byte[]> GetFriendList(string nickname)
        {
            throw new NotImplementedException();
        }

        public void AcceptFriendRequest(string senderNickname, string recipientNickname)
        {
            throw new NotImplementedException();
        }

        public void StartGame(int code)
        {
            var playersByRoom = roomPlayers.Where(player => player.Value.Equals(code)).Select(player => player.Key).ToList();

            foreach (var player in playersByRoom)
            {
                Console.WriteLine(player);
                if (players.ContainsKey(player))
                {
                    players[player].joinToGameCallback.GoToGameWindow();
                }
            }
        }
    }
}
