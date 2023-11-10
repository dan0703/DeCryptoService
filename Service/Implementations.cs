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

namespace Service
{
    public partial class Implementations : IPlayerServices, IAccountServices
    {

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
                                Console.WriteLine($"Error en la propiedad {validationError.PropertyName}: {validationError.ErrorMessage}");
                            }
                        }
                        //enviar excepciones al logger
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
                        //mandar exepcion al logger
                        Console.WriteLine($"Error: {ex.Message}");

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
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool VerifyEmail(Account account)
        {
            try
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var foundAccount = context.AccountSet.Where(accountSet => accountSet.Email == account.email).FirstOrDefault();
                    if (foundAccount == default)
                    {
                        return false;
                    }
                    else
                    {
                        foundAccount.EmailVerify = true;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
        public bool CurrentPassword(Account account, string currentPassword)
        {
            try
            {
                using (DeCryptoEntities context = new DeCryptoEntities()){
                    var foundAccount = context.AccountSet.Where(accountSet => accountSet.Email == account.email).FirstOrDefault();
                    if (foundAccount == default)
                    {
                        return false;
                    }
                    else
                    {
                        if (foundAccount.Password == currentPassword)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                //enviar a logger
                return false;
            }
        }

        public bool ChangePassword(Account account, string newPassword)
        {
            try
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var foundAccount = context.AccountSet.Where(accountSet => accountSet.Email == account.email).FirstOrDefault();
                    if (foundAccount == default)
                    {
                        return false;
                    }
                    else
                    {
                        foundAccount.Password = newPassword;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                //Mandar al logger
                Console.WriteLine($"Error: {ex.Message}");

                return false;
            }
        }

        public bool ExistAccount(string email)
        {
            try
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var foundAccount = context.AccountSet.Where(accountSet => accountSet.Email == email).FirstOrDefault();
                    if (foundAccount == default)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex) {
                //mandar al logger
                Console.WriteLine($"Error: {ex.Message}");

                return false;
            }
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
            catch
            {
                exist = false;
            }
            return exist;
        }
    }



    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class Implementations : IJoinToGame, IChatMessage
    {
        private static readonly List<int> rooms = new List<int>();
        private static readonly Dictionary<string, int> roomPlayers = new Dictionary<string, int>();
        private static readonly Dictionary<int, BlueTeam> bluePlayers = new Dictionary<int, BlueTeam>();
        private static readonly Dictionary<int, RedTeam> redPlayers = new Dictionary<int, RedTeam>();
        private static readonly Dictionary<string, IJoinToGameCallback> players = new Dictionary<string, IJoinToGameCallback>();
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
                players[player].RecivePlayers(profiles);
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
            if (rooms.Contains(code))
            {
                return true;
            }
            else
            {
                return false;
            }
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
                players[player].ReciveBlueTeam(blueTeam);
            }
        }

        private void SetRedteam(int code, RedTeam redTeam)
        {
            var playersList = roomPlayers.Where(player => player.Value.Equals(code)).Select(player => player.Key).ToList();
            foreach (var player in playersList)
            {
                players[player].ReciveRedTeam(redTeam);
            }
        }

        public void JoinToGame(string nickname, byte[] profilePicture)
        {
            if (!players.ContainsKey(nickname))
            {
                players.Add(nickname, OperationContext.Current.GetCallbackChannel<IJoinToGameCallback>());
                profilePictures.Add(nickname, profilePicture);
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
            int amountPlayers = roomPlayers.Where(player => player.Value.Equals(code)).Select(player => player.Key).Count();
            if (amountPlayers >= 4)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                        players[recipientNickname].ReciveFriendRequest(senderNickname, new List<string>());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
    }
}
