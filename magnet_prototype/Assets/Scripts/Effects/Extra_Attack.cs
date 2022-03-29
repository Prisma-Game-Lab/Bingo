namespace MagnetGame
{
	public class Extra_Attack : IMagnetEffect
	{
		public void Effect(Player player, Player opponent, MagnetPile pile) {
			if (opponent.CounterSpell > 0)
				opponent.CounterSpell = 0;
			else
				opponent.Damage();
		}
	}
}

