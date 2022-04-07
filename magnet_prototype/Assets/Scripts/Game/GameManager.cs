using UnityEngine;
using UnityEngine.InputSystem;

namespace MagnetGame {
	public class GameManager : MonoBehaviour
	{
		private void Awake() {
			GameStateManager.Instance.CurrentGameState = GameState.RUNNING;
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

	}
}

