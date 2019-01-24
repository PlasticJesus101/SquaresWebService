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
    public class GridSquaresController : ControllerBase
    {
		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<GridSquare>> Get()
		{
			GridSquaresContext context = HttpContext.RequestServices.GetService(typeof(GridSquaresContext)) as GridSquaresContext;
			return context.GetAllSquares();
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<GridSquare> Get(int id)
		{
			return new GridSquare(0, 0, 0, 0, 0);
		}
	}
}