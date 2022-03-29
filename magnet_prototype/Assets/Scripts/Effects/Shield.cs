namespace MagnetGame
{
	public class Shield : IMagnetEffect
	{
		public void Effect(Player player, Player opponent, MagnetPile pile)
			=> player.Shield = 2;
	}
}

