using Library_App.Models.ViewModel;

namespace Library_App.Pages;

public partial class BookDetailPage : ContentPage
{
	public BookDetailPage()
	{
		InitializeComponent();

        var bookDetailPage = new Book
        {
            BookId = 0,
            Title = "Pride and Prejudice",
            Author = "Jane Austen",
            Genre = "Romance",
            ISBN = "9780553213102",
            DateReleased = "03/01/2003",
            HasCover = true
        };

        BindingContext = bookDetailPage;
    }
}