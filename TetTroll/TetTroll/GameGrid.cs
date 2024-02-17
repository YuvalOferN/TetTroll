using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetTroll
{
	public interface IGameGrid : IEquatable<IGameGrid>
	{
		bool IsRowFull(int row);
		bool IsRowEmpty(int row);
		int RemoveRows(int startingRow = 0);
		bool GameOver();
		void InsertBlock(BlockBase block);
	}

	public struct TetTrollGameGrid : IEquatable<TetTrollGameGrid>
	{
		public TetTrollGameGrid(int width, int height)
		{
			Width = width;
			Height = height;
			Board = new bool[width * height];
			_filledCellCountPerRow = new int[height];
			_structureHeights = new int[width];
			_structureHeight = 0;
		}
		public bool this[int row, int col]
		{
			get => Board[row * Width + col];
			set
			{
				var cell = row * Width + col;
				if (value)
				{
					if (!Board[cell])
					{
						_structureHeights[col]++;
						if (IsRowEmpty(row))
							_structureHeight++;
						_filledCellCountPerRow[row]++;
					}
				}
				else if (Board[cell])
				{
					_filledCellCountPerRow[row]--;
					_structureHeights[col]--;
					if (IsRowEmpty(row))
						_structureHeight--;
				}
				Board[cell] = value;
			}
		}
		public void InsertBlock(TetTrollBlock block)
		{
			var blockCells = block.Cells();
			for (int i = 0; i < blockCells.Count; i++)
				this[blockCells[i].Row, blockCells[i].Col] = true;
		}
		public bool BlockPositionIsLegal(TetTrollBlock block) => block.Cells().All(IsEmpty);
		public bool IsRowFull(int row) => _filledCellCountPerRow[row] == Width;
		public bool IsRowEmpty(int row) => _filledCellCountPerRow[row] == 0;
		public bool IsRowInside(int row) => row >= 0 && row < Height;
		public bool IsColumnInside(int column) => column >= 0 && column < Width;
		public bool IsInside(int row, int column) => IsRowInside(row) && IsColumnInside(column);
		public bool IsEmpty(Position position) => IsEmpty(position.Row, position.Col);
		public bool IsEmpty(int row, int column) => IsInside(row, column) && this[row, column];
		public bool BoardIsFull() => _structureHeight > Height;
		public bool GameOver() => !IsRowEmpty(Height) && !IsRowEmpty(Height - 1);
		public int RemoveRows(int startingRow = 0)
		{
			int structureHeight = _structureHeight;
			int rowsCleaned = 0;
			for (int rowToRemove = startingRow; rowToRemove < structureHeight; rowToRemove++)
			{
				while (IsRowFull(rowToRemove + rowsCleaned) && rowToRemove + rowsCleaned < structureHeight)
					rowsCleaned++;
				if (rowsCleaned == 0)
					continue;
				if (rowToRemove + rowsCleaned < structureHeight)
					for (int col = 0; col < Width; col++)
						this[rowToRemove, col] = this[rowToRemove + rowsCleaned, col];
				else if (!IsRowEmpty(rowToRemove))
					for (int col = 0; col < Width; col++)
						this[rowToRemove, col] = false;
			}
			return rowsCleaned;
		}

		public bool Equals(TetTrollGameGrid other)
		{
			throw new NotImplementedException();
		}

		public int Width { get; }
		public int Height { get; }
		public bool[] Board { get; }
		private int[] _filledCellCountPerRow;
		private int _structureHeight;
		private int[] _structureHeights;
	}

	public class TetTrollGameGridForVisualization
	{
		public TetTrollGameGridForVisualization(int width, int height)
		{
			Width = width;
			Height = height;
			Board = new int[width * height];
			_filledCellCountPerRow = new int[height];
			_structureHeights = new int[width];
			_structureHeight = 0;
		}
		public int this[int row, int col]
		{
			get => Board[row * Width + col];
			set
			{
				var cell = row * Width + col;
				if (value != 0)
				{
					if (Board[cell] == 0)
					{
						_structureHeights[col]++;
						if (IsRowEmpty(row))
							_structureHeight++;
						_filledCellCountPerRow[row]++;
					}
				}
				else if (Board[cell] != 0)
				{
					_filledCellCountPerRow[row]--;
					_structureHeights[col]--;
					if (IsRowEmpty(row))
						_structureHeight--;
				}
				Board[cell] = value;
			}
		}
		public void InsertBlock(BlockBase block)
		{
			for (int i = 0; i < block.Positions.Count; i++)
				this[block.Positions[i].Row, block.Positions[i].Col] = block.GetID();
		}
		public bool IsRowFull(int row) => _filledCellCountPerRow[row] == Width;
		public bool IsRowEmpty(int row) => _filledCellCountPerRow[row] == 0;
		public bool IsRowInside(int row) => row >= 0 && row < Height;
		public bool IsColumnInside(int column) => column >= 0 && column < Width;
		public bool IsInside(int row, int column) => IsRowInside(row) && IsColumnInside(column);
		public bool IsEmpty(Position position) => IsEmpty(position.Row, position.Col);
		public bool IsEmpty(int row, int column) => IsInside(row, column) && this[row, column] == 0;
		public bool BoardIsFull() => _structureHeight > Height;
		public bool GameOver() => !IsRowEmpty(Height) && !IsRowEmpty(Height - 1);
		public int RemoveRows(int startingRow = 0)
		{
			int structureHeight = _structureHeight;
			int rowsCleaned = 0;
			for (int rowToRemove = startingRow; rowToRemove < structureHeight; rowToRemove++)
			{
				while (IsRowFull(rowToRemove + rowsCleaned) && rowToRemove + rowsCleaned < structureHeight)
					rowsCleaned++;
				if (rowsCleaned == 0)
					continue;
				if (rowToRemove + rowsCleaned < structureHeight)
					for (int col = 0; col < Width; col++)
						this[rowToRemove, col] = this[rowToRemove + rowsCleaned, col];
				else if (!IsRowEmpty(rowToRemove))
					for (int col = 0; col < Width; col++)
						this[rowToRemove, col] = 0;
			}
			return rowsCleaned;
		}
		public int Width { get; }
		public int Height { get; }
		public int[] Board { get; }
		private int[] _filledCellCountPerRow;
		private int _structureHeight;
		private int[] _structureHeights;
	}
}
