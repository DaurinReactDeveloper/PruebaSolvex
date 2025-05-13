using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaSolvex.Domain.Entities;
using PruebaSolvex.Infrastructure.Exceptions;
using PruebaSolvex.Infrastructure.Models;
using PruebaSolvex.Persistence.Context;
using PruebaSolvex.Persistence.Core;
using PruebaSolvex.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProduct
    {

        private readonly DbsolvexContext dbsolvexContext;
        private readonly ILogger<ProductRepository> logger;

        public ProductRepository(DbsolvexContext dbsolvexContext, ILogger<ProductRepository> logger) : base(dbsolvexContext)
        {
            this.dbsolvexContext = dbsolvexContext;
            this.logger = logger;
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            try
            {

                var products = await (from pr in dbsolvexContext.Products
                                      where !pr.Deleted
                                      select new ProductModel()
                                      {
                                          Id = pr.Id,
                                          Name = pr.Name,
                                          Description = pr.Description,
                                          ImageUrl = pr.ImageUrl

                                      }).ToListAsync();

                return products;
            }

            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error obteniendo los productos, {ex.ToString()}.");
                throw new ProductException("Ha ocurrido un error obteniendo los productos.");
            }
        }

        public override async Task Add(Product entity)
        {
            try
            {
                await base.Add(entity);
                await base.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error guardando el producto, {ex.ToString()}.");
                throw new ProductException("Ha ocurrido un error guardando el producto.");
            }
        }

        public override async Task Update(Product entity)
        {
            try
            {
                var productUpdate = await base.GetById(entity.Id);

                if (productUpdate is null)
                {
                    throw new ProductException("Ha ocurrido un error obteniendo el producto.");
                }

                productUpdate.Name = entity.Name;
                productUpdate.Description = entity.Description;
                productUpdate.ImageUrl = entity.ImageUrl;
                productUpdate.ModifyDate = DateTime.Now;
                productUpdate.UserMod = entity.UserMod;

                await base.Update(productUpdate);
                await base.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error actualizando el producto, {ex.ToString()}.");
            }
        }

        public override async Task Remove(Product entity)
        {

            try
            {
                var productRemove = await base.GetById(entity.Id);

                if (productRemove is null)
                {
                    throw new ProductException("Ha ocurrido un error obteniendo el producto.");
                }

                productRemove.Deleted = true;
                productRemove.DeletedDate = DateTime.Now;
                productRemove.UserDeleted = entity.UserDeleted;

                await base.Update(productRemove);
                await base.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error eliminando el producto, {ex.ToString()}.");
            }
        }

    }

}
