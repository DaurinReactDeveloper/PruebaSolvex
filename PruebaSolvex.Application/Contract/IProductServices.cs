using PruebaSolvex.Application.Core;
using PruebaSolvex.Application.Dtos.ProductDto;
using PruebaSolvex.Persistence.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Application.Contract
{
    public interface IProductServices : IBaseServices<ProductAddDto, ProductRemoveDto, ProductUpdateDto>
    {

        Task<ServiceResult> GetProducts();

        Task<ServiceResult> SearchProductsByName(string name);

    }
}
