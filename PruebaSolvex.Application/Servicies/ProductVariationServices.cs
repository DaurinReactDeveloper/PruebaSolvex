
using Microsoft.Extensions.Logging;
using PruebaSolvex.Application.Contract;
using PruebaSolvex.Application.Core;
using PruebaSolvex.Application.Dtos.ProductVariationDto;
using PruebaSolvex.Application.Validations;
using PruebaSolvex.Domain.Entities;
using PruebaSolvex.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Application.Servicies
{
    public class ProductVariationServices : IProductVariationServices
    {

        private readonly IProductVariation _productVariation;
        private readonly ILogger<ProductVariationServices> _logger;

        public ProductVariationServices(IProductVariation _productVariation, ILogger<ProductVariationServices> _logger)
        {
            this._productVariation = _productVariation;
            this._logger = _logger;
        }


        public async Task<ServiceResult> GetProductVariationByProductId(int productId)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var productVariationGetAll = await _productVariation.GetProductVariationByProductId(productId);

                if (!ProductVariationValidations.ProductVariationCount(productVariationGetAll, out string messageValid))
                {
                    result.Success = false;
                    result.Message = messageValid;
                    return result;
                }

                result.Data = productVariationGetAll;
                result.Message = "Variacion del producto obtenidos correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo la variacion del producto.";
                _logger.LogError($"Ha ocurrido un error obteniendo la variacion del producto: {ex.Message}.");
            }

            return result;
        }

        public async Task<ServiceResult> Add(ProductVariationAddDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {

                if (!ProductVariationValidations.ProductVariationAddValidation(modelDto, out string message))
                {
                    result.Success = false;
                    result.Message = message;
                    return result;
                }

      
                await _productVariation.Add(new ProductVariation
                {

                    Id = modelDto.Id,
                    ProductId = modelDto.ProductId,
                    Color = modelDto.Color,
                    Price = modelDto.Price, 
                    CreationDate = DateTime.Now,

                });

                await _productVariation.SaveChanges();
                result.Message = "Variacion del producto agregada correctamente";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando la variacion del producto.";
                _logger.LogError($"Ha ocurrido un error guardando la variacion del producto: {ex.Message}.");
            }

            return result;
        }


        public async Task<ServiceResult> Update(ProductVariationUpdateDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var productVariationUpdate = await _productVariation.GetById(modelDto.Id);

                if (!ProductVariationValidations.ProductVariationVerifyId(productVariationUpdate, out string messageVerify))
                {
                    result.Success = false;
                    result.Message = messageVerify;
                    return result;
                }

                if (!ProductVariationValidations.ProductVariationUpdateValidation(modelDto, out string messageValidation))
                {
                    result.Success = false;
                    result.Message = messageValidation;
                    return result;
                }

                productVariationUpdate.Color = modelDto.Color;
                productVariationUpdate.Price = modelDto.Price;
                productVariationUpdate.UserMod = modelDto.ChangeUser;
                productVariationUpdate.ModifyDate = DateTime.Now;

                await _productVariation.Update(productVariationUpdate);
                await _productVariation.SaveChanges();
                result.Message = "Variacion del producto actualizada correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando la variacion del producto.";
                _logger.LogError($"Ha ocurrido un error actualizando la variacion del producto: {ex.Message}.");
            }

            return result;
        }

        public async Task<ServiceResult> Remove(ProductVariationRemoveDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {

                var productVariationRemove = await _productVariation.GetById(modelDto.Id);

                if (!ProductVariationValidations.ProductVariationVerifyId(productVariationRemove, out string messageVerify))
                {
                    result.Success = false;
                    result.Message = messageVerify;
                    return result;
                }

                if (!ProductVariationValidations.ProductVariationRemoveValidation(modelDto, out string messageValidation))
                {
                    result.Success = false;
                    result.Message = messageValidation;
                    return result;
                }

                productVariationRemove.UserDeleted = modelDto.ChangeUser;

                await _productVariation.Remove(productVariationRemove);
                await _productVariation.SaveChanges();
                result.Message = "Variacion del producto eliminada correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando la variacion del producto.";
                _logger.LogError($"Ha ocurrido un error eliminando la variacion del producto: {ex.Message}.");
            }

            return result;
        }


    }
}
