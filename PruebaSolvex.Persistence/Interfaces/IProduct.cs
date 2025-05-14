using PruebaSolvex.Domain.Entities;
using PruebaSolvex.Domain.Repository;
using PruebaSolvex.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Persistence.Interfaces
{
    public interface IProduct : IBaseRepository<Product>
    {

        Task<List<ProductModel>> GetProducts();

        Task<List<ProductModel>> SearchProductsByName(string name);

        Task<bool> ProductExist(string name, string color);

        Task<ProductModel> IsDeletedProductExists(string name, string color);


    }
}
