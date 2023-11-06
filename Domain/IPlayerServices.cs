using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Domain.data;

namespace Domain
{
    [ServiceContract]
    public interface IPlayerServices
    {
        [OperationContract]
        bool RegisterPlayer(User user);

        [OperationContract]
        List<string> GetSimilarsNickNames(string nickname);

        [OperationContract]
        bool ExistNickname(string nickName);
    }
    [ServiceContract(CallbackContract =typeof(IPlayerServicesCallback))]
    public interface IPlayerServicesCallback
    {
        [OperationContract]
        void GetPlayers(List<User> players);
    } 
}
