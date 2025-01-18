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
   
     
    public class PatientDocumentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PatientDocumentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetPatientDocumentById(int id)
        {
            var docFromRepo = _unitOfWork.PatientDocument.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllPatientDocument")]
        public IActionResult GetAllPatientDocument()
        {
            var docFromRepo = _unitOfWork.PatientDocument.GetAll().OrderByDescending(x => x.DocumentID);
            return Ok(docFromRepo);
        }

        [HttpPost("CreatePatientDocument")]
        public IActionResult CreatePatientDocument(PatientDocument doc)
        {
            _unitOfWork.PatientDocument.Add(doc);
            return Ok(doc.DocumentID);
        }


       


    }
}
