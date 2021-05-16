using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallBackSenders
{
    interface ICallbackSender
    {
        public Task SendCallBack();
    }
}
