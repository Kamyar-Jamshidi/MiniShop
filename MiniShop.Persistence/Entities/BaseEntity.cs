using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniShop.Persistence.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
