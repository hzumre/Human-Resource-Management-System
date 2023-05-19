using System.ComponentModel.DataAnnotations;

namespace IK_Project.UI.Models
{
    public class UpdatePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Please write your password.")]
        public string Password { get; set; }
    }
}
