using MvcAndAngularTask.Models;
using MvcAndAngularTask.Models.validation;

namespace MvcAndAngularTask.DataAcessLayer
{
    public class AppoinmentsDAL
    {


        public ApplicationContext _context;
        public DoctorDAL _doctor;
        public ScheduleDAL _schedule;
        public ReservationDAL _reservation;


        public AppoinmentsDAL(ApplicationContext context, DoctorDAL doctor, ScheduleDAL schedule, ReservationDAL reservation)
        {
            _context = context;
            _doctor = doctor;
            _schedule = schedule;
            _reservation = reservation;


        }

        private List<TimeSpan> getTimes(TimeSpan from, TimeSpan To)
        {
            List<TimeSpan> times = new List<TimeSpan>();
            while (from < To)
            {
                times.Add(from);
                from = from.Add(new TimeSpan(0, 30, 0));
            }


            return times;

        }

        public IList<TimeSpan> getFreeAppointment(AppoinmentsValidation appoinment)
        {
            //2023-12-21
            IList<TimeSpan> result = new List<TimeSpan>();
            string[] str_date = appoinment.date.Split('-');
            DateTime date= new DateTime(int.Parse(str_date[0]) , int.Parse(str_date[1]), int.Parse(str_date[2]));
            Doctor doctor = _doctor.get(appoinment.DoctorId);
            // get Schadule For Date
            var schedule = _schedule.getSchedule(date.DayOfWeek.ToString(), doctor.Id);
            if (schedule != null) { 
                List<TimeSpan> scheduleTimes = getTimes(schedule.From.Value, schedule.To.Value);

                var reservationsTime = _reservation.getReservationTimes(date, doctor.Id);

                result = scheduleTimes.Except(reservationsTime).ToList();

            }
            return result;

        }

    }
}
