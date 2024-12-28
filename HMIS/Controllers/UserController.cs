using HMIS.API.Service;
using HMIS.DataAccess.Implementation;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public UserController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]UserMaster model)
        {
            var user = _unitOfWork.UserMaster.GetUserByUsernameAndPassword(model.Username, model.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _tokenService.GenerateJwtToken(user);

            var userDto = new UserMasterDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                token = token  // Include the token in the DTO
            };

            return Ok( userDto );
        }

        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            var docFromRepo = _unitOfWork.UserMaster.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var docFromRepo = _unitOfWork.UserMaster.GetAll().OrderByDescending(x => x.Id);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserMaster doc)
        {
            _unitOfWork.UserMaster.Add(doc);
            return Ok(doc.Id);
        }

    }
}
