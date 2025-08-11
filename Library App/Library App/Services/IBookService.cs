using Library_App.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_App.Services
{
    public interface IBookService
    {
        public Task<List<Book>> GetAllBooksAsync();
        public Task<APIResponse<Book>> CreateBookAsync(Book book);
    }
}
