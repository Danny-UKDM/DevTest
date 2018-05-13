using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DevTest.Models
{
	public class MemberRepository : IMemberRepository
	{
		private readonly MemberContext _context;
		private readonly ILogger<MemberRepository> _logger;

		public MemberRepository(MemberContext context, ILogger<MemberRepository> logger)
		{
			_context = context;
			_logger = logger;
		}

		public void AddMember(Member member)
		{
			_context.Add(member);
		}

		public async Task<bool> SaveChangesAsync()
		{
			_logger.LogInformation("Saving changes to the Database");

			return (await _context.SaveChangesAsync()) > 0;
		}
	}
}
