using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryManagerDAL.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Author { get; set; }

        [ForeignKey("BookId")]
        public ICollection<BookRevision> BookRevisions { get; set; }
    }
}
