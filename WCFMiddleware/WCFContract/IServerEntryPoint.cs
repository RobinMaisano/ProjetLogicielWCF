using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFContract
{
    [ServiceContract]
    public interface IServerEntryPoint
    {
        [OperationContract]
        MSG m_service(MSG message);
    }
}
