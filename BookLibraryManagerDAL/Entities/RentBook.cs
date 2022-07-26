using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerDAL.Entities
{
    public class RentBook : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid LibraryBookId { get; set; }
        public LibraryBook LibraryBook { get; set; }

        public DateTime RentDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}
