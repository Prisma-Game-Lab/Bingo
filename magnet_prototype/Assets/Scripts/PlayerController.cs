using System.Collections.Generic;
using UnityEngine;

namespace MagnetGame
{
	public class PlayerController : MonoBehaviour
	{
		private List<Magnet> magnets = new List<Magnet>();
		private List<Magnet> hand = new List<Magnet>();

		public List<Magnet> Magnets { get => magnets; }
		public List<Magnet> Hand { get => hand; }

		public void AddToMagnets(List<Magnet> magnets) => this.magnets.AddRange(magnets);
		public void AddToMagnets(Magnet magnet) => magnets.Add(magnet);

		public Magnet HandRemoveAt(int index) {
			Magnet magnet = hand[index];
			hand.RemoveAt(index);
			return magnet;
		}

		public void AddToHand(Magnet magnet) => this.hand.Add(magnet);
		public void AddToHand(List<Magnet> magnets) => this.hand.AddRange(magnets);
		public void ClearHand() => this.hand.Clear();
	}
}

