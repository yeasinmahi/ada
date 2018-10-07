using System.Collections.Generic;
using AkijRest.IdentityServer.Repository.Dtos;

namespace AkijRest.IdentityServer.Repository.Repositories.Interfaces
{
    interface IMealRepository
    {
        List<MealDto> Get();
        MealDto Get(int id);
    }
}
