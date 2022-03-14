using System;
using UnityEngine;

namespace MagnetGame
{

	public enum Result { WIN, LOSE, DRAW, }
	public enum Type { SERVICES, ANNIVERSARIES, TOURISM, }

	// TODO: Implement effects
	public class Magnet : MonoBehaviour
	{
		// TODO: Scriptable Object for magnets
		[SerializeField]
		private Type _type;
		public Type type { get => _type; private set => _type = value; }


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

