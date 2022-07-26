using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerBL.DTOs
{
    public class BookRevisionDto
    {
        public Guid RevisionId { get; set; }
        public Guid BookId { get; set; }
        public int PublishingYear { get; set; }
        public int PagesCount { get; set; }
        public float Price { get; set; }
    }
}
