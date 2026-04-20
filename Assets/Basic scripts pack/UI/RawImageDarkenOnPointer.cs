using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Basic.UI
{
    public class RawImageDarkenOnPointer_Legacy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("None gets 1st closest")]
        [SerializeField] private RawImage _rawImage;
        [SerializeField] private float _valueReduction = 0.3f;

        private Color _originalColor;

        private void Awake()
        {
            if (_rawImage == null)
                _rawImage = GetComponentInChildren<RawImage>();

            _originalColor = _rawImage.color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Color.RGBToHSV(_originalColor, out float h, out float s, out float v);
            _rawImage.color = Color.HSVToRGB(h, s, Mathf.Max(0, v - _valueReduction));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _rawImage.color = _originalColor;
        }
    }
}