using System;

namespace MiniShop.Core.Domain
{
    public abstract class BaseDomain
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
