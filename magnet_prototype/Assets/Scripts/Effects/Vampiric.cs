using UnityEngine.Localization;

namespace MagnetGame
{
	public class Vampiric : IMagnetEffect
	{
		public void Effect(Player player, Player opponent, MagnetPile pile) {
			player.Heal();
		}

	}
}

