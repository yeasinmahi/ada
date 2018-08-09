namespace AkijRest.IdentityServer.Repository.Dtos
{
    public class LeaveTypeDto
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string ApplicableFor { get; set; }
        public int CompanyPolicy { get; set; }
        public int MaximumAllowedAtATime { get; set; }
        public bool IsHalfDayAllowed { get; set; }
        public bool IsBalanceChecked { get; set; }
        public bool IsOnlyOneTime { get; set; }
        public int MaxApplicationAtAMonth { get; set; }
        public bool IsRestricted { get; set; }
    }
}