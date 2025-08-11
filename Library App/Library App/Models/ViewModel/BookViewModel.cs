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

        private readonly IBookService _bookService;

        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Book> FilteredBooks { get; set; } = new ObservableCollection<Book>();

        #region Search Properties
        private string _titleQuery;
        public string TitleQuery
        {
            get => _titleQuery;
            set { _titleQuery = value; OnPropertyChanged(nameof(TitleQuery)); }
        }

        private string _authorQuery;
        public string AuthorQuery
        {
            get => _authorQuery;
            set { _authorQuery = value; OnPropertyChanged(nameof(AuthorQuery)); }
        }

        private string _genreQuery;
        public string GenreQuery
        {
            get => _genreQuery;
            set { _genreQuery = value; OnPropertyChanged(nameof(GenreQuery)); }
        }

        private string _isbnQuery;
        public string ISBNQuery
        {
            get => _isbnQuery;
            set { _isbnQuery = value; OnPropertyChanged(nameof(ISBNQuery)); }
        }

        private string _dateReleasedQuery;
        public string DateReleasedQuery
        {
            get => _dateReleasedQuery;
            set { _dateReleasedQuery = value; OnPropertyChanged(nameof(DateReleasedQuery)); }
        }
        #endregion

        #region Create properties
        [ObservableProperty] private string title;
        [ObservableProperty] private string author;
        [ObservableProperty] private string genre;
        [ObservableProperty] private string iSBN;
        [ObservableProperty] private DateTime? dateReleased;
        #endregion

        public BookViewModel(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            DateReleased = DateTime.Today;
        }

        [RelayCommand]
        public async Task SearchAndSelectBook()
        {

            var filtered = Books.Where(book =>
                (string.IsNullOrWhiteSpace(TitleQuery) || book.Title.Contains(TitleQuery, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(AuthorQuery) || book.Author.Contains(AuthorQuery, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(GenreQuery) || book.Genre.Contains(GenreQuery, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(ISBNQuery) || book.ISBN.Contains(ISBNQuery, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(DateReleasedQuery) || book.DateReleased.Contains(DateReleasedQuery))
            ).ToList();

            FilteredBooks.Clear();
            foreach (var book in filtered)
                FilteredBooks.Add(book);
        }

        public async Task LoadBooksAsync()
        {
            ErrorMessage = string.Empty;
            Books.Clear();

            try
            {
                var bookList = await _bookService.GetAllBooksAsync();

                foreach (var book in bookList)
                    Books.Add(book);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in LoadBooksAsync: {ex}");
                ErrorMessage = $"Error: {ex.Message}";
            }
        }

        [RelayCommand]
        public async Task<bool> CreateBook()
        {
            ErrorMessage = string.Empty;

            var newBook = new Book
            {
                Title = Title,
                Author = Author,
                Genre = Genre,
                ISBN = iSBN,
                DateReleased = DateReleased.Value.ToString("yyyy-MM-dd")
            };

            var result = await _bookService.CreateBookAsync(newBook);

            if (result.Success)
            {
                Books.Add(result.Data);
                ClearFormFields();
                return true;
            }
            else
            {
                ErrorMessage = result.ErrorMessage ?? "An unknown error occurred.";
                return false;
            }
        }

        private void ClearFormFields()
        {
            Title = string.Empty;
            Author = string.Empty;
            Genre = string.Empty;
            iSBN = string.Empty;
            DateReleased = null;
        }
    }
}
