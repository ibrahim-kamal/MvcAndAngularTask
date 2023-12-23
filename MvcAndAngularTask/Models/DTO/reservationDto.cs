using Humanizer;

namespace MvcAndAngularTask.Models.DTO
{
    public class reservationDto
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? From { get; set; }
        public TimeSpan? To { get; set; }
        public string doctor { get; set; }
        public string patient { get; set; }
    }
}
