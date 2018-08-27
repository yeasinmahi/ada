using System;

namespace AkijRest.IdentityServer.Repository.Dtos
{
    public class AttendanceDto
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        public int? JobStationId { get; set; }
        public DateTime? AttendanceDate { get; set; }

        public TimeSpan? AttendanceTime { get; set; }

        public int? UserId { get; set; }
        public string UserIp { get; set; }

        public bool? IsProcess { get; set; }
        public string Remark { get; set; }

        public bool? IsLate { get; set; }

        public TimeSpan? LateTime { get; set; }

        public bool? IsEaryLeave { get; set; }

        public TimeSpan? EarlyLeaveTime { get; set; }

        public long? Mrsl { get; set; }
    }
}