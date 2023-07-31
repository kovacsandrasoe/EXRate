using System.ComponentModel.DataAnnotations;

namespace EXRate.Backend.ViewModels
{
    public class RateRecordCreateModel
    {
        [StringLength(10)]
        [Required]
        public string Currency { get; set; }

        [Required]
        public double Value { get; set; }

        [StringLength(100)]
        public string? Comment { get; set; }
    }
}
