using UnityEngine;

namespace MagnetGame
{
	public class DuelController : MonoBehaviour
	{
		private enum State { SETUP, ROUND_START, ROUND_CHOICE, ROUND_PLAY };

		private State state;

		private bool playerChooses = true;

		[SerializeField]
		private PlayerController player;

		[SerializeField]
		private PlayerController AI;

		[SerializeField]
		private MagnetPile pile;

		[SerializeField]
		private MagnetPile discard;

		private void Start() {
			// state = State.SETUP;
		}

		private void Update() { }

		private void Setup() {
			pile.Magnets.AddRange(player.Magnets);
			pile.Magnets.AddRange(AI.Magnets);

			player.ClearHand();
			AI.ClearHand();

			for (int i = 0; i < 3; ++i) {
				player.AddToHand(pile.GetMagnet());
				AI.AddToHand(pile.GetMagnet());
			}

			// TODO: Start Rounds
		}

		private void RoundStart() {
			Magnet[] magnets = new Magnet[2];

			magnets[0] = pile.GetMagnet();
			magnets[1] = pile.GetMagnet();

			if (playerChooses) {
				// TODO: give player choice
			} else {
				// TODO: give AI choice
			}
			playerChooses = !playerChooses;

			// TODO: placeholder while there is no choice
			player.AddToHand(magnets[0]);
			AI.AddToHand(magnets[1]);

			// TODO: Round choice
		}

		private void RoundChoice() {
			// TODO: Give AI and Player choice
			// TODO: Play round
		}

		private void RoundPlay() {
			switch (player.Choice.Compare(AI.Choice)) {
				case Result.WIN: // TODO: Player wins
					break;

				case Result.LOSE: // TODO: Player loses
					break;

				case Result.DRAW: // TODO: Draw
					break;
			}

			// TODO: Next Round
		}

	}
}

