using HMIS.DataAccess.Implementation;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DoctorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DoctorController(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetDoctorById(int id)
        {
            var docFromRepo = _unitOfWork.Doctor.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllDoctors")]
        public IActionResult GetAllDoctors()
        {
            var docFromRepo = _unitOfWork.Doctor.GetAll().Where(x=> x.IsActive==true).OrderByDescending(x => x.DoctorID);
             return Ok(docFromRepo);
        }

        [HttpPost("CreateDoctor")]
        public IActionResult CreateDoctor(Doctor doc)
        {
            _unitOfWork.Doctor.Add(doc);
            _unitOfWork.Save();
            return Ok(doc.DoctorID);
        }

        [HttpDelete("DeleteDoctor")]
        public IActionResult DeleteDoctor(long DoctorId)
        {

            // Optionally, you could check if the Doctor record exists before updating
            var existingDoctor = _unitOfWork.Doctor.GetById(DoctorId);
            if (existingDoctor == null)
            {
                return NotFound($"Doctor with ID {DoctorId} not found.");
            }

            // Update the Doctor information
            existingDoctor.IsActive = false;
            _unitOfWork.Doctor.Update(existingDoctor);
            _unitOfWork.Save();

            return Ok(new { DoctorID = DoctorId, Message = "Doctor deleted successfully." });
        }



    }
}
