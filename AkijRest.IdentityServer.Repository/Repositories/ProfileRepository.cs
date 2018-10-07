using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IdentityServerDbContext _context;

        public ProfileRepository()
        {
            _context = new IdentityServerDbContext();
        }

        public ProfileDto GetByUserName(string userName)
        {
            if (_context != null)
            {
                User user = _context.Users.Include(x=>x.ExternalLoginEmail).FirstOrDefault(x => x.UserName.Equals(userName));
                ProfileDto dto = null;
                if (user != null)
                {
                    dto = GetDto(user);
                }
                return dto;
            }
            throw new Exception();
        }
        public int Update(ProfileDto dto)
        {
            User user = _context.Users.Include(x=>x.ExternalLoginEmail).FirstOrDefault(u => u.Email.Equals(dto.Email));
            if (user!=null)
            {
                user.UserName = dto.UserName;
                user.FullName = dto.FullName;
                user.Email = dto.Email;
                user.Designation = dto.Designation;
                user.Education = dto.Education;
                user.CurrentAddress = dto.CurrentAddress;
                user.ParmanentAddress = dto.ParmanentAddress;
                user.DateOfJoining = dto.DateOfJoining;
                user.Note = dto.Note;
                //user.ExternalLoginEmail.Facebook = dto.FacebookAddress;
                //user.ExternalLoginEmail.Google = dto.GoogleAddress;
                user.Approved = user.Approved;

                _context.SafeAttach(user, x => x.Id);
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();

                return user.Id;
            }
            return 0;
        }
        //public ProfileDto Get(string userName)
        //{
        //    User user = _context.Users.SingleOrDefault(u => u.UserName.Equals(userName));

        //    var listLeave = _context.Leaves.Include(x => x.LeaveType).Include(x => x.User).ToList().Where(l => user != null && l.UserId == user.Id).OrderByDescending(l => l.Date);

        //    List<LeaveDto> dtos = new List<LeaveDto>();

        //    foreach (var leave in listLeave)
        //    {
        //        LeaveDto dto = new LeaveDto();
        //        dto.Id = leave.Id;
        //        dto.UserName = leave.User.UserName;
        //        dto.UserId = leave.UserId;
        //        dto.LeaveTypeName = leave.LeaveType.Name;
        //        dto.LeaveTypeId = leave.LeaveTypeId;
        //        dto.DateStart = Global.Datetime.ToString(leave.Date);
        //        dto.LeaveCause = leave.LeaveCause;
        //        dto.LeaveAddress = leave.LeaveAddress;

        //        dtos.Add(dto);
        //    }

        //    return dtos;
        //}
        private ProfileDto GetDto(User user)
        {
            ProfileDto dto = new ProfileDto();
            dto.UserId = user.Id;
            dto.UserName = user.UserName;
            dto.FullName = user.FullName;
            dto.Email = user.Email;
            dto.Designation = user.Designation;
            dto.Education = user.Education;
            dto.CurrentAddress = user.CurrentAddress;
            dto.ParmanentAddress = user.ParmanentAddress;
            dto.DateOfJoining = user.DateOfJoining;
            dto.Note = user.Note;
            dto.FacebookAddress = user.ExternalLoginEmail.Facebook;
            dto.GoogleAddress = user.ExternalLoginEmail.Google;
            dto.Approved = user.Approved;
            return dto;
        }
    }
}