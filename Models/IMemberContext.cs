namespace DevTest.Models
{
	public interface IMemberContext
	{
		void AddMember(Member member);

		bool CheckIfMemberExists(string email);

		string HashPassword(string password);
	}
}