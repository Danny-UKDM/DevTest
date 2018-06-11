using Microsoft.AspNetCore.Mvc;
using DevTest.Models;
using Microsoft.Extensions.Logging;
using DevTest.ViewModels;
using AutoMapper;
using System;

namespace DevTest.Controllers
{
	public class AppController : Controller
	{
		private readonly IMemberContext _context;
		private readonly ILogger<AppController> _logger;

		public AppController(IMemberContext context, ILogger<AppController> logger)
		{
			_context = context;
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Success()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Post([FromForm]MemberViewModel member)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var newMember = Mapper.Map<Member>(member);
					if(_context.AddMember(newMember)) {
						return View("Success", member);
					} else {
						ModelState.AddModelError("Error", "Email already exists");
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to save new Member: {0}", ex);
			}

			return View("Error", new ErrorViewModel()
			{
				Email = member.Email,
				Message = ModelState.ValidationState.ToString()
			});
		}
	}
}
