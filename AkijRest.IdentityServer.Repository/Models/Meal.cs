using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Models
{
    public class Meal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "{0} can have a max of {1} characters"), MinLength(2, ErrorMessage = "{0} can have a min of {2} characters")]
        public string DayName { get; set; }
        public string MenuList { get; set; }
        public string AltMenuList { get; set; }
    }
}