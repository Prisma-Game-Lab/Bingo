namespace MagnetGame
{
	public class Guarantee : IMagnetEffect
	{
		public void Effect(Player player, Player opponent, MagnetPile pile)
			=> player.Guarantee = 2;

	}
}

