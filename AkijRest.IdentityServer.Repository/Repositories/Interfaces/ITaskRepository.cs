using System.Collections.Generic;
using AkijRest.IdentityServer.Repository.Dtos;

namespace AkijRest.IdentityServer.Repository.Repositories.Interfaces
{
    interface ITaskRepository
    {
        List<TaskDto> Get();
        TaskDto Get(int id);
    }
}
