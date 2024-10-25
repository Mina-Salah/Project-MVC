using System.ComponentModel.DataAnnotations;

namespace DEMO.PL.Models
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }   
    }
}
