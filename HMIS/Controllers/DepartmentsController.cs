using HMIS.API.Service;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]

    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public DepartmentsController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetDepartmentsById(int id)
        {
            var docFromRepo = _unitOfWork.Departments.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllDepartments")]
        public IActionResult GetAllRoleMasters()
        {
            var docFromRepo = _unitOfWork.Departments.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.DepartmentId);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateDepartments")]
        public IActionResult CreateDepartments(Departments departments)
        {
            _unitOfWork.Departments.Add(departments);
            _unitOfWork.Save();
            return Ok(departments.DepartmentId);
        }

        [HttpPost("UpdateDepartments")]
        public IActionResult UpdateRoleMaster(Departments departments)
        {
            _unitOfWork.Departments.Update(departments);
            _unitOfWork.Save();
            return Ok(departments.DepartmentId);
        }


    }
}
