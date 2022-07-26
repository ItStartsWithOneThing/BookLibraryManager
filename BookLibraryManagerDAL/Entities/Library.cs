using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryManagerDAL.Entities
{
    public class Library : BaseEntity
    {
        [ForeignKey("Location")]
        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; }

        public string Name { get; set; }
        public string FullAddress { get; set; }

        [ForeignKey("LibraryId")]
        public ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}
