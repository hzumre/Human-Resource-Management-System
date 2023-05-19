using System.ComponentModel.DataAnnotations;

namespace IK_Project.UI.Models
{
    public class ResetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Please enter your e-mail address registered in the HR system.")]
        public string Email { get; set; }
    }
}
