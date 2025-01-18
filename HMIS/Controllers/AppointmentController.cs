using HMIS.DataAccess.Implementation;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentController(IUnitOfWork unitOfWork)
        {
        _unitOfWork = unitOfWork;
        }

        // Admin and Patient can view appointments
        [HttpGet("GetAppointmentById/{id}")] 
        // [Authorize(Roles = "Admin,Patient")] // Both Admin and Patient can access this

        //public IActionResult GetAppointmentById(int id)
        //{
        //    var docFromRepo = _unitOfWork.Appointment.GetById(id);
        //    return Ok(docFromRepo);
        //}
        //10/01/2024

        public IActionResult GetAppointmentById(int id)
        {
            var docFromRepo = _unitOfWork.Appointment.GetById(id);
            if (docFromRepo == null)
            {
                return NotFound("Appointment not found.");
            }

            // Check if the user is a Patient and is requesting their own appointment
            if (User.IsInRole("Patient") && docFromRepo.PatientID != int.Parse(User.Identity.Name)) // Assuming User.Identity.Name is PatientID
            {
                return Unauthorized("You can only view your own appointments.");
            }

            return Ok(docFromRepo);
        }


        //public IActionResult GetAllAppointments()
        //{
        //    var docFromRepo = _unitOfWork.Appointment.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.AppointmentID);
        //    return Ok(docFromRepo);
        //}

        // Admin can view all active appointments; Patients can view their own active appointments
        [HttpGet("GetAppointments")]
       // [Authorize(Roles = "Admin,Patient")]
        public IActionResult GetAppointments()
        {
            if (User.IsInRole("Admin"))
            {
                var appointments = _unitOfWork.Appointment.GetAll()
                    .Where(x => x.IsActive)
                    .OrderByDescending(x => x.AppointmentID);
                return Ok(appointments);
            }

            // Patient can only view their own appointments
            var patientId = int.Parse(User.Identity.Name);
            var patientAppointments = _unitOfWork.Appointment.GetAll()
                .Where(x => x.PatientID == patientId && x.IsActive)
                .OrderByDescending(x => x.AppointmentID);

            return Ok(patientAppointments);
        }


        [HttpPost("CreateAppointment")]
       // [Authorize(Roles = "Admin")]
        //public IActionResult CreateAppointment(Appointment appt)
        //{
        //    _unitOfWork.Appointment.Add(appt);
        //    _unitOfWork.Save();
        //    return Ok(appt.AppointmentID);
        //}
        // Admin can create an appointment for any patient
       
        public IActionResult CreateAppointment([FromBody] Appointment appt)
        {
            if (appt == null)
            {
                return BadRequest("Appointment data is invalid.");
            }

            _unitOfWork.Appointment.Add(appt);
            _unitOfWork.Save();
            return Ok(new { AppointmentID = appt.AppointmentID });
        }

        [HttpPost("CreateAppointmentForPatient")]
       // [Authorize(Roles = "Patient")]
        public IActionResult CreateAppointmentForPatient([FromBody] Appointment appt)
        {
            if (appt == null)
            {
                return BadRequest("Appointment data is invalid.");
            }

            // Set the PatientID to the current user's PatientID
            //appt.PatientID = int.Parse(User.Identity.Name);

            _unitOfWork.Appointment.Add(appt);
            _unitOfWork.Save();
            return Ok(new { AppointmentID = appt.AppointmentID });
        }


        [HttpPost("UpdateAppointment")]
        //public IActionResult UpdateAppointment(Appointment appt)
        //{
        //    _unitOfWork.Appointment.Update(appt);
        //    _unitOfWork.Save();
        //    return Ok(appt.AppointmentID);
        //}

        // Admin or Patient can update an appointment (for their own appointments)
        [HttpPut("UpdateAppointment")]
       // [Authorize(Roles = "Admin,Patient")]
        public IActionResult UpdateAppointment([FromBody] Appointment appt)
        {
            if (appt == null)
            {
                return BadRequest("Appointment data is invalid.");
            }

            var existingAppointment = _unitOfWork.Appointment.GetById(appt.AppointmentID);
            if (existingAppointment == null)
            {
                return NotFound("Appointment not found.");
            }

            // Ensure that a Patient can only update their own appointment
            if (User.IsInRole("Patient") && existingAppointment.PatientID != int.Parse(User.Identity.Name))
            {
                return Unauthorized("You can only update your own appointments.");
            }

            _unitOfWork.Appointment.Update(appt);
            _unitOfWork.Save();
            return Ok(new { AppointmentID = appt.AppointmentID });
        }

        // Admin or Patient can cancel an appointment (for their own appointments)
        [HttpDelete("CancelAppointment/{id}")]
        //[Authorize(Roles = "Admin,Patient")]
        public IActionResult CancelAppointment(int id)
        {
            var appointment = _unitOfWork.Appointment.GetById(id);
            if (appointment == null)
            {
                return NotFound("Appointment not found.");
            }

            // Ensure that a Patient can only cancel their own appointment
            if (User.IsInRole("Patient") && appointment.PatientID != int.Parse(User.Identity.Name))
            {
                return Unauthorized("You can only cancel your own appointments.");
            }

            appointment.IsActive = false; // Mark appointment as canceled---- Change
            _unitOfWork.Appointment.Update(appointment);
            _unitOfWork.Save();
            return Ok(new { AppointmentID = id, Status = "Canceled" });
        }

        [HttpDelete("DeleteAppointment")]
        public IActionResult DeleteAppointment(long AppointmentId)
        {

            // Optionally, you could check if the Appointment record exists before updating
            var existingAppointment = _unitOfWork.Appointment.GetById(AppointmentId);
            if (existingAppointment == null)
            {
                return NotFound($"Appointment with ID {AppointmentId} not found.");
            }

            // Update the Appointment information
            existingAppointment.IsActive = false;
            _unitOfWork.Appointment.Update(existingAppointment);
            _unitOfWork.Save();

            return Ok(new { AppointmentID = AppointmentId, Message = "Appointment deleted successfully." });
        }


    }
}
