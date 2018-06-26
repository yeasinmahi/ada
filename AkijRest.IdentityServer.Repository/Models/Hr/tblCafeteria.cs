namespace AkijRest.IdentityServer.Repository.Models.Hr
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCafeteria")]
    public partial class tblCafeteria
    {
        [Key]
        public int intRow { get; set; }

        public int? intEnroll { get; set; }

        public int? intGroup { get; set; }

        public int? intType { get; set; }

        public int? intMealOption { get; set; }

        [StringLength(250)]
        public string strNarration { get; set; }

        public int? intActionBy { get; set; }

        public DateTime? dteAction { get; set; }
    }
}
