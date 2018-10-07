using System;

namespace AkijRest.IdentityServer.Repository.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public int Enroll { get; set; }
        public DateTime Date { get; set; }
        public string KeyPoint { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DeadLine { get; set; }
        public int CompletionPercentage { get; set; }
        public string Type { get; set; }
        public DateTime? EmailSendTime { get; set; }
    }
}