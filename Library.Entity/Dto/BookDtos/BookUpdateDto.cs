using Library.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entity.Dto.BookDtos
{
    public class BookUpdateDto:IDto
    {
        public int BookId { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? ImagteUrl { get; set; }
        public bool? IsAvailableInLibrary { get; set; }
    }
}
