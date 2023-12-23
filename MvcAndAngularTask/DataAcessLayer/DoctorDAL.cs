using MvcAndAngularTask.Models;

namespace MvcAndAngularTask.DataAcessLayer
{
    public class DoctorDAL
    {
        ApplicationContext _context;
        public DoctorDAL(ApplicationContext context)
        {
            _context = context;
        }

        public List<Doctor> getAll()
        {
            return _context.Doctors.ToList();
        }


        public Doctor get(String DoctorId)
        {
            return _context.Doctors.Where(e => e.DoctorId == DoctorId).FirstOrDefault();
        }
    }
}
