namespace Library.Services.Models
{
    /// <summary>
    /// This is a generic response model used with the base controller
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Success status of the cal
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Result from the call
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Any error that occurred
        /// </summary>
        public string ErrorMessage { get; set; }

        public ApiResponse(T data)
        {
            Success = true;
            Data = data;
        }

        public ApiResponse(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }
    }

}
