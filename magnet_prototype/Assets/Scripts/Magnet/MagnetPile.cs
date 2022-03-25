using System.Collections.Generic;
using UnityEngine;

namespace MagnetGame
{
	public class MagnetPile : MonoBehaviour
	{
		private List<MagnetSO> stock = new List<MagnetSO>();
		private List<MagnetSO> wastepile = new List<MagnetSO>();

		public MagnetSO Draw() {
			if (stock.Count == 0) {
				var tmp = stock;
				stock = wastepile;
				wastepile = tmp;
			}

			MagnetSO magnet = stock[0];
			stock.RemoveAt(0);
			return magnet;
		}

		public void AddToStock(MagnetSO magnet)
			=> InsertRand<MagnetSO>(stock, magnet);

		public void AddToStock(List<MagnetSO> magnets)
			=> magnets.ForEach(AddToStock);

		public void Discard(MagnetSO magnet)
			=> InsertRand<MagnetSO>(wastepile, magnet);

		public void Discard(List<MagnetSO> magnets)
			=> magnets.ForEach(Discard);

		private static void InsertRand<T>(List<T> list, T element)
			=> list.Insert(Random.Range(0, list.Count), element);

	}
}

