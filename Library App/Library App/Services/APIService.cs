using Library_App.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library_App.Services
{
    public class APIService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public APIService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://10.0.2.2:7262/");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<APIResponse<List<Book>>> GetBooksAsync()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new APIResponse<List<Book>>
                {
                    Data = new List<Book>(),
                    Success = false,
                    ErrorMessage = "No Internet Connection"
                };

            try
            {
                // Fetch and deserialize in one step
                var books = await _client.GetFromJsonAsync<List<Book>>("api/Media", _jsonOptions)
                    ?? new List<Book>();
                
                return new APIResponse<List<Book>> { Data = books, Success = true};
            }
            catch (HttpRequestException)
            {
                // Handle network errors
                return new APIResponse<List<Book>>
                {
                    Data = new List<Book>(),
                    Success = false,
                    ErrorMessage = "There was a network error."
                };
            }
            catch (NotSupportedException)
            {
                // Handle content not being JSON
                return new APIResponse<List<Book>> 
                { 
                    Data = new List<Book>(), 
                    Success = false,
                    ErrorMessage = "The content returned was not in JSON format." 
                };
            }
            catch (JsonException)
            {
                // Handle JSON parsing errors
                return new APIResponse<List<Book>>
                {
                    Data = new List<Book>(),
                    Success = false,
                    ErrorMessage = "The content could not be parsed."
                };
            }
        }

        public async Task<APIResponse<Book>> GetBookByIdAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new APIResponse<Book>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "No Internet Connection"
                };

            try
            {
                var book = await _client.GetFromJsonAsync<Book>($"Book/{id}", _jsonOptions);

                if (book == null)
                {
                    return new APIResponse<Book>
                    {
                        Data = null,
                        Success = false,
                        ErrorMessage = "Book not found."
                    };
                }

                return new APIResponse<Book> { Data = book, Success = true };
            }
            catch (HttpRequestException)
            {
                return new APIResponse<Book>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "There was a network error."
                };
            }
            catch (NotSupportedException)
            {
                return new APIResponse<Book>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "The content returned was not in JSON format."
                };
            }
            catch (JsonException)
            {
                return new APIResponse<Book>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "The content could not be parsed."
                };
            }
        }

    }
}
