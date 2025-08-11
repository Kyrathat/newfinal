using Library_App.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library_App.Services
{
    internal class BookService : IBookService
    {

            private readonly APIService _api;

            public BookService(APIService api)
            {
                _api = api;
            }

            public async Task<List<Book>> GetAllBooksAsync()
            {
                try
                {
                    var response = await _api.GetBooksAsync();
                    if (response.Success && response.Data != null)
                    {
                        return response.Data;
                    }
                    else
                    {
                        // Optionally throw or return an empty list
                        throw new Exception(response?.ErrorMessage ?? "Unknown API error.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception in BookService.GetBooksAsync: {ex}");
                    throw;
                }
            }

        public async Task<APIResponse<Book>> CreateBookAsync(Book book)
        {
            return await _api.CreateBookAsync(book);
        }
    }

    }
