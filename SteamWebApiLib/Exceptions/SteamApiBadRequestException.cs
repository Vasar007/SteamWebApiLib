using System;
using System.Runtime.Serialization;

namespace SteamWebApiLib.Exceptions
{
    /// <summary>
    /// Provides library specific exception to show that the problem occurred while the client was 
    /// running.
    /// </summary>
    [Serializable]
    public class SteamApiBadRequestException : Exception
    {
        /// <summary>
        /// Creates the exception.
        /// </summary>
        public SteamApiBadRequestException()
        {
        }

        /// <summary>
        /// Creates the exception with description.
        /// </summary>
        /// <param name="message">Exception description.</param>
        public SteamApiBadRequestException(string message)
          : base(message)
        {
        }

        /// <summary>
        /// Creates the exception with description and inner cause.
        /// </summary>
        /// <param name="message">Exception description.</param>
        /// <param name="innerException">Exception inner cause.</param>
        public SteamApiBadRequestException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates the exception from serialized data.
        /// Usual scenario is when exception is occured somewhere on the remote workstation
        /// and we have to re-create/re-throw the exception on the local machine.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Serialization context.</param>
        protected SteamApiBadRequestException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}
