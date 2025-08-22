namespace Library.UI.Model.ViewModels
{
    public class Home
    {
        public string UserName { get; set; }
        public List<CollectionCards> Collections { get; set; }
    }

    public class CollectionCards
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }


}
