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
            var docFromRepo = _unitOfWork.Patient.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.PatientID);
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
        [HttpGet("SearchPatientByMobile")]
        public IActionResult SearchPatientByMobile(string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber))
            {
                return BadRequest("Mobile number cannot be empty.");
            }

            // Check if the patient already exists in the system by mobile number
            var existingPatient = _unitOfWork.Patient.GetAll()
                                      .FirstOrDefault(p => p.MobileNumber == mobileNumber);

            if (existingPatient != null)
            {
                // Patient found, return patient details
                return Ok(new
                {
                    PatientID = existingPatient.PatientID,
                    FirstName = existingPatient.FirstName,
                    LastName = existingPatient.LastName,
                    MobileNumber = existingPatient.MobileNumber,
                    Message = "Patient already exists with this mobile number."
                });
            }
            else
            {
                // Patient not found, allow registration of new patient
                return NotFound("No existing patient found with this mobile number. You can proceed to register a new patient.");
            }
        }

        [HttpPost("RegisterPatient")]
        public IActionResult RegisterPatient([FromBody] Patient newPatient)
        {
            if (newPatient == null)
            {
                return BadRequest("Patient details are required.");
            }

            // Check if the mobile number already exists
            var existingPatient = _unitOfWork.Patient.GetAll()
                                       .FirstOrDefault(p => p.MobileNumber == newPatient.MobileNumber);

            if (existingPatient != null)
            {
                return Conflict("A patient already exists with this mobile number.");
            }

            // If no patient with the given mobile number exists, register the new patient
            _unitOfWork.Patient.Add(newPatient);
            _unitOfWork.Save();

            return Ok(new { PatientID = newPatient.PatientID, Message = "New patient registered successfully." });
        }

        [HttpDelete("DeletePatient")]
        public IActionResult DeletePatient(long PatientId)
        {
            // Check if the patient exists in the database
            var existingPatient = _unitOfWork.Patient.GetById(PatientId);

            if (existingPatient == null)
            {
                return NotFound($"Patient with ID {PatientId} not found.");
            }

            // If the patient is already inactive, return a message indicating no further action is needed
            if (!existingPatient.IsActive)
            {
                return BadRequest($"Patient with ID {PatientId} is already marked as inactive.");
            }

            // Perform the "soft delete" by setting IsActive to false
            existingPatient.IsActive = false;

            // Update the patient record in the database
            _unitOfWork.Patient.Update(existingPatient);
            _unitOfWork.Save();

            // Return success response
            return Ok(new { PatientID = PatientId, Message = "Patient has been deactivated successfully." });
        }
    }




    //[HttpDelete("DeletePatient")]
    //public IActionResult DeletePatient(long PatientId)
    //{

    //    // Optionally, you could check if the Patient record exists before updating
    //    var existingPatient = _unitOfWork.Patient.GetById(PatientId);
    //    if (existingPatient == null)
    //    {
    //        return NotFound($"Patient with ID {PatientId} not found.");
    //    }

    //    // Update the Patient information
    //    existingPatient.IsActive = false;
    //    _unitOfWork.Patient.Update(existingPatient);
    //    _unitOfWork.Save();

    //    return Ok(new { PatientID = PatientId, Message = "Patient deleted successfully." });
    //}
   



}

