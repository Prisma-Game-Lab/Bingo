using UnityEngine;
using UnityEngine.UI;

namespace MagnetGame {
	public class MagnetEffectDescriptionPanelController : MonoBehaviour
	{
		[SerializeField] private MagnetEffectDescriptionController[] effects
			= new MagnetEffectDescriptionController[2];
		[SerializeField] private GameObject[] effectsGO = new GameObject[2];

		private Image panelBackground;

		private void Awake() {
			panelBackground = GetComponent<Image>();
			panelBackground.enabled = false;

			foreach (var effectGO in effectsGO)
				effectGO.SetActive(false);
		}

		private void OnEnable() {
			Magnet.OnMagnetHovered += Show;
			Magnet.OnMagnetExited += Hide;
		}

		private void OnDisable() {
			Magnet.OnMagnetHovered -= Show;
			Magnet.OnMagnetExited -= Hide;
		}

		public void Show(MagnetSO magnetStats) {
			foreach (var effectGO in effectsGO)
				effectGO.SetActive(true);

			for (int i = 0; i < effects.Length; ++i)
				effects[i].UpdateReferenceID(magnetStats.effects[i].GetLabel());

			panelBackground.enabled = true;
		}

		public void Hide() {
			foreach (var effectGO in effectsGO)
				effectGO.SetActive(false);

			panelBackground.enabled = false;
		}
	}
}

