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
        bool SendToken(string email, string title, string message, int code);
    }
}
