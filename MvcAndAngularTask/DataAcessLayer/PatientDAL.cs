using MvcAndAngularTask.Models;

namespace MvcAndAngularTask.DataAcessLayer
{
    public class PatientDAL
    {
        ApplicationContext _context;
        public PatientDAL(ApplicationContext context)
        {
            _context = context;
        }

        public int get(String nationalId) {
            Patient patient = _context.Patients.FirstOrDefault(p => p.NationalId == nationalId);
            if (patient != null)
            {
                return patient.Id;
            }
            return 0;
        }

        public int insert(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return patient.Id;
        }

    }
}
