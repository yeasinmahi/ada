using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkijRest.IdentityServer.Repository.Models
{
    public class LeaveType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "{0} can have a max of {1} characters"),MinLength(3, ErrorMessage = "{0} can have a min of {2} characters")]
        [Column("LeaveTypeName")]
        public string Name { get; set; }
        [DefaultValue('B')]
        public char ApplicationFor { get; set; }
        [DefaultValue(0)]
        [Range(0, 500)]
        public int CompanyPolicy { get; set; }
        [DefaultValue(0)]
        [Range(0, 500)]
        public int MaximumAllowedAtATime { get; set; }
        [DefaultValue(false)]
        public bool IsHalfDayAllowed { get; set; }
        [DefaultValue(true)]
        public bool IsBalanceChecked { get; set; }
        [DefaultValue(false)]
        public bool IsOnlyOneTime { get; set; }
        [DefaultValue(0)]
        [Range(0, 500)]
        public int MaxApplicationAtAMonth { get; set; }
        [DefaultValue(false)]
        public bool IsRestricted { get; set; }

    }
}