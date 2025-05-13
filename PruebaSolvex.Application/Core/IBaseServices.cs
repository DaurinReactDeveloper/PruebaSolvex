using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Application.Core
{
    public interface IBaseServices<AddDto, RemoveDto, UpdateDto>
    {

        Task<ServiceResult> Add(AddDto modelDto);

        Task<ServiceResult> Update(UpdateDto modelDto);

        Task<ServiceResult> Remove(RemoveDto modelDto);


    }
}
