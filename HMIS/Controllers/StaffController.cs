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
   // [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public StaffController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetStaffById(int id)
        {
            var docFromRepo = _unitOfWork.Staff.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllStaff")]
        public IActionResult GetAllStaff()
        {
            var docFromRepo = _unitOfWork.Staff.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.StaffID);
            return Ok(docFromRepo);
        }






        [HttpPost("AddStaff")]
        public IActionResult AddStaff([FromForm] IFormFile[] files, [FromForm] IFormFile StaffPic, [FromForm] Staff doc)
        {
            _unitOfWork.Staff.Add(doc);
            _unitOfWork.Save();


            //2.Define a folder to store the files
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "HmisDocs/StaffProfile", doc.StaffID.ToString());

            //Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // 3.Process each uploaded file
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                //  Save the file to the disk
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                //4.Save the file path to the StaffDocument table
                var StaffDocument = new StaffDocument
                {
                    StaffId = doc.StaffID,
                    DocumentPath = filePath
                };

                _unitOfWork.StaffDocument.Add(StaffDocument);
                _unitOfWork.Save();
            }


            folderPath = Path.Combine(Directory.GetCurrentDirectory(), "HmisDocs/StaffPic", doc.StaffID.ToString());

            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (StaffPic != null)
            {
                var staffPicName = Path.GetFileName(StaffPic.FileName);
                var staffPicPath = Path.Combine(folderPath, staffPicName);

                //Save the Staff picture to the disk
                using (var picStream = new FileStream(staffPicPath, FileMode.Create))
                {
                    StaffPic.CopyTo(picStream);
                }

                // 5.Update the Staffs profile with the picture path
                doc.StaffPic = staffPicPath;  // Assuming there is a 'ProfilePicturePath' field in the Patient model
                _unitOfWork.Staff.Update(doc);
                _unitOfWork.Save();
            }

            //6.Return the Staff ID as a response
            return Ok(doc.StaffID);
        }

            [HttpPost("UpdateStaff")]
            public IActionResult UpdateStaff([FromBody] Staff Staff)
            {
                if (Staff == null)
                {
                    return BadRequest("Staff data is required.");
                }

                // Optionally, you could check if the Staff record exists before updating
                var existingStaff = _unitOfWork.Staff.GetById(Staff.StaffID);
                if (existingStaff == null)
                {
                    return NotFound($"Staff with ID {Staff.StaffID} not found.");
                }

                // Update the Staff information
                _unitOfWork.Staff .Update(Staff);
                _unitOfWork.Save();

                return Ok(new { StaffID = Staff.StaffID, Message = "Staff updated successfully." });
            }


        [HttpPost("DeleteStaff")]
        public IActionResult DeleteStaff(int staffId)
        {
            
            // Optionally, you could check if the Staff record exists before updating
            var existingStaff = _unitOfWork.Staff.GetById(staffId);
            if (existingStaff == null)
            {
                return NotFound($"Staff with ID {staffId} not found.");
            }

            // Update the Staff information
            existingStaff.IsActive = false;
            _unitOfWork.Staff.Update(existingStaff);
            _unitOfWork.Save();

            return Ok(new { StaffID = staffId, Message = "Staff updated successfully." });
        }



    }



}




