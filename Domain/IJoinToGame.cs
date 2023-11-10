using Domain.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [ServiceContract(CallbackContract = typeof(IJoinToGameCallback))]
    public interface IJoinToGame
    {
        [OperationContract]
        int CreateRoom();

        [OperationContract(IsOneWay = true)]
        void JoinToRoom(int code, string nickname);

        [OperationContract]
        void LeaveRoom(string nickname, int code, BlueTeam blueTeam, RedTeam redTeam);

        [OperationContract]
        bool AllreadyExistRoom(int code);

        [OperationContract(IsOneWay = true)]
        void JoinToBlueTeam(BlueTeam blueTeam, int code);

        [OperationContract (IsOneWay = true)]
        void JoinToRedTeam(RedTeam redTeam, int code);

        [OperationContract]
        void JoinToGame(string nickname, byte[] profilePicture);

        [OperationContract]
        void LeaveGame(string nickname);
        [OperationContract]
        bool IsFullRoom(int code);

        [OperationContract(IsOneWay = true)]
        void SendFriendRequest(string senderNickname, string recipientNickname);

        [OperationContract]
        Dictionary<string, byte[]> GetFriendList(string nickname);

        [OperationContract (IsOneWay =true)]
        void AcceptFriendRequest(string senderNickname, string recipientNickname);
    }

    [ServiceContract]
    public interface IJoinToGameCallback
    {
        [OperationContract]
        void RecivePlayers(Dictionary<string, byte[]> profiles);

        [OperationContract]
        void ReciveBlueTeam(BlueTeam blueTeam);

        [OperationContract]
        void ReciveRedTeam(RedTeam redTeam);

        [OperationContract]
        void ReciveFriendRequest(string senderNickname, List<string> friendRequestList);

        [OperationContract]
        void SetFriendList(List<string> friendList);
    }
}