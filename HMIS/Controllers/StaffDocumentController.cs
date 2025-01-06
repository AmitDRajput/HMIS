using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffDocumentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public StaffDocumentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetStaffDocumentById(int id)
        {
            var docFromRepo = _unitOfWork.StaffDocument.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllStaffDocument")]
        public IActionResult GetAllPatientDocument()
        {
            var docFromRepo = _unitOfWork.StaffDocument.GetAll().OrderByDescending(x => x.StaffDocumnetID);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateStaffDocument")]
        public IActionResult CreatePatientDocument(PatientDocument doc)
        {
            _unitOfWork.PatientDocument.Add(doc);
            return Ok(doc.DocumentID);
        }

    }






    // Staff Profile with Picture

    //[HttpPost("CreateStaffProfile")]
    //public IActionResult CreateStaff(IFormFile[] files, IFormFile StaffPic, [FromForm] Staff doc)
    //{
    //    _unitOfWork.Staff.Add(doc);
    //    _unitOfWork.Save();

    //    // 2. Define a folder to store the files
    //    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "HmisDocs/StaffPic", doc.StaffID.ToString());

    //    // Ensure the directory exists
    //    if (!Directory.Exists(folderPath))
    //    {
    //        Directory.CreateDirectory(folderPath);
    //    }

    //    // 3. Process each uploaded file
    //    foreach (var file in files)
    //    {
    //        var fileName = Path.GetFileName(file.FileName);
    //        var filePath = Path.Combine(folderPath, fileName);

    //        // Save the file to the disk
    //        using (var fileStream = new FileStream(filePath, FileMode.Create))
    //        {
    //            file.CopyTo(fileStream);
    //        }

    //        // 4. Save the file path to the Staff Document table
    //        var StaffDocument = new StaffDocument
    //        {
    //            StaffID = doc.StaffID,
    //            DocumentPath = filePath
    //        };

    //        _unitOfWork.StaffDocument.Add(SatffDocument);
    //        _unitOfWork.Save();
    //    }


    //    folderPath = Path.Combine(Directory.GetCurrentDirectory(), "HmisDocs/StaffProfile", doc.StaffID.ToString());

    //    // Ensure the directory exists
    //    if (!Directory.Exists(folderPath))
    //    {
    //        Directory.CreateDirectory(folderPath);
    //    }
    //    if (StaffPic != null)
    //    {
    //        var StaffPicName = Path.GetFileName(StaffPic.FileName);
    //        var StaffPicPath = Path.Combine(folderPath, staffprofileName);

    //        // Save the patient picture to the disk
    //        using (var picStream = new FileStream(staffprofilePath, FileMode.Create))
    //        {
    //            patientPic.CopyTo(picStream);
    //        }

    //        // 5. Update the patient's profile with the picture path
    //        doc.PatientPic = patientPicPath;  // Assuming there is a 'ProfilePicturePath' field in the Patient model
    //        _unitOfWork.Patient.Update(doc);
    //        _unitOfWork.Save();
    //    }

    //    // 6. Return the Patient ID as a response
    //    return Ok(doc.PatientID);
    //}
}
