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
            if (inputMessage is ContentMessage)
            {
                var contentMessage = (ContentMessage) inputMessage;
                if (contentMessage.Content.Contains("葫芦"))
                    replyContent = new StringContent(
                        inputMessage.ReplyContent("请使用菜单收听，谢谢！").ToXml().ToString(), Encoding.UTF8, "xml/application");
                else if (contentMessage.Content.Contains("图文测试"))
                {
                    var mediaMessage = inputMessage.ReplyMedia(new List<ImageObject>
                    {
                        new ImageObject
                        {
                            Description = "葫芦山里关着蝎子精和蛇精。一只穿山甲不小心打穿了山洞，两个妖精逃了出来，从此百姓遭难。穿山甲急忙去告诉一个老汉，只有种出七色葫芦，才能消灭这两个妖精。老汉种出了红、橙、黄、绿、青、蓝、紫七个大葫芦，却被妖精从如意镜中窥见。他们摧毁不了这七个葫芦，就把老汉和穿山甲抓去。七个葫芦成熟了，相继落地变成七个男孩，一个接一个去与妖精搏斗，却被妖精抓住弱点，各个击破。最终7个葫芦娃齐心协力与妖精展开殊死拼搏，并打败妖精，把他们收进宝葫芦里。",
                            ImageUrl = "https://mmbiz.qlogo.cn/mmbiz/u1YPNZkLJC6SI64hPj9FlEvhj2hH4t6azlULlE05icc4qEnRKFNqv2LKlhhDu4iaeibHiawcRebTL6rSOdxr43pkYQ/0",
                            Title = "葫芦娃",
                            Url = "http://mp.weixin.qq.com/s?__biz=MzA5NTU0MTMzOQ==&mid=204243491&idx=1&sn=e3dff38e2f8816e95362466e0021902f#rd"
                        },
                        new ImageObject
                        {
                            Description = "一位老爷爷在山上采药，无意中进入了一个山洞，在洞中他救下一只穿山甲。穿山甲告诉老爷爷自己不小心穿破葫芦山，放走了蛇蝎二妖。穿山甲帮助老爷爷取出了能降妖服魔的宝葫芦籽。",
                            ImageUrl = "http://mmbiz.qpic.cn/mmbiz/u1YPNZkLJC7ZiaX1JJrQJiclnemAosfwGXPP0X5OvLnkvCTpAbPCzJRr9QxVn9Sib0GUDSeWasu9S3KfaCPiaqXYDA/640?tp=webp&wxfrom=5",
                            Title = "第一集： 神峰奇遇",
                            Url = "http://mp.weixin.qq.com/s?__biz=MzA5NTU0MTMzOQ==&mid=204243491&idx=2&sn=3131ea09b538535d6f41ac4aafc067d7#rd"
                        }
                    });

                    replyContent = new StringContent(mediaMessage.ToXml().ToString(SaveOptions.DisableFormatting), Encoding.UTF8, "xml/application");
                }
            }
        
            if(replyContent == null)
                replyContent = new StringContent(
                    inputMessage.ReplyContent("您请求的项目暂时不存在，加微信号supertigerzou直接告诉我们您的需求.").ToXml().ToString(), Encoding.UTF8, "xml/application");

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
