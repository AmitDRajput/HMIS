using HMIS.API.Service;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodbankMasterController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public BloodbankMasterController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetBloodbankMasterById(int id)
        {
            var docFromRepo = _unitOfWork.BloodbankMaster.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllBloodbankMaster")]
        public IActionResult GetAllBloodbankMaster()
        {
            var docFromRepo = _unitOfWork.BloodbankMaster.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.BloodbankMasterID);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateBloodbankMaster")]
        public IActionResult CreateBloodbankMaster(BloodbankMaster BloodbankMaster)
        {
            _unitOfWork.BloodbankMaster.Add(BloodbankMaster);
            return Ok(BloodbankMaster.BloodBankID);
        }

        [HttpPost("UpdateBloodbankMaster")]
        public IActionResult UpdateBloodbankMaster(BloodbankMaster BloodbankMaster)
        {
            _unitOfWork.BloodbankMaster.Update(BloodbankMaster);
            _unitOfWork.Save();
            return Ok(BloodbankMaster.BloodbankMasterID);
        }

        [HttpDelete("DeleteBloodbankMaster")]
        public IActionResult DeleteBloodbankMaster(long BloodbankMasterId)
        {

            // Optionally, you could check if the BloodbankMaster record exists before updating
            var existingBloodbankMaster = _unitOfWork.BloodbankMaster.GetById(BloodbankMasterId);
            if (existingBloodbankMaster == null)
            {
                return NotFound($"BloodbankMaster with ID {BloodbankMasterId} not found.");
            }

            // Update the BloodbankMaster information
            existingBloodbankMaster.IsActive = false;
            _unitOfWork.BloodbankMaster.Update(existingBloodbankMaster);
            _unitOfWork.Save();

            return Ok(new { BloodbankMasterID = BloodbankMasterId, Message = "BloodbankMaster deleted successfully." });
        }




    }
}
