using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkijRest.IdentityServer.Repository.Dtos;

namespace AkijRest.IdentityServer.Repository.Repositories.Interfaces
{
    public interface IRoleGroupRepository
    {
        List<RoleGroupDto> Get();
    }
}
