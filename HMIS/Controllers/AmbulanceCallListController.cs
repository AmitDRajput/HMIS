using HMIS.API.Service;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmbulanceCallListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public AmbulanceCallListController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetAmbulanceCallListById(int id)
        {
            var docFromRepo = _unitOfWork.AmbulanceCallList.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllAmbulanceCallList")]
        public IActionResult GetAllAmbulanceCallList()
        {
            var docFromRepo = _unitOfWork.AmbulanceCallList.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.AmbulanceID);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateAmbulanceCallList")]
        public IActionResult CreateAmbulanceCallList(AmbulanceCallList AmbulanceCallList)
        {
            _unitOfWork.AmbulanceCallList.Add(AmbulanceCallList);
            return Ok(AmbulanceCallList.AmbulanceID);
        }

        [HttpPost("UpdateAmbulanceCallList")]
        public IActionResult UpdateAmbulanceCallList(AmbulanceCallList AmbulanceCallList)
        {
            _unitOfWork.AmbulanceCallList.Update(AmbulanceCallList);
            _unitOfWork.Save();
            return Ok(AmbulanceCallList.AmbulanceID);
        }


        [HttpDelete("DeleteAmbulanceCallList")]
        public IActionResult DeleteStaff(long AmbulanceCallListId)
        {

            // Optionally, you could check if the AmbulanceCallList record exists before updating
            var existingAmbulanceCallList = _unitOfWork.AmbulanceCallList.GetById(AmbulanceCallListId);
            if (existingAmbulanceCallList == null)
            {
                return NotFound($"AmbulanceCallList with ID {AmbulanceCallListId} not found.");
            }

            // Update the AmbulanceCallList information
            existingAmbulanceCallList.IsActive = false;
            _unitOfWork.AmbulanceCallList.Update(existingAmbulanceCallList);
            _unitOfWork.Save();

            return Ok(new { AmbulanceCallListID = AmbulanceCallListId, Message = "AmbulanceCallList deleted successfully." });
        }




    }


}

