using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class LeaveRepository: ILeaveRepository
    {
        private readonly IdentityServerDbContext _context;

        public LeaveRepository()
        {
            _context = new IdentityServerDbContext();
        }

        public List<LeaveDto> Get()
        {
            List<LeaveDto> leaveDtos = new List<LeaveDto>();

            if (_context != null)
            {
                var leaves = _context.Leaves.ToList();

                foreach (var leave in leaves)
                {
                    LeaveDto dto = new LeaveDto
                    {
                        Id = leave.Id,
                        DateStart = Global.Datetime.ToString(leave.Date),
                        LeaveTypeName = leave.LeaveType.Name,
                        LeaveCause = leave.LeaveCause,
                        LeaveAddress = leave.LeaveAddress,
                        LeaveTypeId = leave.LeaveTypeId,
                        UserName = leave.User.UserName,
                        UserId = leave.UserId
                    };
                    leaveDtos.Add(dto);
                }

                return leaveDtos;
            }

            throw new Exception();
        }
        public LeaveDto Get(int id)
        {
            if (_context != null)
            {
                Leave leave = _context.Leaves.FirstOrDefault(x => x.Id.Equals(id));
                LeaveDto dto = null;
                if (leave != null)
                {
                    dto = new LeaveDto
                    {
                        Id = leave.Id,
                        DateStart = Global.Datetime.ToString(leave.Date),
                        LeaveTypeName = leave.LeaveType.Name,
                        LeaveCause = leave.LeaveCause,
                        LeaveAddress = leave.LeaveAddress,
                        LeaveTypeId = leave.LeaveTypeId,
                        UserName = leave.User.UserName,
                        UserId = leave.UserId
                    };

                }
                return dto;
            }
            throw new Exception();
        }
        public List<LeaveDto> GetLeaveByUserId(int userId)
        {
            List<LeaveDto> leaveDtos = new List<LeaveDto>();

            if (_context != null)
            {
                var leaves = _context.Leaves.ToList().Where(x=>x.UserId.Equals(userId));

                foreach (var leave in leaves)
                {
                    LeaveDto dto = new LeaveDto
                    {
                        Id = leave.Id,
                        DateStart = Global.Datetime.ToString(leave.Date),
                        LeaveTypeName = leave.LeaveType.Name,
                        LeaveCause = leave.LeaveCause,
                        LeaveAddress = leave.LeaveAddress,
                        LeaveTypeId = leave.LeaveTypeId,
                        UserName = leave.User.UserName,
                        UserId = leave.UserId
                    };
                    leaveDtos.Add(dto);
                }

                return leaveDtos;
            }
            throw new Exception();
        }
        public List<LeaveDto> GetLeaveByUserName(string userName)
        {
            User user = _context.Users.SingleOrDefault(u => u.UserName.Equals(userName));

            var listLeave = _context
                .Leaves
                .ToList()
                .Where
                (l => user != null && l.UserId == user.Id)
                .OrderByDescending(l => l.Date);

            List<LeaveDto> dtos = new List<LeaveDto>();

            foreach (var leave in listLeave)
            {
                LeaveDto dto = new LeaveDto();
                dto.Id = leave.Id;
                dto.UserName = leave.User.UserName;
                dto.LeaveTypeName = _context.LeaveTypes.SingleOrDefault(l => l.Id == leave.LeaveTypeId)?.Name;
                dto.DateStart = Global.Datetime.ToString(leave.Date);
                dto.LeaveCause = leave.LeaveCause;
                dto.LeaveAddress = leave.LeaveAddress;

                dtos.Add(dto);
            }

            return dtos;
        }
        public LeaveDto Create(LeaveDto leaveDto)
        {
            DateTime dateTimeFrom
                = Global.Datetime.ToDateTime(leaveDto.DateStart);

            DateTime dateTimeTo
                = Global.Datetime.ToDateTime(leaveDto.DateEnd);
            
                for (; ; )
                {
                    if (_context != null)
                    {
                        var leave = new Leave
                        {
                            LeaveTypeId = leaveDto.LeaveTypeId,

                            UserId = _context
                                .Users
                                .SingleOrDefault
                                (
                                    u =>u.UserName.Equals(leaveDto.UserName)
                                ).Id,

                            Date = dateTimeFrom,

                            LeaveCause = leaveDto.LeaveCause,
                            LeaveAddress = leaveDto.LeaveAddress
                        };

                        _context.Leaves.Add(leave);
                    }

                    dateTimeFrom = dateTimeFrom.AddDays(1);

                    if (dateTimeFrom > dateTimeTo)
                    {
                        break;
                    }
                }
            _context?.SaveChanges();
            return (leaveDto);
        }
        public int Update(LeaveDto leaveDto)
        {
            Leave leave = new Leave
            {
                Id = leaveDto.Id,
                Date = Global.Datetime.ToDateTime(leaveDto.DateStart),
                LeaveAddress = leaveDto.LeaveAddress,
                LeaveCause = leaveDto.LeaveCause,
                LeaveTypeId = leaveDto.LeaveTypeId,
                UserId = _context
                    .Users
                    .SingleOrDefault
                    (
                        u => u.UserName.Equals(leaveDto.UserName)
                    ).Id

            };
            _context.Leaves.Attach(leave);
            _context.Entry(leave).State = EntityState.Modified;
            _context.SaveChanges();

            return leave.Id;
        }
    }
}