namespace Contracts.V1.SharedModels
{
    /// <summary>
    /// Error Reponse
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Error Status Code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        ///  Error Details.
        /// </summary>
        public string? Message { get; set; }


        /// <summary>
        /// Error Status Code.
        /// </summary>
        public object? ErrorDetails { get; set; }

        /// <summary>
        /// Error Stack Trace
        /// </summary>
        public string? StackTrace { get; set; }


    }
}
