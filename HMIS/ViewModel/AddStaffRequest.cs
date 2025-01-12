using HMIS.Domain.Entities;

namespace HMIS.API.ViewModel
{
    public class AddStaffRequest
    {
        public IFormFile[] Files { get; set; }
        public IFormFile StaffPic { get; set; }
        public Staff Doc { get; set; }
    }

}
