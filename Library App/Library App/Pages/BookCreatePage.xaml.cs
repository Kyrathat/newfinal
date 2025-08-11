using Library_App.Models.ViewModel;

namespace Library_App.Pages;

public partial class BookCreatePage : ContentPage
{
    private readonly BookViewModel _viewModel;

    public BookCreatePage(BookViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void OnCreateBookClicked(object sender, EventArgs e)
    {
        var success = await _viewModel.CreateBook();

        if (success)
        {
            await DisplayAlert("Success", "Book created successfully.", "OK");
            await Navigation.PopAsync(); // Navigate back to previous page (HomePage)
        }
        else
        {
            await DisplayAlert("Error", _viewModel.ErrorMessage ?? "Failed to create book.", "OK");
        }
    }
}
