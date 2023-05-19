using IK_Project.Domain.Entities.Abstract;
using IK_Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Domain.Entities.Concrete
{
    public class Employee :PersonEntity ,IEntity<Guid>, IBaseEntity
    {
        public Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status? Status { get; set; }

        public decimal? Salary { get; set; }
        public List<Expense>? Expenses { get; set; }
        public List<Advance>? Advances { get; set; }
        public List<Permission>? Permissions { get; set; }


    }
}
