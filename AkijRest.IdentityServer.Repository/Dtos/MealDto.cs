using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Dtos
{
    public class MealDto
    {
        public int Id { get; set; }
        public string DayName { get; set; }
        public string MenuList { get; set; }
        public string AltMenuList { get; set; }
    }
}