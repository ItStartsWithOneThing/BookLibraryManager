using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibraryManagerDAL.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
