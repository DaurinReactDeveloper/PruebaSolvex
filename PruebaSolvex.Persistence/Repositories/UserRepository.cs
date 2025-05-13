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
    public class UserRepository : BaseRepository<User>, IUser
    {
        private readonly DbsolvexContext dbsolvexContext;
        private readonly ILogger<UserRepository> logger;

        public UserRepository(DbsolvexContext dbsolvexContext, ILogger<UserRepository> logger) : base(dbsolvexContext)
        {
            this.dbsolvexContext = dbsolvexContext;
            this.logger = logger;
        }

        public async Task<List<UserModel>> GetUser()
        {
            try
            {

                var users = await (from pr in dbsolvexContext.Users
                                   where !pr.Deleted
                                   select new UserModel()
                                   {

                                       Id = pr.Id,
                                       Name = pr.Name,
                                       Email = pr.Email,
                                       Role = pr.Role,

                                   }).ToListAsync();

                return users;
            }

            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error obteniendo los usuarios, {ex.ToString()}.");
                throw new UserException("Ha ocurrido un error obteniendo los usuarios.");
            }

        }

        public override async Task Add(User entity)
        {
            try
            {
                await base.Add(entity);
                await base.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error guardando el usuario, {ex.ToString()}.");
                throw new UserException("Ha ocurrido un error guardando el usuario.");
            }
        }

        public override async Task Update(User entity)
        {
            try
            {
                var userUpdate = await base.GetById(entity.Id);

                if (userUpdate is null)
                {
                    throw new UserException("Ha ocurrido un error obteniendo el producto.");
                }

                userUpdate.Name = entity.Name;
                userUpdate.Email = entity.Email;
                userUpdate.PasswordHash = entity.PasswordHash;
                userUpdate.ModifyDate = DateTime.Now;
                userUpdate.UserMod = entity.UserMod;

                await base.Update(userUpdate);
                await base.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error actualizando el usuario, {ex.ToString()}.");
            }
        }

        public override async Task Remove(User entity)
        {

            try
            {
                var userRemove = await base.GetById(entity.Id);

                if (userRemove is null)
                {
                    throw new UserException("Ha ocurrido un error obteniendo el usuario.");
                }

                userRemove.Deleted = true;
                userRemove.DeletedDate = DateTime.Now;
                userRemove.UserDeleted = entity.UserDeleted;

                await base.Update(userRemove);
                await base.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un error eliminando el usuario, {ex.ToString()}.");
            }

        }

    }
}
