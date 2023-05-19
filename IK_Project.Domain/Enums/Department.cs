using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IK_Project.Domain.Enums
{
    public enum Department
    {

        Software,
        [Display(Name = "Human Resources")]
        HumanResources,
        Informatics,
        Management,
        Production,
        Marketing,
        Service,
        Health,
        Education,
        Security,
        Finance,
        Accounting,
        Law,
        [Display(Name = "R&D")]
        RD
    }
}
