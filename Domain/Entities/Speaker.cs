using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Speaker
    {
        public int Id { get; set; }

        [StringLength(256)]
        public string? FirstName { get; set; }

        [StringLength(256)]
        public string? LastName { get; set; }

        [MaxLength(255)]
        [Column("Email")]
        public string? Email { get; set; } = string.Empty;

        [StringLength(256)]
        public string? Employer { get; set; }

        [Required]
        [Column("IsBlog")]
        public bool IsBlog { get; set; }

        [MaxLength(255)]
        [Column("BlogUrl")]
        public string? BlogUrl { get; set; }

        [StringLength(256)]
        public string? WebBrowserName { get; set; }
        public int? WebBrowserMajorVersion { get; set; }
        public int YearsOfExperience { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RegistrationFee {  get; set; }

        public ICollection<Certification> Certifications { get; set; } = new List<Certification>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
