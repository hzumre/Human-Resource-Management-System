using IK_Project.Domain.Entities.Concrete;
using IK_Project.Application.Models.DTOs.AppUserDTOs;
using IK_Project.Domain.Enums;
using IK_Project.Application.Models.DTOs.CompanyDTOs;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace IK_Project.UI.Areas.Admin.Models.ViewModels.CompanyManagerVMs
{
    public class CompanyManagerCreateVM
    {
        public Guid Id { get; set; }
        public string? ProfilePhoto { get; set; }
        [Required(ErrorMessage = "*Name is required.")]
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        [Required(ErrorMessage = "*Last Name is required.")]
        public string? LastName { get; set; }
        public string? SecondLastName { get; set; }
        [Required(ErrorMessage = "*Birth Date is required.")]
        public DateTime? BirthDate { get; set; }
        [Required(ErrorMessage = "*Birht Place is required.")]
        public string? BirhtPlace { get; set; }
        [Required(ErrorMessage = "*CitizenId is required.")]
        public string? CitizenId { get; set; }
        [Required(ErrorMessage = "*Start Date is required.")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "*Profession is required.")]
        public Profession? Profession { get; set; }
        [Required(ErrorMessage = "*Department is required.")]
        public Department? Department { get; set; }
        [Required(ErrorMessage = "*Email is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "*Address is required.")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "*PhoneNumber is required.")]
        public string? PhoneNumber { get; set; }
        public Guid AppUserID { get; set; }
        public int CompanyID { get; set; }
        public List<CompanyListDTO>? CompanyDTOs { get; set; }
        public Company? Company { get; set; }
        public AppUser? AppUser { get; set; }
        [Required(ErrorMessage = "*User Password is required.")]
        public string? userPassword { get; set; }
        
        public string? userName { get; set; }

        public Status Status => Status.Active;

    }
}
