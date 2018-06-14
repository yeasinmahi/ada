using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class LeaveTypeRepository: ILeaveTypeRepository
    {
        private IdentityServerDbContext context;
        public LeaveTypeRepository()
        {
            this.context = new IdentityServerDbContext();
        }

        public List<LeaveTypeDto> Get()
        {
            List<LeaveTypeDto> listLeaveTypeDto = new List<LeaveTypeDto>();

            if (context != null)
            {
                var leaveTypes = context.LeaveTypes.ToList();

                foreach (var leaveType in leaveTypes)
                {
                    LeaveTypeDto dto = new LeaveTypeDto();

                    dto.Name = leaveType.Name;
                    dto.Id = leaveType.Id;

                    listLeaveTypeDto.Add(dto);
                }

                return listLeaveTypeDto;
            }

            throw new Exception();
        }

    }
}