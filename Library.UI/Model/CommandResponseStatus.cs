namespace Library.UI.Model
{
    public class CommandResponseStatus
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; }
    }
}
