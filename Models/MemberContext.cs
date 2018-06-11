using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace DevTest.Models
{
	public class MemberContext : IMemberContext
	{
		private readonly IConfigurationRoot _config;

		public MemberContext(IConfigurationRoot config)
		{
			_config = config;
		}

		public bool AddMember(Member member)
		{
			string commandText = @"
			
	
if exists select 1 from dbo.Users where email = @email then
	return 0
else 
	INSERT into dbo.Users (Email, Password) VALUES (@Email, @Password)
	select 1
end if		
			
			";
			string passwordHash = HashPassword(member.Password);

			bool result = false;

			using (var con = new SqlConnection(_config.GetConnectionString("MemberContextConnection")))
			{
				var cmd = new SqlCommand(commandText, con);

				cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 256).Value = member.Email;
				cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 450).Value = passwordHash;

				con.Open();
				 result = Convert.ToBoolean(cmd.ExecuteScalar());
				con.Close();
			}

			return result;
		}

		public string HashPassword(string password)
		{
			byte[] salt = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));

			return hashed;
		}
	}
}
