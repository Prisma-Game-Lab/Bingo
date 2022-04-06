using UnityEngine;
using UnityEngine.Localization;
using TMPro;

namespace MagnetGame {
	public class MagnetEffectDescriptionController : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI effectNameTMP;
		[SerializeField] private TextMeshProUGUI effectDescriptionTMP;

		private LocalizedString effectNameLS = new LocalizedString();
		private LocalizedString effectDescriptionLS = new LocalizedString();

		public void UpdateReferenceID(string id) {
			effectNameLS.SetReference("EffectNames", id);
			effectDescriptionLS.SetReference("EffectDescription", id);
		}

		private void OnEnable() {
			effectNameLS.StringChanged += UpdateEffectName;
			effectDescriptionLS.StringChanged += UpdateEffectDescription;
		}

		private void OnDisable() {
			effectNameLS.StringChanged -= UpdateEffectName;
			effectDescriptionLS.StringChanged -= UpdateEffectDescription;
		}

		private void UpdateEffectName(string s) => effectNameTMP.SetText(s);
		private void UpdateEffectDescription(string s) => effectDescriptionTMP.SetText(s);
	}
}

