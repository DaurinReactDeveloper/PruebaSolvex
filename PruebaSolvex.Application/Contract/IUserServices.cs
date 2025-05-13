using PruebaSolvex.Application.Core;
using PruebaSolvex.Application.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Application.Contract
{
    public interface IUserServices : IBaseServices<UserAddDto, UserRemoveDto, UserUpdateDto>
    {

        Task<ServiceResult> GetUsers();

        Task<ServiceResult> GetUser(string email, string password);

    }
}
