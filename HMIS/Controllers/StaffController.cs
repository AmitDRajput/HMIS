using HMIS.API.ViewModel;
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
    public class StaffController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IConfiguration _configuration;
        public StaffController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetStaffById(long id)
        {
            var docFromRepo = _unitOfWork.Staff.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllStaff")]
        public IActionResult GetAllStaff()
        {
            //var docFromRepo = _unitOfWork.Staff.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.StaffID);
            var docFromRepo = (from staff in _unitOfWork.Staff.GetAll().Where(s => s.IsActive == true) // Filter first
                               join doc in _unitOfWork.StaffDocument.GetAll()
                               on staff.StaffID equals doc.StaffId into staffDocs
                               from doc in staffDocs.DefaultIfEmpty() // Ensures it's a left join
                               orderby staff.StaffID descending
                               select new
                               {
                                   Staff = staff,
                                   StaffDocument = doc // `doc` can be null if no document exists
                               }).ToList();


            return Ok(docFromRepo);
        }

        [HttpPost("AddStaff")]
        public IActionResult AddStaff(Staff staff)
        {
            _unitOfWork.Staff.Add(staff);
            _unitOfWork.Save();
            return Ok(staff.StaffID);
        }

        [HttpPut("AddStaffDoc/{StaffID}")]
        public IActionResult AddStaffDoc(long StaffID,IFormFile[]? files, IFormFile? StaffPic)
        {
            var doc = _unitOfWork.Staff.GetById(StaffID);
            string staffProfilePath = Path.Combine(_configuration.GetValue<string>("FileUploadBasePath"), "StaffProfile", StaffID.ToString());
            string staffPicPath = Path.Combine(_configuration.GetValue<string>("FileUploadBasePath"), "StaffPic", StaffID.ToString());

            // Ensure directories exist
            Directory.CreateDirectory(staffProfilePath);
            Directory.CreateDirectory(staffPicPath);

            // Process documents
            if (files != null)
            {
                if (files.Count() > 0)
                {
                    foreach (var file in files)
                    {
                        var uniqueFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var filePath = Path.Combine(staffProfilePath, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        var staffDocument = new StaffDocument
                        {
                            StaffId = StaffID,
                            DocumentName = System.IO.Path.GetFileNameWithoutExtension(file.FileName),
                            DocumentPath = filePath
                        };

                        _unitOfWork.StaffDocument.Add(staffDocument);
                    }
                }
            }
            // Process staff picture
            if (StaffPic != null)
            {
                var uniquePicName = $"{Path.GetFileNameWithoutExtension(StaffPic.FileName)}_{Guid.NewGuid()}{Path.GetExtension(StaffPic.FileName)}";
                var picPath = Path.Combine(staffPicPath, uniquePicName);

                using (var picStream = new FileStream(picPath, FileMode.Create))
                {
                    StaffPic.CopyTo(picStream);
                }

                doc.StaffPic = picPath;
                _unitOfWork.Staff.Update(doc);
                _unitOfWork.Save();
            }

            //_unitOfWork.Save();
            return Ok(new { StaffID = doc.StaffID, Message = "Staff documents added successfully." });

        }


        [HttpPost("UpdateStaff")]
        public IActionResult UpdateStaff([FromBody] Staff staff)
        {
            if (staff == null)
            {
                return BadRequest("Staff data is required.");
            }

            // Check if the record exists
            var existingStaff = _unitOfWork.Staff.GetById(staff.StaffID);
            if (existingStaff == null)
            {
                return NotFound($"Staff with ID {staff.StaffID} not found.");
            }

            // Detach the existing entity to avoid tracking conflict
            _unitOfWork.Detach(existingStaff);

            // Update the Staff information
            _unitOfWork.Staff.Update(staff);
            _unitOfWork.Save();

            return Ok(new { StaffID = staff.StaffID, Message = "Staff updated successfully." });
        }




        [HttpDelete("DeleteStaff")]
        public IActionResult DeleteStaff(long staffId)
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

            return Ok(new { StaffID = staffId, Message = "Staff deleted successfully." });
        }



    }



}




