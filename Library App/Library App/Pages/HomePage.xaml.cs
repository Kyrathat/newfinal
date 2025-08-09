using Library_App.Models.ViewModel;

namespace Library_App.Pages;

public partial class HomePage : ContentPage
{
	private readonly BookViewModel _viewModel;
	public HomePage()
	{
		InitializeComponent();

		var vm = new BookViewModel();

		BindingContext = vm;

		vm.PropertyChanged += async (s, e) =>
		{
			if (e.PropertyName == nameof(vm.SelectedBook) && vm.SelectedBook != null)
			{
				await Navigation.PushAsync(new BookDetailPage(vm.SelectedBook));

				vm.SelectedBook = null;
			}
		};
    }
}