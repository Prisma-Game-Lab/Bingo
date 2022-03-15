using System;
using UnityEngine;

namespace MagnetGame
{

	public enum Result { WIN, LOSE, DRAW, }
	public enum Type { SERVICES, ANNIVERSARIES, TOURISM, }

	// TODO: Implement effects
	public class Magnet : MonoBehaviour
	{
		[SerializeField]
		private MagnetSO magnet_stats;

		// TODO: bloody naming consistency
		public Type type { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }

		private void Start() {
			type = magnet_stats.magnet_type;
			Name = magnet_stats.magnet_name;
			Description = magnet_stats.magnet_description;
		}

		public Result Compare(Type type) => Compare(this.type, type);
		public Result Compare(Magnet magnet) => Compare(this.type, magnet.type);

		public static Result Compare(Type type1, Type type2) => type1 switch {
			Type.SERVICES => type2 switch {
				Type.SERVICES		=> Result.DRAW,
				Type.ANNIVERSARIES	=> Result.WIN,
				Type.TOURISM		=> Result.LOSE,
				_ => throw new ArgumentOutOfRangeException(nameof(type2), $"Not expected type2 value: {type2}"),
			},

			Type.ANNIVERSARIES => type2 switch {
				Type.SERVICES		=> Result.LOSE,
				Type.ANNIVERSARIES	=> Result.DRAW,
				Type.TOURISM		=> Result.WIN,
				_ => throw new ArgumentOutOfRangeException(nameof(type2), $"Not expected type2 value: {type2}"),
			},

			Type.TOURISM => type2 switch {
				Type.SERVICES		=> Result.WIN,
				Type.ANNIVERSARIES	=> Result.LOSE,
				Type.TOURISM		=> Result.DRAW,
				_ => throw new ArgumentOutOfRangeException(nameof(type2), $"Not expected type2 value: {type2}"),
			},

			_ => throw new ArgumentOutOfRangeException(nameof(type1), $"Not expected type1 value: {type1}"),
		};

	}
}

