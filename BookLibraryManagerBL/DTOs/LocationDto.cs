using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookLibraryManagerBL.DTOs
{
    public class LocationDto
    {
        [Required(ErrorMessage = "This field is required to fill out")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        public double Longitude { get; set; }
    }
}
