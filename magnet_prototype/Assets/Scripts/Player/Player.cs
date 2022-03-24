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

		public void AddToMagnets(List<MagnetSO> magnets) => this.magnets.AddRange(magnets);
		public void AddToMagnets(MagnetSO magnet) => magnets.Add(magnet);

		public void AddToHand(MagnetSO magnet) => this.hand.Add(magnet);
		public void AddToHand(List<MagnetSO> magnets) => this.hand.AddRange(magnets);
		public void RemoveFromHand(MagnetSO magnet)  => hand.Remove(magnet);
		public void RemoveFromHandAt(int index) => hand.RemoveAt(index);
		public void ClearHand() => this.hand.Clear();

		public bool Damage(int damage = 1) {
			health -= damage;

			if (health <= 0)
				return true;
			else
				return false;
		}

		private void Awake() {
			if (defaultMagnets.Count > 0)
				AddToMagnets(defaultMagnets);
		}

	}
}

