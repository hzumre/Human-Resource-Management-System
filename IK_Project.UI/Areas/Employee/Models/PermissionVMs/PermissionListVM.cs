using IK_Project.Domain.Enums;

namespace IK_Project.UI.Areas.Employee.Models.PermissionVMs
{
    public class PermissionListVM
    {
        public int Id { get; set; }
        public PermissionType PermissionType { get; set; }
        public DateTime StartDate { get; set; }
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

        public int DateDifference
        {
            get
            {
                TimeSpan span = EndDate.Subtract(StartDate);
                return span.Days;
            }
        }

        public IK_Project.Domain.Entities.Concrete.Employee? Employee { get; set; }
    }
}
