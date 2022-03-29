namespace MagnetGame
{
	public enum MagnetEffect {
		VAMPIRIC,
		INVIGORATE,
		COUNTERSPELL,
		SHIELD,
		OMEN,
		EXTRA_ATTACK,
		RETURN_CARD,
		GUARANTEE,
	}

	public static class MagnetEffectExtensions
	{
		public static string GetLabel(this MagnetEffect effect) => effect switch {
			MagnetEffect.VAMPIRIC => "vampiric",
			_ => null,
		};

		public static IMagnetEffect GetScript(this MagnetEffect effect) => effect switch {
			MagnetEffect.VAMPIRIC => new Vampiric(),
			_ => null,
		};

	}
}

