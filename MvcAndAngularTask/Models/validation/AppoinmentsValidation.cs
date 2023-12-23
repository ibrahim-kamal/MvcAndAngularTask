using System.ComponentModel.DataAnnotations;

namespace MvcAndAngularTask.Models.validation
{
    public class AppoinmentsValidation
    {
        [Required]
        public string DoctorId { get; set; }
        [Required]
        public String date { get; set; }




    }
}
