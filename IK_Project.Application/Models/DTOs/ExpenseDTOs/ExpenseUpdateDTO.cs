using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Models.DTOs.ExpenseDTOs
{
    public class ExpenseUpdateDTO
    {
        public int Id { get; set; }
        public string ExpenseFilePath { get; set; }
        public int Amount { get; set; }
        public DateTime? DateOfReply { get; set; }
        public CurrecyUnit CurrencyUnit { get; set; }
        public string ReasonOfRejection { get; set; }
        public string Description { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public ConfirmationStatus ConfirmationStatus { get; set; } 

        public Guid EmployeeId { get; set; }
        public Guid AppUserId { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } 
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status? Status { get; set; }

        public Employee? Employee { get; set; }
    }
}
