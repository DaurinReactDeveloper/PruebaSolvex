using PruebaSolvex.Application.Dtos.ProductDto;
using PruebaSolvex.Application.Dtos.UserDto;
using PruebaSolvex.Domain.Entities;
using PruebaSolvex.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Application.Validations
{
    public static class ProductValidations
    {

        public static bool ProductAddValidation(ProductAddDto productAdd, out string message)
        {

            message = string.Empty;

            if (productAdd is null)
            {
                message = "El producto no puede ser nulo.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(productAdd.Name) || productAdd.Name.Length <= 8 || productAdd.Name.Length >= 49)
            {
                message = "El nombre del producto debe tener al menos 9 caracteres y no puede estar vacío.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(productAdd.Description) || productAdd.Description.Length <= 19 || productAdd.Description.Length >= 150)
            {
                message = "La Descripcion del producto debe tener al menos 20 caracteres y no puede estar vacío.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(productAdd.ImageUrl))
            {
                message = "La Imagen del producto no puede estar vacía.";
                return false;
            }


            return true;
        }

        public static bool ProductUpdateValidation(ProductUpdateDto productUpdate, out string message)
        {

            message = string.Empty;

            if (productUpdate is null)
            {
                message = "El producto no puede ser nulo.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(productUpdate.Name) || productUpdate.Name.Length <= 8 || productUpdate.Name.Length >= 49)
            {
                message = "El nombre del producto debe tener al menos 9 caracteres y no puede estar vacío.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(productUpdate.Description) || productUpdate.Description.Length <= 19 || productUpdate.Description.Length >= 150)
            {
                message = "La Descripcion del producto debe tener al menos 20 caracteres y no puede estar vacío.";
                return false;
            }

            return true;

        }

        public static bool ProductoRemoveValidation(ProductRemoveDto productRemove, out string message)
        {

            message = string.Empty;

            if (productRemove.Id <= 0)
            {
                message = "No se pudo obtener el producto.";
                return false;

            }

            return true;

        }

        public static bool ProductVerifyId(Product productId, out string message)
        {
            message = string.Empty;

            if (productId is null)
            {
                message = "No se pudo obtener el Producto.";
                return false;
            }

            return true;

        }

        public static bool ProductCount(List<ProductModel> products, out string message)
        {

            message = string.Empty;

            if (products.Count <= 0)
            {
                message = "No hay productos registrados.";
                return false;

            }

            return true;

        }

    }
}
