using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [ServiceContract(CallbackContract = typeof(IFriendsServicesCallback))]

    public interface IFriendsServices
    {
        [OperationContract(IsOneWay = true)]
        void SendFriendRequest(string senderNickname, string recipientNickname);

        [OperationContract]
        Dictionary<string, byte[]> GetFriendList(string nickname);

        [OperationContract(IsOneWay = true)]
        void AcceptFriendRequest(string senderNickname, string recipientNickname);

        [OperationContract]
        void jointToFriendRequestService(string nickname);
    }

    [ServiceContract]
    public interface IFriendsServicesCallback
    {
        [OperationContract]
        void ReciveFriendRequest(string senderNickname, List<string> friendRequestList);

        [OperationContract]
        void SetFriendList(List<string> friendList);
    }
}
