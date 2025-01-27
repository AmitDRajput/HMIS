using HMIS.API.Service;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveMasterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public LeaveMasterController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetLeaveById(int id)
        {
            var docFromRepo = _unitOfWork.LeaveMaster.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllLeave")]
        public IActionResult GetAllLeave()
        {
            var docFromRepo = _unitOfWork.LeaveMaster.GetAllLeaves();
            return Ok(docFromRepo);
        }


        [HttpPost("CreateLeave")]

        public IActionResult CreateLeave([FromBody] LeaveMaster StaffId)
        {
            if (StaffId == null)
            {
                return BadRequest("Leave data is invalid.");
            }

            _unitOfWork.LeaveMaster.Add(StaffId);
            _unitOfWork.Save();
           
            return Ok(new { LeaveID = StaffId.LeaveID });
        }


        [HttpPost("UpdateLeave")]
        public IActionResult UpdateLeave([FromBody] LeaveMaster leave)
        {
            if (leave == null)
            {
                return BadRequest("Leave data is required.");
            }

            // Check if the record exists
            var existingStaff = _unitOfWork.LeaveMaster.GetById(leave.LeaveID);
            if (existingStaff == null)
            {
                return NotFound($"leave with ID {leave.LeaveID} not found.");
            }

            // Detach the existing entity to avoid tracking conflict
            _unitOfWork.Detach(existingStaff);

            // Update the Staff information
            _unitOfWork.LeaveMaster.Update(leave);
            _unitOfWork.Save();

            return Ok(new { LeaveID = leave.LeaveID, Message = "Leave updated successfully." });
        }

        [HttpDelete("DeleteLeave")]
        public IActionResult DeleteLeave(long LeaveId)
        {

            // Optionally, you could check if the Leave record exists before updating
            var existingLeaveMaster = _unitOfWork.LeaveMaster.GetById(LeaveId);
            if (existingLeaveMaster== null)
            {
                return NotFound($"Leave with ID {LeaveId} not found.");
            }

            // Update the Holiday information
            existingLeaveMaster.IsActive = false;
            _unitOfWork.LeaveMaster.Update(existingLeaveMaster);
            _unitOfWork.Save();

            return Ok(new { LeaveID = LeaveId, Message = "Leave deleted successfully." });
        }



    }
}
