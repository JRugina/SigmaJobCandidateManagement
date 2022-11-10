using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobCandidateManagement.DataAccess.Entities
{
    public class JobCandidate
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email Format")]
        public string EmailAddress { get; set; } = string.Empty;

        public TimeSpan TimeInterval { get; set; }
        public string LinkedInUrl { get; set; } = string.Empty;
        public string GitHubUrl { get; set; } = string.Empty;

        [Required]
        public string Comments { get; set; } = string.Empty;
    }
}
