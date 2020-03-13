using System;
using System.Runtime.Serialization;

namespace Kmd.Logic.Cvr.Client
{
    [Serializable]
    public class CvrConfigurationException : Exception
    {
        [Obsolete("This is no longer used and returns the Message.")]
        public string InnerMessage
        {
            get
            {
                return this.Message;
            }
        }

        public CvrConfigurationException()
        {
        }

        public CvrConfigurationException(string message)
            : base(message)
        {
        }

        public CvrConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CvrConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}