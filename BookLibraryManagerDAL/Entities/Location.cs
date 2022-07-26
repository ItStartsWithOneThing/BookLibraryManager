

using System;

namespace BookLibraryManagerDAL.Entities
{
    public class Location : BaseEntity
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Library Library { get; set; }
    }
}
