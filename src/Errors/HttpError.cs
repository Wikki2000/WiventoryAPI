using System;
using System.Net;

namespace WiventoryAPI.Errors
{
    /// <summary>
    /// Custom HTTP error class for handling API errors with status code.
    /// </summary>
    public class HttpError : Exception
    {
        /// <summary>
        /// HTTP status code (e.g., 400, 404, 500)
        /// </summary>
        public int Status { get; }

        /// <summary>
        /// Creates a new instance of HttpError.
        /// </summary>
        /// <param name="status">HTTP status code.</param>
        /// <param name="message">Error message describing the issue.</param>
        public HttpError(int status, string message) 
            : base(message)
        {
            Status = status;
        }
    }
}
