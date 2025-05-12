﻿using PruebaSolvex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Infrastructure.Models
{
    public partial class ProductModel
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public virtual ICollection<Productvariation> Productvariations { get; set; } = new List<Productvariation>();

    }
}
