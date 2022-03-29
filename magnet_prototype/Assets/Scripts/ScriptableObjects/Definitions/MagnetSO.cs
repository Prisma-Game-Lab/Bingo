using UnityEngine;
using UnityEngine.Localization;

namespace MagnetGame
{
	[CreateAssetMenu(fileName = "Magnet", menuName = "ScriptableObjects/Magnet")]
	public class MagnetSO : ScriptableObject
	{
		public LocalizedString title;
		public LocalizedString description;
		public MagnetType type;
		public MagnetEffect[] effects = new MagnetEffect[2];
		public Sprite sprite;
	}
}

