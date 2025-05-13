using PruebaSolvex.Domain.Entities;
using PruebaSolvex.Domain.Repository;
using PruebaSolvex.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Persistence.Interfaces
{
    public interface IUser : IBaseRepository<User>
    {

        Task<List<UserModel>> GetUser();

    }
}
