namespace AkijRest.IdentityServer.Repository.Models.Hr
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblDay")]
    public partial class tblDay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int intDayOffId { get; set; }

        [StringLength(100)]
        public string strDayName { get; set; }

        [StringLength(750)]
        public string strMenuList { get; set; }

        [StringLength(750)]
        public string strMenuList0 { get; set; }
    }
}
