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
   // [Authorize]
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
        public async Task<IActionResult> AddStaff([FromForm] AddStaffRequest request)
        {
            try
            {
                var doc = request.Doc;

                // Add the staff to the database
                _unitOfWork.Staff.Add(doc);
                _unitOfWork.Save();

                string staffProfilePath = Path.Combine(_configuration.GetValue<string>("FileUploadBasePath"), "StaffProfile", doc.StaffID.ToString());
                string staffPicPath = Path.Combine(_configuration.GetValue<string>("FileUploadBasePath"), "StaffPic", doc.StaffID.ToString());

                // Ensure directories exist
                Directory.CreateDirectory(staffProfilePath);
                Directory.CreateDirectory(staffPicPath);

                // Process documents
                foreach (var file in request.Files)
                {
                    var uniqueFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(staffProfilePath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    var staffDocument = new StaffDocument
                    {
                        StaffId = doc.StaffID,
                        DocumentName = System.IO.Path.GetFileNameWithoutExtension(file.FileName),
                        DocumentPath = filePath
                    };

                    _unitOfWork.StaffDocument.Add(staffDocument);
                }

                // Process staff picture
                if (request.StaffPic != null)
                {
                    var uniquePicName = $"{Path.GetFileNameWithoutExtension(request.StaffPic.FileName)}_{Guid.NewGuid()}{Path.GetExtension(request.StaffPic.FileName)}";
                    var picPath = Path.Combine(staffPicPath, uniquePicName);

                    using (var picStream = new FileStream(picPath, FileMode.Create))
                    {
                        await request.StaffPic.CopyToAsync(picStream);
                    }

                    doc.StaffPic = picPath;
                    _unitOfWork.Staff.Update(doc);
                }

                _unitOfWork.Save();

                return Ok(doc.StaffID);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
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




