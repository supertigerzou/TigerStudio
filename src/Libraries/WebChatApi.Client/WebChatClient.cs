using System;

namespace ifunction.WebChatApi.Client
{
    /// <summary>
    /// Class WebChatClient.
    /// </summary>
    public partial class WebChatClient
    {
        #region Constants

        /// <summary>
        /// The API service base URL
        /// </summary>
        protected const string apiServiceBaseUrl = "https://api.weixin.qq.com/cgi-bin/";

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the application unique identifier.
        /// </summary>
        /// <value>The application unique identifier.</value>
        public string AppID { get; protected set; }

        /// <summary>
        /// Gets or sets the application secret.
        /// </summary>
        /// <value>The application secret.</value>
        public string AppSecret { get; protected set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="WebChatClient"/> class.
        /// </summary>
        /// <param name="appId">The application unique identifier.</param>
        /// <param name="appSecret">The application secret.</param>
        public WebChatClient(string appId, string appSecret)
        {
            this.AppID = AppID;
            this.AppSecret = AppSecret;
        }

        /// <summary>
        /// Generates the URI.
        /// </summary>
        /// <param name="relevantUrl">The relevant URL.</param>
        /// <returns>Uri.</returns>
        public Uri GenerateUri(string relevantUrl)
        {
            return new Uri(apiServiceBaseUrl + (relevantUrl.GetStringValue().TrimStart('/')));
        }

        #region Static methods

        /// <summary>
        /// Generates the signature.
        /// </summary>
        /// <param name="signatureParameters">The signature parameters.</param>
        /// <returns>System.String.</returns>
        public static string GenerateSignature(string[] signatureParameters)
        {
            string signature = string.Empty;

            if (signatureParameters != null && signatureParameters.Length > 0)
            {
                Array.Sort(signatureParameters);
                signature = string.Join(string.Empty, signatureParameters);
            }

            return signature.EncryptToSHA1();
        }

        #endregion
    }
}
