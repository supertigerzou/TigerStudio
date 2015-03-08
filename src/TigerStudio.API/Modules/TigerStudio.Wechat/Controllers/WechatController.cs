using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        [Route("")]
        public IHttpActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            Trace.TraceInformation(string.Format("Signature: {0}, Timestamp: {1}, Nonce: {2}, Echostr: {3}", signature, timestamp, nonce, echostr));
            if (CheckSignature(signature, timestamp, nonce)) 
                return Ok(echostr);
            
            return BadRequest();
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
