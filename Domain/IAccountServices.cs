using Domain.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    [ServiceContract]
    public interface IAccountServices
    {
        [OperationContract]
        bool RegisterAccount(Account account);
        [OperationContract]
        Account Login(Account account);
        [OperationContract]
        Account LoginAsGuest();
        [OperationContract]
        bool SendToken(string email, string title, string message, int code);
        [OperationContract]
        bool VerifyEmail(Account account);
        [OperationContract]
        bool ChangePassword(Account account, string newPassword);
        [OperationContract]
        bool IsCurrentPassword(Account account, string currentPasswor);
        [OperationContract]
        bool ExistAccount(String email);
    }
}
