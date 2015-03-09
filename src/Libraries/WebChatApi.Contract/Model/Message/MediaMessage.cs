using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace ifunction.WebChatApi.Contract
{
    /// <summary>
    /// Class ImageMessage.
    /// </summary>
    [DataContract]
    [KnownType(typeof(Message))]
    public class MediaMessage : Message
    {
        #region Constants

        /// <summary>
        /// The node name_ image URL
        /// </summary>
        protected const string nodeName_ImageUrl = "PicUrl";

        #endregion

        /// <summary>
        /// Gets or sets the image collection.
        /// </summary>
        /// <value>The image collection.</value>
        [DataMember]
        public ImageObjectCollection ImageCollection { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMessage"/> class.
        /// </summary>
        public MediaMessage()
            : base(MessageType.Image)
        {
            this.ImageCollection = new ImageObjectCollection();
        }

        /// <summary>
        /// Fills the XML data.
        /// </summary>
        /// <param name="xml">The XML.</param>
        protected override void FillXmlData(XElement xml)
        {
            base.FillXmlData(xml);

            if (this.ImageCollection != null)
            {
                xml.Add(this.ImageCollection.ToXml());
            }
        }
    }
}
