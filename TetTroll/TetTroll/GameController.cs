using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetTroll
{
    class GameController
    {
		public GameController()
		{
			NewActiveBlock();
		}
        public void RotateCW()
        {
            ActiveBlock.RotateCW();
            if (!ActiveBlockPositionIsLegal())
                ActiveBlock.RotateCCW();
        }
        public void RotateCCW()
        {
            ActiveBlock.RotateCCW();
            if (!ActiveBlockPositionIsLegal())
                ActiveBlock.RotateCW();
        }
        public void MoveBlockRight()
        {
            ActiveBlock.Offset.Col++;
            if (!ActiveBlockPositionIsLegal())
                ActiveBlock.Offset.Col--;
        }
        public void MoveBlockLeft()
        {
            ActiveBlock.Offset.Col--;
            if (!ActiveBlockPositionIsLegal())
                ActiveBlock.Offset.Col++;
        }

        public bool MoveBlockDown()
        {
            ActiveBlock.Offset.Row--;
			if (ActiveBlockPositionIsLegal())
				return false;

			ActiveBlock.Offset.Row++;
			if (!ActiveBlockPositionIsLegal())
				ActiveBlock.Offset.Row++;

			GameGrid.InsertBlock(ActiveBlock);

			if (IsGameOver())
				return true;

			Score += ActiveBlock.Positions.Count; // ??

            var linesCleanded = GameGrid.RemoveRows();

			Score += (int)Math.Round(GameGrid.Width * (linesCleanded * (1 + 0.25 * (linesCleanded - 1)))); // Should be in grid

			NewActiveBlock();

			return true;
		}

		public void NewActiveBlock()
		{
			ActiveBlock = RandomBlocksQueue.PopBlock();
			ActiveBlock.Offset += new Position(GRID_COLS / 4, GAME_GRID_ROWS - 1);
		}
		public bool ActiveBlockPositionIsLegal() => ActiveBlock.Positions.All(GameGrid.IsEmpty);
		public bool IsGameOver() => !GameGrid.IsRowEmpty(GRID_ROWS - 1) || !GameGrid.IsRowEmpty(GRID_ROWS - 2);

        public TetTrollGameGridForVisualization GameGrid = new TetTrollGameGridForVisualization(GRID_COLS, GRID_ROWS);
        public int Score { get; private set; }
        public RandomBlocksQueue RandomBlocksQueue = new RandomBlocksQueue();
		public BlockBase ActiveBlock { get; private set; }

        public const int GRID_ROWS = GAME_GRID_ROWS + HIDDEN_ROWS;
        public const int GAME_GRID_ROWS = 22;
        public const int HIDDEN_ROWS = 2;
        public const int GRID_COLS = 12;
    }
}
