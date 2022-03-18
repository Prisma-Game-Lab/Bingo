using System.Collections.Generic;
using UnityEngine;

namespace MagnetGame
{
	public class MagnetPile : MonoBehaviour
	{
		private List<MagnetSO> magnets = new List<MagnetSO>();

		public List<MagnetSO> Magnets { get => magnets; }

		public void InsertMagnet(MagnetSO magnet) => magnets.Add(magnet);
		public void InsertMagnet(Magnet magnet) {
			magnets.Add(magnet.magnetStats);
		}

		public MagnetSO GetMagnet() {
			int index = Random.Range(0, magnets.Count);
			MagnetSO magnet = magnets[index];
			magnets.RemoveAt(index);
			return magnet;
		}
	}
}

