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

		public void AddMember(Member member)
		{
			string commandText = "INSERT into dbo.Users (Email, Password) VALUES (@Email, @Password)";
			string passwordHash = HashPassword(member.Password);

			//var email = new SqlParameter(SqlDbType);
			//var password = new SqlParameter("@Password", passwordHash);

			using (var con = new SqlConnection(_config.GetConnectionString("MemberContextConnection")))
			{
				var cmd = new SqlCommand(commandText, con);

				cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 256).Value = member.Email;
				cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 450).Value = passwordHash;

				con.Open();
				cmd.ExecuteNonQuery();
				con.Close();
			}
		}

		public bool CheckIfMemberExists(string email)
		{
			string commandText = "SELECT * FROM dbo.Users WHERE Email = @Email";
			var result = String.Empty;

			using (var con = new SqlConnection(_config.GetConnectionString("MemberContextConnection")))
			{
				var cmd = new SqlCommand(commandText, con);

				cmd.Parameters.AddWithValue("@Email", email);

				con.Open();
				result = Convert.ToString(cmd.ExecuteScalar());
				cmd.Dispose();
				con.Close();
			}

			return (result != String.Empty) ? true : false;
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
