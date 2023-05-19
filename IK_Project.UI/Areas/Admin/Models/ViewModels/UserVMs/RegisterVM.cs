using System.ComponentModel.DataAnnotations;

namespace IK_Project.UI.Areas.Admin.Models.ViewModels.UserVMs
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Kullanıcı Adını Boş Geçemezsiniz.")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parolayı Boş Geçemezsiniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parola Tekrarı Boş Geçemezsiniz.")]
        [Compare("Password", ErrorMessage = "Parola ile Parola Tekrar Aynı Değil")]
        [DataType(DataType.Password)]
        [Display(Name = "ParolaTekrar")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email Boş Geçemezsiniz.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "Adres")]
        public string Address { get; set; }
    }
}
