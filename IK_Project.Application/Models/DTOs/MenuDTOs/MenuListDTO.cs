using IK_Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Application.Models.DTOs.MenuDTOs
{
    public class MenuListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int UserAmount { get; set; }
        public CurrecyUnit Currecy { get; set; }
        public Status Status { get; set; }
    }
}
