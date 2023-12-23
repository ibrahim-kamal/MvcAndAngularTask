using Microsoft.AspNetCore.Mvc;
using MvcAndAngularTask.DataAcessLayer;
using MvcAndAngularTask.Models;
using System.Data;
using System.Text.Json;

namespace MvcAndAngularTask.Controllers
{
    public class Doctors : ControllerBase
    {
        DoctorDAL doctor;
        public Doctors(DoctorDAL _doctor) { 
            doctor = _doctor;
        }    
        public String Index()
        {

            var doctors = doctor.getAll();
            return JsonSerializer.Serialize(doctors);
        }
    }
}
