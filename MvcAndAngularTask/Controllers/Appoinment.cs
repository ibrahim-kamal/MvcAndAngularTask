using Microsoft.AspNetCore.Mvc;
using MvcAndAngularTask.DataAcessLayer;
using MvcAndAngularTask.Models;
using MvcAndAngularTask.Models.validation;
using System.Text.Json;

namespace MvcAndAngularTask.Controllers
{
    public class Appoinment : ControllerBase
    {

        AppoinmentsDAL _appoinment;
        public Appoinment(AppoinmentsDAL appoinment)
        {
            _appoinment = appoinment;
        }
        [HttpPost]
        public String AvaliableAppoinment(AppoinmentsValidation appoinment)
        {

            
            return JsonSerializer.Serialize(_appoinment.getFreeAppointment(appoinment));
        }
    }
}
