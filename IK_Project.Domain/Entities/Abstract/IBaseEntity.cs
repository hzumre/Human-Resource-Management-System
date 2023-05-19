using IK_Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Domain.Entities.Abstract
{
    public interface IBaseEntity
    {
        string? CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        string? ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        Status? Status { get; set; }
    }
}
