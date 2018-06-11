namespace DevTest.Models
{
	public interface IMemberContext
	{
		bool AddMember(Member member);

		string HashPassword(string password);
	}
}