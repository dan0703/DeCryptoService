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
        void LeaveRoom(string nickname, int code);
    }

    [ServiceContract]
    public interface IJoinToGameCallback
    {
        [OperationContract]
        void RecivePlayers(Dictionary<string, byte[]> profiles);

    }
}
