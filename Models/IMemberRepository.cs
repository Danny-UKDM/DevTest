using System.Threading.Tasks;

namespace DevTest.Models
{
	public interface IMemberRepository
	{
		void AddMember(Member member);

		Task<bool> SaveChangesAsync();
	}
}