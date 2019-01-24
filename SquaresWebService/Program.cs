using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SquaresWebService
{
	public class Program
	{
//		public static SquaresDataSource DataSource { get; set; }

		public static void Main(string[] args)
		{
			//DataSource = DataSourceFactory.Create<SquaresDataSource>(new SqlDBCredentials(SqlDataSource.MYSQL_NATIVE_DRIVER, "thufir", "squares", "squares_app", "Squares2019"));

			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
