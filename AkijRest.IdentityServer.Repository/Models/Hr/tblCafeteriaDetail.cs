namespace AkijRest.IdentityServer.Repository.Models.Hr
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblCafeteriaDetail
    {
        [Key]
        public int intRow { get; set; }

        public int? intEnroll { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dteMeal { get; set; }

        public int? intMealFor { get; set; }

        public int? intCountMeal { get; set; }

        public int? intSpendMeal { get; set; }

        public bool? isOwnGuest { get; set; }

        public bool? isPayable { get; set; }

        [StringLength(250)]
        public string strNarration { get; set; }

        public int? intActionBy { get; set; }

        public DateTime? dteAction { get; set; }

        public bool? ysnMenualEntry { get; set; }
    }
}
