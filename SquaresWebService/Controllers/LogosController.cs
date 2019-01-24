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
    public class LogosController : ControllerBase
    {
		// GET: api/logos/h/1
		[HttpGet("h/{id}")]
		public IActionResult GetHorizontal(int id)
		{
			FootballDataContext context = HttpContext.RequestServices.GetService(typeof(FootballDataContext)) as FootballDataContext;
			Team team = context.GetTeam((UInt32)id);
			return File(team.HorizontalLogoBytes, "image/png");
		}

		// GET: api/logos/v/1
		[HttpGet("v/{id}")]
		public IActionResult GetVertical(int id)
		{
			FootballDataContext context = HttpContext.RequestServices.GetService(typeof(FootballDataContext)) as FootballDataContext;
			Team team = context.GetTeam((UInt32)id);
			return File(team.VerticalLogoBytes, "image/png");
		}
	}
}