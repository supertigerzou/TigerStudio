﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using ifunction.WebChatApi;



namespace ifunction.WebChatApi.Contract
{
    /// <summary>
    /// Model class for service exception, which is inherited from the <seealso cref="BaseServiceException"/> class.
    /// </summary>
    public class ResourceNotFoundServiceException : BaseServiceException
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundServiceException"/> class.
        /// </summary>
        public ResourceNotFoundServiceException()
            : base(FaultCode.ResourceNotFound)
        {
        }

        #endregion
    }
}
