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
                var leaves = _context.Leaves.Include(x => x.LeaveType).Include(x => x.User).ToList();

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
                Leave leave = _context.Leaves.Include(x => x.LeaveType).Include(x => x.User).FirstOrDefault(x => x.Id.Equals(id));
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
                var leaves = _context.Leaves.Include(x => x.LeaveType).Include(x => x.User).ToList().Where(x=>x.UserId.Equals(userId));

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

            var listLeave = _context.Leaves.Include(x => x.LeaveType).Include(x => x.User).ToList().Where(l => user != null && l.UserId == user.Id).OrderByDescending(l => l.Date);

            List<LeaveDto> dtos = new List<LeaveDto>();

            foreach (var leave in listLeave)
            {
                LeaveDto dto = new LeaveDto();
                dto.Id = leave.Id;
                dto.UserName = leave.User.UserName;
                dto.UserId = leave.UserId;
                dto.LeaveTypeName = leave.LeaveType.Name;
                dto.LeaveTypeId = leave.LeaveTypeId;
                dto.DateStart = Global.Datetime.ToString(leave.Date);
                dto.LeaveCause = leave.LeaveCause;
                dto.LeaveAddress = leave.LeaveAddress;

                dtos.Add(dto);
            }

            return dtos;
        }
        public int Create(LeaveDto leaveDto)
        {
            DateTime dateTimeFrom
                = Global.Datetime.ToDateTime(leaveDto.DateStart);

            DateTime dateTimeTo
                = Global.Datetime.ToDateTime(leaveDto.DateEnd);
            
                for (; ; )
                {
                    if (_context != null)
                    {
                        if (!Create(leaveDto, dateTimeFrom))
                        {
                            return 0;
                        }
                    }

                    dateTimeFrom = dateTimeFrom.AddDays(1);

                    if (dateTimeFrom > dateTimeTo)
                    {
                        break;
                    }
                }
            if (_context != null)
            {
                int result = _context.SaveChanges();
                return result;
            }
            return 0;
        }
        private bool Create(LeaveDto leaveDto,DateTime date)
        {
            try
            {
                Leave leave = new Leave();
                leave.LeaveTypeId = leaveDto.LeaveTypeId;
                leave.UserId = leaveDto.UserId > 0 ? leaveDto.UserId : _context.Users.SingleOrDefault(u => u.UserName.Equals(leaveDto.UserName)).Id;

                leave.Date = date;
                leave.LeaveCause = leaveDto.LeaveCause;
                leave.LeaveAddress = leaveDto.LeaveAddress;

                _context.Leaves.Add(leave);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        public int Update(LeaveDto leaveDto)
        {
            Leave leave = new Leave();
            leave.Id = leaveDto.Id;
            leave.Date = Global.Datetime.ToDateTime(leaveDto.DateStart);
            leave.LeaveAddress = leaveDto.LeaveAddress;
            leave.LeaveCause = leaveDto.LeaveCause;
            leave.LeaveTypeId = leaveDto.LeaveTypeId;
            leave.UserId = leaveDto.UserId > 0 ? leaveDto.UserId : _context.Users.SingleOrDefault(u => u.UserName.Equals(leaveDto.UserName)).Id;

            _context.SafeAttach(leave, x => x.Id);
            _context.Entry(leave).State = EntityState.Modified;
            _context.SaveChanges();

            return leave.Id;
        }
        public int Delete(int id)
        {
            Leave leave = new Leave() { Id = id };
            //_context.LeaveTypes.Attach(leaveType);
            _context.SafeAttach(leave, x => x.Id);
            _context.Leaves.Remove(leave);
            return _context.SaveChanges();
        }
    }
}