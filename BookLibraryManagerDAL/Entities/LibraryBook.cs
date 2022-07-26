using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryManagerDAL.Entities
{
    public class LibraryBook : BaseEntity
    {
        public Guid RevisionId { get; set; }
        public BookRevision BookRevision { get; set; }

        public Guid LibraryId { get; set; }
        public Library Library { get; set; }

        [ForeignKey("LibraryBookId")]
        public ICollection<RentBook> RentBooks { get; set; }
    }
}
