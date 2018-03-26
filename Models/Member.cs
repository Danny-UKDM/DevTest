using System;

namespace DevTest.Models
{
	public class Member
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
