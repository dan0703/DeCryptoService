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

namespace Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class Implementations : IPlayerServices, IAccountServices
    {
        private static readonly Dictionary<string, IPlayerServicesCallback> players = new Dictionary<string, IPlayerServicesCallback>();

        public void AddPlayer(string nickname)
        {
            if (players.ContainsKey(nickname))
            {
                players.Add(nickname, OperationContext.Current.GetCallbackChannel<IPlayerServicesCallback>());
                foreach (var player in players)
                {
                    Console.WriteLine(player);
                    players[player.Key].GetPlayers(GetPlayersList());
                }
                
            }
        }

        private List<User> GetPlayersList()
        {
            var list = new List<User>();
            foreach(var player in players)
            {
                var newPlayer = new User()
                {
                    accountNickname = player.Key
                };

                list.Add(newPlayer);
            }
            return list;
        }

        public bool Login(Account account)
        {
            if(account == null)
            {
                return false;
            }
            else
            {
                using (DeCryptoEntities context = new DeCryptoEntities())
                {
                    var foundUser = context.AccountSet.Where(accountSet => accountSet.Email == account.email).FirstOrDefault();
                    if (foundUser == default)
                    {
                        return false;
                    }
                    else
                    {
                        if (account.password == foundUser.Password)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
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
                    }catch (DbEntityValidationException ex)
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
            if(user == null) 
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

                    }catch(DbUpdateException ex)
                    {
                        //mandar exepcion al logger
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
    }

    public partial class Implementations : IPlayerServices
    {

    } 
}
