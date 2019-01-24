using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GridData;
using MySql.Data.MySqlClient;

namespace SquaresWebService.Models
{
	public class FootballDataContext
	{
		public string ConnectionString { get; set; }

		public FootballDataContext(string connectionString)
		{
			this.ConnectionString = connectionString;
		}

		public List<GameGrid> GetAllGrids()
		{
			List<GameGrid> list = new List<GameGrid>();

			try
			{
				using(MySqlConnection conn = GetConnection())
				{
					conn.Open();
					MySqlCommand cmd = new MySqlCommand("select * from grids", conn);
					using(var reader = cmd.ExecuteReader())
					{
						Console.WriteLine("\n\ncMYSQL reader = {0}\n\n", reader == null ? "NULL" : reader.ToString());
						while(reader.Read())
						{
							list.Add(new GameGrid()
							{
								GridID = Convert.ToUInt32(reader["grid_id"]),
								Name = reader["name"].ToString(),
								SquareCost = Convert.ToDecimal(reader["square_cost"]),
								Team1ID = Convert.ToUInt32(reader["team1_id"]),
								Team2ID = Convert.ToUInt32(reader["team2_id"]),
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

		public GameGrid GetGrid(UInt32 gridID)
		{
			GameGrid grid = null;

			try
			{
				List<SquareOwner> owners = null;
				using(MySqlConnection conn = GetConnection())
				{
					conn.Open();
					String sql = String.Format(
						"SELECT G.*, S.*, O.* FROM squares.grids G \n" +
						"LEFT JOIN squares.squares S on S.grid_id = G.grid_id \n" +
						"LEFT JOIN squares.owners O on O.owner_id = S.owner_id \n" +
						"WHERE G.grid_id = {0}", gridID);

					Console.WriteLine(">>>>>>>>>>>>>>>>> SQL:\n{0}\n\n", sql);

					MySqlCommand cmd = new MySqlCommand(sql, conn);
					using(var reader = cmd.ExecuteReader())
					{
						Console.WriteLine("\n\ncMYSQL reader = {0}\n\n", reader == null ? "NULL" : reader.ToString());
						while(reader.Read())
						{
							if(grid == null)
							{
								owners = GetAllOwners();
								grid = new GameGrid()
								{
									GridID = Convert.ToUInt32(reader["grid_id"]),
									Name = reader["name"].ToString(),
									SquareCost = Convert.ToDecimal(reader["square_cost"]),
									Team1ID = Convert.ToUInt32(reader["team1_id"]),
									Team2ID = Convert.ToUInt32(reader["team2_id"]),
									Squares = new List<GridSquare>()
								};

								Team team1 = GetTeam(grid.Team1ID);
								Team team2 = GetTeam(grid.Team2ID);
								grid.Team1 = team1;
								grid.Team2 = team2;
							}

							GridSquare square = new GridSquare()
							{
								SquareID = Convert.ToUInt32(reader["square_id"]),
								GridID = grid.GridID,
								OwnerID = Convert.ToUInt32(reader["owner_id"]),
								Row = Convert.ToUInt32(reader["row"]),
								Column = Convert.ToUInt32(reader["col"]),
							};
							grid.Squares.Add(square);

							if(square.OwnerID != 0)
							{
								square.Owner = owners.Find(o => o.OwnerID == square.OwnerID);
							}
							else
							{
								square.Owner = new SquareOwner(0, "--", null, null)
								{
									InitialsURL = String.Format("{0}/initials/0", Constants.BaseApiUrl),
									PhotoURL = String.Format("{0}/photos/0", Constants.BaseApiUrl)
								};
							}
						}
					}
				}
			}
			catch(Exception e)
			{
				Console.WriteLine("EXCEPTION: {0}", e.Message);
			}
			return grid;
		}

		public List<SquareOwner> GetAllOwners()
		{
			List<SquareOwner> list = new List<SquareOwner>();

			try
			{
				using(MySqlConnection conn = GetConnection())
				{
					conn.Open();
					MySqlCommand cmd = new MySqlCommand("select * from owners", conn);
					using(var reader = cmd.ExecuteReader())
					{
						Console.WriteLine("\n\ncMYSQL reader = {0}\n\n", reader == null ? "NULL" : reader.ToString());
						while(reader.Read())
						{
							SquareOwner owner = new SquareOwner()
							{
								OwnerID = Convert.ToUInt32(reader["owner_id"]),
								Name = reader["name"].ToString(),
								InitialsBytes = (byte[])reader["initials_bitmap"],
								PhotoBytes = (byte[])reader["photo_bitmap"],
							};
							owner.InitialsURL = String.Format("{0}/initials/{1}", Constants.BaseApiUrl, owner.OwnerID);
							owner.PhotoURL = String.Format("{0}/photos/{1}", Constants.BaseApiUrl, owner.OwnerID);

							list.Add(owner);
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

		public SquareOwner GetOwner(UInt32 ownerID)
		{
			SquareOwner owner = null;

			try
			{
				using(MySqlConnection conn = GetConnection())
				{
					conn.Open();

					String sql = String.Format("select * from owners where owner_id = {0}", ownerID);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					using(var reader = cmd.ExecuteReader())
					{
						if(reader.Read())
						{
							owner = new SquareOwner()
							{
								OwnerID = Convert.ToUInt32(reader["owner_id"]),
								Name = reader["name"].ToString(),
								InitialsBytes = (byte[])reader["initials_bitmap"],
								PhotoBytes = (byte[])reader["photo_bitmap"],
							};

						}
					}
				}
			}
			catch(Exception e)
			{
				Console.WriteLine("EXCEPTION: {0}", e.Message);
			}
			return owner;
		}

		public Team GetTeam(UInt32 teamID)
		{
			Team team = null;

			try
			{
				using(MySqlConnection conn = GetConnection())
				{
					conn.Open();

					String sql = String.Format("select * from teams where team_id = {0}", teamID);
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					using(var reader = cmd.ExecuteReader())
					{
						if(reader.Read())
						{
							team = new Team()
							{
								TeamID = Convert.ToUInt32(reader["team_id"]),
								Name = reader["name"].ToString(),
								HorizontalLogoBytes = (byte[])reader["logoh"],
								VerticalLogoBytes = (byte[])reader["logov"],
							};
							team.HorizontalLogoURL = String.Format("{0}/logos/h/{1}", Constants.BaseApiUrl, team.TeamID);
							team.VerticalLogoURL = String.Format("{0}/logos/v/{1}", Constants.BaseApiUrl, team.TeamID);
						}
					}
				}
			}
			catch(Exception e)
			{
				Console.WriteLine("EXCEPTION: {0}", e.Message);
			}
			return team;
		}

		private MySqlConnection GetConnection()
		{
			return new MySqlConnection(ConnectionString);
		}
	}

}
