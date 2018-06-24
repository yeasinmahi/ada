using AkijRest.IdentityServer.Repository.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;

namespace AkijRest.IdentityServer.Api.Helpers.Roles
{
    public class Helper
    {
        public static List<RoleAssignedDto> CombineWithAllRoles(List<RoleDto> listRoleUser)
        {
            using (IdentityServerDbContext context = new IdentityServerDbContext())
            {

                var roleAll = context.Roles.ToList();

                List<RoleAssignedDto> listRoleAssignedDto = new List<RoleAssignedDto>();


                foreach (var role in roleAll)
                {
                    bool flag = false;

                    foreach ( var roleUser in listRoleUser)
                    {
                        if (role.Name.Equals(roleUser.Name))
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (flag)
                    {
                        RoleAssignedDto dto = new RoleAssignedDto
                        {
                            Id = role.Id,
                            Name = role.Name,
                            IsAssigned = true
                        };

                        listRoleAssignedDto.Add(dto);
                    }
                    else
                    {
                        RoleAssignedDto dto = new RoleAssignedDto
                        {
                            Id = role.Id,
                            Name = role.Name,
                            IsAssigned = false
                        };

                        listRoleAssignedDto.Add(dto);
                    }

                }

                return listRoleAssignedDto;

            }
        }
    }
}