using System.ComponentModel.DataAnnotations;

namespace IK_Project.UI.Areas.Admin.Models.ViewModels
{
    public class LoginVM
    {
        //[StringLength(20, ErrorMessage = "Please enter username between 4 and 20 characters...", MinimumLength = 4)]
        [Required(ErrorMessage = "*Username is required.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*Password is required.")]
        public string Password { get; set; }
    }
}
