using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WCFClientDecrypt.proxy;

namespace WCFClientDecrypt
{
    class ResultHandler
    {
        private MSG msg;
        private TextBlock ResultBlock;
        private Controller controller;
        private WCFClientDecrypt.MsgHandlerDelegate callback;

        public ResultHandler(MSG msg, TextBlock ResultBlock, Controller controller, MsgHandlerDelegate callbackDelegate)
        {
            this.msg = msg;
            this.ResultBlock = ResultBlock;
            this.controller = controller;
            this.callback = callbackDelegate;
        }

        public void ResultRequestStarter()
        {
            this.msg = this.controller.m_checkIsDecrypted_loop(this.msg);

            this.ResultBlock.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, callback);
        }

        public MSG GetMsg()
        {
            return this.msg;
        }
    }
}
