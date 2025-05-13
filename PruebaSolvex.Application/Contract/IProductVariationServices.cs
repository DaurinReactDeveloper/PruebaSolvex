using PruebaSolvex.Application.Core;
using PruebaSolvex.Application.Dtos.ProductVariationDto;
using PruebaSolvex.Application.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Application.Contract
{
    public interface IProductVariationServices : IBaseServices<ProductVariationAddDto, ProductVariationRemoveDto, ProductVariationUpdateDto>
    {

       Task<ServiceResult> GetProductVariationByProductId(int productId);

    }
}
