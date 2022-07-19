using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerDAL.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public Library Library { get; set; }
    }
}
