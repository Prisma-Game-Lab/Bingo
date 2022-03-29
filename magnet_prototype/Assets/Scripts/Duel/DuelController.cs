using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace MagnetGame
{
	public class DuelController : MonoBehaviour
	{
		[SerializeField] private DuelStateManager duelStateManager;
		[SerializeField] private GameObject playerEffectSelection;
		[SerializeField] private GameObject[] playerEffectSelectionButtons;
		[SerializeField] private PlayerController player;
		[SerializeField] private AIController ai;
		[SerializeField] private MagnetPile pile;
		[SerializeField] private GameObject[] magnetsGO;
		[SerializeField] private GameObject[] magnetPairGO;
		[SerializeField] private GameObject cardPair;
		[SerializeField] private GameObject resultUI;
		[SerializeField] private GameObject roundResultText;

		private Dictionary<Magnet, GameObject> magnetGO;

		private bool playerChooses = true;
		private bool omen = false;
		private Magnet selectedMagnet = null;
		private int selectedEffect = 0;

		private void Awake() {
			Magnet.OnMagnetClicked += OnMagnetClicked;
			duelStateManager.OnDuelStateChanged += OnDuelStateChanged;

			magnetGO = new Dictionary<Magnet, GameObject>();

			foreach (var magnet in magnetsGO)
				magnetGO.Add(magnet.GetComponent<Magnet>(), magnet);

		}

		private void Start() {
			resultUI.SetActive(false);
			cardPair.SetActive(false);
			playerEffectSelection.SetActive(false);
			duelStateManager.SetupDuel();
		}

		private void OnDestroy() {
			Magnet.OnMagnetClicked -= OnMagnetClicked;
			duelStateManager.OnDuelStateChanged -= OnDuelStateChanged;
		}

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

				case DuelState.ROUND_CONFIRM:
					RoundConfirm();
					break;

				case DuelState.ROUND_OMEN:
					RoundOmen();
					break;

				case DuelState.ROUND_DRAW:
					RoundDraw();
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
			foreach (var magnet in magnetPairGO)
				magnet.GetComponent<Magnet>().isSelectable = false;

			cardPair.SetActive(false);

			duelStateManager.CurrentDuelState = DuelState.ROUND_CHOICE;
		}

		public void OnMagnetClicked(Magnet magnet) {
			switch (duelStateManager.CurrentDuelState) {
				case DuelState.ROUND_CHOICE:
					if (ai.Choice == null) return;
					if (selectedMagnet != null) selectedMagnet.IsSelected = false;
					selectedMagnet = magnet;
					magnet.IsSelected = true;
					if (!playerEffectSelection.activeInHierarchy)
						playerEffectSelection.SetActive(true);

					MagnetEffect[] selectedMagnetEffect = selectedMagnet
															.GetComponent<Magnet>()
															.MagnetStats.effects;

					for (int i = 0; i < 2; ++i)
						playerEffectSelectionButtons[i]
							.GetComponent<LocalizeStringEvent>()
							.StringReference.SetReference("EffectNames", selectedMagnetEffect[i].GetLabel());

					break;

				case DuelState.ROUND_DRAW:
					PlayerDraws(magnet.GetComponent<Magnet>().MagnetStats);
					magnet.IsSelected = false;
					cardPair.SetActive(false);
					break;

				default:
					return;
			}

		}

		public void ConfirmCardEffect(int effect) {
			if (duelStateManager.CurrentDuelState != DuelState.ROUND_CHOICE
					|| selectedMagnet == null)
				return;

			player.Hand.Remove(selectedMagnet.MagnetStats);
			player.Choice = selectedMagnet.MagnetStats;
			magnetGO[selectedMagnet].SetActive(false);

			selectedEffect = effect;

			duelStateManager.CurrentDuelState = DuelState.ROUND_PLAY;
		}

		private void RoundChoice() {
			ai.Choice = ai.ChoosePlay();
		}

		private void RoundPlay() {
			MagnetSO playerMagnet = player.Choice;
			MagnetSO aiMagnet = ai.Choice;

			playerEffectSelection.SetActive(false);

			Result result = playerMagnet.type.Compare(aiMagnet.type);

			if (player.Guarantee > 0) {
				if (result < Result.WIN)
					++result;
				player.Guarantee = 0;
			} else if (ai.Guarantee > 0) {
				if (result > Result.LOSE)
					--result;
				ai.Guarantee = 0;
			}

			switch (result) {
				case Result.WIN:
					roundResultText.GetComponent<LocalizeStringEvent>()
						.StringReference.SetReference("UI", "round_victory");

					ai.Damage();
					if (playerMagnet.effects[selectedEffect] != MagnetEffect.OMEN)
						playerMagnet.effects[selectedEffect].GetScript().Effect(player, ai, pile);
					else
						omen = true;
					break;

				case Result.LOSE:
					roundResultText.GetComponent<LocalizeStringEvent>()
						.StringReference
						.SetReference("UI", "round_lose");

					player.Damage();
					aiMagnet.effects[0].GetScript().Effect(ai, player, pile);
					break;

				case Result.DRAW:
					roundResultText.GetComponent<LocalizeStringEvent>()
						.StringReference
						.SetReference("UI", "round_draw");
					break;
			}

			pile.Discard(playerMagnet);
			pile.Discard(aiMagnet);

			selectedMagnet.IsSelected = false;

			duelStateManager.CurrentDuelState = DuelState.ROUND_CONFIRM;
		}

		private void RoundConfirm() {
			resultUI.SetActive(true);
			cardPair.SetActive(true);

			magnetPairGO[0].GetComponent<Magnet>().MagnetStats = player.Choice;
			magnetPairGO[1].GetComponent<Magnet>().MagnetStats = ai.Choice;

			player.TryClearEffects();
			ai.TryClearEffects();
		}

		public void Continue() {
			if (duelStateManager.CurrentDuelState != DuelState.ROUND_CONFIRM
					&& duelStateManager.CurrentDuelState != DuelState.ROUND_OMEN)
				return;

			resultUI.SetActive(false);
			cardPair.SetActive(false);

			duelStateManager.CurrentDuelState = omen ? DuelState.ROUND_OMEN
														: DuelState.ROUND_DRAW;
		}

		private void RoundOmen() {
			if (ai.CounterSpell > 0) {
				ai.CounterSpell = 0;
				duelStateManager.CurrentDuelState = DuelState.ROUND_DRAW;
			}

			resultUI.SetActive(true);
			cardPair.SetActive(true);

			magnetPairGO[0].GetComponent<Magnet>().isSelectable = false;
			magnetPairGO[1].GetComponent<Magnet>().isSelectable = false;

			magnetPairGO[0].GetComponent<Magnet>().MagnetStats = ai.Hand[0];
			magnetPairGO[1].GetComponent<Magnet>().MagnetStats = ai.Hand[1];

			omen = false;
		}

		private void RoundDraw() {

			player.Choice = null;
			ai.Choice = null;

			if (selectedMagnet != null) {
				if (player.Hand.Count < 3 && ai.Hand.Count < 3) {
					MagnetSO[] magnets = new MagnetSO[2];

					magnets[0] = pile.Draw();
					magnets[1] = pile.Draw();

					if (playerChooses) {
						cardPair.SetActive(true);

						foreach (var magnet in magnetPairGO)
							magnet.GetComponent<Magnet>().isSelectable = true;

						magnetPairGO[0].GetComponent<Magnet>().MagnetStats = magnets[0];
						magnetPairGO[1].GetComponent<Magnet>().MagnetStats = magnets[1];
					} else {
						ai.AddToHand(magnets[1]);
						PlayerDraws(magnets[0]);
					}
				} else {
					while (ai.Hand.Count < 3)
						ai.AddToHand(pile.Draw());

					while (player.Hand.Count < 3)
						player.AddToHand(pile.Draw());
				}
			}
		}

		public void PlayerDraws(MagnetSO magnet) {
			player.AddToHand(magnet);
			magnetGO[selectedMagnet].SetActive(true);
			selectedMagnet.MagnetStats = magnet;
			selectedMagnet = null;

			playerChooses = !playerChooses;

			duelStateManager.CurrentDuelState = DuelState.ROUND_START;
		}

	}
}

