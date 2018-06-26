using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkijRest.IdentityServer.Repository.Dtos;

namespace AkijRest.IdentityServer.Repository.Repositories.Interfaces
{
    interface IMealRepository
    {
        List<MealDto> Get();
        MealDto Get(int id);
    }
}
