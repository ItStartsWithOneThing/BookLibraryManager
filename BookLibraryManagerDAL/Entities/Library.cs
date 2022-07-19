using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryManagerDAL.Entities
{
    public class Library : BaseEntity
    {
        [ForeignKey("Location")]
        public Guid LocationId { get; set; }

        public Point Location { get; set; }

        [ForeignKey("City")]
        public Guid CityId { get; set; }

        public City City { get; set; }

        public string FullAddress { get; set; }

        public ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}
