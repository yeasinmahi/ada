using System;
using System.Collections.Generic;
using AkijRest.IdentityServer.Repository.Dtos;

namespace AkijRest.IdentityServer.Repository.Repositories.Interfaces
{
    interface IAttendanceRepository
    {
        List<AttendanceDto> Get();
        AttendanceDto Get(int id);
        List<AttendanceDto> GetByEnroll(int enroll);
        List<AttendanceDto> GetByDate(DateTime date);
    }
}
