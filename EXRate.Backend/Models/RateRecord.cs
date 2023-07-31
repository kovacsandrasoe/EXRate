using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXRate.Backend.Models
{
    public class RateRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(10)]
        [Required]
        public string Currency { get; set; }

        [Required]
        public double Value { get; set; }

        [StringLength(100)]
        public string? Comment { get; set; }

        [Required]
        public DateTime TimeAdded { get; set; }

        [Required]
        public string Creator { get; set; }

        public RateRecord()
        {
            TimeAdded = DateTime.Now;
        }
    }
}
