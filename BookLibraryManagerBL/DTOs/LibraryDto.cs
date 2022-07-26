using BookLibraryManagerBL.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibraryManagerBL.Models
{
    public class LibraryDto
    {
        public IEnumerable<BookDto> Books { get; set; }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [MinLength(2, ErrorMessage = "Min length - 2")]
        [MaxLength(50, ErrorMessage = "Max length - 30")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [MinLength(2, ErrorMessage = "Min length - 2")]
        [MaxLength(50, ErrorMessage = "Max length - 30")]
        public string FullAddress { get; set; }
    }
}
