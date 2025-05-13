using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Logging;
using PruebaSolvex.Application.Contract;
using PruebaSolvex.Application.Core;
using PruebaSolvex.Application.Dtos.UserDto;
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
    public class UserServices : IUserServices
    {

        private readonly IUser _user;
        private readonly ILogger<UserServices> _logger;
        private readonly IPasswordHelperServices _passwordHelperServices;

        public UserServices(IUser user, ILogger<UserServices> logger, IPasswordHelperServices passwordHelperServices)
        {
            this._user = user;
            this._logger = logger;
            this._passwordHelperServices = passwordHelperServices;
        }

        public async Task<ServiceResult> GetUsers()
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var userGetAll = await _user.GetUsers();

                if (!UserValidations.UserCount(userGetAll, out string messageValid))
                {
                    result.Success = false;
                    result.Message = messageValid;
                    return result;
                }

                result.Data = userGetAll;
                result.Message = "Usuarios obtenidos correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo los usuarios.";
                _logger.LogError($"Ha ocurrido un error obteniendo los usuarios: {ex.Message}.");
            }

            return result;
        }

        public async Task<ServiceResult> GetUser(string email, string password)
        {
            ServiceResult result = new ServiceResult();

            try
            {

                var getUserEmail = await _user.GetUserEmail(email);

                if (!UserValidations.UserEmail(getUserEmail, out string messageEmail))
                {
                    result.Success = false;
                    result.Message = messageEmail;
                    return result;
                }

                if (!AuthenticationValidations.AuthenticationValidationPassword(password, getUserEmail.PasswordHash, out string messagePasswordValidation))
                {
                    result.Success = false;
                    result.Message = messagePasswordValidation;
                    return result;
                }

                var getUser = await _user.GetUser(email, getUserEmail.PasswordHash);

                result.Data = getUser;
                result.Message = "Inicio de sesión exitoso.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error obteniendo el usuario.";
                _logger.LogError($"Ha ocurrido un error obteniendo el usuario: {ex.Message}.");
            }

            return result;
        }

        public async Task<ServiceResult> Add(UserAddDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {

                var userExistent = await _user.GetUserEmail(modelDto.Email);

                if (!UserValidations.UserEmailExists(userExistent, out string messageEmail))
                {
                    result.Success = false;
                    result.Message = messageEmail;
                    return result;
                }

                if (!UserValidations.UserAddValidation(modelDto, out string message))
                {
                    result.Success = false;
                    result.Message = message;
                    return result;
                }

                string passwordHash = _passwordHelperServices.HashPassword(modelDto.PasswordHash);


                await _user.Add(new User
                {

                    Name = modelDto.Name,
                    Email = modelDto.Email,
                    PasswordHash = passwordHash,
                    Role = "user",
                    CreationDate = DateTime.Now,

                });

                await _user.SaveChanges();
                result.Message = "Usuario agregado correctamente";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error guardando el usuario.";
                _logger.LogError($"Ha ocurrido un error guardando el usuario: {ex.Message}.");
            }

            return result;
        }

        public async Task<ServiceResult> Update(UserUpdateDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var userUpdate = await _user.GetById(modelDto.Id);

                if (!UserValidations.UserVerifyId(userUpdate, out string messageVerify))
                {
                    result.Success = false;
                    result.Message = messageVerify;
                    return result;
                }

                if (!UserValidations.UserUpdateValidation(modelDto, out string messageValidation))
                {
                    result.Success = false;
                    result.Message = messageValidation;
                    return result;
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(modelDto.PasswordHash);


                userUpdate.Name = modelDto.Name;
                userUpdate.Email = modelDto.Email;
                userUpdate.PasswordHash = passwordHash;
                userUpdate.Role = modelDto.Role;
                userUpdate.UserMod = modelDto.ChangeUser;
                userUpdate.ModifyDate = DateTime.Now;

                await _user.Update(userUpdate);
                await _user.SaveChanges();
                result.Message = "Usuario actualizado correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error actualizando el usuario.";
                _logger.LogError($"Ha ocurrido un error actualizando el usuario: {ex.Message}.");
            }

            return result;
        }

        public async Task<ServiceResult> Remove(UserRemoveDto modelDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var userRemove = await _user.GetById(modelDto.Id);

                if (!UserValidations.UserVerifyId(userRemove, out string messageVerify))
                {
                    result.Success = false;
                    result.Message = messageVerify;
                    return result;
                }

                if (!UserValidations.UserRemoveValidation(modelDto, out string messageValidation))
                {
                    result.Success = false;
                    result.Message = messageValidation;
                    return result;
                }

                userRemove.UserDeleted = modelDto.ChangeUser;

                await _user.Remove(userRemove);
                await _user.SaveChanges();
                result.Message = "Usuario eliminado correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ha ocurrido un error eliminando el usuario.";
                _logger.LogError($"Ha ocurrido un error eliminando el usuario: {ex.Message}.");
            }

            return result;
        }

    }

}
