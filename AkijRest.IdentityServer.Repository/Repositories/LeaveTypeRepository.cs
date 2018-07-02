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
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly IdentityServerDbContext _context;
        public LeaveTypeRepository()
        {
            this._context = new IdentityServerDbContext();
        }

        public List<LeaveTypeDto> Get()
        {
            List<LeaveTypeDto> listLeaveTypeDto = new List<LeaveTypeDto>();

            if (_context != null)
            {
                var leaveTypes = _context.LeaveTypes.ToList();

                foreach (var leaveType in leaveTypes)
                {
                    LeaveTypeDto dto = new LeaveTypeDto
                    {
                        Id = leaveType.Id,
                        Name = leaveType.Name,
                        ApplicationFor = leaveType.ApplicationFor,
                        CompanyPolicy = leaveType.CompanyPolicy,
                        IsBalanceChecked = leaveType.IsBalanceChecked,
                        IsHalfDayAllowed = leaveType.IsHalfDayAllowed,
                        IsOnlyOneTime = leaveType.IsOnlyOneTime,
                        IsRestricted = leaveType.IsRestricted,
                        MaxApplicationAtAMonth = leaveType.MaxApplicationAtAMonth,
                        MaximumAllowedAtATime = leaveType.MaximumAllowedAtATime



                    };


                    listLeaveTypeDto.Add(dto);
                }

                return listLeaveTypeDto;
            }

            throw new Exception();
        }
        public LeaveTypeDto Get(int id)
        {
            if (_context != null)
            {
                LeaveType leaveType = _context.LeaveTypes.FirstOrDefault(x=>x.Id.Equals(id));
                LeaveTypeDto dto = null;
                if (leaveType != null)
                {
                    dto = new LeaveTypeDto
                    {
                        Id = leaveType.Id,
                        Name = leaveType.Name,
                        ApplicationFor = leaveType.ApplicationFor,
                        CompanyPolicy = leaveType.CompanyPolicy,
                        IsBalanceChecked = leaveType.IsBalanceChecked,
                        IsHalfDayAllowed = leaveType.IsHalfDayAllowed,
                        IsOnlyOneTime = leaveType.IsOnlyOneTime,
                        IsRestricted = leaveType.IsRestricted,
                        MaxApplicationAtAMonth = leaveType.MaxApplicationAtAMonth,
                        MaximumAllowedAtATime = leaveType.MaximumAllowedAtATime
                    };

                }
                return dto;
            }
            throw new Exception();
        }
        public int Create(LeaveTypeDto leaveTypeDto)
        {
            LeaveType leaveType = new LeaveType
            {
                ApplicationFor = leaveTypeDto.ApplicationFor,
                CompanyPolicy = leaveTypeDto.CompanyPolicy,
                IsBalanceChecked = leaveTypeDto.IsBalanceChecked,
                IsHalfDayAllowed = leaveTypeDto.IsHalfDayAllowed,
                IsOnlyOneTime = leaveTypeDto.IsOnlyOneTime,
                IsRestricted = leaveTypeDto.IsRestricted,
                MaxApplicationAtAMonth = leaveTypeDto.MaxApplicationAtAMonth,
                MaximumAllowedAtATime = leaveTypeDto.MaximumAllowedAtATime,
                Name = leaveTypeDto.Name

            };
            _context.LeaveTypes.Add(leaveType);
            _context.SaveChanges();

            return (leaveType.Id);
        }
        

    }
}