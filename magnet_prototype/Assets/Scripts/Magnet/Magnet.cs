using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using TMPro;

namespace MagnetGame
{
	public class Magnet : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private static readonly Vector3 selectedPos = new Vector3(0.0f, 0.3f, 0.0f);

		private static Dictionary<MagnetType, Sprite> typeSpritesDict = null;

		[SerializeField] private MagnetSO magnetStats;
		[SerializeField] private TextMeshPro cardTitleTMP;
		[SerializeField] private TextMeshPro cardDescriptionTMP;
		[SerializeField] private GameObject graphics;
		[SerializeField] private SpriteRenderer typeIcon;
		[SerializeField] private SpriteRenderer[] effectIcon;

		[SerializeField] private Sprite[] typeSprites;

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

		public bool isSelectable = true;

		public delegate void MagnetClickedEvent(Magnet source);
		public delegate void MagnetHoveredEvent(MagnetSO source);
		public delegate void MagnetExitedEvent();

		public static event MagnetClickedEvent OnMagnetClicked;
		public static event MagnetHoveredEvent OnMagnetHovered;
		public static event MagnetExitedEvent OnMagnetExited;

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
			if (typeSpritesDict == null) {
				typeSpritesDict = new Dictionary<MagnetType, Sprite>();
				typeSpritesDict.Add(MagnetType.ANNIVERSARIES, typeSprites[0]);
				typeSpritesDict.Add(MagnetType.SERVICES, typeSprites[1]);
				typeSpritesDict.Add(MagnetType.TOURISM, typeSprites[2]);
			}

			if (magnetStats != null) UpdateMagnetSO();
		}

		private void OnEnable() {
			if (title != null)
				title.StringChanged += UpdateTitleString;

			if (description != null)
				description.StringChanged += UpdateDescriptionString;
		}

		private void OnDisable() {
			if (title != null)
				title.StringChanged -= UpdateTitleString;

			if (description != null)
				description.StringChanged -= UpdateDescriptionString;
		}

		private void UpdateMagnetSO() {
			type = magnetStats.type;
			title = magnetStats.title;
			description = magnetStats.description;
			graphics.GetComponent<SpriteRenderer>().sprite = magnetStats.sprite;
			typeIcon.sprite = typeSpritesDict[type];
		}

		public void OnPointerClick(PointerEventData eventData) {
			if (!isSelectable || GameStateManager.IsPaused())
				return;

			OnMagnetClicked?.Invoke(this);
		}

		public void OnPointerEnter(PointerEventData eventData) {
			if (GameStateManager.IsPaused())
				return;

			OnMagnetHovered?.Invoke(this.MagnetStats);

			if (!isSelectable)
				return;

			AudioManager.instance.Play("card_pop_up");
			graphics.transform.localPosition = selectedPos;
		}

		public void OnPointerExit(PointerEventData eventData) {
			OnMagnetExited?.Invoke();

			if (!isSelectable)
				return;
			if (!isSelected) {
				graphics.transform.localPosition = Vector3.zero;
				if (GameStateManager.IsPaused())
					return;
				AudioManager.instance.Play("card_pop_down");
			}
		}

		private void UpdateTitleString(string s) => cardTitleTMP.SetText(s);
		private void UpdateDescriptionString(string s) => cardDescriptionTMP.SetText(s);

		public Result Compare(Magnet magnet) => this.type.Compare(magnet.type);

	}
}

