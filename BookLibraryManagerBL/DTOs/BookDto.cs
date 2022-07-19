using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibraryManagerBL.DTOs
{
    public class BookDto
    {
        
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [MinLength(2, ErrorMessage = "Min length - 2")]
        [MaxLength(30, ErrorMessage = "Max length - 30")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [MinLength(2, ErrorMessage = "Min length - 2")]
        [MaxLength(30, ErrorMessage = "Max length - 30")]
        public string Author { get; set; }

        public IEnumerable<BookRevisionDto> BookRevisions { get; set; }
    }
}
