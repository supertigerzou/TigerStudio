using System;
using System.Text;
using System.Web;
using System.Xml.Linq;
using ifunction.WebChatApi.Contract;

namespace ifunction.WebChatApi.Client
{
    /// <summary>
    /// Class BaseMessageResponser.
    /// </summary>
    public abstract class BaseMessageResponser
    {
        #region Property

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        public string Token { get; protected set; }

        /// <summary>
        /// Gets or sets the default help message.
        /// </summary>
        /// <value>The default help message.</value>
        public string DefaultHelpMessage { get; protected set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMessageResponser" /> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="defaultHelpMessage">The default help message.</param>
        public BaseMessageResponser(string token, string defaultHelpMessage)
        {
            this.Token = token;
            this.DefaultHelpMessage = defaultHelpMessage;
        }

        /// <summary>
        /// Responses the specified HTTP request.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="httpResponse">The HTTP response.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <exception cref="OperationFailureException">Response</exception>
        public virtual void Response(HttpRequest httpRequest, HttpResponse httpResponse, bool throwException = false)
        {
            try
            {
                httpRequest.CheckNullObject("httpRequest");
                httpResponse.CheckNullObject("httpResponse");

                string result = string.Empty;
                string contentType = "text/plain";

                if (ValidateSignature(httpRequest, out result))
                {
                    var postDataBytes = httpRequest.GetPostDataFromHttpWebRequest();

                    if (postDataBytes != null && postDataBytes.Length > 0)
                    {
                        var userInput = Encoding.UTF8.GetString(postDataBytes);

                        MessageLog log = new MessageLog();
                        log.OriginalContent = userInput;

                        try
                        {
                            log.RequestMessage = ConvertMessage(userInput);
                        }
                        catch { }

                        ResponseUserRequest(log, true);
                        SaveMessageLog(log);

                        if (log.ResponseMessage != null)
                        {
                            result = log.ResponseMessage.ToXml().ToString();
                            contentType = "xml/application";
                        }
                    }
                }
                else
                {
                    //// Failed to validate signature.
                    //// Do nothing here.
                }

                httpResponse.ContentType = contentType;
                httpResponse.Write(result);
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    throw new OperationFailureException("Response", ex);
                }
            }
        }

        /// <summary>
        /// Responses the specified HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <exception cref="OperationFailureException">Response</exception>
        public virtual void Response(HttpContext httpContext, bool throwException = false)
        {
            try
            {
                httpContext.CheckNullObject("httpContext");

                this.Response(httpContext.Request, httpContext.Response);
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    throw new OperationFailureException("Response", ex);
                }
            }
        }

        /// <summary>
        /// Responses the user request.
        /// </summary>
        /// <param name="messageLog">The message log.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <returns>MessageLog.</returns>
        /// <exception cref="OperationFailureException">Response</exception>
        protected virtual void ResponseUserRequest(MessageLog messageLog, bool throwException = false)
        {
            try
            {
                messageLog.CheckNullObject("messageLog");

                if (messageLog.RequestMessage != null)
                {
                    messageLog.ResponseMessage = ResponseMessage(messageLog.RequestMessage);
                }
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    throw new OperationFailureException("Response", ex);
                }
            }
        }

        /// <summary>
        /// Converts the message.
        /// </summary>
        /// <param name="messageXml">The message XML.</param>
        /// <returns>Message.</returns>
        /// <exception cref="OperationFailureException">ConvertMessage</exception>
        protected Message ConvertMessage(string messageXml)
        {
            try
            {
                return Message.ConvertMessage(XElement.Parse(messageXml));
            }
            catch (Exception ex)
            {
                throw new OperationFailureException("ConvertMessage", ex, messageXml);
            }
        }

        /// <summary>
        /// Validates the signature.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="echoString">The echo string.</param>
        /// <returns><c>true</c> if pass validation, <c>false</c> otherwise.</returns>
        protected virtual bool ValidateSignature(HttpRequest httpRequest, out string echoString)
        {
            bool result = false;
            echoString = string.Empty;

            if (httpRequest != null)
            {

                string signature = httpRequest.QueryString["signature"];
                string timeStamp = httpRequest.QueryString["timestamp"];
                string nonce = httpRequest.QueryString["nonce"];
                echoString = httpRequest.QueryString["echostr"];

                string[] signatureParameters = new string[] { 
                    this.Token.GetStringValue(),
                    timeStamp,
                    nonce
                };

                result = signature.Equals(WebChatClient.GenerateSignature(signatureParameters), StringComparison.InvariantCultureIgnoreCase);
            }

            return result;
        }

        /// <summary>
        /// Responses the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Message.</returns>
        protected abstract Message ResponseMessage(Message message);

        /// <summary>
        /// Saves the message log.
        /// </summary>
        /// <param name="messageLog">The message log.</param>
        protected abstract void SaveMessageLog(MessageLog messageLog);
    }
}
