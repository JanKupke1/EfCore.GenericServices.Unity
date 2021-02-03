// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace GenericServices.Unity.Setup
{
    [Serializable]
    internal class InitializeServiceException : Exception
    {
        public InitializeServiceException()
        {
        }

        public InitializeServiceException(string message) : base(message)
        {
        }

        public InitializeServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InitializeServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}