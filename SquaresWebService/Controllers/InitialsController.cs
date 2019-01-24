using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GridData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using SquaresWebService.Models;

namespace SquaresWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialsController : ControllerBase
    {
		private readonly IHostingEnvironment _hostingEnvironment;

		public InitialsController(IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
		}

		// GET: api/photos/1
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			if(id != 0)
			{
				FootballDataContext context = HttpContext.RequestServices.GetService(typeof(FootballDataContext)) as FootballDataContext;
				SquareOwner owner = context.GetOwner((UInt32)id);
				return File(owner.InitialsBytes, "image/png");
			}
			else
			{
				FileStream image = System.IO.File.OpenRead("grids/empty_square.png");
				return File(image, "image/png");
			}
		}

		[HttpPost("{id}")]
		public async Task Post(IFormFile file)
		{
			var uploads = Path.Combine(".", "uploads");
			if(file.Length > 0)
			{
				using(var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}
			}
		}
	}
}