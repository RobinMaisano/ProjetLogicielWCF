using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFContract;

namespace WCFMiddleware
{
    public interface IService
    {
        MSG ExecuteService(MSG message);
    }
}
