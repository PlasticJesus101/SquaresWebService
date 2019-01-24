using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquaresWebService.Models
{
	public class GameGrid
	{
		public UInt32 GridID { get; set; }
		public String Name { get; set; }
		public Decimal SquareCost { get; set; }
		public UInt32 Team1ID { get; set; }
		public UInt32 Team2ID { get; set; }
		public DateTime CreateTimestamp { get; set; }

		public GameGrid()
			: this(String.Empty, 0, 0, 0) {}

		public GameGrid(String name, Decimal squareCost, UInt32 team1ID, UInt32 team2ID)
		{
			Name = name;
			SquareCost = squareCost;
			Team1ID = team1ID;
			Team2ID = team2ID;
		}
	}
}