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
    public class MenuRoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public MenuRoleController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetMenuRoleById(int id)
        {
            var docFromRepo = _unitOfWork.MenuRoles.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetMenuRoles")]
        public IActionResult GetMenuRoles()
        {
            var docFromRepo = _unitOfWork.MenuRoles.GetAll();
            return Ok(docFromRepo);
        }

        [HttpPost("CreateMenus")]
        public IActionResult CreateMenuRole(MenuRole menu)
        {
            _unitOfWork.MenuRoles.Add(menu);
            _unitOfWork.Save();
            return Ok(menu.MenuId);

        }

        [HttpPost("UpdateMenuRole")]
        public IActionResult UpdateMenuRole(MenuRole menu)
        {
            _unitOfWork.MenuRoles.Update(menu);
            _unitOfWork.Save();
            return Ok(menu.RoleMasterId);
        }

        [HttpDelete("DeleteMenuRole/{Id}")]
        public IActionResult DeleteMenuRole(int Id)
        {

            // Optionally, you could check if the Holiday record exists before updating
            var existingHoliday = _unitOfWork.MenuRoles.GetById(Id);
            if (existingHoliday == null)
            {
                return NotFound($"MenuRole with ID {Id} not found.");
            }

            _unitOfWork.MenuRoles.Remove(existingHoliday);
            _unitOfWork.Save();

            return Ok(new { ID = Id, Message = "Menu Role deleted successfully." });
        }



    }
}
