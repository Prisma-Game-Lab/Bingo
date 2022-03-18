using UnityEngine;

namespace MagnetGame {
	public class AIController : Player
	{
		public MagnetSO ChoosePlay() {
			int index = Random.Range(0, Hand.Count);
			MagnetSO magnet = Hand[index];
			RemoveFromHandAt(index);
			return magnet;
		}

		public int ChooseBuy(MagnetSO[] magnets) {
			return Random.Range(0, magnets.Length);
		}
	}
}

