using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IK_Project.Domain.Enums
{
    public enum PermissionType
    {
        [Display(Name = "Annual Permit")]
        AnnualPermit = 1,
        [Display(Name = "Maternity Leave")]
        MaternityLeave,
        [Display(Name = "Marriage Permission")]
        MarriagePermission,
        [Display(Name = "Death Permission")]
        DeathPermission,
        [Display(Name = "Paternity Leave")]
        PaternityLeave,
        [Display(Name = "Breast Feeding Leave")]
        BreastFeedingLeave,
        [Display(Name = "Disability Treatment Leave")]
        DisabilityTreatmentLeave,
        [Display(Name = "Periodic Control Permission")]
        PeriodicControlPermission,
        [Display(Name = "Military Duty Leave")]
        MilitaryDutyLeave,
        [Display(Name = "New Job Search Permit")]
        NewJobSearchPermit
    }
}
