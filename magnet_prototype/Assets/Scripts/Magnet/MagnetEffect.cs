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

	// TODO: Some delegates and static classes would be nice.
	public static class MagnetEffectExtensions
	{
		public static string GetLabel(this MagnetEffect effect) => effect switch {
			MagnetEffect.COUNTERSPELL => "counterspell",
			MagnetEffect.EXTRA_ATTACK => "extra_attack",
			MagnetEffect.GUARANTEE => "guarantee",
			MagnetEffect.INVIGORATE => "invigorate",
			MagnetEffect.OMEN => "omen",
			MagnetEffect.RETURN_CARD => "return_card",
			MagnetEffect.SHIELD => "shield",
			MagnetEffect.VAMPIRIC => "vampiric",
			_ => null,
		};

		public static IMagnetEffect GetScript(this MagnetEffect effect) => effect switch {
			MagnetEffect.COUNTERSPELL => new Counterspell(),
			MagnetEffect.EXTRA_ATTACK => new Extra_Attack(),
			MagnetEffect.GUARANTEE => new Guarantee(),
			MagnetEffect.INVIGORATE => new Invigorate(),
			MagnetEffect.OMEN => new Omen(),
			MagnetEffect.RETURN_CARD => new Return_Card(),
			MagnetEffect.SHIELD => new Shield(),
			MagnetEffect.VAMPIRIC => new Vampiric(),
			_ => null,
		};

	}
}

