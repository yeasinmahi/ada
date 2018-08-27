using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkijRest.IdentityServer.Repository.Models.Global
{
    [Table("tblTaskRecord")]
    public partial class tblTaskRecord
    {
        [Key]
        public int intID { get; set; }

        public int intEnroll { get; set; }

        [Column(TypeName = "date")]
        public DateTime dteDate { get; set; }

        [Required]
        [StringLength(500)]
        public string strKeyPoint { get; set; }

        [Required]
        [StringLength(500)]
        public string strStatus { get; set; }

        [Required]
        [StringLength(500)]
        public string strRemarks { get; set; }

        public DateTime dteStartTime { get; set; }

        public DateTime dteEndTime { get; set; }

        public DateTime dteDeadLine { get; set; }

        public int intComplete { get; set; }

        [Required]
        [StringLength(50)]
        public string strType { get; set; }

        public DateTime? dteEmailSend { get; set; }
    }
}
