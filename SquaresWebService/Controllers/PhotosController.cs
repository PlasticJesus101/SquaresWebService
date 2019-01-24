using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GridData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SquaresWebService.Models;

namespace SquaresWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
		// GET: api/photos/1
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			FootballDataContext context = HttpContext.RequestServices.GetService(typeof(FootballDataContext)) as FootballDataContext;
			SquareOwner owner = context.GetOwner((UInt32)id);
			return File(owner.PhotoBytes, "image/png");
		}
	}
}