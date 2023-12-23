using MvcAndAngularTask.Models;

namespace MvcAndAngularTask.DataAcessLayer
{
    public class ScheduleDAL
    {

        ApplicationContext _context;
        public ScheduleDAL(ApplicationContext context)
        {
            _context = context;
        }


        public Schedule getSchedule(String day, int doctorId)
        {
            return _context.Schedules.Where(s => s.DoctorId == doctorId && s.Day == day).FirstOrDefault();
        }
    }
}
