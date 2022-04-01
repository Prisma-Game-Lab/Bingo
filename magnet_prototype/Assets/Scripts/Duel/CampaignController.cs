using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace MagnetGame {
	public class CampaignController : MonoBehaviour
	{

		[field: SerializeField]	private List<MagnetSO> magnets { get; set; }
		[field: SerializeField]	private List<LevelSO> levels { get; set; }
		[SerializeField] private DuelStateManager duelStateManager;
		[SerializeField] private PlayerController playerController;
		[SerializeField] private AIController aiController;
		[SerializeField] private Image scenary;
		[SerializeField] private Image opponent;
		[SerializeField] private TextMeshProUGUI playerHP;
		[SerializeField] private TextMeshProUGUI aiHP;

		private List<MagnetSO>[] defaultMagnetStocks;
		private int currentLevel = 1;

		private void Awake() {
			defaultMagnetStocks = new List<MagnetSO>[levels.Count + 1];

			defaultMagnetStocks[0] = new List<MagnetSO>();
			for (int i = 0; i < 3; ++i) {
				int randomIdx = (int)Random.Range(0, magnets.Count-1);
				defaultMagnetStocks[0].Add(magnets[randomIdx]);
				magnets.RemoveAt(randomIdx);
			}

			for (int i = 1; i <= 3; ++i) {
				defaultMagnetStocks[i] = new List<MagnetSO>();
				for (int j = 0; j < 5; ++j) {
					int randomIdx = (int)Random.Range(0, magnets.Count-1);
					defaultMagnetStocks[i].Add(magnets[randomIdx]);
					magnets.RemoveAt(randomIdx);
				}
			}

			playerController.OnPlayerHealthChanged += OnPlayerDamaged;
			aiController.OnPlayerHealthChanged += OnAIDamaged;
		}

		private void OnDestroy() {
			playerController.OnPlayerHealthChanged -= OnPlayerDamaged;
			aiController.OnPlayerHealthChanged -= OnAIDamaged;
		}

		private void Start() {
			playerController.Reset(defaultMagnetStocks[0]);
			LoadLevel(1);
		}

		private void OnAIDamaged(int health) {
			if (health <= 0) {
				aiHP.text = "0/" + aiController.maxHealth;

				if (currentLevel >= levels.Count) {
					SceneManager.LoadScene("VictoryScene");
				} else {
					++playerController.maxHealth;
					++aiController.maxHealth;
					playerController.AddToMagnets(aiController.Magnets);
					LoadLevel(++currentLevel);
				}
			} else {
				aiHP.text = aiController.Health + "/" + aiController.maxHealth;
			}
		}

		private void OnPlayerDamaged(int health) {
			if (health <= 0) {
				playerHP.text = "0/" + playerController.maxHealth;
				SceneManager.LoadScene("DefeatScene");
			} else {
				playerHP.text = playerController.Health + "/" + playerController.maxHealth;
			}
		}

		private void LoadLevel(int idx) {
			playerController.Reset();
			aiController.Reset(defaultMagnetStocks[idx--]);

			if (levels[idx].scenarySprite != null)
				scenary.sprite = levels[idx].scenarySprite;

			if (levels[idx].opponentSprite != null)
				opponent.sprite = levels[idx].opponentSprite;

			duelStateManager.SetupDuel();
		}
	}
}

