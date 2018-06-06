using System.ComponentModel.DataAnnotations;

namespace DevTest.ViewModels
{
	public class MemberViewModel
	{
		[Required]
		[EmailAddress (ErrorMessage = "Not a valid email address")]
		public string Email { get; set; }

		[Required]
		[MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
		public string Password { get; set; }
	}
}
