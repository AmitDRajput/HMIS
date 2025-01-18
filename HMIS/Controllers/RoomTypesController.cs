using HMIS.API.Service;
using HMIS.Domain.Entities;
using HMIS.Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.API.Controllers
{



    public class RoomTypesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        public RoomTypesController(IUnitOfWork unitOfWork, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetRoomTypesById(int id)
        {
            var docFromRepo = _unitOfWork.RoomTypes.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllRoomTypes")]
        public IActionResult GetAllRoomTypes()
        {
            var docFromRepo = _unitOfWork.RoomTypes.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.RoomTypesId);
            return Ok(docFromRepo);
        }


        [HttpPost("CreateRoomTypes")]
        public IActionResult CreateRoomTypes([FromBody] RoomTypes roomTypes)
        {
            // Check if roomTypes is null
            if (roomTypes == null)
            {
                return BadRequest("Invalid RoomTypes data."); // Return BadRequest if roomTypes is null
            }

            // Add the roomTypes to the database
            _unitOfWork.RoomTypes.Add(roomTypes);
            _unitOfWork.Save(); // Ensure changes are saved to the database

            // Return success response with the created RoomTypesId
            return Ok(new { RoomTypesId = roomTypes.RoomTypesId });
        }

        [HttpPut("UpdateRoomTypes/{id}")]
        public IActionResult UpdateRoomTypes(int id, [FromBody] RoomTypes roomTypes)
        {
            // Check if the roomTypes object is null
            if (roomTypes == null)
            {
                return BadRequest("Invalid RoomTypes data.");
            }

            // Check if the roomType exists in the database
            var existingRoomType = _unitOfWork.RoomTypes.GetById(id); 
            if (existingRoomType == null)
            {
                return NotFound($"RoomType with id {id} not found.");
            }

            existingRoomType.RoomTypesId = roomTypes.RoomTypesId; 
          

            _unitOfWork.RoomTypes.Update(existingRoomType); // Assuming an Update method is available
            _unitOfWork.Save(); // Save changes to the database

            return Ok(new { RoomTypesId = existingRoomType.RoomTypesId });
        }

        [HttpDelete("DeleteRoomTypes")]
        public IActionResult DeleteRole(long RoomTypesId)
        {

            // Optionally, you could check if the RoomTypes record exists before updating
            var existingRoomTypes = _unitOfWork.RoomTypes.GetById(RoomTypesId);
            if (existingRoomTypes == null)
            {
                return NotFound($"Role with ID {RoomTypesId} not found.");
            }

            // Update the RoomTypes information
            existingRoomTypes.IsActive = false;
            _unitOfWork.RoomTypes.Update(existingRoomTypes);
            _unitOfWork.Save();

            return Ok(new { RoomTypesId = RoomTypesId, Message = "RoomTypes deleted successfully." });
        }











    }
}   
