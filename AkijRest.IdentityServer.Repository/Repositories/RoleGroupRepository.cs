using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class RoleGroupRepository : IRoleGroupRepository
    {
        private IdentityServerDbContext context;

        public RoleGroupRepository()
        {
            this.context = new IdentityServerDbContext();
        }
        public List<RoleGroupDto> Get()
        {
            List<RoleGroupDto> roleGroupDtos = new List<RoleGroupDto>();

            if (context != null)
            {
                List<RoleGroup> roleGroups
                    = context.RoleGroups.ToList();

                foreach (RoleGroup roleGroup in roleGroups)
                {
                    RoleGroupDto dto = new RoleGroupDto();

                    dto.Name = roleGroup.Name;
                    dto.Id = roleGroup.Id;
                    dto.Icon = roleGroup.Icon;

                    roleGroupDtos.Add(dto);
                }

                return roleGroupDtos;
            }

            throw new Exception();
        }
        
    }
}