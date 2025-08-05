namespace Library_App.Models.ViewModel
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public string DateReleased { get; set; }
        public bool HasCover { get; set; }
    }

}
