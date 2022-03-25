using System;

namespace MagnetGame
{
	public enum Result { WIN, LOSE, DRAW, }
	public enum MagnetType { SERVICES, ANNIVERSARIES, TOURISM, }

	public static class MagnetTypeExtensions
	{
		public static Result Compare(this MagnetType type1, MagnetType type2) => type1 switch {
			MagnetType.SERVICES => type2 switch {
				MagnetType.SERVICES		=> Result.DRAW,
				MagnetType.ANNIVERSARIES	=> Result.WIN,
				MagnetType.TOURISM		=> Result.LOSE,
				_ => throw new ArgumentOutOfRangeException(nameof(type2), $"Not expected type2 value: {type2}"),
			},

			MagnetType.ANNIVERSARIES => type2 switch {
				MagnetType.SERVICES		=> Result.LOSE,
				MagnetType.ANNIVERSARIES	=> Result.DRAW,
				MagnetType.TOURISM		=> Result.WIN,
				_ => throw new ArgumentOutOfRangeException(nameof(type2), $"Not expected type2 value: {type2}"),
			},

			MagnetType.TOURISM => type2 switch {
				MagnetType.SERVICES		=> Result.WIN,
				MagnetType.ANNIVERSARIES	=> Result.LOSE,
				MagnetType.TOURISM		=> Result.DRAW,
				_ => throw new ArgumentOutOfRangeException(nameof(type2), $"Not expected type2 value: {type2}"),
			},

			_ => throw new ArgumentOutOfRangeException(nameof(type1), $"Not expected type1 value: {type1}"),
		};
	}
}

