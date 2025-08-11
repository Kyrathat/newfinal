using Library_App.Models.ViewModel;
using Library_App.Pages;

namespace Library_App.Pages
{
    public partial class HomePage : ContentPage
    {
        private readonly BookViewModel _viewModel;

        public HomePage(BookViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            await _viewModel.SearchAndSelectBook(); // filters books

            if (_viewModel.FilteredBooks.Any())
            {
                await Navigation.PushAsync(new SearchResultPage(_viewModel.FilteredBooks.ToList()));
            }
            else
            {
                await DisplayAlert("No Results", "No books matched your search criteria.", "OK");
            }
        }

        private async void OnCreateBookButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BookCreatePage(_viewModel));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!_viewModel.Books.Any())
            {
                await _viewModel.LoadBooksAsync(); // load on first open
            }
        }
    }
}
