using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevTest.Models;

namespace DevTest.Controllers
{
	public class AppController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
