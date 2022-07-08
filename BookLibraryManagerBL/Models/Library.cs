﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibraryManagerBL.Models
{
    public class Library
    {
        public IEnumerable<Book> Books { get; set; }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [MinLength(2, ErrorMessage = "Min length - 2")]
        [MaxLength(30, ErrorMessage = "Max length - 30")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        [MinLength(2, ErrorMessage = "Min length - 2")]
        [MaxLength(30, ErrorMessage = "Max length - 30")]
        public string City { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "This field is required to fill out")]
        public double Longitude { get; set; }
    }
}
