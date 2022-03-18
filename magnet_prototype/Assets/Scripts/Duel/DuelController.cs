using UnityEngine;

namespace MagnetGame
{
	public class DuelController : MonoBehaviour
	{
		[SerializeField] private DuelStateManager duelStateManager;

		[SerializeField] private PlayerController player;
		[SerializeField] private PlayerController AI;
		[SerializeField] private MagnetPile pile;

		private bool playerChooses = true;

		private void Awake() {
			Magnet.OnMagnetClicked += OnMagnetClicked;
			duelStateManager.OnDuelStateChanged += OnDuelStateChanged;
		}

		private void OnDestroy() {
			Magnet.OnMagnetClicked -= OnMagnetClicked;
			duelStateManager.OnDuelStateChanged -= OnDuelStateChanged;
		}

		private void Start() {
			// state = State.SETUP;
		}

		private void Update() { }

		public void OnDuelStateChanged(DuelState state) {
			switch (state) {
				case DuelState.SETUP:
					RoundSetup();
					break;

				case DuelState.ROUND_CHOICE:
					RoundChoice();
					break;

				case DuelState.ROUND_START:
					RoundStart();
					break;

				case DuelState.ROUND_PLAY:
					RoundPlay();
					break;
			}
		}

		// TODO: generate Magnet prefab objects

		private void RoundSetup() {
			pile.AddToStock(player.Magnets);
			pile.AddToStock(AI.Magnets);

			player.ClearHand();
			AI.ClearHand();

			for (int i = 0; i < 6; ++i) {
				player.AddToHand(pile.Draw());
				AI.AddToHand(pile.Draw());
			}

			duelStateManager.CurrentDuelState = DuelState.ROUND_START;
		}

		private void RoundStart() {
			MagnetSO[] magnets = new MagnetSO[2];

			magnets[0] = pile.Draw();
			magnets[1] = pile.Draw();

			if (playerChooses) {
				// TODO: give player choice
			} else {
				// TODO: give AI choice
			}
			playerChooses = !playerChooses;

			// TODO: placeholder while there is no choice
			player.AddToHand(magnets[0]);
			AI.AddToHand(magnets[1]);

			duelStateManager.CurrentDuelState = DuelState.ROUND_CHOICE;
		}

		public void OnMagnetClicked(Magnet magnet) {
			if (duelStateManager.CurrentDuelState != DuelState.ROUND_CHOICE)
				return;
			player.Hand.Remove(magnet.magnetStats);
			player.Choice = magnet;
			duelStateManager.CurrentDuelState = DuelState.ROUND_PLAY;
		}

		private void RoundChoice() {
			// TODO: Give AI and Player choice
			// TODO: Play round
		}

		private void RoundPlay() {
			Magnet playerMagnet = player.Choice;
			Magnet aiMagnet = AI.Choice;

			switch (playerMagnet.Compare(aiMagnet)) {
				case Result.WIN: // TODO: Player wins
					break;

				case Result.LOSE: // TODO: Player loses
					break;

				case Result.DRAW: // TODO: Draw
					break;
			}

			player.Choice = null;
			AI.Choice = null;

			pile.Discard(playerMagnet.magnetStats);
			pile.Discard(aiMagnet.magnetStats);

			Destroy(playerMagnet);
			Destroy(aiMagnet);

			duelStateManager.CurrentDuelState = DuelState.ROUND_START;
		}

	}
}

