using PruebaSolvex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Infrastructure.Models
{
    public partial class ProductVariationModel
    {

        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Color { get; set; } = null!;

        public decimal Price { get; set; }

        public virtual Product Product { get; set; } = null!;

    }
}
