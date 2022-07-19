using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryManagerDAL.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [ForeignKey("UserId")]
        public ICollection<RentBook> RentBooks { get; set; }
    }
}
