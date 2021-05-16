using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace CallBackSenders
{
    public abstract class CallbackSender : ICallbackSender
    {
        private string _url;

        internal CallbackSender(string url)
        {
            _url = url ?? throw new ArgumentNullException(nameof(url));
        }

        protected abstract object GetCallBackData();

        public async Task SendCallBack()
        {
            var data = GetCallBackData();
            await _url.PostJsonAsync(data);

        }
    }
}
