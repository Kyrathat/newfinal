using Library_App.Models.ViewModel;
using System.Collections.ObjectModel;

namespace Library_App.Pages
{
    public partial class SearchResultPage : ContentPage
    {
        public ObservableCollection<Book> FilteredBooks { get; }

        public SearchResultPage(List<Book> filteredBooks)
        {
            InitializeComponent();
            FilteredBooks = new ObservableCollection<Book>(filteredBooks);
            BindingContext = this;
        }

        private async void OnBookSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedBook = e.CurrentSelection.FirstOrDefault() as Book;
            if (selectedBook == null)
                return;

            await Navigation.PushAsync(new BookDetailPage(selectedBook));

            ((CollectionView)sender).SelectedItem = null; // Deselect to allow re-selection
        }
    }

}