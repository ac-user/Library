namespace Library.UI.Model.ViewModels.Media
{
    public class SelectableCollection
    {
        /// <summary>
        /// Content/Collection identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Collection Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Is the element selected to be in the collection
        /// </summary>
        public bool Selected { get; set; }
    }
}
