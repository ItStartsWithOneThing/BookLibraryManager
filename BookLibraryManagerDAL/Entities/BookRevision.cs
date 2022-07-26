using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryManagerDAL.Entities
{
    public class BookRevision : BaseEntity
    {
        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public int PublishingYear { get; set; }

        public int PagesCount { get; set; }

        public float Price { get; set; }

        [ForeignKey("RevisionId")]
        public ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}
