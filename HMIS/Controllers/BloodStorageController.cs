using HMIS.API.Service;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodStorageController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public BloodStorageController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetBloodStorageById(int id)
        {
            var docFromRepo = _unitOfWork.BloodStorage.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllBloodStorage")]
        public IActionResult GetAllBloodStorage()
        {
            var docFromRepo = _unitOfWork.BloodStorage.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.BloodStorageId);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateBloodStorage")]
        public IActionResult CreateBloodStorage(BloodStorage BloodStorage)
        {
            _unitOfWork.BloodStorage.Add(BloodStorage);
            return Ok(BloodStorage.BloodStorageId);
        }

        [HttpPost("UpdateBloodStorage")]
        public IActionResult UpdateBloodStorage(BloodStorage BloodStorage)
        {
            _unitOfWork.BloodStorage.Update(BloodStorage);
            _unitOfWork.Save();
            return Ok(BloodStorage.BloodStorageId);
        }
    }
}
