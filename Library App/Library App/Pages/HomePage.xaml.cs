using Library_App.Models.ViewModel;

namespace Library_App.Pages;

public partial class HomePage : ContentPage
{
	private readonly BookViewModel _viewModel;
	public HomePage()
	{
		InitializeComponent();
		_viewModel = new BookViewModel();
		BindingContext = _viewModel;
	}

    private async void OnSearchClicked(Object sender, EventArgs e)
    {
        await _viewModel.LoadBooksAsync();

        if (_viewModel.Books.Any())
        {
            var firstBook = _viewModel.Books.First();
            await Navigation.PushAsync(new BookDetailPage(firstBook));
        }
        else
        {
            await DisplayAlert("No Results", "No books found matching your search.", "OK");
        }
    }


}