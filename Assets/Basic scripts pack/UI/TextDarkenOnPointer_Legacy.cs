using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Basic.UI
{
    public class TextDarkenOnPointer_Legacy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("None gets 1st closest")]
        [SerializeField] private Text _text;
        [SerializeField] private float _valueReduction = 0.3f;

        private Color _originalColor;

        private void Awake()
        {
            if (_text == null)
                _text = GetComponentInChildren<Text>();

            _originalColor = _text.color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Color.RGBToHSV(_originalColor, out float h, out float s, out float v);
            _text.color = Color.HSVToRGB(h, s, Mathf.Max(0, v - _valueReduction));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _text.color = _originalColor;
        }
    }
}