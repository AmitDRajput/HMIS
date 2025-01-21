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
    [Authorize]
    public class RoleMasterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public RoleMasterController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetRoleMasterById(int id)
        {
            var docFromRepo = _unitOfWork.RoleMaster.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllRoleMasters")]
        public IActionResult GetAllRoleMasters()
        {
            var docFromRepo = _unitOfWork.RoleMaster.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.RoleMasterId);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateRoleMaster")]
        public IActionResult CreateRoleMaster(RoleMaster roleMst)
        {
            _unitOfWork.RoleMaster.Add(roleMst);
            return Ok(roleMst.RoleMasterId);
        }

        [HttpPost("UpdateRoleMaster")]
        public IActionResult UpdateRoleMaster(RoleMaster roleMst)
        {
            _unitOfWork.RoleMaster.Update(roleMst);
            _unitOfWork.Save();
            return Ok(roleMst.RoleMasterId);
        }


        [HttpDelete("DeleteRole")]
        public IActionResult DeleteRole(long RoleId)
        {

            // Optionally, you could check if the RoleMaster record exists before updating
            var existingRole = _unitOfWork.RoleMaster.GetById(RoleId);
            if (existingRole == null)
            {
                return NotFound($"Role with ID {RoleId} not found.");
            }

            // Update the RoleMaster information
            existingRole.IsActive = false;
            _unitOfWork.RoleMaster.Update(existingRole);
            _unitOfWork.Save();

            return Ok(new { RoleID = RoleId, Message = "RoleMaster deleted successfully." });
        }

    }
}
