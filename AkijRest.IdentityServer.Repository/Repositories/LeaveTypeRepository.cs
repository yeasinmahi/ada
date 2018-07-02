using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AkijRest.IdentityServer.Repository.Models;

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
        //public LeaveDto Create(LeaveDto leaveDto)
        //{
        //    DateTime dateTime
        //        = Global.Datetime.ToDateTime(leaveDto.DateStart);

        //    DateTime dateTimeEnd
        //        = Global.Datetime.ToDateTime(leaveDto.DateEnd);


        //    for (; ; )
        //    {
        //        var leave = new Leave
        //        {
        //            LeaveTypeId = leaveDto.LeaveTypeId,

        //            UserId = context
        //                .Users
        //                .SingleOrDefault
        //                (
        //                    u =>
        //                        u.UserName.Equals(leaveDto.UserName)
        //                ).Id,

        //            Date = dateTime,

        //            LeaveCause = leaveDto.LeaveCause,
        //            LeaveAddress = leaveDto.LeaveAddress
        //        };

        //        context.Leaves.Add(leave);

        //        dateTime = dateTime.AddDays(1);

        //        if (dateTime > dateTimeEnd)
        //        {
        //            break;
        //        }
        //    }

        //    context.SaveChanges();

        //    return (leaveDto);
        //}

    }
}