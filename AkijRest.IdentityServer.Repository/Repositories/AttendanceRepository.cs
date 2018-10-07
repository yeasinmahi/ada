using System;
using System.Collections.Generic;
using System.Linq;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models.Hr;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly HrDbContext _context;
        public AttendanceRepository()
        {
            _context = new HrDbContext();
        }

        public List<AttendanceDto> Get()
        {
            List<AttendanceDto> attendanceDtos = new List<AttendanceDto>();
            if (_context != null)
            {
                List<tblEmployeeAttendance> employeeAttendances = _context.tblEmployeeAttendances.ToList();
                foreach (tblEmployeeAttendance attendance in employeeAttendances)
                {
                    AttendanceDto dto = GetDto(attendance);
                    attendanceDtos.Add(dto);
                }
            }
            return attendanceDtos;
        }

        public AttendanceDto Get(int id)
        {
            if (_context != null)
            {
                tblEmployeeAttendance employeeAttendance = _context.tblEmployeeAttendances.FirstOrDefault(x=>x.intAutoIncrement.Equals(id));
                AttendanceDto dto = GetDto(employeeAttendance);
                return dto;
            }
            return null;
        }

        public List<AttendanceDto> GetByEnroll(int enroll)
        {
            return GetDtos(x => x.intEmployeeID.Equals(enroll));
        }

        public List<AttendanceDto> GetByDate(DateTime date)
        {
            return GetDtos(x => x.dteAttendanceDate.Equals(date));
        }
        public List<AttendanceDto> GetByEnrollAndDateRange(int enroll, DateTime fromDate, DateTime toDate)
        {
            return GetDtos(x =>x.intEmployeeID==enroll && x.dteAttendanceDate>=fromDate && x.dteAttendanceDate<=toDate );
        }
        private List<AttendanceDto> GetDtos(System.Linq.Expressions.Expression<Func<tblEmployeeAttendance, bool>> predicate)
        {
            List<AttendanceDto> attendanceDtos = new List<AttendanceDto>();
            if (_context != null)
            {
                List<tblEmployeeAttendance> employeeAttendances = _context.tblEmployeeAttendances.Where(predicate).ToList();
                foreach (tblEmployeeAttendance attendance in employeeAttendances)
                {
                    AttendanceDto dto = GetDto(attendance);
                    attendanceDtos.Add(dto);
                }
            }
            return attendanceDtos;
        }
        private AttendanceDto GetDto(tblEmployeeAttendance attendance)
        {
            AttendanceDto dto = new AttendanceDto();

            dto.Id = attendance.intAutoIncrement;
            dto.EmployeeId = attendance.intEmployeeID;
            dto.JobStationId = attendance.intJobStationID;
            dto.AttendanceDate = attendance.dteAttendanceDate;
            dto.AttendanceTime = attendance.dteAttendanceTime;
            dto.UserId = attendance.intUserID;
            dto.IsProcess = attendance.ysnProcess;
            dto.Remark = attendance.strRemark;
            dto.IsLate = attendance.ysnLate;
            dto.LateTime = attendance.timeHoursLate;
            dto.IsEaryLeave = attendance.ysnEaryLeave;
            dto.EarlyLeaveTime = attendance.timeHoursEarlyLeave;
            dto.Mrsl = attendance.intMRSL;

            return dto;
        }
    }
}