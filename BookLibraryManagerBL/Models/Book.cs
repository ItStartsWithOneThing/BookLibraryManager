using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookLibraryManagerBL.Models
{
    public class Book
    {
        public IEnumerable<Library> Libraries { get; set; }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [MinLength(2, ErrorMessage = "Min length - 2")]
        [MaxLength(30, ErrorMessage = "Max length - 30")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [Range(3, 50_000, ErrorMessage = "Pages amount should be between 3 and 50 000")]
        public int PagesCount { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        public int PublishingYear { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [MinLength(2, ErrorMessage = "Min length - 2")]
        [MaxLength(30, ErrorMessage = "Max length - 30")]
        public string Author { get; set; }

        public int RentCount { get; set; }
    }
}
