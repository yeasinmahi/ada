using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class LeaveRepository: ILeaveRepository
    {
        private IdentityServerDbContext context;

        public LeaveRepository()
        {
            context = new IdentityServerDbContext();
        }


        public List<LeaveEveryDayDto> GetLeaveByUserName(string userName)
        {
            User user = context.Users.SingleOrDefault(u => u.UserName.Equals(userName));

            var listLeave = context
                .Leaves
                .ToList()
                .Where
                (
                    l => l.UserId == user.Id
                )
                .OrderByDescending(l => l.Date);

            List<LeaveEveryDayDto> listLeaveDto = new List<LeaveEveryDayDto>();

            foreach (var leave in listLeave)
            {
                LeaveEveryDayDto dto = new LeaveEveryDayDto();

                dto.LeaveTypeName = context.LeaveTypes.SingleOrDefault(l => l.Id == leave.LeaveTypeId).Name;
                dto.Date = leave.Date.ToString("dd/MM/yyyy");
                dto.LeaveCause = leave.LeaveCause;
                dto.LeaveAddress = leave.LeaveAddress;

                listLeaveDto.Add(dto);
            }

            return listLeaveDto;
        }

        public LeaveDto Create(LeaveDto leaveDto)
        {
            DateTime dateTime
                = Global.Datetime.ToDateTime(leaveDto.DateStart);

            DateTime dateTimeEnd
                = Global.Datetime.ToDateTime(leaveDto.DateEnd);


            for (;;)
            {
                var leave = new Leave
                {
                    LeaveTypeId = leaveDto.LeaveTypeId,

                    UserId = context
                            .Users
                            .SingleOrDefault
                            (
                                u =>
                                    u.UserName.Equals(leaveDto.UserName)
                            ).Id,

                    Date = dateTime ,

                    LeaveCause = leaveDto.LeaveCause,
                    LeaveAddress = leaveDto.LeaveAddress
                };

                context.Leaves.Add(leave);

                dateTime = dateTime.AddDays(1);

                if (dateTime > dateTimeEnd)
                {
                    break;
                }
            }
            
            context.SaveChanges();

            return (leaveDto);
        }
    }
}