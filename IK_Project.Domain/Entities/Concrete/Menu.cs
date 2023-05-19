using IK_Project.Domain.Entities.Abstract;
using IK_Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Domain.Entities.Concrete
{
    public class Menu : IEntity<int>, IBaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Period { get; set; }
        public decimal? UnitPrice { get; set; }

        public DateTime? DeletedDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? UserAmount { get; set; }
        public CurrecyUnit? Currecy { get; set; }


        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status? Status { get; set; }
    }
}
