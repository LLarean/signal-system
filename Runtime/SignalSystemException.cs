#nullable enable

using System;

namespace GameSignals
{
    /// <summary>
    /// The exception thrown when an event handler fails during <see cref="SignalSystem.Raise{TSubscriber}"/>.
    /// </summary>
    public class SignalSystemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignalSystemException"/> class.
        /// </summary>
        public SignalSystemException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalSystemException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public SignalSystemException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalSystemException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The exception that caused this failure.</param>
        public SignalSystemException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
