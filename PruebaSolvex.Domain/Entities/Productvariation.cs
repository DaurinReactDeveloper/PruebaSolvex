using PruebaSolvex.Domain.Core;
using System;
using System.Collections.Generic;

namespace PruebaSolvex.Domain.Entities
{
    public partial class ProductVariation : SimilarFields
    {

        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Color { get; set; } = null!;

        public decimal Price { get; set; }

        public virtual Product Product { get; set; } = null!;

    }
}
