using HMIS.API.Service;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceProviderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public InsuranceProviderController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetInsuranceProviderById(int id)
        {
            var docFromRepo = _unitOfWork.InsuranceProvider.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllInsuranceProvider")]
        public IActionResult GetAllInsuranceProvider()
        {
            var docFromRepo = _unitOfWork.InsuranceProvider.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.InsuranceProviderID);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateInsuranceProvider")]
        public IActionResult CreateInsuranceProvider(InsuranceProvider InsuranceProvider)
        {
            _unitOfWork.InsuranceProvider.Add(InsuranceProvider);
            return Ok(InsuranceProvider.InsuranceProviderID);
        }

        [HttpPost("UpdateInsuranceProvider")]
        public IActionResult UpdateInsuranceProvider(InsuranceProvider InsuranceProvider)
        {
            _unitOfWork.InsuranceProvider.Update(InsuranceProvider);
            _unitOfWork.Save();
            return Ok(InsuranceProvider.InsuranceProviderID);
        }
    }


}

