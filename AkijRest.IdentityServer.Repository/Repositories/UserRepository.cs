using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class UserRepository: IUserRepository
    {
        private IdentityServerDbContext context;

        public UserRepository()
        {
            this.context = new IdentityServerDbContext();
        }

        public Task<User> GetAsync(string userName, string password)
        {
            var user = context
                .Users
                .SingleOrDefaultAsync(
                    u => u.UserName.Equals(userName)
                    && u.Password.Equals(password)
                );


            return user;
        }

        public User Get(string userName, string password)
        {
            var encryptedPassword = HashHelper.Sha512(password + userName);

            var user = context
                .Users
                .SingleOrDefault(
                    u => u.UserName.Equals(userName)
                    && u.Password.Equals(encryptedPassword)
                );

            // HashHelper.Sha512(userDTO.Password + userDTO.UserName)
            return user;
        }

        public Task<User> GetAsync(string userName)
        {
            var user = context
                .Users
                .SingleOrDefaultAsync(
                    u => u.UserName.Equals(userName)
                );


            return user;
        }


        public User Create(UserDto userDTO)
        {
            var user = new User
            {
                Email = userDTO.Email,
                UserName = userDTO.UserName,
                FullName = userDTO.FullName,
                Password = HashHelper.Sha512(userDTO.Password + userDTO.UserName),
                Approved = false
            };

            context.Users.Add(user);
            context.SaveChanges();

            return (user);
        }

        public List<UserDto> Get()
        {
            List<UserDto> listUserDTO = new List<UserDto>();

            if (context != null)
            {
                List<User> listUser
                = context.Users.ToList();

                foreach (User user in listUser)
                {
                    UserDto dto = new UserDto();

                    dto.UserName = user.UserName;
                    dto.FullName = user.FullName;
                    dto.Email = user.Email;

                    listUserDTO.Add(dto);
                }

                return listUserDTO;
            }

            throw new Exception();
        }

        public string GetUserNameByGoogleEmail(string gmail)
        {
            var user = context.Users
                .Include(u => u.ExternalLoginEmail)
                .SingleOrDefault(u => u.ExternalLoginEmail.Google.Equals(gmail));

            // functional style

            if (user != null)
            {
                return user.UserName;
            }
            return null;
        }


        public string GetUserNameByFacebookEmail(string facebookMail)
        {
            var user = context.Users
                .Include(u => u.ExternalLoginEmail)
                .SingleOrDefault(u => u.ExternalLoginEmail.Facebook.Equals(facebookMail));

            // functional style

            if (user != null)
            {
                return user.UserName;
            }
            return null;

        }

    }
}