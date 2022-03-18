using System.Collections.Generic;
using UnityEngine;

namespace MagnetGame
{
	public class PlayerController : MonoBehaviour
	{
		private List<MagnetSO> magnets = new List<MagnetSO>();
		private List<MagnetSO> hand = new List<MagnetSO>();

		public List<MagnetSO> Magnets { get => magnets; }
		public List<MagnetSO> Hand { get => hand; }
		public Magnet Choice { get; set; }

		public void AddToMagnets(List<MagnetSO> magnets) => this.magnets.AddRange(magnets);
		public void AddToMagnets(MagnetSO magnet) => magnets.Add(magnet);

		public MagnetSO HandRemoveAt(int index) {
			MagnetSO magnet = hand[index];
			hand.RemoveAt(index);
			return magnet;
		}

		public void AddToHand(MagnetSO magnet) => this.hand.Add(magnet);
		public void AddToHand(List<MagnetSO> magnets) => this.hand.AddRange(magnets);
		public void ClearHand() => this.hand.Clear();
	}
}

