using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CarbonInput {
    /// <summary>
    /// Touch control simulating a single gamepad button.
    /// </summary>
    public class TouchButton : BaseTouchInput, IPointerDownHandler, IPointerUpHandler, IDragHandler {
        /// <summary>
        /// The <see cref="CButton"/> this control emulates.
        /// </summary>
        public CButton Button;
        /// <summary>
        /// Opacity of this control if it is pressed.
        /// </summary>
        [Tooltip("Opacity of this control if it is pressed.")]
        [Range(0, 1)]
        public float OpacityPressed = 0.5f;
        /// <summary>
        /// Opacity of this control if it is not pressed.
        /// </summary>
        [Tooltip("Opacity of this control if it is not pressed.")]
        [Range(0, 1)]
        public float OpacityReleased = 1f;

        void Start() {
            InitMapping();
            UpdateState(false);
        }

        /// <summary>
        /// Updates the state of this control. This methods sets the opacity and the state in the <see cref="TouchMapping"/>.
        /// </summary>
        /// <param name="pressed"></param>
        public void UpdateState(bool pressed) {
            var image = GetComponent<Image>();
            var color = image.color;
            color.a = pressed ? OpacityPressed : OpacityReleased;
            image.color = color;
            if(Mapping != null) Mapping[Button] = pressed;
        }
        
        public void OnPointerDown(PointerEventData eventData) {
            UpdateState(true);
        }

        public void OnPointerUp(PointerEventData eventData) {
            UpdateState(false);
        }

        public void OnDrag(PointerEventData eventData) {
            RectTransform rect = GetComponent<RectTransform>();
            UpdateState(RectTransformUtility.RectangleContainsScreenPoint(rect, eventData.position));
        }
    }
}
