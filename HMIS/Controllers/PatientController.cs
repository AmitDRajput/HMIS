using HMIS.API.Service;
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
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public PatientController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

    
        [HttpGet]
        public IActionResult GetPatientById(int id)
        {
            var docFromRepo = _unitOfWork.Patient.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllPatients")]
        public IActionResult GetAllPatients()
        {
            var docFromRepo = _unitOfWork.Patient.GetAll().OrderByDescending(x => x.PatientID);
            return Ok(docFromRepo);
        }

        [HttpPost("CreatePatient")]
        public IActionResult CreatePatient(Patient doc)
        {
            _unitOfWork.Patient.Add(doc);
            return Ok(doc.PatientID);
        }

    }
}
