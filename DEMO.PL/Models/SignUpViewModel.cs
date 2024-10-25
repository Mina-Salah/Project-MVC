using System.ComponentModel.DataAnnotations;

namespace DEMO.PL.Models
{
    public class SignUpViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid format for Email")]
        public string Email { get; set; }
        [Required]
        [MaxLength(7)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password),ErrorMessage ="password not match")]
		public string confirmPassword { get; set; }
        [Required]
        public bool ISAgree { get; set; }   
    }
}
