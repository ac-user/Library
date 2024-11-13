namespace Library.Services.Models
{
    public class ResponseStatus
    {
        /// <summary>
        /// New identifier for the created item
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Was the modification successful
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Notes about the modification (includes errors)
        /// </summary>
        public List<string> Messages { get; set; }
    }
}
