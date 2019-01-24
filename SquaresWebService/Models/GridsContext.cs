using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GridData;
using MySql.Data.MySqlClient;

namespace SquaresWebService.Models
{
	public class GridsContext
	{
		public string ConnectionString { get; set; }

		public GridsContext(string connectionString)
		{
			this.ConnectionString = connectionString;
		}

		private MySqlConnection GetConnection()
		{
			return new MySqlConnection(ConnectionString);
		}

	}
}
