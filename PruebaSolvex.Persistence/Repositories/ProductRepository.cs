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
                var products = await dbsolvexContext.Products
                    .Where(pr => !pr.Deleted)
                    .Select(pr => new ProductModel
                    {
                        Id = pr.Id,
                        Name = pr.Name,
                        Description = pr.Description,
                        ImageUrl = pr.ImageUrl,
                        Color = pr.Color,
                        Price = pr.Price,


                    })
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error obteniendo los productos, {ex.ToString()}.");
                throw new ProductException("Ha ocurrido un error obteniendo los productos.");
            }
        }

        public async Task<List<ProductModel>> SearchProductsByName(string name)
        {
            try
            {
                var products = await dbsolvexContext.Products
                    .Where(pr => !pr.Deleted && EF.Functions.Like(pr.Name, $"%{name}%"))
                    .Select(pr => new ProductModel
                    {
                        Id = pr.Id,
                        Name = pr.Name,
                        Description = pr.Description,
                        ImageUrl = pr.ImageUrl,
                        Color = pr.Color,
                        Price = pr.Price
                    })
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error buscando productos por nombre, {ex.ToString()}.");
                throw new ProductException("Ha ocurrido un error buscando productos por nombre.");
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
                productUpdate.Color = entity.Color;
                productUpdate.Price = entity.Price;
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

        public override async Task UpdateDeleteProduct(Product entity)
        {

            try
            {
                var productUpdate = await base.GetById(entity.Id);

                if (productUpdate is null)
                {
                    throw new ProductException("Ha ocurrido un error obteniendo el producto.");
                }

                productUpdate.Deleted = false;
                productUpdate.ModifyDate = DateTime.Now;
                productUpdate.UserMod = entity.UserMod;

                await base.UpdateDeleteProduct(productUpdate);
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

        public async Task<bool> ProductExist(string name, string color)
        {
            try
            {
                var exists = await dbsolvexContext.Products
                    .AnyAsync(pr => !pr.Deleted &&
                                    EF.Functions.Like(pr.Name.Trim(), $"%{name.Trim()}%") &&
                                    pr.Color == color);

                return exists;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error verificando la existencia del producto, {ex.ToString()}.");
                throw new ProductException("Ha ocurrido un error verificando la existencia del producto.");
            }
        }

        public async Task<ProductModel> IsDeletedProductExists(string name, string color)
        {
            try
            {
                var product = await dbsolvexContext.Products
                    .Where(pr => pr.Deleted &&
                                 EF.Functions.Like(pr.Name.Trim(), $"%{name.Trim()}%") &&
                                 pr.Color == color)
                    .FirstOrDefaultAsync();

                if (product is null) return null;

                return new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Color = product.Color,
                    Price = product.Price
                };
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error verificando la existencia del producto eliminado, {ex}");
                throw new ProductException("Ha ocurrido un error verificando la existencia del producto eliminado.");
            }
        }

    }

}
