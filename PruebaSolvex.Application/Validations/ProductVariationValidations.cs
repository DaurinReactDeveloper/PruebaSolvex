using PruebaSolvex.Application.Dtos.ProductVariationDto;
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
    public static class ProductVariationValidations
    {

        public static bool ProductVariationAddValidation(ProductVariationAddDto productVariationAdd, out string message)
        {

            message = string.Empty;

            if (productVariationAdd is null)
            {
                message = "La variacion del producto no puede ser nulo.";
                return false;
            }

            if (productVariationAdd.ProductId <=0)
            {
                message = "El ID del producto es obligatorio";
            }

            if (productVariationAdd.Color.Length <= 8 || productVariationAdd.Color.Length >= 25)
            {
                message = "El color debe tener al menos 9 caracteres y menos de 25";
            }

            if (string.IsNullOrWhiteSpace(productVariationAdd.Color))
            {
                message = "El color no puede estar vacío.";
                return false;
            }

            if (productVariationAdd.Price <= 0)
            {
                message = "El precio del producto es obligatorio";
            }


            return true;
        }

        public static bool ProductVariationUpdateValidation(ProductVariationUpdateDto productVariationAdd, out string message)
        {

            message = string.Empty;

            if (productVariationAdd is null)
            {
                message = "La variacion del producto no puede ser nulo.";
                return false;
            }

            if (productVariationAdd.Color.Length <= 8 || productVariationAdd.Color.Length >= 25)
            {
                message = "El color debe tener al menos 9 caracteres y menos de 25";
            }

            if (string.IsNullOrWhiteSpace(productVariationAdd.Color))
            {
                message = "El color no puede estar vacío.";
                return false;
            }

            if (productVariationAdd.Price <= 0)
            {
                message = "El precio del producto es obligatorio";
            }


            return true;
        }

        public static bool ProductVariationRemoveValidation(ProductVariationRemoveDto productVariationRemove, out string message)
        {

            message = string.Empty;

            if (productVariationRemove.Id <= 0)
            {
                message = "No se pudo obtener el ID de la variacion del producto.";
                return false;

            }

            return true;

        }

        public static bool ProductVariationCount(List<ProductVariationModel> productVariation, out string message)
        {

            message = string.Empty;

            if (productVariation.Count <= 0)
            {
                message = "No se encontraron las variaciones del producto.";
                return false;

            }

            return true;

        }

        public static bool ProductVariationVerifyId(ProductVariation userId, out string message)
        {
            message = string.Empty;

            if (userId is null)
            {
                message = "No se pudo obtener el ID de la variacion del producto.";
                return false;
            }

            return true;

        }

    }
}
