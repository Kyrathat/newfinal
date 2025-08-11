using Library_App.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            _client.BaseAddress = new Uri("http://10.0.2.2:5280/");
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
                var response = await _client.GetAsync("api/Media");

                if (!response.IsSuccessStatusCode)
                {
                    return new APIResponse<List<Book>>
                    {
                        Data = new List<Book>(),
                        Success = false,
                        ErrorMessage = $"HTTP Error: {response.StatusCode}"
                    };
                }

                var json = await response.Content.ReadAsStringAsync();

                var books = JsonSerializer.Deserialize<List<Book>>(json, _jsonOptions)
                    ?? new List<Book>();

                return new APIResponse<List<Book>> { Data = books, Success = true };
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

        public async Task<APIResponse<Book>> CreateBookAsync(Book book)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return new APIResponse<Book>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "No Internet Connection"
                };
            }

            try
            {
                var response = await _client.PostAsJsonAsync("api/Media", book);

                var json = JsonSerializer.Serialize(book);
                Debug.WriteLine(json);

                if (!response.IsSuccessStatusCode)
                {
                    return new APIResponse<Book>
                    {
                        Data = null,
                        Success = false,
                        ErrorMessage = $"HTTP Error: {response.StatusCode}"
                    };
                }

                var createdBook = await response.Content.ReadFromJsonAsync<Book>(_jsonOptions);

                return new APIResponse<Book>
                {
                    Data = createdBook,
                    Success = true
                };
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
