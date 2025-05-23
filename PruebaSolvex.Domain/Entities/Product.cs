﻿using PruebaSolvex.Domain.Core;
using System;
using System.Collections.Generic;

namespace PruebaSolvex.Domain.Entities
{
    public partial class Product : SimilarFields
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string Color { get; set; } = null!;

        public decimal Price { get; set; }

    }

}
