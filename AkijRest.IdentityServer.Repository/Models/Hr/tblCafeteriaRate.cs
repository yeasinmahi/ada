namespace AkijRest.IdentityServer.Repository.Models.Hr
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCafeteriaRate")]
    public partial class tblCafeteriaRate
    {
        [Key]
        public int intRow { get; set; }

        [StringLength(250)]
        public string strGroup { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dteFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dteTo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? monAmount { get; set; }

        [StringLength(250)]
        public string strNarration { get; set; }

        public int? intActionBy { get; set; }

        public DateTime? dteAction { get; set; }
    }
}
