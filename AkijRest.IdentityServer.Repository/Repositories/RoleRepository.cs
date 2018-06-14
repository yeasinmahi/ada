using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class RoleRepository
    {
        private IdentityServerDbContext context;

        public RoleRepository()
        {
            this.context = new IdentityServerDbContext();
        }


        public List<RoleDto> Get()
        {
            List<RoleDto> listRoleDto = new List<RoleDto>();

            if (context != null)
            {
                List<Role> listRole
                = context.Roles.ToList();

                foreach (Role role in listRole)
                {
                    RoleDto dto = new RoleDto();

                    dto.Name = role.Name;
                    dto.RoleGroupId = role.RoleGroupId;

                    listRoleDto.Add(dto);
                }

                return listRoleDto;
            }

            throw new Exception();
        }


        public List<RoleDto> GetRolesByUserName(string userName)
        {
            List<RoleDto> listRoleDto = new List<RoleDto>();

            User userByUserName = context
                .Users                
                .Where(u => u.UserName.Equals(userName))
                .SingleOrDefault();

            if (context != null)
            {
                /*
                var rolesAll = 
                    context
                        .Roles
                        .Include(r => r.Users)
                        .ToList()
                        
                        ;
                */

                var rolesAll = context
                    .Roles
                    .Include(r => r.Users)
                    .ToList();
                    


                foreach(var role in rolesAll)
                {
                    var users = role.Users.ToList();
                    

                    foreach (var user in users)
                    {
                        if (user.UserName.Equals(userName))
                        {
                            RoleDto dto = new RoleDto();

                            dto.Id = role.Id;
                            dto.Name = role.Name;
                            dto.RoleGroupId = role.RoleGroupId;
                            dto.DisplayName = role.DisplayName;
                            dto.Controller = role.Controller;
                            dto.Action = role.Action;
                            dto.Icon = role.Icon;

                            listRoleDto.Add(dto);
                        }
                    }
                }


                return listRoleDto;
            }

            throw new Exception();
        }

        public List<string> GetRoleNamesByUserName(string userName)
        {
            List<string> listRoleNameDto = new List<string>();

            User userByUserName = context
                .Users
                .Where(u => u.UserName.Equals(userName))
                .SingleOrDefault();

            if (context != null)
            {
                /*
                var rolesAll = 
                    context
                        .Roles
                        .Include(r => r.Users)
                        .ToList()
                        
                        ;
                */

                var rolesAll = context
                    .Roles
                    .Include(r => r.Users)
                    .ToList();



                foreach (var role in rolesAll)
                {
                    var users = role.Users.ToList();


                    foreach (var user in users)
                    {
                        if (user.UserName.Equals(userName))
                        {
                            RoleDto dto = new RoleDto();
                            dto.Id = role.Id;
                            dto.Name = role.Name;
                            dto.RoleGroupId = role.RoleGroupId;

                            listRoleNameDto.Add(dto.Name);
                        }
                    }
                }

                return listRoleNameDto;
            }

            throw new Exception();
        }


        public void UpdateRoles(string userName, string statusData)
        {
            User user = context
                .Users
                .Where(u => u.UserName.Equals(userName))
                .Include("Roles")
                .SingleOrDefault();
            
            


            string[] splittedData = statusData.Split('|');

            List<Role> listRole = new List<Role>();

            for (int i = 0; i < splittedData.Length/2; i++)
            {
                var roleName = splittedData[i * 2];

                var role = context.Roles
                                .SingleOrDefault(r => 
                                    r.Name.Equals(roleName)
                                    );
                bool status = Convert.ToBoolean(splittedData[i * 2 + 1]);

                if (status)
                {
                    listRole.Add(role);
                }
            }

            user.Roles = listRole;

            context.SaveChanges();


        }
    }
}