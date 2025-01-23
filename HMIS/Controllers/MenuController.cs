using HMIS.API.Service;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public MenuController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetMenuById(int id)
        {
            var docFromRepo = _unitOfWork.Menus.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllMenus")]
        public IActionResult GetAllMenus()
        {
            var docFromRepo = _unitOfWork.Menus.GetAll();
            return Ok(docFromRepo);
        }

        [HttpPost("CreateMenus")]
        public IActionResult CreateMenus(Menu menu)
        {
            _unitOfWork.Menus.Add(menu);
            _unitOfWork.Save();
            return Ok(menu.Id);

        }

        [HttpPost("UpdateMenu")]
        public IActionResult UpdateMenu(Menu menu)
        {
            _unitOfWork.Menus.Update(menu);
            _unitOfWork.Save();
            return Ok(menu.Id);
        }

        [HttpDelete("DeleteMenu/{Id}")]
        public IActionResult DeleteMenu(int Id)
        {

            // Optionally, you could check if the Holiday record exists before updating
            var existingHoliday = _unitOfWork.Menus.GetById(Id);
            if (existingHoliday == null)
            {
                return NotFound($"Holiday with ID {Id} not found.");
            }

            _unitOfWork.Menus.Remove(existingHoliday);
            _unitOfWork.Save();

            return Ok(new { ID = Id, Message = "Menu deleted successfully." });
        }



    }
}
