using UnityEngine;

namespace MagnetGame
{
	// TODO: singleton
	public class DuelStateManager : MonoBehaviour
	{
		private DuelState currentDuelState;
		public DuelState CurrentDuelState {
			get => currentDuelState;
			set {
				if (value == currentDuelState)
					return;

				currentDuelState = value;
				OnDuelStateChanged?.Invoke(value);
			}
		}

		public delegate void DuelStateChangeHandler(DuelState state);
		public event DuelStateChangeHandler OnDuelStateChanged;

		public void SetupDuel() {
			CurrentDuelState = DuelState.SETUP;
			OnDuelStateChanged?.Invoke(currentDuelState);
		}
	}
}

