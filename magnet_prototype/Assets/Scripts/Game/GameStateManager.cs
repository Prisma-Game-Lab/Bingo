using UnityEngine;

namespace MagnetGame {
	public class GameStateManager
	{
		private static GameStateManager _instance;
		public static GameStateManager Instance {
			get {
				if (_instance == null)
					_instance = new GameStateManager();

				return _instance;
			}
		}

		private GameState _currentGameState = GameState.RUNNING;
		public GameState CurrentGameState {
			get => _currentGameState;
			set {
				if (value == _currentGameState)
					return;

				_currentGameState = value;
				OnGameStateChanged?.Invoke(value);
			}
		}

		public delegate void GameStateChangeEvent(GameState newGameState);
		public event GameStateChangeEvent OnGameStateChanged;

		private GameStateManager() {}

		public static bool IsPaused()
			=> Instance.CurrentGameState == GameState.PAUSED;

	}
}

