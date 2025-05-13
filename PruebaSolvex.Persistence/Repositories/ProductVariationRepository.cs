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
    public class ProductVariationRepository : BaseRepository<ProductVariation>, IProductVariation
    {

        private readonly DbsolvexContext dbsolvexContext;
        private readonly ILogger<ProductVariationRepository> logger;

        public ProductVariationRepository(DbsolvexContext dbsolvexContext, ILogger<ProductVariationRepository> logger) : base(dbsolvexContext)
        {
            this.dbsolvexContext = dbsolvexContext;
            this.logger = logger;
        }

        public async Task<List<ProductVariationModel>> GetProductVariation()
        {
            try
            {

                var productVariation = await (from prv in dbsolvexContext.Productvariations
                                              where !prv.Deleted
                                              select new ProductVariationModel()
                                              {

                                                  Id = prv.Id,
                                                  ProductId = prv.ProductId,
                                                  Color = prv.Color,
                                                  Price = prv.Price,

                                              }).ToListAsync();

                return productVariation;
            }

            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error obteniendo la variacion de productos, {ex.ToString()}.");
                throw new ProductVariationException("Ha ocurrido un error obteniendo la variacion de productos.");
            }
        }

        public override async Task Add(ProductVariation entity)
        {
            try
            {
                await base.Add(entity);
                await base.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error guardando el producto por variacion, {ex.ToString()}.");
                throw new ProductVariationException("Ha ocurrido un error guardando el producto por variacion.");
            }
        }

        public override async Task Update(ProductVariation entity)
        {

            try
            {
                var productVariationUpdate = await base.GetById(entity.Id);

                if (productVariationUpdate is null)
                {
                    throw new ProductVariationException("Ha ocurrido un error obteniendo el producto por variacion.");
                }

                productVariationUpdate.Color = entity.Color;
                productVariationUpdate.Price = entity.Price;
                productVariationUpdate.ModifyDate = DateTime.Now;
                productVariationUpdate.UserMod = entity.UserMod;

                await base.Update(productVariationUpdate);
                await base.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error actualizando el producto por variacion, {ex.ToString()}.");
            }

        }

        public override async Task Remove(ProductVariation entity)
        {
            try
            {
                var productVariationRemove = await base.GetById(entity.Id);

                if (productVariationRemove is null)
                {
                    throw new ProductVariationException("Ha ocurrido un error obteniendo el producto por variacion.");
                }

                productVariationRemove.Deleted = true;
                productVariationRemove.DeletedDate = DateTime.Now;
                productVariationRemove.UserDeleted = entity.UserDeleted;

                await base.Update(productVariationRemove);
                await base.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error eliminando el producto por variacion, {ex.ToString()}.");
            }
        }

    }
}

