using AutoMapper;
using DevTest.Models;
using DevTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DevTest.Controllers.Api
{
	[Route("api/members")]
	public class MembersController : Controller
	{
		private readonly IMemberRepository _repository;
		private readonly ILogger<MembersController> _logger;

		public MembersController(IMemberRepository repository, ILogger<MembersController> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpPost("")]
		public async Task<IActionResult> Post([FromBody]MemberViewModel member)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var newMember = Mapper.Map<Member>(member);

					_repository.AddMember(newMember);

					if (await _repository.SaveChangesAsync())
					{
						return Created($"api/members", Mapper.Map<MemberViewModel>(newMember));
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to save new Member: {0}", ex);
			}

			return BadRequest("Failed to save new Member");
		}
	}
}
