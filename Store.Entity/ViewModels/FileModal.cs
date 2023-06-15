using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Entity.ViewModels
{
    public class FileModal
    {
        public int UserId { get; set; }
        public IFormFile FormFile { get; set; } = null!;
    }
}
