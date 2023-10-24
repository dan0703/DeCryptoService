using Domain.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IChatMessage
    {
        [OperationContract]
        void SendMessage(ChatMessage chatMessage);

        [OperationContract(IsOneWay = true)]
        void JoinChat(string nickname, int code);

        [OperationContract]
        void LeaveChat(string nickname, int code);
    }
    [ServiceContract]
    public interface IChatServicesCallback
    {
        [OperationContract]
        void GetChatMessages(List<ChatMessage> messages);
    }
}
