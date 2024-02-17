using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetTroll
{
	class Troll
	{
		private static double SimpleHuristics(TetTrollState state)
		{
			var scoreWeight = 1;
			var timeWeight = -0.01;
			var movesWeight = -0.01;

			var res = scoreWeight * state.Score + movesWeight * state.Depth;
			return res;
		}

		public static double CalculateHuristics(TetTrollState state)
		{
			if (state.Board.GameOver())
				return -1000;
			return SimpleHuristics(state);
		}

		public TetTrollState RunPlayerAction(TetTrollState state)
		{
			throw new NotImplementedException();

		}

		public double RunTrollChoosingBlockAction(TetTrollState state)
		{
			var startTime = DateTime.Now;
			if (state.TimeLimit < TIME_LIMIT_MARGIN_PER_STATE)
				return CalculateHuristics(state);

			var statesLeft = BlocksInstances.OptionsCount;
			var globalScore = double.PositiveInfinity;
			var minScoreState = state;
			for (int blockID = LineBlock.ID; blockID < BlocksInstances.Blocks.Count; blockID++)
				for (int stateID = 0; stateID < BlocksInstances.Blocks[blockID].RotationStates.Count; stateID++)
				{
					var timeSpent = (DateTime.Now - startTime).Milliseconds;
					var timeLimitPerState = (state.TimeLimit - timeSpent) / statesLeft - TIME_LIMIT_MARGIN_PER_ACTION;
					var newBlock = new TetTrollBlock(blockID, stateID);
					var newState = new TetTrollState(state.Board, state.Score, newBlock, timeLimitPerState, state.Depth + 1);
					var localScore = RunTrollChoosingStepAction(newState);
					if (localScore < globalScore)
					{
						globalScore = localScore;
						minScoreState = newState;
					}
					statesLeft--;
				}
			return globalScore;
		}

		public double RunTrollChoosingStepAction(TetTrollState state)
		{
			var startTime = DateTime.Now;
			if (state.TimeLimit< TIME_LIMIT_MARGIN_PER_STATE)
				return CalculateHuristics(state);

			var stepsLeft = ActionMap.Count;
			var globalScore = double.NegativeInfinity;
			var maxScoreState = state;
			for (var step = 0; step < ActionMap.Count - 1; step++)
			{
				var timeSpent = (DateTime.Now - startTime).Milliseconds;
				var timeLimitPerState = (state.TimeLimit - timeSpent) / stepsLeft - TIME_LIMIT_MARGIN_PER_ACTION;
				var newLocalBlock = ActionMap[step](state.ActiveBlock);
				if (!state.Board.BlockPositionIsLegal(newLocalBlock))
					continue;
				var newState = new TetTrollState(state.Board, state.Score, newLocalBlock, timeLimitPerState, state.Depth);
				var localScore = RunTrollChoosingStepAction(newState);
				if (localScore > globalScore)
				{
					globalScore = localScore;
					maxScoreState = newState;
				}
				stepsLeft--;
			}

			var newBlock = state.ActiveBlock.MoveDown();
			if (state.Board.BlockPositionIsLegal(newBlock))
			{
				// Need to find all known board states before moving to choosing blocks.
				var timeSpent = (DateTime.Now - startTime).Milliseconds;
				var timeLimitPerState = (state.TimeLimit - timeSpent) - TIME_LIMIT_MARGIN_PER_ACTION;
				var newState = new TetTrollState(state.Board, state.Score, newBlock, timeLimitPerState, state.Depth);
				var localScore = RunTrollChoosingStepAction(newState);
				if (localScore > globalScore)
				{
					globalScore = localScore;
					maxScoreState = newState;
				}
			}
			else
			{
				// Moving to the next section of minMax tree
				var timeSpent = (DateTime.Now - startTime).Milliseconds;
				var timeLimitPerState = (state.TimeLimit - timeSpent) - TIME_LIMIT_MARGIN_PER_ACTION;
				var newState = state.DefineLastMove();
				var localScore = RunTrollChoosingBlockAction(newState);
			}

			return 0;
		}

		public Tree<TetTrollBlock> RenderBoardStatesPerBlock(TetTrollState state)
		{
			var statesSet = new HashSet<TetTrollBlock>();
			var statesPaths = new Tree<TetTrollBlock>(state.ActiveBlock);
			RenderBoardStatesPerBlockRecursive(state.Board, statesPaths.Root, statesSet);
			return statesPaths;
		}

		public void RenderBoardStatesPerBlockRecursive(TetTrollGameGrid board, TreeNode<TetTrollBlock> statesPaths, HashSet<TetTrollBlock> statesSet)
		{
			var newBlock = ActionMap[0](statesPaths.Value);
			if (statesSet.Add(newBlock) && board.BlockPositionIsLegal(newBlock))
				RenderBoardStatesPerBlockRecursive(board, statesPaths.AddChild(newBlock, true), statesSet);

			// loop over all actions except "down"
			for (var step = 1; step < ActionMap.Count; step++)
			{
				newBlock = ActionMap[step](statesPaths.Value);
				if (statesSet.Add(newBlock) && board.BlockPositionIsLegal(newBlock))
					RenderBoardStatesPerBlockRecursive(board, statesPaths.AddChild(newBlock), statesSet);
			}
		}

		private Dictionary<IGameGrid, int> _states;
		private static DateTime _startingTime;
		private double _maxDiffs;
		private double _minDiffs;
		private static double TIME_LIMIT_MARGIN_PER_STATE = 1;
		private static double TIME_LIMIT_MARGIN_PER_ACTION = 1;

		private static TetTrollBlock MoveLeft(TetTrollBlock block) => block.MoveLeft();
		private static TetTrollBlock MoveRight(TetTrollBlock block) => block.MoveRight();
		private static TetTrollBlock MoveDown(TetTrollBlock block) => block.MoveDown();
		private static TetTrollBlock Rotate(TetTrollBlock block) => block.Rotate();
		private static Dictionary<int, Func<TetTrollBlock, TetTrollBlock>> ActionMap = new Dictionary<int, Func<TetTrollBlock, TetTrollBlock>>
		{
			{0, MoveDown},		// Importat taht MoveDown will be first.
			{1, MoveLeft},
			{2, MoveRight},
			{3, Rotate},		// Better rotation to be last.
		};
	}
	public class TetTrollStateContainer
	{
		private HashSet<TetTrollState> _uniqueStates;
	}
	public struct TetTrollBlock : IEquatable<TetTrollBlock>
	{
		public TetTrollBlock(int blockID, int rotationStateID, Position offset)
		{
			BlockID = blockID;
			RotationStateID = rotationStateID;
			Offset = offset;
		}
		public TetTrollBlock(int blockID, int stateID)
		{
			BlockID = blockID;
			RotationStateID = stateID;
			Offset = new Position();

		}

		public Position Offset { get;}
		public int BlockID { get;}
		public int RotationStateID { get; private set; }
		public List<Position> Cells() => BlocksInstances.Blocks[BlockID].RotationStates[RotationStateID];
		public TetTrollBlock Rotate() => new TetTrollBlock(BlockID, (RotationStateID + 1) % BlocksInstances.Blocks[BlockID].RotationStates.Count, Offset);

		public TetTrollBlock MoveLeft() => new TetTrollBlock(BlockID, RotationStateID, Offset.Left());
		public TetTrollBlock MoveRight() => new TetTrollBlock(BlockID, RotationStateID, Offset.Right());
		public TetTrollBlock MoveDown() => new TetTrollBlock(BlockID, RotationStateID, Offset.Down());

		public bool Equals(TetTrollBlock other) => BlockID == other.BlockID && RotationStateID == other.RotationStateID && Offset == other.Offset;
	}

	public struct TetTrollState : IEquatable<TetTrollState>
	{
		public TetTrollState(TetTrollGameGrid board, int score, TetTrollBlock activeBlock, double timeLimit, int depth)
		{
			Board = board;
			ActiveBlock = activeBlock;
			Score = score;
			TimeLimit = timeLimit;
			TimeOccured = DateTime.Now;
			Depth = depth;
		}
		public TetTrollBlock ActiveBlock { get;}
		public double TimeLimit { get;}
		public int Depth { get;}
		public DateTime TimeOccured { get;}
		public TetTrollGameGrid Board { get;}
		public int Score { get;}
		public bool Equals(TetTrollState other)
		{
			return Board.Equals(other.Board);
		}

		public bool BlockInLegalPosition() => Board.BlockPositionIsLegal(ActiveBlock);

		public TetTrollState DefineLastMove()
		{
			Board.InsertBlock(ActiveBlock);
			var linesCleanded = Board.RemoveRows();

			var score = Score + ActiveBlock.Cells().Count + (int)Math.Round(Board.Width * (linesCleanded * (1 + 0.25 * (linesCleanded - 1)))); // Should be in grid

			return new TetTrollState(Board, score, ActiveBlock, TimeLimit, Depth + 1);
		}
	}

	public class TetrisExpectimax
	{
		private const int BoardWidth = 10;
		private const int BoardHeight = 20;

		public static int RunExpectimax(TetTrollState state, int depth, bool isMaxPlayer)
		{
			if (depth == 0)
			{
				return CalculateHeuristics(state);
			}

			if (isMaxPlayer)
			{
				int maxEval = int.MinValue;

				foreach (var nextState in GetPossibleStates(state))
				{
					int eval = RunExpectimax(nextState, depth - 1, false);
					maxEval = Math.Max(maxEval, eval);
				}

				return maxEval;
			}
			else
			{
				int minEval = int.MaxValue;

				foreach (var nextState in GetPossibleStates(state))
				{
					int eval = RunExpectimax(nextState, depth - 1, true);
					minEval = Math.Min(minEval, eval);
				}

				return minEval;
			}
		}

		private static IEnumerable<TetTrollState> GetPossibleStates(TetTrollState currentState)
		{
			// Generate all possible states for the next move
			// This is where you'd add logic to simulate the Tetris gameplay
			// For simplicity, this example just generates random moves

			List<TetTrollState> possibleStates = new List<TetTrollState>();

			for (int i = 0; i < 3; i++)
			{
				var newBoard = (int[])currentState.Board.Board.Clone();
				//var board = new TetTrollBoard();
				// Apply a random move (for demonstration purposes)
				// You need to implement the actual Tetris gameplay logic here
				// ...
				//possibleStates.Add(new TetTrollState (){ Board = board, Score = currentState.Score + 100 });
			}

			return possibleStates;
		}

		private static int CalculateHeuristics(TetTrollState state)
		{
			// Replace this with your actual heuristic function
			// The heuristic function should evaluate the desirability of the current state for Player 1
			// It can take into account features like the height of the highest column, number of completed lines, etc.
			return state.Score;
		}
	}
}
