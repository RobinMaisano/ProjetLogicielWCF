using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Configuration;
using WCFClientDecrypt.proxy;
using System.Configuration;

namespace WCFClientDecrypt
{
    class Connection
    {
        private proxy.MSG msg;
        private proxy.IServerEntryPoint prox;
        private Fake_middleware_test middleware_test;

        public Connection()
        {
            this.msg = new MSG();
            this.prox = new proxy.ServerEntryPointClient("ServerEntryPoint");
        }

        public void Set_middleware_test(Fake_middleware_test middleware_test)
        {
            this.middleware_test = middleware_test;
        }

        public MSG m_send(MSG msg)
        {
            this.msg = msg;
            this.msg.tokenApp = ConfigurationManager.AppSettings.Get("tokenApp");
            this.msg.appVersion = ConfigurationManager.AppSettings.Get("appVersion");

            if (ConfigurationManager.AppSettings.Get("test") == "y")
            {
                this.msg = this.middleware_test.m_service(this.msg);

                return this.msg;
            }
            try
            {
                this.msg = this.prox.m_service(this.msg);
            }
            catch(EndpointNotFoundException ex)
            {
                this.msg.statusOp = false;
                this.msg.info = ex.ToString();
            }
            catch (Exception ex)
            {
                this.msg.statusOp = false;
                this.msg.info = ex.ToString();
            }

            return this.msg;
        }
    }
}
