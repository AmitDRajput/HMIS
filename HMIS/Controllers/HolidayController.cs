using HMIS.API.Service;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public HolidayController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetHolidayById(int id)
        {
            var docFromRepo = _unitOfWork.Holiday.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllHoliday")]
        public IActionResult GetAllHoliday()
        {
            var docFromRepo = _unitOfWork.Holiday.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.HolidayID);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateHoliday")]
        public IActionResult CreateHoliday(Holiday Holiday)
        {
            _unitOfWork.Holiday.Add(Holiday);
            _unitOfWork.Save();
            return Ok(Holiday.HolidayID);

        }

        [HttpPost("UpdateHoliday")]
        public IActionResult UpdateHoliday(Holiday Holiday)
        {
            _unitOfWork.Holiday.Update(Holiday);
            _unitOfWork.Save();
            return Ok(Holiday.HolidayID);
        }


    }
}
