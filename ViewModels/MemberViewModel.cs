using System;
using System.ComponentModel.DataAnnotations;

namespace DevTest.ViewModels
{
	public class MemberViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public DateTime DateCreated { get; set; } = DateTime.Now;
	}
}
