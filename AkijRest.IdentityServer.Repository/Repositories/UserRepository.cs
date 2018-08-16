using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityServerDbContext _context;

        public UserRepository()
        {
            _context = new IdentityServerDbContext();
        }
        public Task<User> GetAsync(string userName, string password)
        {
            var user = _context
                .Users
                .SingleOrDefaultAsync(
                    u => u.UserName.Equals(userName)
                    && u.Password.Equals(password)
                );
            return user;
        }

        public User Get(string userName, string password)
        {
            User user = null;
            if (!userName.EndsWith("@akij.net"))
            {
                userName += "@akij.net";
            }
            if (IsAdAuthentication(userName,password))
            {
                user = _context.Users.SingleOrDefault(u => u.Email.ToLower().Equals(userName) && u.Approved.Equals(true));
            }
            //var encryptedPassword = HashHelper.Sha512(password + userName);
            
            // HashHelper.Sha512(userDTO.Password + userDTO.UserName)
            return user;
        }

        public Task<User> GetAsync(string userName)
        {
            var user = _context
                .Users
                .SingleOrDefaultAsync(
                    u => u.UserName.Equals(userName)
                );


            return user;
        }
        public User Create(UserDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                UserName = userDto.UserName,
                FullName = userDto.FullName,
                Password = HashHelper.Sha512(userDto.Password + userDto.UserName),
                Approved = false
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return (user);
        }

        public List<UserDto> Get()
        {
            List<UserDto> listUserDto = new List<UserDto>();

            if (_context != null)
            {
                List<User> listUser
                = _context.Users.ToList();

                foreach (User user in listUser)
                {
                    UserDto dto = new UserDto();
                    dto.Id = user.Id;
                    dto.UserName = user.UserName;
                    dto.FullName = user.FullName;
                    dto.Email = user.Email;
                    dto.Approved = user.Approved;
                    listUserDto.Add(dto);
                }

                return listUserDto;
            }

            throw new Exception();
        }
        public List<UserDto> Get(string serachKey)
        {
            List<UserDto> listUserDto = new List<UserDto>();

            if (_context != null)
            {
                List<User> listUser= _context.Users.Where(x=>x.UserName.Contains(serachKey) || x.Id.ToString().Contains(serachKey)).ToList();

                foreach (User user in listUser)
                {
                    UserDto dto = new UserDto();
                    dto.Id = user.Id;
                    dto.UserName = user.UserName;
                    dto.FullName = user.FullName;
                    dto.Email = user.Email;
                    dto.Approved = user.Approved;
                    listUserDto.Add(dto);
                }

                return listUserDto;
            }

            throw new Exception();
        }
        public UserDto Get(int userId)
        {
            if (_context != null)
            {
                User user = _context.Users.FirstOrDefault(x => x.Id.Equals(userId));
                UserDto dto = null;
                if (user != null)
                {
                    dto = new UserDto();
                    dto.Id = user.Id;
                    dto.UserName = user.UserName;
                    dto.FullName = user.FullName;
                    dto.Email = user.Email;
                    dto.Approved = user.Approved;
                }
                return dto;
            }
            throw new Exception();
        }
        public UserDto GetByUserName(string userName)
        {
            if (_context != null)
            {
                User user = _context.Users.FirstOrDefault(x => x.UserName.Equals(userName));
                UserDto dto = null;
                if (user != null)
                {
                    dto = new UserDto();
                    dto.Id = user.Id;
                    dto.UserName = user.UserName;
                    dto.FullName = user.FullName;
                    dto.Email = user.Email;
                    dto.Approved = user.Approved;
                }
                return dto;
            }
            throw new Exception();
        }
        public string GetUserNameByGoogleEmail(string gmail)
        {
            var user = _context.Users
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
            var user = _context.Users
                .Include(u => u.ExternalLoginEmail)
                .SingleOrDefault(u => u.ExternalLoginEmail.Facebook.Equals(facebookMail));

            // functional style

            if (user != null)
            {
                return user.UserName;
            }
            return null;

        }
        public UserDto GetSuppervisorEmailByUserName(string userName)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName.Equals(userName));
            if (user != null)
            {
                var supervisor = _context.Users.FirstOrDefault(x => x.Id.Equals(user.SuperVisorId));
                if (supervisor != null)
                {
                    UserDto dto = new UserDto
                    {
                        UserName = supervisor.UserName,
                        Email = supervisor.Email,
                        FullName = supervisor.FullName
                    };
                    return dto;
                }
            }
            return null;
        }
        public bool IsAdAuthentication(string email, string password)
        {
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://akij.net", email, password);
                object nativeObject = entry.NativeObject;
               return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
    }
}