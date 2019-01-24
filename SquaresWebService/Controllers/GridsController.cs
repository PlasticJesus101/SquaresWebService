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
    public class GridsController : ControllerBase
    {
		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<GameGrid>> Get()
		{
			FootballDataContext context = HttpContext.RequestServices.GetService(typeof(FootballDataContext)) as FootballDataContext;
			return context.GetAllGrids();
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<GameGrid> Get(int id)
		{
			FootballDataContext context = HttpContext.RequestServices.GetService(typeof(FootballDataContext)) as FootballDataContext;
			return context.GetGrid((UInt32)id);
		}

	}
}