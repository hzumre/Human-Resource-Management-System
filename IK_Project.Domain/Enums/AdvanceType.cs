using IK_Project.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IK_Project.Domain.Enums
{
    public enum AdvanceType
    {
        [Display(Name = "Wage Advance")]
        WageAdvance,
        [Display(Name = "Job Advance")]
        JobAdvance,
        [Display(Name = "Annual Paid Advance")]
        AnnualPaidAdvance


    }
}
