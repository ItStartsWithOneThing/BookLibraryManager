using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookLibraryManagerDAL.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("CityId")]
        public ICollection<Library> Libraries { get; set; }
    }
}
