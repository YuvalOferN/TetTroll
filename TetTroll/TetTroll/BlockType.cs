using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TetTroll
{
	public class EmptyBlock : BlockBase
	{
		public override List<List<Position>> RotationStates => _options;
		private static List<List<Position>> _options = new List<List<Position>>();
		public override ImageSource TileImage => new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative));
		public override ImageSource BlockImage => new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative));
		public override int GetID() => ID;
	}
	public class LineBlock : BlockBase
    {
		public override List<List<Position>> RotationStates => ComputerMode ? _computerModeOptions : _options;
		private static List<List<Position>> _options = new List<List<Position>>
        {
            new List<Position>{new Position(0, 1), new Position(1, 1), new Position(2, 1), new Position(3, 1) },
            new List<Position>{new Position(2, 0), new Position(2, 1), new Position(2, 2), new Position(2, 3) },
            new List<Position>{new Position(0, 2), new Position(1, 2), new Position(2, 2), new Position(3, 2) },
            new List<Position>{new Position(1, 0), new Position(1, 1), new Position(1, 2), new Position(1, 3) },
        };
		private static List<List<Position>> _computerModeOptions = new List<List<Position>>
		{
			new List<Position>{new Position(0, 1), new Position(1, 1), new Position(2, 1), new Position(3, 1) },
			new List<Position>{new Position(2, 0), new Position(2, 1), new Position(2, 2), new Position(2, 3) },
		};
		public override int GetID() => ID;
		public new static int ID => 1;
		public override ImageSource TileImage => new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative));
		public override ImageSource BlockImage => new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative));
	}

    public class OBlock : BlockBase
    {
        public override List<List<Position>> RotationStates => _options;
        private static List<List<Position>> _options = new List<List<Position>>
        {
            new List<Position>{new Position(0, 0), new Position(0, 1), new Position(1, 0), new Position(1, 1) },
        };
		public override int GetID() => ID;
		public static new int ID => 2;
		public override ImageSource TileImage => new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative));
		public override ImageSource BlockImage => new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative));
	}

	public class LBlock : BlockBase
    {
        public override List<List<Position>> RotationStates => _options;
        private static List<List<Position>> _options = new List<List<Position>>
        {
            new List<Position>{new Position(0, 1), new Position(1, 1), new Position(2, 1), new Position(2, 0) },
            new List<Position>{new Position(1, 0), new Position(1, 1), new Position(1, 2), new Position(2, 2) },
            new List<Position>{new Position(0, 2), new Position(0, 1), new Position(1, 1), new Position(2, 1) },
            new List<Position>{new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(1, 2) },
        };
		public override int GetID() => ID;
		public static new int ID => 3;
		public override ImageSource TileImage => new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative));
		public override ImageSource BlockImage => new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative));
	}

	public class JBlock : BlockBase
    {
        public override List<List<Position>> RotationStates => _options;
        private static List<List<Position>> _options = new List<List<Position>>
        {
            new List<Position>{new Position(0, 1), new Position(1, 1), new Position(2, 1), new Position(2, 2) },
            new List<Position>{new Position(1, 0), new Position(1, 1), new Position(1, 2), new Position(0, 2) },
            new List<Position>{new Position(0, 0), new Position(0, 1), new Position(1, 1), new Position(2, 1) },
            new List<Position>{new Position(2, 0), new Position(1, 0), new Position(1, 1), new Position(1, 2) },
        };
		public override int GetID() => ID;
		public static new int ID => 4;
		public override ImageSource TileImage => new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative));
		public override ImageSource BlockImage => new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative));
	}

	public class TBlock : BlockBase
    {
        public override List<List<Position>> RotationStates => _options;
        private static List<List<Position>> _options = new List<List<Position>>
        {
            new List<Position>{new Position(0, 1), new Position(1, 1), new Position(1, 0), new Position(2, 1) },
            new List<Position>{new Position(1, 0), new Position(1, 1), new Position(1, 2), new Position(2, 1) },
            new List<Position>{new Position(0, 1), new Position(1, 1), new Position(1, 2), new Position(2, 1) },
            new List<Position>{new Position(0, 1), new Position(1, 0), new Position(1, 1), new Position(1, 2) },
        };
		public override int GetID() => ID;
		public static new int ID => 5;
		public override ImageSource TileImage => new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative));
		public override ImageSource BlockImage => new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative));
	}

	public class ZBlock : BlockBase
    {
		public override List<List<Position>> RotationStates => ComputerMode ? _computerModeOptions : _options;
		private static List<List<Position>> _options = new List<List<Position>>
        {
            new List<Position>{new Position(0, 1), new Position(1, 1), new Position(1, 2), new Position(2, 2) },
            new List<Position>{new Position(1, 0), new Position(1, 1), new Position(0, 1), new Position(0, 2) },
            new List<Position>{new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(2, 1) },
            new List<Position>{new Position(2, 0), new Position(2, 1), new Position(1, 1), new Position(1, 2) },
        };
		private static List<List<Position>> _computerModeOptions = new List<List<Position>>
		{
			new List<Position>{new Position(0, 1), new Position(1, 1), new Position(1, 2), new Position(2, 2) },
			new List<Position>{new Position(1, 0), new Position(1, 1), new Position(0, 1), new Position(0, 2) },
		};
		public override int GetID() => ID;
		public static new int ID => 6;
		public override ImageSource TileImage => new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative));
		public override ImageSource BlockImage => new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative));
	}

	public class SBlock : BlockBase
    {
        public override List<List<Position>> RotationStates => ComputerMode? _computerModeOptions : _options;
        private static List<List<Position>> _options = new List<List<Position>>
        {
            new List<Position>{new Position(0, 1), new Position(1, 1), new Position(1, 0), new Position(2, 0) },
            new List<Position>{new Position(1, 0), new Position(1, 1), new Position(2, 1), new Position(2, 2) },
            new List<Position>{new Position(0, 2), new Position(1, 2), new Position(1, 1), new Position(2, 1) },
            new List<Position>{new Position(0, 0), new Position(0, 1), new Position(1, 1), new Position(1, 2) },
        };
		private static List<List<Position>> _computerModeOptions = new List<List<Position>>
		{
			new List<Position>{new Position(0, 1), new Position(1, 1), new Position(1, 0), new Position(2, 0) },
			new List<Position>{new Position(1, 0), new Position(1, 1), new Position(2, 1), new Position(2, 2) },
		};
		public override int GetID() => ID;
		public new const int ID = 7;
		public override ImageSource TileImage => new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative));
		public override ImageSource BlockImage => new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative));
	}

	public class CorruptedJBlock : BlockBase
	{
		public override List<List<Position>> RotationStates => _options;
		private static List<List<Position>> _options = new List<List<Position>>
		{
			new List<Position>{new Position(0, 1), new Position(1, 1), new Position(2, 1), new Position(2, 2) },
			new List<Position>{new Position(1, 0), new Position(1, 1), new Position(1, 2), new Position(0, 2) },
			new List<Position>{new Position(0, 2), new Position(0, 1), new Position(1, 1), new Position(0, 0) },
			new List<Position>{new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(2, 0) },
		};
		public override int GetID() => ID;
		public static new int ID => 8;
		public override ImageSource TileImage => new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative));
		public override ImageSource BlockImage => new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative));
	}

	public static class BlocksInstances
	{
		public static Dictionary<int, BlockBase> Blocks = new Dictionary<int, BlockBase>
		{
			{EmptyBlock.ID, new EmptyBlock() },
			{LineBlock.ID, new LineBlock() },
			{OBlock.ID, new OBlock() },
			{LBlock.ID, new LBlock() },
			{ JBlock.ID, new JBlock() },
			{ TBlock.ID, new TBlock() },
			{ ZBlock.ID, new ZBlock() },
			{ SBlock.ID, new SBlock() },
			//{ CorruptedRLBlock.ID, new CorruptedRLBlock() },
		};

		public static int OptionsCount { get; private set; } = Blocks.Values.Sum(b => b.RotationStates.Count);
	}
}
