using UnityEngine;
using UnityEngine.EventSystems;

namespace Basic.UI
{

    public class DraggablePanel : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [Header("Must be reset or set to center mode with pivot 0.5 0.5")]
        [SerializeField] private RectTransform _panelRectTransform;
        [Header("For bound limit to work it must be in fullsize rect even when assigned here or a direct child of canvas")]
        [SerializeField] private Canvas _canvas;
        [Tooltip("There can be huger gap in horizontal, when canvas is not 1:1")][SerializeField] private float _edgePadding = 10f;

        private Vector2 _dragStartPosition;
        private Vector2 _minPosition;
        private Vector2 _maxPosition;

        private void Awake()
        {
            if (_panelRectTransform == null)
                _panelRectTransform = GetComponent<RectTransform>();

            if (_canvas == null)
                _canvas = GetComponentInParent<Canvas>();

            CalculateBounds();
        }

        private void CalculateBounds()
        {
            Vector2 panelSize = _panelRectTransform.sizeDelta / 2;
            Vector2 canvasSize = _canvas.GetComponent<RectTransform>().sizeDelta / 2;

            _minPosition = -canvasSize + panelSize + new Vector2(_edgePadding, _edgePadding);
            _maxPosition = canvasSize - panelSize - new Vector2(_edgePadding, _edgePadding);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Vector2 mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out mousePosition
            );

            _dragStartPosition = _panelRectTransform.anchoredPosition - mousePosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 mousePosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out mousePosition
            ))
            {
                Vector2 newPosition = mousePosition + _dragStartPosition;
                newPosition.x = Mathf.Clamp(newPosition.x, _minPosition.x, _maxPosition.x);
                newPosition.y = Mathf.Clamp(newPosition.y, _minPosition.y, _maxPosition.y);
                _panelRectTransform.anchoredPosition = newPosition;
            }
        }
    }
}