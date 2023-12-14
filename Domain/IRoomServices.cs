using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [ServiceContract]
    public interface IRoomServices
    {
        [OperationContract]
        bool IsFullRoom(int code);

        [OperationContract]
        bool AllreadyExistRoom(int code);
    }
}
