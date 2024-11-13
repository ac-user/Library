namespace Library.Services.Models.Media.Book
{
    public class BookModificationRequest
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public int TimesRead { get; set; }
        public bool Ongoing { get; set; }
        public bool IsActivelyReading { get; set; }
    }
}