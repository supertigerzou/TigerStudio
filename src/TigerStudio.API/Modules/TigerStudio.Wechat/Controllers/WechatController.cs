using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using System.Xml;
using System.Xml.Linq;
using ifunction.WebChatApi.Contract;

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

            return new HttpResponseMessage() { Content = new StringContent(stringToReturn, Encoding.UTF8, "text/html") };
        }

        // <xml><URL><![CDATA[http://tigerstudioapi.azurewebsites.net/api/wechat/message]]></URL><ToUserName><![CDATA[tigerartstudio]]></ToUserName><FromUserName><![CDATA[supertigerzou]]></FromUserName><CreateTime>1448841860</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[test]]></Content><MsgId>1234367812123456</MsgId></xml>
        [Route("")]
        [HttpPost]
        public HttpResponseMessage ReplyUser(HttpRequestMessage request)
        {
            var reader = new StreamReader(request.Content.ReadAsStreamAsync().Result);
            var inputMessageXml = reader.ReadToEnd();
            var inputMessage = Message.ConvertMessage(XElement.Parse(inputMessageXml));

            StringContent replyContent = null;
            if (inputMessage is EventMessage)
            {
                var mediaMessage = inputMessage.ReplyMedia(new List<ImageObject>
                    {
                        new ImageObject()
                        {
                            Description = "world",
                            ImageUrl = "http://mmbiz.qpic.cn/mmbiz/u1YPNZkLJC6zYpt4jJJXRyYiaXMzrVyIFqeRicRUq4kmhzzAAjTIzYAp8EJ7BXbQ4ibicsv9sjr18QBbwkoAxO75GA/640?tp=webp&wxfrom=5",
                            Title = "hello",
                            Url =
                                "http://mp.weixin.qq.com/s?__biz=MzA5NTU0MTMzOQ==&mid=204860621&idx=1&sn=ce5c822c25bc7526c36a4ca104bf04c8#rd"
                        }
                    });

                replyContent = new StringContent(mediaMessage.ToXml().ToString(SaveOptions.DisableFormatting), Encoding.UTF8, "xml/application");
            }
            else if (inputMessage is ContentMessage)
            {
                if (((ContentMessage)inputMessage).Content.Contains("葫芦"))
                    replyContent = new StringContent(
                        inputMessage.ReplyContent("请使用菜单收听，谢谢！").ToXml().ToString(), Encoding.UTF8, "xml/application");
                else if (((ContentMessage)inputMessage).Content.Contains("图文测试"))
                {
                    var mediaMessage = inputMessage.ReplyMedia(new List<ImageObject>
                    {
                        new ImageObject()
                        {
                            Description = "world",
                            ImageUrl = "http://mmbiz.qpic.cn/mmbiz/u1YPNZkLJC6zYpt4jJJXRyYiaXMzrVyIFqeRicRUq4kmhzzAAjTIzYAp8EJ7BXbQ4ibicsv9sjr18QBbwkoAxO75GA/640?tp=webp&wxfrom=5",
                            Title = "hello",
                            Url =
                                "http://mp.weixin.qq.com/s?__biz=MzA5NTU0MTMzOQ==&mid=204860621&idx=1&sn=ce5c822c25bc7526c36a4ca104bf04c8#rd"
                        }
                    });

                    replyContent = new StringContent(mediaMessage.ToXml().ToString(SaveOptions.DisableFormatting), Encoding.UTF8, "xml/application");
                }
            }
            else
            {
                replyContent = new StringContent(
                    inputMessage.ReplyContent("您请求的项目暂时不存在，加微信号supertigerzou直接告诉我们您的需求.").ToXml().ToString(), Encoding.UTF8, "xml/application");
            }

            return new HttpResponseMessage() { Content = replyContent };
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
