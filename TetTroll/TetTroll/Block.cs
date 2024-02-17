using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TetTroll
{
	public abstract class BlockBase
	{
		public void RotateCCW()
		{
			if (--Option < 0)
				Option = RotationStates.Count - 1;
		}
		public void RotateCW() => Option = (++Option) % RotationStates.Count;

		public BlockBase Reset()
		{
			Option = 0;
			Offset = new Position(RotationStates[0].Max(p => p.Col) / 2, -RotationStates[0].Max(p => p.Row));
			return this;
		}
		public List<Position> Positions => RotationStates[Option].Select(p => p + Offset).ToList();

		public int Option { get; private set; }
		public Position Offset { get; set; }
		public abstract List<List<Position>> RotationStates { get; }
		public abstract ImageSource TileImage { get; }
		public abstract ImageSource BlockImage { get; }
		public abstract int GetID();
		public static bool ComputerMode { get; set; } = false;
		public static int ID;

    }
}
