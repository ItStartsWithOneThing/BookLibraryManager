using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookLibraryManagerBL.DTOs
{
    public class CityDto
    {
        [Required(ErrorMessage = "This field is required to fill out")]
        public string Name { get; set; }
    }
}
