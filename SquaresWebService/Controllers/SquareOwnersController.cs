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
    public class SquareOwnersController : ControllerBase
    {
		// GET: api/GridOwner
		[HttpGet]
		public IEnumerable<SquareOwner> Get()
		{
			FootballDataContext context = HttpContext.RequestServices.GetService(typeof(FootballDataContext)) as FootballDataContext;
			return context.GetAllOwners();
		}

		// GET: api/GridOwner/5
		//[HttpGet("{id}", Name = "Get")]
		//public string Get(int id)
		//{
		//	return "value";
		//}

		// POST: api/GridOwner
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT: api/GridOwner/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}