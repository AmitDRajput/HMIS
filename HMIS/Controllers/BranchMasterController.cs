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
    public class BranchMasterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BranchMasterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetBranchById(int id)
        {
            var docFromRepo = _unitOfWork.BranchMaster.GetById(id);
            return Ok(docFromRepo);
        }

        [HttpGet("GetAllBranch")]
        public IActionResult GetAllBranch()
        {
            var docFromRepo = _unitOfWork.BranchMaster.GetAll().Where(x => x.IsActive == true).OrderByDescending(x => x.BranchId);
            return Ok(docFromRepo);
        }

        [HttpPost("CreateBranch")]
        public IActionResult CreateBranch(BranchMaster Branch)
        {
            _unitOfWork.BranchMaster.Add(Branch);
            _unitOfWork.Save();
            return Ok(Branch.BranchId);

        }


        [HttpDelete("DeleteBranchMaster")]
        public IActionResult DeleteBranch(long BranchId)
        {

            // Optionally, you could check if the BranchMaster record exists before updating
            var existingBranchMaster = _unitOfWork.BranchMaster.GetById(BranchId);
            if (existingBranchMaster == null)
            {
                return NotFound($"Branch with ID {BranchId} not found.");
            }

            // Update the BranchMaster information
            existingBranchMaster.IsActive = false;
            _unitOfWork.BranchMaster.Update(existingBranchMaster);
            _unitOfWork.Save();

            return Ok(new { BranchID = BranchId, Message = "Branch deleted successfully." });
        }



    }
}
