using System.Collections.Generic;
using UnityEngine;

namespace MagnetGame
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private List<MagnetSO> defaultMagnets;

		private List<MagnetSO> magnets = new List<MagnetSO>();
		private List<MagnetSO> hand = new List<MagnetSO>();
		private int health = 3;

		public List<MagnetSO> Magnets { get => magnets; }
		public List<MagnetSO> Hand { get => hand; }
		public MagnetSO Choice { get; set; }
		public int Health { get => health; }
		public int Shield { get; set; }
		public int Guarantee { get; set; }
		public int CounterSpell { get; set; }

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

			return health <= 0;
		}

		public void Heal(int heal = 1) {
			health += heal;

			if (health > 3)
				health = 3;
		}

		public void TryClearEffects() {
			--Shield;
			--Guarantee;
			--CounterSpell;
		}

		private void Awake() {
			if (defaultMagnets.Count > 0)
				AddToMagnets(defaultMagnets);
		}

	}
}

