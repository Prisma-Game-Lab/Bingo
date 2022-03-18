using UnityEngine;
using UnityEngine.Localization;

namespace MagnetGame
{
	[CreateAssetMenu(fileName = "Magnet", menuName = "ScriptableObjects/MagnetScriptableObject")]
	public class MagnetSO : ScriptableObject
	{
		public LocalizedString title;
		public LocalizedString description;
		public Type type;
		public Sprite sprite;
		// TODO: magnet effect
	}
}

