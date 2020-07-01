using System.ComponentModel;
using WCFContract;

namespace WCFMiddleware
{
    internal class DecryptedService : IService
    {
        public MSG ExecuteService(MSG message)
        {
            return BAC.Dispatch(message);
        }
    }
}