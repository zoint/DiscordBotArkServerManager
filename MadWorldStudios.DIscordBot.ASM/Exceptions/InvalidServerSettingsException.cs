using System;
using System.Runtime.Serialization;

namespace MadWorldStudios.DIscordBot.ASM.Exceptions
{
    [Serializable]
    internal class InvalidServerSettingsException : Exception
    {
        public InvalidServerSettingsException()
        {
        }

        public InvalidServerSettingsException(string message) : base(message)
        {
        }

        public InvalidServerSettingsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidServerSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}