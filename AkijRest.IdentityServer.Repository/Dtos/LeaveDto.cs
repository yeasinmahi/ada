namespace AkijRest.IdentityServer.Repository.Dtos
{
    public class LeaveDto
    {        
        public int Id { get; set; }
        public byte LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }

        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string LeaveCause { get; set; }
        public string LeaveAddress { get; set; }
    }
}