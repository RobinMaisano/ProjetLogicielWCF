using WCFContract;

namespace WCFMiddleware
{
    internal class IsDecryptedService : IService
    {
        public MSG ExecuteService(MSG message)
        {
            return BAC.Dispatch(message);
        }
    }
}