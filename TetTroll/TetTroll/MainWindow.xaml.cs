using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TetTroll
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
			InitImageControls(_gameController);
        }

		private Image[,] InitImageControls(GameController controller)
		{
			var gameGrid = controller.GameGrid;
			UI_c_GameCanvas.Width = GameController.GRID_COLS * CELL_SIZE;
			UI_c_GameCanvas.Height = GameController.GRID_ROWS * CELL_SIZE;

			_imageControls = new Image[gameGrid.Height, gameGrid.Width];
			for (int row =0; row < gameGrid.Height; row++)
				for (int col = 0; col < gameGrid.Width; col++)
				{
					Image imageControl = new Image { Width = CELL_SIZE, Height = CELL_SIZE };
					Canvas.SetBottom(imageControl, row * CELL_SIZE);
					Canvas.SetLeft(imageControl, col * CELL_SIZE);
					UI_c_GameCanvas.Children.Add(imageControl);
					_imageControls[row, col] = imageControl;
				}
			return _imageControls;
		}

		private async Task GameLoop()
		{
			while (!GameOver)
				await GameStep();

			GameOverMenu.Visibility = Visibility.Visible;
			FinalScoreText.Text = _gameController.Score.ToString();
		}
		private async Task GameStep()
		{
			await Task.Delay(GAME_START_TICKING - GAME_TICK * _gameLevel);
			_gameController.MoveBlockDown();
			DrawGameStep();

			_gameLevel = (int)Math.Round((DateTime.Now - _startingTime).TotalSeconds) / 20;
		}
		private void DrawGameStep()
		{
			DrawGameGrid(_gameController.GameGrid);
			DrawBlock(_gameController.ActiveBlock);
			DrawNextBlock(_gameController.RandomBlocksQueue);
			UpdateTexts();
		}
		private void DrawGameGrid(TetTrollGameGridForVisualization grid)
		{
			for (int row = 0; row < grid.Height; row++)
				for (int col = 0; col < grid.Width; col++)
					_imageControls[row, col].Source = BlocksInstances.Blocks[grid[row, col]].TileImage;
		}
		private void DrawBlock(BlockBase block)
		{
			foreach(var position in block.Positions)
				_imageControls[position.Row, position.Col].Source = block.TileImage;

		}
		private void DrawNextBlock(RandomBlocksQueue blocksQueue)
		{
			NextBlockImage.Source = BlocksInstances.Blocks[blocksQueue.PeekBlock().GetID()].BlockImage;
		}
		private void UpdateTexts()
		{
			ScoreText.Text = $"Score: {_gameController.Score}";
			UI_txt_LevelText.Text = $"Level: {_gameLevel + 1}";
		}
		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (GameOver)
				return;

			switch (e.Key)
			{
				case Key.Left:
					_gameController.MoveBlockLeft();
					break;
				case Key.Right:
					_gameController.MoveBlockRight();
					break;
				case Key.Up:
					_gameController.RotateCW();
					break;
				case Key.Down:
					_gameController.MoveBlockDown();
					break;
			}

			DrawGameStep();
		}

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
//			DrawGameStep();
//			_startingTime = DateTime.Now;
//			await GameLoop();
        }

		private async void UI_btt_Play(object sender, RoutedEventArgs e)
		{
			await StartNewGame();
		}
		private async Task StartNewGame()
		{
			GameOverMenu.Visibility = Visibility.Hidden;
			WelcomeMenu.Visibility = Visibility.Hidden;
			_gameController = new GameController();
			_gameLevel = 0;
			_startingTime = DateTime.Now;
			await GameLoop();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{

		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{

		}

		private void UI_btt_BackToMenu(object sender, RoutedEventArgs e)
		{
			GameOverMenu.Visibility = Visibility.Hidden;
			WelcomeMenu.Visibility = Visibility.Visible;
		}

		private void Button_Click_4(object sender, RoutedEventArgs e)
		{

		}

		public bool GameOver => _gameController.IsGameOver();

		private const int CELL_SIZE = 25;
		private const int GAME_TICK = 25;
		private const int GAME_START_TICKING = 1500;

		private GameController _gameController = new GameController();
		private int _gameLevel = 0;
		private DateTime _startingTime;
		private Image[,] _imageControls;
	}
}
