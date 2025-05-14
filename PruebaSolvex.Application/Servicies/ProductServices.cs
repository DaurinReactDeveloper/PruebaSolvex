using Microsoft.Extensions.Logging;
using PruebaSolvex.Application.Contract;
using PruebaSolvex.Application.Core;
using PruebaSolvex.Application.Dtos.ProductDto;
using PruebaSolvex.Application.Mappers;
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
    public class ProductServices : IProductServices
    {

        private readonly IProduct _product;
        private readonly ILogger<ProductServices> _logger;
        private readonly ICloudinaryServices _cloudinaryServices;

        public ProductServices(IProduct _product, ILogger<ProductServices> _logger, ICloudinaryServices _cloudinaryServices)
        {
            this._product = _product;
            this._logger = _logger;
            this._cloudinaryServices = _cloudinaryServices;
        }

        public async Task<ServiceResult> SearchProductsByName(string name)
        {
            ServiceResult result = new ServiceResult();

            try
            {

                if (!ProductValidations.ProductSearchValidation(name, out string message))
                {
                    result.Success = false;
                    result.Message = message;
                    return result;
                }

                var products = await _product.SearchProductsByName(name);

                if (!ProductValidations.ProductCount(products, out string messageValid))
                {
                    result.Success = false;
                    result.Message = messageValid;
                    return result;
                }

                result.Data = products;
                result.Message = "Productos encontrados correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error buscando los productos.";
                _logger.LogError($"Error buscando productos por nombre: {ex.Message}");
            }

            return result;
        }

        public async Task<ServiceResult> GetProducts()
        {

            ServiceResult result = new ServiceResult();

            try
            {
                var productGetAll = await _product.GetProducts();

                if (!ProductValidations.ProductCount(productGetAll, out string messageValid))
                {
                    result.Success = false;
                    result.Message = messageValid;
                    return result;
                }

                result.Data = productGetAll;
                result.Message = "Productos obtenidos correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los productos.";
                _logger.LogError($"Ha ocurrido un error obteniendo los productos: {ex.Message}.");
            }

            return result;
        }

        public async Task<ServiceResult> Add(ProductAddDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {

                var exists = await _product.ProductExist(modelDto.Name, modelDto.Color);

                if (!ProductValidations.ProductExists(exists, out string messageVerifyProduct))
                {
                    result.Success = false;
                    result.Message = messageVerifyProduct;
                    return result;
                }

                var existsDelete = await _product.IsDeletedProductExists(modelDto.Name, modelDto.Color);

                if (!ProductValidations.ProductExistsDelete(existsDelete, out string messageVerifyProductDelete))
                {

                    var productExistsDeleteUpdate = ProductMapper.MapToProduct(existsDelete, modelDto.ChangeUser);

                    await _product.UpdateDeleteProduct(productExistsDeleteUpdate);
                    result.Message = messageVerifyProductDelete;
                    return result;
                }

                if (!ProductValidations.ProductAddValidation(modelDto, out string message))
                {
                    result.Success = false;
                    result.Message = message;
                    return result;
                }

                var urlDecodificada = Uri.UnescapeDataString(modelDto.ImageUrl);

                if (!CloudinaryValidations.IsValidImage(urlDecodificada, out string messageImage))
                {
                    result.Success = false;
                    result.Message = messageImage;
                    return result;
                }

                var imageUrl = await _cloudinaryServices.UploadImageFromUrlAsync(urlDecodificada);


                await _product.Add(new Product
                {

                    Name = modelDto.Name,
                    Description = modelDto.Description,
                    ImageUrl = imageUrl,
                    Color = modelDto.Color,
                    Price = modelDto.Price,
                    CreationDate = DateTime.Now,
                    CreationUser = modelDto.ChangeUser,

                });

                result.Message = "Producto agregado correctamente";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el producto.";
                _logger.LogError($"Ha ocurrido un error guardando el producto: {ex.Message}.");
            }

            return result;
        }

        public async Task<ServiceResult> Update(ProductUpdateDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var productUpdate = await _product.GetById(modelDto.Id);

                if (!ProductValidations.ProductVerifyId(productUpdate, out string messageVerify))
                {
                    result.Success = false;
                    result.Message = messageVerify;
                    return result;
                }

                if (!ProductValidations.ProductUpdateValidation(modelDto, out string messageValidation))
                {
                    result.Success = false;
                    result.Message = messageValidation;
                    return result;
                }

                productUpdate.Name = modelDto.Name;
                productUpdate.Description = modelDto.Description;
                productUpdate.Price = modelDto.Price;
                productUpdate.Color = modelDto.Color;
                productUpdate.UserMod = modelDto.ChangeUser;
                productUpdate.ModifyDate = DateTime.Now;

                await _product.Update(productUpdate);
                result.Message = "Producto actualizado correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el producto.";
                _logger.LogError($"Ha ocurrido un error actualizando el producto: {ex.Message}.");
            }

            return result;
        }

        public async Task<ServiceResult> Remove(ProductRemoveDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {

                var productRemove = await _product.GetById(modelDto.Id);

                if (!ProductValidations.ProductVerifyId(productRemove, out string messageVerify))
                {
                    result.Success = false;
                    result.Message = messageVerify;
                    return result;
                }

                if (!ProductValidations.ProductoRemoveValidation(modelDto, out string messageValidation))
                {
                    result.Success = false;
                    result.Message = messageValidation;
                    return result;
                }

                productRemove.UserDeleted = modelDto.ChangeUser;

                await _product.Remove(productRemove);
                result.Message = "Producto eliminado correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el producto.";
                _logger.LogError($"Ha ocurrido un error eliminando el producto: {ex.Message}.");
            }

            return result;
        }

    }
}
