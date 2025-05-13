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
    public static class UserValidations
    {

        public static bool UserAddValidation(UserAddDto userAdd, out string message)
        {

            message = string.Empty;

            if (userAdd is null)
            {
                message = "El usuario no puede ser nulo.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userAdd.Name) || userAdd.Name.Length <= 8 || userAdd.Name.Length >= 24)
            {
                message = "El nombre debe tener al menos 9 caracteres y no puede estar vacío.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userAdd.Email) || !IsValidEmail(userAdd.Email))
            {
                message = "El correo electrónico no es válido o está vacío.";
                return false;
            }


            if (userAdd.Email.Length <= 10 || userAdd.Email.Length >= 100)
            {
                message = "El correo electrónico debe tener al menos 11 caracteres.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userAdd.PasswordHash) || userAdd.PasswordHash.Length < 8 || !HasNumberAndLetter(userAdd.PasswordHash) || userAdd.PasswordHash.Length >= 15)
            {
                message = "La contraseña debe tener al menos 8 caracteres y contener tanto letras como números.";
                return false;
            }

            return true;
        }

        public static bool UserUpdateValidation(UserUpdateDto userUpdateDto, out string message)
        {

            message = string.Empty;

            if (userUpdateDto.Id <= 0)
            {
                message = "No se pudo obtener el usuario.";
                return false;

            }

            if (userUpdateDto is null)
            {
                message = "El usuario no puede ser nulo.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userUpdateDto.Name) || userUpdateDto.Name.Length <= 8 || userUpdateDto.Name.Length >= 24)
            {
                message = "El nombre debe tener al menos 9 caracteres y no puede estar vacío.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userUpdateDto.Email) || !IsValidEmail(userUpdateDto.Email))
            {
                message = "El correo electrónico no es válido o está vacío.";
                return false;
            }


            if (userUpdateDto.Email.Length <= 10 || userUpdateDto.Email.Length >= 100)
            {
                message = "El correo electrónico debe tener al menos 11 caracteres.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userUpdateDto.PasswordHash) || userUpdateDto.PasswordHash.Length < 8 || !HasNumberAndLetter(userUpdateDto.PasswordHash) || userUpdateDto.PasswordHash.Length >= 15)
            {
                message = "La contraseña debe tener al menos 8 caracteres y contener tanto letras como números.";
                return false;
            }

            return true;

        }

        public static bool UserRemoveValidation(UserRemoveDto userRemove, out string message)
        {

            message = string.Empty;

            if (userRemove.Id <= 0)
            {
                message = "No se pudo obtener el usuario.";
                return false;

            }

            return true;

        }

        public static bool UserVerifyId(User userId, out string message)
        {
            message = string.Empty;

            if (userId is null)
            {
                message = "No se pudo obtener el Usuario.";
                return false;
            }

            return true;

        }

        public static bool UserEmail(UserModel user, out string message)
        {

            message = string.Empty;

            if (user is null)
            {
                message = "Email Incorrecto";
                return false;

            }

            return true;
        }
       
        public static bool UserEmailExists(UserModel user, out string message)
        {
            message = string.Empty;

            if (user != null)
            {
                message = "El correo electrónico ya está registrado.";
                return false;
            }

            return true;
        }

        public static bool UserCount(List<UserModel> users, out string message)
        {

            message = string.Empty;

            if (users.Count <= 0)
            {
                message = "No hay usuarios registrados.";
                return false;

            }


            return true;

        }

        private static bool IsValidEmail(string email)
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        private static bool HasNumberAndLetter(string password)
        {
            bool hasLetter = false;
            bool hasNumber = false;

            foreach (char c in password)
            {
                if (char.IsLetter(c)) hasLetter = true;
                if (char.IsDigit(c)) hasNumber = true;
            }

            return hasLetter && hasNumber;

        }

    }
}
