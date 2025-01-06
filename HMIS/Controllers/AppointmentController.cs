using HMIS.DataAccess.Implementation;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentController(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAppointmentById(int id)
        {
            var docFromRepo = _unitOfWork.Appointment.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllAppointments")]
        public IActionResult GetAllAppointments()
        {
            var docFromRepo = _unitOfWork.Appointment.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.AppointmentID);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateAppointment")]
        public IActionResult CreateAppointment(Appointment appt)
        {
            _unitOfWork.Appointment.Add(appt);
            _unitOfWork.Save();
            return Ok(appt.AppointmentID);
        }

        [HttpPost("UpdateAppointment")]
        public IActionResult UpdateAppointment(Appointment appt)
        {
            _unitOfWork.Appointment.Update(appt);
            _unitOfWork.Save();
            return Ok(appt.AppointmentID);
        }

    }
}
