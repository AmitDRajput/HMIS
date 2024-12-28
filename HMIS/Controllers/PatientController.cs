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
        public IActionResult CreatePatient(IFormFile[] files, IFormFile patientPic, [FromForm] Patient doc)
        {
            _unitOfWork.Patient.Add(doc);
            _unitOfWork.Save();

            // 2. Define a folder to store the files
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "HmisDocs/PatientProfile", doc.PatientID.ToString());

            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // 3. Process each uploaded file
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                // Save the file to the disk
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                     file.CopyTo(fileStream);
                }

                // 4. Save the file path to the PatientDocument table
                var patientDocument = new PatientDocument
                {
                    PatientID = doc.PatientID,
                    DocumentPath = filePath
                };

                _unitOfWork.PatientDocument.Add(patientDocument);
                _unitOfWork.Save();
            }


            folderPath = Path.Combine(Directory.GetCurrentDirectory(), "HmisDocs/PatientPic", doc.PatientID.ToString());

            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (patientPic != null)
            {
                var patientPicName = Path.GetFileName(patientPic.FileName);
                var patientPicPath = Path.Combine(folderPath, patientPicName);

                // Save the patient picture to the disk
                using (var picStream = new FileStream(patientPicPath, FileMode.Create))
                {
                    patientPic.CopyTo(picStream);
                }

                // 5. Update the patient's profile with the picture path
                doc.PatientPic = patientPicPath;  // Assuming there is a 'ProfilePicturePath' field in the Patient model
                _unitOfWork.Patient.Update(doc);
                _unitOfWork.Save();
            }

            // 6. Return the Patient ID as a response
            return Ok(doc.PatientID);
        }

    }
}
