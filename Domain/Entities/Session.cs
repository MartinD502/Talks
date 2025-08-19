using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }

        [ForeignKey("SpeakerId")]
        public Speaker? Speaker { get; set; } = null!;

        [StringLength(256)]
        public string? Title { get; set; }

        [StringLength(256)]
        public string? Description { get; set; }
        public bool IsApproved {  get; set; }
    }
}
