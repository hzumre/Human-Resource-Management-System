using IK_Project.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace IK_Project.UI.Areas.Admin.Models.ViewModels.RoleVMs
{
    public class RoleEditVM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Role required.")]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}
