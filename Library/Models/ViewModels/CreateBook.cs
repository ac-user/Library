namespace Library.Models.ViewModels
{
    public class CreateBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Published { get; set; }
        public string Summary { get; set; }
        public string Author { get; set; }
    }
}
