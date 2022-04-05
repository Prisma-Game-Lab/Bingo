using System.Collections.Generic;
using UnityEngine;

namespace MagnetGame
{
	public class Player : MonoBehaviour
	{
		private const int defaultMaxHealth = 4;

		[SerializeField] private List<MagnetSO> defaultMagnets;

		private List<MagnetSO> magnets = new List<MagnetSO>();
		private List<MagnetSO> hand = new List<MagnetSO>();
		private int health = defaultMaxHealth;
		public int maxHealth { get; set; }

		public List<MagnetSO> Magnets { get => magnets; }
		public List<MagnetSO> Hand { get => hand; }
		public MagnetSO Choice { get; set; }
		public int Health { get => health; }
		public int Shield { get; set; }
		public int Guarantee { get; set; }
		public int CounterSpell { get; set; }

		public delegate void PlayerHealthChangedHandler(int health);
		public event PlayerHealthChangedHandler OnPlayerHealthChanged;

		public void Reset(List<MagnetSO> magnets) {
			this.magnets = magnets;
			Reset();
		}

		public void Reset() {
			health = maxHealth;
			OnPlayerHealthChanged?.Invoke(health);
			hand.Clear();
			Shield = 0;
			Guarantee = 0;
			CounterSpell = 0;
		}

		public void AddToMagnets(List<MagnetSO> magnets) => this.magnets.AddRange(magnets);
		public void AddToMagnets(MagnetSO magnet) => magnets.Add(magnet);

		public void AddToHand(MagnetSO magnet) => this.hand.Add(magnet);
		public void AddToHand(List<MagnetSO> magnets) => this.hand.AddRange(magnets);
		public void RemoveFromHand(MagnetSO magnet)  => hand.Remove(magnet);
		public void RemoveFromHandAt(int index) => hand.RemoveAt(index);
		public void ClearHand() => this.hand.Clear();

		public bool Damage(int damage = 1) {
			if (Shield > 0)
				Shield = 0;
			else
				health -= damage;

			OnPlayerHealthChanged?.Invoke(health);

			return health <= 0;
		}

		public void Heal(int heal = 1) {
			health += heal;

			if (health > maxHealth)
				health = maxHealth;

			OnPlayerHealthChanged?.Invoke(health);
		}

		public void TryClearEffects() {
			--Shield;
			--Guarantee;
			--CounterSpell;
		}

		private void Awake() {
			maxHealth = defaultMaxHealth;
			if (defaultMagnets.Count > 0)
				AddToMagnets(defaultMagnets);
		}

	}
}

