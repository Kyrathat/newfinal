using Library_App.Models.ViewModel;

namespace Library_App.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

	private void SearchButton_Clicked(object sender, EventArgs e)
	{
		var bookDetailViewModel = new Book
		{
            BookId = 0,
            Title = "Pride and Prejudice",
            Author = "Jane Austen",
            Genre = "Romance",
            ISBN = "9780553213102",
            DateReleased = "03/01/2003",
            HasCover = true
        };

		var bookDetailPage = new BookDetailPage();
		bookDetailPage.BindingContext = bookDetailViewModel;
		Navigation.PushAsync(bookDetailPage);
	}
}