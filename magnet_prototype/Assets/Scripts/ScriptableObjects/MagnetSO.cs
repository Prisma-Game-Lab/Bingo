using UnityEngine;

namespace MagnetGame
{
	[CreateAssetMenu(fileName = "Magnet", menuName = "ScriptableObjects/MagnetScriptableObject")]
	public class MagnetSO : ScriptableObject
	{
		public string magnet_name;
		public string magnet_description;
		public Type magnet_type;
		// TODO: magnet effect
		// TODO: magnet sprite
	}
}

