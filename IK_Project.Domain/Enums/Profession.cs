using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IK_Project.Domain.Enums
{
    public enum Profession
    {

        [Display(Name = "General Manager")]
        GeneralManager,
        Expert,
        Engineer,
        Officer,
        [Display(Name = "Finance Manager")]
        Finance,
        [Display(Name = "Sales Manager")]
        Sales,
        [Display(Name = "Project Manager")]
        Project,
        [Display(Name = "HR Manager")]
        HumanResources,
        [Display(Name = "Marketing Manager")]
        Marketing,
        [Display(Name = "Production Manager")]
        Production
    }
}
