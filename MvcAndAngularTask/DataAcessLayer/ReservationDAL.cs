using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MvcAndAngularTask.Models;
using MvcAndAngularTask.Models.DTO;

namespace MvcAndAngularTask.DataAcessLayer
{
    public class ReservationDAL
    {
        ApplicationContext _context;
        public ReservationDAL(ApplicationContext context)
        {
            _context = context;
        }

        public int insert(Reservation reservation)
        {

            try { 
                _context.Reservations.Add(reservation);
                _context.SaveChanges();
            }
            catch (Exception ex) {
                return 0;
            }
            return reservation.Id;
        }

        public List<TimeSpan> getReservationTimes(DateTime date, int doctorId)
        {
            List<TimeSpan> times = new List<TimeSpan>();
            var reservations = _context.Reservations.Where(r => r.DoctorId == doctorId && r.Date >= date && r.Date < date.AddDays(1));
            foreach (var reservation in reservations)
            {

                times.Add(reservation.From.Value);
            }
            return times;

        }

        public IList<reservationDto> GetReservations() {
            //return _context.Reservations.ToList();
            IList<reservationDto> reservationDto =  _context.Reservations.Join(
                _context.Doctors,
                r => r.DoctorId,
                d => d.Id,
                (res , doc) => new {
                    Id = res.Id ,
                    PatientId = res.PatientId , 
                    Date = res.Date , 
                    From = res.From , 
                    To = res.To , 
                    doctor = doc.Name
                }
            )
            .Where(res => res.Date > DateTime.Now.AddDays(-1))
            .Join(
                _context.Patients,
                r => r.PatientId,
                P => P.Id,
                (res, patient) => new reservationDto
                {
                    Id = res.Id,
                    PatientId = res.PatientId,
                    Date = res.Date,
                    From = res.From,
                    To = res.To,
                    doctor = res.doctor,
                    patient = patient.Name
                }
            )
            .OrderBy(res => res.Date)
            .ThenBy(res => res.from)
            .ToList();
            return reservationDto;
            //return _context.Reservations.Include(r => r.Doctor).Include(r => r.Patient).ToList();
        }
        
        public IList<reservationDto> GetReservations(string DoctorId ,DateTime fromDate , DateTime toDate) {
            //return _context.Reservations.ToList();
            IList<reservationDto> reservationDto =  _context.Reservations.Join(
                _context.Doctors,
                r => r.DoctorId,
                d => d.Id,
                (res , doc) => new {
                    Id = res.Id ,
                    PatientId = res.PatientId , 
                    Date = res.Date , 
                    From = res.From , 
                    To = res.To , 
                    doctor = doc.Name,
                    DoctorId = doc.DoctorId
                }
            ).Where(d => d.DoctorId == DoctorId && d.Date >= fromDate && d.Date <= toDate)
                .Join(
                _context.Patients,
                r => r.PatientId,
                P => P.Id,
                (res, patient) => new reservationDto
                {
                    Id = res.Id,
                    PatientId = res.PatientId,
                    Date = res.Date,
                    From = res.From,
                    To = res.To,
                    doctor = res.doctor,
                    patient = patient.Name
                }
            ).ToList();
            return reservationDto;
            //return _context.Reservations.Include(r => r.Doctor).Include(r => r.Patient).ToList();
        }
        
        
        public IList<ReservationcountByDoctorDto> getDoctorReservationNumber_old() {
            //return _context.Reservations.ToList();
            IList<ReservationcountByDoctorDto> reservationcountByDoctorDtos =  _context.Doctors.GroupJoin(
                _context.Reservations,
                d => d.Id,
                r => r.DoctorId,
                (doc ,res) => new {
                    doc,res
                }
            )
            .SelectMany(
                r => r.res.DefaultIfEmpty(),
                (doc,res) => new { doc = doc.doc, res = res }
            )
           
            .GroupBy(res => res.doc.Id)
            .Select(g => new ReservationcountByDoctorDto
            { 
                name = g.First().doc.Name,
                count = g.Count()
            })
            .ToList();
            return reservationcountByDoctorDtos;
            //return _context.Reservations.Include(r => r.Doctor).Include(r => r.Patient).ToList();
        }
        
        public IList<ReservationcountByDoctorDto> getDoctorReservationNumber() {
            //return _context.Reservations.ToList();
            IList<ReservationcountByDoctorDto> reservationcountByDoctorDtos = _context.ReservationcountByDoctorDto.FromSqlRaw("select * from getDoctorReservationNumber").ToList();
            
            return reservationcountByDoctorDtos;
            //return _context.Reservations.Include(r => r.Doctor).Include(r => r.Patient).ToList();
        }
        
    }
}
