using HMIS.API.Service;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodDonorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public BloodDonorController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetBloodDonorById(int id)
        {
            var docFromRepo = _unitOfWork.BloodDonor.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllBloodDonor")]
        public IActionResult GetAllBloodDonor()
        {
            var docFromRepo = _unitOfWork.BloodDonor.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.BloodDonorID);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateBloodDonor")]
        public IActionResult CreateBloodDonor(BloodDonor BloodDonor)
        {
            _unitOfWork.BloodDonor.Add(BloodDonor);
            return Ok(BloodDonor.BloodDonorID);
        }

        [HttpPost("UpdateBloodDonor")]
        public IActionResult UpdateBloodDonor(BloodDonor BloodDonor)
        {
            _unitOfWork.BloodDonor.Update(BloodDonor);
            _unitOfWork.Save();
            return Ok(BloodDonor.BloodDonorID);



        }

        [HttpDelete("DeleteBloodDonor")]
        public IActionResult DeleteBloodDonor(long BloodDonorId)
        {

            // Optionally, you could check if the BloodDonor record exists before updating
            var existingBloodDonor = _unitOfWork.BloodDonor.GetById(BloodDonorId);
            if (existingBloodDonor == null)
            {
                return NotFound($"BloodDonor with ID {BloodDonorId} not found.");
            }

            // Update the BloodDonor information
            existingBloodDonor.IsActive = false;
            _unitOfWork.BloodDonor.Update(existingBloodDonor);
            _unitOfWork.Save();

            return Ok(new { BloodDonorID = BloodDonorId, Message = "BloodDonor deleted successfully." });
        }



    }
}
