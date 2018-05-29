﻿using Microsoft.AspNetCore.Mvc;
using DevTest.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DevTest.ViewModels;
using AutoMapper;
using System;

namespace DevTest.Controllers
{
	public class AppController : Controller
	{
		private readonly IMemberRepository _repository;
		private readonly ILogger<AppController> _logger;

		public AppController(IMemberRepository repository, ILogger<AppController> logger)
		{
			_repository = repository;
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

		[HttpPost("")]
		public async Task<IActionResult> Post([FromForm]MemberViewModel member)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// TODO: Save to database here with ADO.NET flavour. 

					/*
					if (await _userManager.FindByEmailAsync(member.Email) == null)
					{
						var newMember = Mapper.Map<Member>(member);
						newMember.UserName = newMember.Email;

						var password = member.Password;

						var result = await _userManager.CreateAsync(newMember, password);

						if (result.Succeeded)
							return View("Success", member);


						foreach (var prop in result.Errors)
						{
							ModelState.AddModelError("Error", prop.Description);
						}


					}
					else
					{
						ModelState.AddModelError("Error", "Email already exists");
					}
					*/
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
