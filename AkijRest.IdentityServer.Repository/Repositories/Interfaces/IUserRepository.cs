using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string userName, string password);
        User Create(UserDto userDTO);
        Task<User> GetAsync(string userName);
    }
}