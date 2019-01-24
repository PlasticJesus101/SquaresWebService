using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GridData;
using MySql.Data.MySqlClient;

namespace SquaresWebService.Models
{
	public class GridSquaresContext
	{
		public string ConnectionString { get; set; }

		public GridSquaresContext(string connectionString)
		{
			this.ConnectionString = connectionString;
		}

		public List<GridSquare> GetAllSquares()
		{
			List<GridSquare> list = new List<GridSquare>();

			Console.WriteLine("\n\n***************        GetAllSquares 1");

			try
			{
				Console.WriteLine("\n\n***************        GetAllSquares 2");
				using(MySqlConnection conn = GetConnection())
				{
					Console.WriteLine("\n\n***************        GetAllSquares 3");
					conn.Open();
					MySqlCommand cmd = new MySqlCommand("select * from squares", conn);
					using(var reader = cmd.ExecuteReader())
					{
						Console.WriteLine("\n\nMYSQL reader = {0}\n\n", reader == null ? "NULL" : reader.ToString());
						while(reader.Read())
						{
							list.Add(new GridSquare()
							{
								SquareID = Convert.ToUInt32(reader["square_id"]),
								GridID = Convert.ToUInt32(reader["grid_id"]),
								OwnerID = Convert.ToUInt32(reader["owner_id"]),
								Row = Convert.ToUInt32(reader["row"]),
								Column = Convert.ToUInt32(reader["col"]),
							});
						}
					}
				}
			}
			catch(Exception e)
			{
				Console.WriteLine("EXCEPTION: {0}", e.Message);
			}
			return list;
		}

		private MySqlConnection GetConnection()
		{
			return new MySqlConnection(ConnectionString);
		}
	}
}
