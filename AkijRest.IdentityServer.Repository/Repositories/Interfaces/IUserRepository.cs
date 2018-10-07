using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Models;
using System.Threading.Tasks;

namespace AkijRest.IdentityServer.Repository.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string userName, string password);
        User Create(UserDto userDto);
        Task<User> GetAsync(string userName);
    }
}