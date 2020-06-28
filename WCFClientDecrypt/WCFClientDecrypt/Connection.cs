using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCFClientDecrypt.proxy;

namespace WCFClientDecrypt
{
    class Connection
    {
        private proxy.MSG msg;
        private proxy.IServerEntryPoint prox;

        public Connection()
        {
            this.msg = new MSG();
            this.prox = new proxy.ServerEntryPointClient();
        }

        public MSG m_send(MSG msg)
        {
            this.msg = msg;
            this.msg.tokenApp = "4ppT0k3n";
            this.msg.appVersion = "V1";


            this.msg = this.prox.m_service(this.msg);

            return this.msg;
        }
    }
}
