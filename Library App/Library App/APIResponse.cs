using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_App
{
    public class APIResponse<T>
    {
        public T Data { get; set; } = default!;

        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
