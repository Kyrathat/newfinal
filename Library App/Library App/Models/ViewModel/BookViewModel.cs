using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Library_App.Models.ViewModel;
using Library_App.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Library_App.Models.ViewModel
{
    public partial class BookViewModel : ObservableObject
    {
        private readonly APIService _api;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private Book returnedBook;

        [ObservableProperty]
        private Book selectedBook;

        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();

        public BookViewModel(APIService api = null)
        {
            _api = api ?? new APIService(); 
        }

        [RelayCommand]
        public async Task SearchAndSelectBook()
        {
            Debug.WriteLine("SearchAndSelectBookAsync invoked");
            await LoadBooksAsync();

            if (Books.Any())
                SelectedBook = Books.First();
        }

        public async Task LoadBooksAsync()
        {
            ErrorMessage = string.Empty; // clear previous errors
            Books.Clear();               // optional: clear existing books before loading

            try
            {
                // Call your API service
                var response = await _api.GetBooksAsync();

                if (response.Success && response.Data != null)
                {
                    foreach (var book in response.Data)
                        Books.Add(book);
                }
                else
                {
                    ErrorMessage = response?.ErrorMessage ?? "Failed to load books.";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in LoadBooksAsync: {ex}");
                ErrorMessage = $"Error: {ex.Message}";
            }
        }

        [RelayCommand]
        public async Task LoadBookAsync(int id)
        {
            ErrorMessage = string.Empty;

            // Try from local list
            var book = Books.FirstOrDefault(b => b.BookId == id);
            if (book != null)
            {
                ReturnedBook = book;
                return;
            }

            // Fallback to API
            var response = await _api.GetBookByIdAsync(id);
            if (response.Success)
            {
                ReturnedBook = response.Data;
                // Optionally add to the Books list if needed
                Books.Add(response.Data);
            }
            else
            {
                ErrorMessage = response?.ErrorMessage ?? "Failed to load book.";
            }
        }
    }
}
