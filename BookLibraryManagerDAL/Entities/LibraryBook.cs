using System;
using System.Collections.Generic;

namespace BookLibraryManagerDAL.Entities
{
    public class LibraryBook : BaseEntity
    {
        public Guid RevisionId { get; set; }

        public Guid LibraryId { get; set; }

        public ICollection<BookRevision> BookRevisions { get; set; } 

        public ICollection<RentBook> RentBooks { get; set; }

        public ICollection<Library> Libraries { get; set; }
    }
}
