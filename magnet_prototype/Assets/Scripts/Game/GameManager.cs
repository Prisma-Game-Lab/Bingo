using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace MagnetGame {
	public class GameManager : MonoBehaviour
	{
		[SerializeField] GameObject pauseMenu;

		private void Awake() {
			GameStateManager.Instance.CurrentGameState = GameState.RUNNING;
		}

		private void OnEnable() {
			GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
		}

		private void OnDisable() {
			GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
		}

		public void TogglePause(InputAction.CallbackContext ctx) {
			if (!ctx.performed)
				return;

			GameState currentGameState = GameStateManager.Instance.CurrentGameState;
			GameStateManager.Instance.CurrentGameState =
				currentGameState == GameState.RUNNING
				? GameState.PAUSED
				: GameState.RUNNING;
		}

		public void Pause() {
			if (GameStateManager.Instance.CurrentGameState != GameState.PAUSED)
				GameStateManager.Instance.CurrentGameState = GameState.PAUSED;
		}

		public void Unpause() {
			if (GameStateManager.Instance.CurrentGameState == GameState.PAUSED)
				GameStateManager.Instance.CurrentGameState = GameState.RUNNING;
		}

		public void MainMenu() {
			AudioManager.instance.Play("botao_sair");
			SceneManager.LoadScene("MainMenu");
		}

		public void OnGameStateChanged(GameState newGameState) {
			if (newGameState == GameState.PAUSED)
				pauseMenu.SetActive(true);
			else
				pauseMenu.SetActive(false);
		}

	}
}

