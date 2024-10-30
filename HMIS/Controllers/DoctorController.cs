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
    [Authorize]
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
            var docFromRepo = _unitOfWork.Doctor.GetAll().OrderByDescending(x => x.DoctorID);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateDoctor")]
        public IActionResult CreateDoctor(Doctor doc)
        {
            _unitOfWork.Doctor.Add(doc);
            return Ok(doc.DoctorID);
        }

    }
}
