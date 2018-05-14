using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevTest.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DevTest.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;

namespace DevTest.Controllers
{
	public class AppController : Controller
	{
		private readonly IMemberRepository _repository;
		private readonly ILogger<AppController> _logger;
		private readonly UserManager<Member> _userManager;

		public AppController(IMemberRepository repository, ILogger<AppController> logger, UserManager<Member> userManager)
		{
			_repository = repository;
			_logger = logger;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpPost("api/members")]
		public async Task<IActionResult> Post([FromBody]MemberViewModel member)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (await _userManager.FindByEmailAsync(member.Email) == null)
					{
						var newMember = Mapper.Map<Member>(member);
						newMember.UserName = newMember.Email;

						var password = member.Password;

						var result = await _userManager.CreateAsync(newMember, password);

						if (result.Succeeded)
							return Created($"api/members", Mapper.Map<MemberViewModel>(member));

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
