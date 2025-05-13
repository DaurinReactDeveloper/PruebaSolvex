using PruebaSolvex.Domain.Core;
using System;
using System.Collections.Generic;

namespace PruebaSolvex.Domain.Entities
{

    public partial class User : SimilarFields
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = null!;

    }
}
