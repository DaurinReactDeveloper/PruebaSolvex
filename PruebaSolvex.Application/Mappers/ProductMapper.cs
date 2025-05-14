using PruebaSolvex.Application.Dtos.ProductDto;
using PruebaSolvex.Domain.Entities;
using PruebaSolvex.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Application.Mappers
{
    public static class ProductMapper
    {
        public static Product MapToProduct(ProductModel modelDto,int changerUser)
        {
            if (modelDto == null)
                throw new ArgumentNullException(nameof(modelDto));

            return new Product
            {
                Id = modelDto.Id,
                Deleted = false,
               UserMod = changerUser,

            };
        }

    }
}
