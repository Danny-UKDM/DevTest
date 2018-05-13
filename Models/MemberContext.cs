using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevTest.Models
{
	public class MemberContext : DbContext 
	{
		private readonly IConfigurationRoot _config;

		public MemberContext(IConfigurationRoot config, DbContextOptions options) : base(options)
		{
			_config = config;
		}

		public DbSet<Member> Members { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer(_config.GetConnectionString("MemberContextConnection"));
		}
	}
}
