using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibraryManagerBL.Models
{
    public class UserDto
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

        [Required]
        [RegularExpression(@"^\\S+@\\S+\\.\\S+$", ErrorMessage = "Please enter a valid email\tExapmle: myMail@mymail.com")]
        [MaxLength(50, ErrorMessage = "Max length - 50")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [MinLength(6, ErrorMessage = "Min length - 6")]
        [MaxLength(30, ErrorMessage = "Max length - 30")]
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
