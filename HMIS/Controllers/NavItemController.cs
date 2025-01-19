using HMIS.DataAccess.Implementation;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public NavItemController(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetNavItemById(int id)
        {
            var docFromRepo = _unitOfWork.RoleMaster.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllNavItems")]
        public IActionResult GetAllNavItems()
        {
            var docFromRepo = _unitOfWork.NavItem.GetAll().Where(x=> x.IsActive==true).OrderByDescending(x => x.Id);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateNavItem")]
        public IActionResult CreateNavItem(NavItem nav)
        {
            nav.IsActive = true;
            _unitOfWork.NavItem.Add(nav);
            _unitOfWork.Save();
            return Ok(nav.Id);
        }

        [HttpPost("UpdateNavItem")]
        public IActionResult UpdateNavItem(NavItem nav)
        {
            nav.IsActive = true;
            _unitOfWork.NavItem.Update(nav);
            _unitOfWork.Save();
            return Ok(nav.Id);
        }

        [HttpDelete("DeleteNavItem")]
        public IActionResult DeleteNavItem(int Id)
        {

            // Optionally, you could check if the Staff record exists before updating
            var existingStaff = _unitOfWork.NavItem.GetById(Id);
            if (existingStaff == null)
            {
                return NotFound($"Navitem with ID {Id} not found.");
            }

            // Update the Staff information
            existingStaff.IsActive = false;
            _unitOfWork.NavItem.Update(existingStaff);
            _unitOfWork.Save();

            return Ok(new { NavID = Id, Message = "Navitem deleted successfully." });
        }

       
    }
}
