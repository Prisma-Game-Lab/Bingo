using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using TMPro;

namespace MagnetGame
{

	// TODO: Implement effects
	public class Magnet : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private static readonly Vector3 selectedPos = new Vector3(0.0f, 0.3f, 0.0f);

		[SerializeField] private MagnetSO magnetStats;
		[SerializeField] private TextMeshPro cardTitleTMP;
		[SerializeField] private TextMeshPro cardDescriptionTMP;
		[SerializeField] private GameObject graphics;

		[field: SerializeField] public MagnetType type { get; private set; }

		public LocalizedString title { get; private set; }
		public LocalizedString description { get; private set; }

		private bool isSelected = false;
		public bool IsSelected {
			get => isSelected;
			set {
				isSelected = value;
				if (!isSelected)
					graphics.transform.localPosition = Vector3.zero;
			}
		}

		public delegate void MagnetClickedEvent(Magnet source);
		public static event MagnetClickedEvent OnMagnetClicked;

		public MagnetSO MagnetStats {
			get => magnetStats;
			set {
				magnetStats = value;
				// TODO: make this less bad
				OnDisable();
				UpdateMagnetSO();
				OnEnable();
			}
		}

		private void Awake() {
			if (magnetStats != null) UpdateMagnetSO();
		}

		private void OnEnable() {
			title.StringChanged += UpdateTitleString;
			description.StringChanged += UpdateDescriptionString;
		}

		private void OnDisable() {
			title.StringChanged -= UpdateTitleString;
			description.StringChanged -= UpdateDescriptionString;
		}

		private void UpdateMagnetSO() {
			type = magnetStats.type;
			title = magnetStats.title;
			description = magnetStats.description;
		}

		public void OnPointerClick(PointerEventData eventData)
			=> OnMagnetClicked?.Invoke(this);

		public void OnPointerEnter(PointerEventData eventData)
			=> graphics.transform.localPosition = selectedPos;

		public void OnPointerExit(PointerEventData eventData) {
			if (!isSelected)
				graphics.transform.localPosition = Vector3.zero;
		}

		private void UpdateTitleString(string s) => cardTitleTMP.SetText(s);
		private void UpdateDescriptionString(string s) => cardDescriptionTMP.SetText(s);

		public Result Compare(Magnet magnet) => this.type.Compare(magnet.type);

	}
}

