using Domain.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [ServiceContract(CallbackContract = typeof(IGameServiceCallback))]
    public interface IGameServices
    {
        [OperationContract(IsOneWay = true)]
        void JoinToGameBoard(byte[] profilePicture, string nickname, int code);

        [OperationContract(IsOneWay = true)]
        void MakeWaitForClues(string targetNickname, int code);

        [OperationContract]
        List<string> GetBlueTeamWords(int code);

        [OperationContract]
        List<string> GetRedTeamWords(int code);

        [OperationContract(IsOneWay =true)]
        void GiveBlueTeamClues(List<string> blueTeamClues, int code, string ownNickname);

        [OperationContract(IsOneWay = true)]
        void GiveRedTeamClues(List<string> redTeamClues, int code, string ownNickname);

        [OperationContract(IsOneWay = true)]
        void GiveBlueTeamGuesses(List<string> blueTeamGuesses, int code, string ownNickname);

        [OperationContract(IsOneWay = true)]
        void GiveRedTeamGesses(List<string> redTeamGuesses, int code, string ownNickname);

        [OperationContract(IsOneWay = true)]
        void SubmitBlueTeamInterceptionResult(bool isCorrectInterception, int code);

        [OperationContract(IsOneWay = true)]
        void SubmitRedTeamIntercepcionResult(bool isCorrectInterception, int code);

        [OperationContract(IsOneWay = true)]
        void SubmitRedTeamDecryptResult(bool isCorrectDecrypt, int code);

        [OperationContract(IsOneWay = true)]
        void SubmitBlueTeamDecryptResult(bool isCorrectDecrypt, int code);
    }

    [ServiceContract]
    public interface IGameServiceCallback
    {
        [OperationContract]
        void SetBlueTeamClues(List<string>[] blueTeamClues);        

        [OperationContract]
        void SetRedTeamClues(List<string>[] redTeamClues);

        [OperationContract]
        void SetBlueTeamScore(int blueTeamInterception, int blueTeamMisComunications);

        [OperationContract]
        void SetRedTeamScore(int redTeamInterception, int redTeamMisComunications);

        [OperationContract]
        void WaitForGuesses();

        [OperationContract]
        void SetBoard(GameConfiguration gameConfiguration);
    }
}
