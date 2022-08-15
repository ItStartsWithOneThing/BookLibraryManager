﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryManagerDAL.Entities
{
    
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public string Password { get; set; }

        [ForeignKey("Role")]
        public Guid? RoleId { get; set; }
        public Role Role { get; set; }

        [ForeignKey("UserId")]
        public ICollection<RentBook> RentBooks { get; set; }
    }
}
