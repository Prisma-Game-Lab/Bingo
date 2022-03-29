namespace MagnetGame
{
	public class Counterspell : IMagnetEffect
	{
		public void Effect(Player player, Player opponent, MagnetPile pile)
			=> player.CounterSpell = 2;

	}
}

