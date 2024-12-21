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
            var docFromRepo = _unitOfWork.NavItem.GetAll().OrderByDescending(x => x.Id);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateNavItem")]
        public IActionResult CreateNavItem(NavItem nav)
        {
            _unitOfWork.NavItem.Add(nav);
            _unitOfWork.Save();
            return Ok(nav.Id);
        }

        [HttpPost("UpdateNavItem")]
        public IActionResult UpdateNavItem(NavItem nav)
        {
            _unitOfWork.NavItem.Update(nav);
            _unitOfWork.Save();
            return Ok(nav.Id);
        }

        [HttpPost("DeleteNavItem")]
        public IActionResult DeleteNavItem(NavItem appt)
        {
            _unitOfWork.NavItem.Remove(appt);
            _unitOfWork.Save();
            return Ok();
        }

    }
}
