using IK_Project.Domain.Entities.Concrete;
using IK_Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Models.DTOs.EmployeeDTOs
{
    public class EmployeeCreateDTO
    {
        public Guid Id { get; set; }

        public Status? Status { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public string? LastName { get; set; }
        public string? SecondLastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirhtPlace { get; set; }
        public string? CitizenId { get; set; }
        public DateTime? StartDate { get; set; }
        public Profession? Profession { get; set; }
        public Department? Department { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? Salary { get; set; }

        public int? CompanyID { get; set; }
        public Guid AppUserID { get; set; }

        //Navigtaion Property
        public Company? Company { get; set; }
        public AppUser? AppUser { get; set; }

        public string? userPassword { get; set; }
        //public IK_Project.Domain.Entities.Concrete.AppRole AppRole { get; set; }
    }
}
