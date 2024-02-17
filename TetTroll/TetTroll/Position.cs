using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetTroll
{
    public class Position : IEquatable<Position>
    {
        public int Col { get; set; }
        public int Row { get; set; }

		public Position()
		{
			Col = 0;
			Row = 0;
		}
        public Position(int x, int y)
        {
            Col = x;
            Row = y;
        }
		public Position Left() => new Position(--Col, Row);
		public Position Right() => new Position(Col++, Row);
		public Position Down() => new Position(Col, --Row);

		public bool Equals(Position other) => Col == other.Col && Row == other.Row;

		public static Position operator +(Position a, Position b) => new Position(a.Col + b.Col, a.Row + b.Row);
        public static Position operator -(Position a, Position b) => new Position(a.Col - b.Col, a.Row - b.Row);
	}
}
