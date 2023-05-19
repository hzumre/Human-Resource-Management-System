
using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IK_Project.Application.Models.DTOs.PermissionDTOs
{
    public class PermissionListDTO
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

        public Employee? Employee { get; set; }
    }
}