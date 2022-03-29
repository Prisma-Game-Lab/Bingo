namespace MagnetGame
{
	public class Invigorate : IMagnetEffect
	{
		public void Effect(Player player, Player opponent, MagnetPile pile) {
			int count = 0;
			foreach (var magnet in player.Hand) {
				pile.Discard(magnet);
				++count;
			}

			player.ClearHand();

			for (; count > 0; --count)
				player.AddToHand(pile.Draw());
		}
	}
}

