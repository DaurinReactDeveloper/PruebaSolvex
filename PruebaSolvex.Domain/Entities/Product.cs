using System;
using System.Collections.Generic;

namespace PruebaSolvex.Domain.Entities
{
    public partial class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public virtual ICollection<Productvariation> Productvariations { get; set; } = new List<Productvariation>();
    }

}
