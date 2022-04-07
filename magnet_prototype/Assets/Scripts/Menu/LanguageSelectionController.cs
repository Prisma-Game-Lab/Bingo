using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace MagnetGame {
	public class LanguageSelectionController : MonoBehaviour
	{
		public void SetLocale(Locale locale)
			=> LocalizationSettings.SelectedLocale = locale;
	}
}

