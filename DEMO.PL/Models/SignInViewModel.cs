using System.ComponentModel.DataAnnotations;

namespace DEMO.PL.Models
{
	public class SignInViewModel
	{

		[Required]
		[EmailAddress(ErrorMessage = "Invalid format for Email")]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		public bool RememderME { get; set; }
	}
}
