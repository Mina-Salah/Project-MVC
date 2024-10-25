using System.ComponentModel.DataAnnotations;

namespace DEMO.PL.Models
{
    public class ResetPasswordViewModel
    {


        [Required]
        [MaxLength(7)]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "password not match")]
        public string confirmPassword { get; set; }
    }
}
