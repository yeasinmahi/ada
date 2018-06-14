using AkijRest.IdentityServer.Repository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        List<RoleDto> Get();
    }
}