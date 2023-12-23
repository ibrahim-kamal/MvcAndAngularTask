using Microsoft.AspNetCore.Mvc;
using MvcAndAngularTask.DataAcessLayer;
using MvcAndAngularTask.DataView;
using MvcAndAngularTask.Models;
using MvcAndAngularTask.Models.DTO;
using MvcAndAngularTask.Models.response;
using MvcAndAngularTask.Models.validation;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MvcAndAngularTask.Controllers
{
    public class Reservation : ControllerBase
    {
        ReservationDAL reservationDAL;
        PatientDAL _patientDAL;
        DoctorDAL _doctorDAL;
        private readonly JsonSerializerOptions _jsonOptions;
        public Reservation(ReservationDAL _reservationDAL ,PatientDAL patientDAL , DoctorDAL doctorDAL)
        {
            reservationDAL = _reservationDAL;
            _patientDAL = patientDAL;
            _doctorDAL  = doctorDAL;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
        }
        [HttpPost]
        public string reservation(ReservationValidation reservationValidation)
        {
            defaultResponse defaultResponse;
            if (!ModelState.IsValid)
                defaultResponse = new defaultResponse("Reservation Done", 1, new object());
            else { 
                // get Patient By NotionalId
                int id = _patientDAL.get(reservationValidation.NationalId);
                // check is exist
                if (id == 0) { 
                    // if not insert
                    Patient patient = new Patient();
                    patient.NationalId = reservationValidation.NationalId;
                    patient.Name = reservationValidation.Name;
                    patient.Gender = reservationValidation.Gender == 1 ? true : false;
                    id = _patientDAL.insert(patient);
                }
                // reservation
                MvcAndAngularTask.Models.Reservation reservation = new MvcAndAngularTask.Models.Reservation();
                string[] str_date = reservationValidation.Date.Split("-");

                reservation.Date = new DateTime(int.Parse(str_date[0]), int.Parse(str_date[1]), int.Parse(str_date[2]));
                reservation.DoctorId = _doctorDAL.get(reservationValidation.doctorId).Id;
                reservation.PatientId = id;
                reservation.From = reservationValidation.From;
                reservation.To = reservationValidation.To;
            
                int reservationId = reservationDAL.insert(reservation);
                if (reservationId == 0)
                {
                 defaultResponse = new defaultResponse("Failed To Reservation", 0, new object());

                }
                else { 
                 defaultResponse = new defaultResponse("Reservation Done", 1, new object());
                }
            }
            return JsonSerializer.Serialize(defaultResponse);
        }


        public IActionResult getReservation() {
            IList<reservationDto> reservations = reservationDAL.GetReservations();



            var json = JsonSerializer.Serialize(reservations, _jsonOptions);

            return Content(json, "application/json");

        }




        [HttpPost]
        public IActionResult getDoctorReservation(DoctorReservation doctorReservation)
        {
            IList<reservationDto> reservations = reservationDAL.GetReservations(doctorReservation.DoctorId, doctorReservation.fromDate, doctorReservation.toDate);



            var json = JsonSerializer.Serialize(reservations, _jsonOptions);

            return Content(json, "application/json");

        }



        public IActionResult getDoctorReservationNumber()
        {
            IList<ReservationcountByDoctorDto> reservationcountByDoctorDtos = reservationDAL.getDoctorReservationNumber();
            DataViewReservationNumber dataViewReservationNumber = new DataViewReservationNumber();
            dataViewReservationNumber.names = new List<string>();
            dataViewReservationNumber.numbers = new List<int>();
            foreach (var item in reservationcountByDoctorDtos)
            {
                dataViewReservationNumber.names.Add(item.name);
                dataViewReservationNumber.numbers.Add(item.count);
            }

            return Ok(dataViewReservationNumber);


        }


    }
}
