using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;

namespace TigerStudio.Wechat.Controllers
{
    [RoutePrefix("api/wechat")]
    public class WechatController : ApiController
    {
        private const string Token = "alibaba";

        // sample: api/wechat?signature=af30f7826dd7c71e23291dcb9ab94be53b7b87dd&timestamp=1425805977&nonce=739636270&echostr=hello
        [Route("")]
        public HttpResponseMessage Get(string signature, string timestamp, string nonce, string echostr)
        {
            var stringToReturn = string.Empty;

            Trace.TraceInformation(string.Format("Signature: {0}, Timestamp: {1}, Nonce: {2}, Echostr: {3}", signature, timestamp, nonce, echostr));
            if (CheckSignature(signature, timestamp, nonce)) stringToReturn = echostr;

            return new HttpResponseMessage()
            { Content = new StringContent(stringToReturn, Encoding.UTF8, "text/html") };
        }

        private bool CheckSignature(string signature, string timestamp, string nonce)
        {
            string[] arrTmp = { Token, timestamp, nonce };
            Array.Sort(arrTmp);

            var tmpStr = string.Join("", arrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");

            return tmpStr != null && tmpStr.ToLower() == signature;
        }
    }
}
