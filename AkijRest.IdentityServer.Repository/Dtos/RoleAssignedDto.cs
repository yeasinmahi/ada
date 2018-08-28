namespace AkijRest.IdentityServer.Repository.Dtos
{
    public class RoleAssignedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAssigned { get; set; }
    }
}