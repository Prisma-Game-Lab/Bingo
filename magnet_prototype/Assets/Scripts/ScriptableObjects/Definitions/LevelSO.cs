using System.Collections.Generic;
using UnityEngine;

namespace MagnetGame
{
	[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
	public class LevelSO : ScriptableObject
	{
		public string opponentName;
		public Sprite scenarySprite;
		public Sprite opponentSprite;
	}
}

