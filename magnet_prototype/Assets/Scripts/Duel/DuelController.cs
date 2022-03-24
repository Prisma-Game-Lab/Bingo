using System.Collections.Generic;
using UnityEngine;

namespace MagnetGame
{
	public class DuelController : MonoBehaviour
	{
		[SerializeField] private DuelStateManager duelStateManager;
		[SerializeField] private GameObject playerEffectSelection;

		[SerializeField] private PlayerController player;
		[SerializeField] private AIController ai;
		[SerializeField] private MagnetPile pile;
		[SerializeField] private GameObject[] magnetsGO;

		private Dictionary<Magnet, GameObject> magnetGO;

		private bool playerChooses = true;
		private Magnet selectedMagnet = null;

		private void Awake() {
			Magnet.OnMagnetClicked += OnMagnetClicked;
			duelStateManager.OnDuelStateChanged += OnDuelStateChanged;

			magnetGO = new Dictionary<Magnet, GameObject>();

			foreach (var magnet in magnetsGO)
				magnetGO.Add(magnet.GetComponent<Magnet>(), magnet);
		}

		private void OnDestroy() {
			Magnet.OnMagnetClicked -= OnMagnetClicked;
			duelStateManager.OnDuelStateChanged -= OnDuelStateChanged;
		}

		private void Start() {
			playerEffectSelection.SetActive(false);
			duelStateManager.SetupDuel();
		}

		public void OnDuelStateChanged(DuelState state) {
			switch (state) {
				case DuelState.SETUP:
					RoundSetup();
					break;

				case DuelState.ROUND_CHOICE:
					RoundChoice();
					break;

				case DuelState.ROUND_CONFIRM:
					RoundConfirm();
					break;

				case DuelState.ROUND_START:
					RoundStart();
					break;

				case DuelState.ROUND_PLAY:
					RoundPlay();
					break;
			}
		}

		private void RoundSetup() {
			pile.AddToStock(player.Magnets);
			pile.AddToStock(ai.Magnets);

			player.ClearHand();
			ai.ClearHand();

			for (int i = 0; i < 3; ++i) {
				player.AddToHand(pile.Draw());
				ai.AddToHand(pile.Draw());
			}

			for (int i = 0; i < magnetsGO.Length; ++i)
				magnetsGO[i].GetComponent<Magnet>().MagnetStats = player.Hand[i];

			duelStateManager.CurrentDuelState = DuelState.ROUND_START;
		}

		private void RoundStart() {
			playerEffectSelection.SetActive(false);

			if (selectedMagnet != null) {
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
				ai.AddToHand(magnets[1]);

				magnetGO[selectedMagnet].SetActive(true);
				selectedMagnet.MagnetStats = magnets[0];
				selectedMagnet = null;
			}

			duelStateManager.CurrentDuelState = DuelState.ROUND_CHOICE;
		}

		public void OnMagnetClicked(Magnet magnet) {
			if (ai.Choice == null || duelStateManager.CurrentDuelState != DuelState.ROUND_CHOICE)
				return;
			if (selectedMagnet != null)
				selectedMagnet.IsSelected = false;
			selectedMagnet = magnet;
			magnet.IsSelected = true;
			if (!playerEffectSelection.activeInHierarchy)
				playerEffectSelection.SetActive(true);
		}

		public void ConfirmCardEffect(int effect) {
			if (duelStateManager.CurrentDuelState != DuelState.ROUND_CHOICE
					|| selectedMagnet == null)
				return;

			player.Hand.Remove(selectedMagnet.MagnetStats);
			player.Choice = selectedMagnet.MagnetStats;
			magnetGO[selectedMagnet].SetActive(false);

			duelStateManager.CurrentDuelState = DuelState.ROUND_PLAY;
		}

		private void RoundChoice() {
			// TODO: Give AI and Player choice
			// TODO: Play round
			ai.Choice = ai.ChoosePlay();
		}

		private void RoundConfirm() {

		}

		private void RoundPlay() {
			MagnetSO playerMagnet = player.Choice;
			MagnetSO aiMagnet = ai.Choice;

			switch (playerMagnet.type.Compare(aiMagnet.type)) {
				case Result.WIN: // TODO: Player wins
					break;

				case Result.LOSE: // TODO: Player loses
					break;

				case Result.DRAW: // TODO: Draw
					break;
			}

			player.Choice = null;
			ai.Choice = null;

			pile.Discard(playerMagnet);
			pile.Discard(aiMagnet);

			selectedMagnet.IsSelected = false;

			duelStateManager.CurrentDuelState = DuelState.ROUND_START;
		}

	}
}

