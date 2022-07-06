using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookLibraryManagerBL.Models
{
    public class Client
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [MinLength(2, ErrorMessage = "Min length - 2")]
        [MaxLength(30, ErrorMessage = "Max length - 30")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [MinLength(2, ErrorMessage = "Min length - 2")]
        [MaxLength(30, ErrorMessage = "Max length - 30")]
        public string LastName { get; set; }
    }
}
