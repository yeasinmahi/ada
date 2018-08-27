using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkijRest.IdentityServer.Repository.Models.Hr
{
    [Table("tblEmployeeAttendance")]
    public partial class tblEmployeeAttendance
    {
        [Key]
        public int intAutoIncrement { get; set; }

        [StringLength(100)]
        public string strEmployeeBarcode { get; set; }

        public int? intEmployeeID { get; set; }

        public int? intJobStationID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dteAttendanceDate { get; set; }

        public TimeSpan? dteAttendanceTime { get; set; }

        public int? intUserID { get; set; }

        [StringLength(100)]
        public string strUserIP { get; set; }

        public bool? ysnProcess { get; set; }

        [StringLength(100)]
        public string strRemark { get; set; }

        public bool? ysnLate { get; set; }

        public TimeSpan? timeHoursLate { get; set; }

        public bool? ysnEaryLeave { get; set; }

        public TimeSpan? timeHoursEarlyLeave { get; set; }

        public long? intMRSL { get; set; }
    }
}
