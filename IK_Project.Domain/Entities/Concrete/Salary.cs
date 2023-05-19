using IK_Project.Domain.Entities.Abstract;
using IK_Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Domain.Entities.Concrete
{
    public class Salary:IEntity<int>, IBaseEntity
    {
        public int Id { get; set; }
        public int SalaryAmount { get; set; }
        public CurrecyUnit CurrecyUnit { get; set; }
        public int SalaryYear { get; set; }
        public int SalaryMonth { get; set; }
        public Guid EmployeeID { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status? Status { get; set; }
    }
}
