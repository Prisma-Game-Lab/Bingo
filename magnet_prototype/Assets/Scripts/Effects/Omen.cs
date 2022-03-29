namespace MagnetGame
{
	public class Omen : IMagnetEffect
	{
		public void Effect(Player player, Player opponent, MagnetPile pile) {
			if (opponent.CounterSpell > 0)
				opponent.CounterSpell = 0;
			else {
				// TODO: implementation
			}
		}
	}
}

