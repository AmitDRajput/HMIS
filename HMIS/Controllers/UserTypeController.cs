using HMIS.API.Service;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]

    public class UserTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public UserTypeController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetUserTypeById(int id)
        {
            var docFromRepo = _unitOfWork.UserType.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllUserType")]
        public IActionResult GetAllRoleMasters()
        {
            var docFromRepo = _unitOfWork.UserType.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.UserTypeId);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateUserType")]
        public IActionResult CreateUserType(UserType UserType)
        {
            _unitOfWork.UserType.Add(UserType);
            _unitOfWork.Save();
            return Ok(UserType.UserTypeId);
        }

        [HttpPost("UpdateUserType")]
        public IActionResult UpdateRoleMaster(UserType UserType)
        {
            _unitOfWork.UserType.Update(UserType);
            _unitOfWork.Save();
            return Ok(UserType.UserTypeId);
        }

        [HttpDelete("DeleteUserType")]
        public IActionResult DeleteUserType(int userTypeId)
        {

            // Optionally, you could check if the Staff record exists before updating
            var existingStaff = _unitOfWork.UserType.GetById(userTypeId);
            if (existingStaff == null)
            {
                return NotFound($"Dept with ID {userTypeId} not found.");
            }

            // Update the Staff information
            existingStaff.IsActive = false;
            _unitOfWork.UserType.Update(existingStaff);
            _unitOfWork.Save();

            return Ok(new { DeptId = userTypeId, Message = "UserType deleted successfully." });
        }
    }
}
