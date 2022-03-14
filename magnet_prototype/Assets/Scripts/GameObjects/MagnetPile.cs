using System.Collections.Generic;
using UnityEngine;

namespace MagnetGame
{
	public class MagnetPile : MonoBehaviour
	{
		private List<Magnet> magnets = new List<Magnet>();

		public List<Magnet> Magnets { get => magnets; }

		public void InsertMagnet(Magnet magnet) => magnets.Add(magnet);

		public Magnet getMagnet() {
			int index = Random.Range(0, magnets.Count);
			Magnet magnet = magnets[index];
			magnets.RemoveAt(index);
			return magnet;
		}
	}
}

