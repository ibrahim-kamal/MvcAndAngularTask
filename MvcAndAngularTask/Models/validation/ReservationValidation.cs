using System.ComponentModel.DataAnnotations;

namespace MvcAndAngularTask.Models.validation
{
    public class ReservationValidation
    {
        [Required, MaxLength(255)]
        public string? Name { get; set; }
        [Required]
        public byte Gender { get; set; }
        [Required, MaxLength(25)]
        public string? NationalId { get; set; }
        [Required]
        public string? doctorId { get; set; }
        [Required]
        public TimeSpan? From { get; set; }
        [Required]
        public TimeSpan? To { get; set; }
        [Required]
        public String Date { get; set; }

    }
}
