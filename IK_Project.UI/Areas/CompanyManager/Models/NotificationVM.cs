using IK_Project.Application.Models.DTOs.AdvanceDTOs;
using IK_Project.Application.Models.DTOs.ExpenseDTOs;
using IK_Project.Application.Models.DTOs.PermissionDTOs;
using IK_Project.Domain.Entities.Concrete;
using IK_Project.UI.Areas.Employee.Models.AdvanceVMs;
using IK_Project.UI.Areas.Employee.Models.ExpenseVMs;
using IK_Project.UI.Areas.Employee.Models.PermissionVMs;

namespace IK_Project.UI.Areas.CompanyManager.Models
{
    public class NotificationVM
    {
       public List<PermissionListVM>? Permissions { get; set; }
        public List<AdvanceListVM>? Advances { get; set; }
        public List<ExpenseListVM>? Expenses { get; set; }
    }
}
