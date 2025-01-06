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
            var docFromRepo = _unitOfWork.staff.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllSraff")]
        public IActionResult GetAllStaff()
        {
            var docFromRepo = _unitOfWork.staff.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.StaffID);
            return Ok(docFromRepo);
        }

       




        [HttpPost("CreateStaffwithDocument")]
        public IActionResult CreateStaffWithDocument(IFormFile[] files, IFormFile StaffPic, [FromBody] Staff doc)
        {
            _unitOfWork.staff.Add(doc);
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
                _unitOfWork.staff.Update(doc);
                _unitOfWork.Save();
            }

            //6.Return the Staff ID as a response
            return Ok(doc.StaffID);
        }



    }
}



