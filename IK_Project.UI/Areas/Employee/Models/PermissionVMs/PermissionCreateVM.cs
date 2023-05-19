using IK_Project.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace IK_Project.UI.Areas.Employee.Models.PermissionVMs
{
    public class PermissionCreateVM
    {
        public string? PermissionFilePath { get; set; }
        public PermissionType PermissionType { get; set; }
        [Required(ErrorMessage = "*Start Date is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "*End Date is required")]
        public DateTime EndDate { get; set; } 
        public int NumberOfDays { get; set; }
        public DateTime? DateOfReply { get; set; }
        public ConfirmationStatus ConfirmationStatus { get; set; } = ConfirmationStatus.Pending;
        public string? ReasonOfRejection { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid AppUserId { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status? Status { get; set; }

        public TimeSpan DateDifference => EndDate - StartDate;
        //public int DateDifference
        //{
        //    get
        //    {
        //        TimeSpan span = EndDate.Subtract(StartDate);
        //        return span.Days;
        //    }
        //    set
        //    {

        //    }
        //}

        public IK_Project.Domain.Entities.Concrete.Employee? Employee { get; set; }
    }
}
