

using System;

namespace BookLibraryManagerDAL.Entities
{
    public class Point : BaseEntity
    {
        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public Library Library { get; set; }
    }
}
