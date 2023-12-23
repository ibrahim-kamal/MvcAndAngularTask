using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace MvcAndAngularTask.Models.DTO
{
    [Keyless]
    public class ReservationcountByDoctorDto
    {
        
        public String name { get; set; }
        public int count { get; set; }
    }
}
