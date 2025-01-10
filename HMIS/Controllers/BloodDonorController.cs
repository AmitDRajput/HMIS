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

    }
}
