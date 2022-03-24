using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using TMPro;

namespace MagnetGame
{

	public enum Result { WIN, LOSE, DRAW, }
	public enum Type { SERVICES, ANNIVERSARIES, TOURISM, }

	// TODO: Implement effects
	public class Magnet : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] private MagnetSO magnetStats;
		[SerializeField] private TextMeshPro cardTitleTMP;
		[SerializeField] private TextMeshPro cardDescriptionTMP;

		[field: SerializeField] public Type type { get; private set; }

		public LocalizedString title { get; private set; }
		public LocalizedString description { get; private set; }

		public delegate void MagnetClickedEvent(Magnet source);
		public static event MagnetClickedEvent OnMagnetClicked;

		public MagnetSO MagnetStats {
			get => magnetStats;
			set {
				magnetStats = value;
				UpdateMagnetSO();
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

		public void OnPointerClick(PointerEventData eventData) => OnMagnetClicked?.Invoke(this);
		public void OnPointerEnter(PointerEventData eventData) => Debug.Log("pointer enter", this);
		public void OnPointerExit(PointerEventData eventData) => Debug.Log("pointer exit", this);

		private void UpdateTitleString(string s) => cardTitleTMP.SetText(s);
		private void UpdateDescriptionString(string s) => cardDescriptionTMP.SetText(s);

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

