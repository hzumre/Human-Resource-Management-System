using IK_Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using IK_Project.Domain.Entities.Abstract;

namespace IK_Project.Domain.Entities.Concrete
{
    public class Expense: IEntity<int>, IBaseEntity
    {
        public int Id { get; set; }
        public string? ExpenseFilePath { get; set; }
        public int? Amount { get; set; }
        public DateTime? DateOfReply { get; set; }
        public CurrecyUnit CurrencyUnit { get; set; }
        public string? ReasonOfRejection { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public ConfirmationStatus ConfirmationStatus { get; set; } = ConfirmationStatus.Pending;

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
