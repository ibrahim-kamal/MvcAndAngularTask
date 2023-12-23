using Microsoft.AspNetCore.Mvc;
using MvcAndAngularTask.Models;

namespace MvcAndAngularTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        ApplicationContext _context;
        public WeatherForecastController(ApplicationContext context) { 
            _context = context;
        }
        [HttpGet]
        public String test()
        {

            Patient patient = _context.Patients.FirstOrDefault(p => p.NationalId == "1234567");
            return "";
        }
    }
}