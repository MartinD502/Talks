using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Certification
    {
        public int Id { get; set; }

        [ForeignKey("SpeakerId")]
        public Speaker? Speaker { get; set; } = null!;

        [StringLength(256)]
        public required string Name { get; set; } = string.Empty;
    }
}
