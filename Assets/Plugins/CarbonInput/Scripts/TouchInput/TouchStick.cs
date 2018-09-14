using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace CarbonInput {
    /// <summary>
    /// Touch control simulating a thumbstick.
    /// </summary>
    public class TouchStick : BaseTouchInput, IPointerDownHandler, IPointerUpHandler, IDragHandler {
        private const float NearZero = 0.0001f;

        /// <summary>
        /// Horizontal axis of this control.
        /// </summary>
        [Tooltip("Horizontal axis")]
        public CAxis X = CAxis.LX;
        /// <summary>
        /// Vertical axis of this control.
        /// </summary>
        [Tooltip("Vertical axis")]
        public CAxis Y = CAxis.LY;

        /// <summary>
        /// Touches inside this area will be handled by the stick.
        /// </summary>
        [Tooltip("Touches inside this area will be handled by the stick.")]
        public RectTransform TouchArea;
        /// <summary>
        /// Base of the joystick.
        /// </summary>
        [Tooltip("Base of the joystick.")]
        public RectTransform Base;
        /// <summary>
        /// Knob of the joystick.
        /// </summary>
        [Tooltip("Knob of the joystick.")]
        public RectTransform Stick;

        /// <summary>
        /// Maximum distance between center of base and center of stick.
        /// </summary>
        [Tooltip("Maximum distance between center of base and center of stick.")]
        [Range(20, 120)]
        public float Range = 60;
        /// <summary>
        /// Should the joystick disappear on release?
        /// </summary>
        [Tooltip("Should the joystick disappear on release?")]
        public bool HideOnRelease;
        /// <summary>
        /// If HideOnRelease is set to true, this value will determine after which time the joystick will start to fade out.
        /// </summary>
        [Tooltip("If HideOnRelease is set to true, this value will determine after which time the joystick will start to fade out.")]
        public float FadeoutDelay;
        /// <summary>
        /// If HideOnRelease is set to true, this value will determine how long the fadeout will last.
        /// </summary>
        [Tooltip("If HideOnRelease is set to true, this value will determine how long the fadeout will last.")]
        public float FadeoutTime = 1f;
        /// <summary>
        /// If the user moves to far away from the stick, should the stick follow?
        /// </summary>
        [Tooltip("If the user moves to far away from the stick, should the stick follow?")]
        public bool Movable;

        private CanvasRenderer[] childRenderer;

        void Start() {
            InitMapping();
            childRenderer = GetComponentsInChildren<CanvasRenderer>();
            if(HideOnRelease) Hide(false);
        }

        /// <summary>
        /// Shows this control.
        /// </summary>
        public void Show() {
            StopAllCoroutines();
            SetOpacity(1f);
        }

        /// <summary>
        /// Hides this control.
        /// </summary>
        /// <param name="fadeout">If true, the control will slowly fade out.</param>
        public void Hide(bool fadeout) {
            StopAllCoroutines();
            if(fadeout) StartCoroutine(FadeSequence());
            else SetOpacity(0f);
        }

        /// <summary>
        /// Sets the opacity of this control and all children.
        /// </summary>
        /// <param name="opacity"></param>
        private void SetOpacity(float opacity) {
            foreach(CanvasRenderer renderer in childRenderer) renderer.SetAlpha(opacity);
        }

        /// <summary>
        /// Coroutine used to slowly fadeout.
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeSequence() {
            if(FadeoutDelay > 0) yield return new WaitForSeconds(FadeoutDelay);
            float opacity = 1f;
            float speed = 1f / FadeoutTime;
            while(opacity >= 0.0f) {
                opacity -= Time.deltaTime * speed;
                if(opacity < 0) opacity = 0;
                SetOpacity(opacity);
                yield return null;
            }
        }

        /// <summary>
        /// Sets the value of this stick in the <see cref="TouchMapping"/> and also sets the knob position. 
        /// If <see cref="Movable"/> is true, it will also follow the user.
        /// </summary>
        /// <param name="pos">Touch position in world space</param>
        private void UpdateStick(Vector2 pos) {
            // get direction in local space
            Vector2 direction = (pos - (Vector2)Base.position);
            direction.x /= Base.lossyScale.x;
            direction.y /= Base.lossyScale.y;
            float length = direction.magnitude;
            if(length < NearZero) {
                UpdateAxis(Vector2.zero);
                return;
            }
            if(length > Range) {
                if(Movable) {
                    Vector2 delta = direction.normalized * (length - Range);
                    Vector2 newPos = (Vector2)Base.localPosition + delta;
                    newPos.x = Mathf.Clamp(newPos.x, TouchArea.rect.xMin, TouchArea.rect.xMax);
                    newPos.y = Mathf.Clamp(newPos.y, TouchArea.rect.yMin, TouchArea.rect.yMax);
                    Base.localPosition = newPos;
                }
                length = Range;
            }
            UpdateAxis(direction.normalized * (length / Range));
        }

        /// <summary>
        /// Updates the <see cref="AxisMapping"/>.
        /// </summary>
        /// <param name="axis"></param>
        private void UpdateAxis(Vector2 axis) {
            if(Mapping == null) return;
            Stick.localPosition = axis * Range;
            Mapping[X] = axis.x;
            Mapping[Y] = -axis.y; // invert to match "normal" controller axis
        }

        public void OnPointerDown(PointerEventData data) {
            Show();
            if(RectTransformUtility.RectangleContainsScreenPoint(Stick, data.position) ||
                RectTransformUtility.RectangleContainsScreenPoint(Base, data.position)) {
                UpdateStick(data.position);
            } else if(Movable) {
                Base.position = data.position;
                UpdateAxis(Vector2.zero);
            }
        }

        public void OnPointerUp(PointerEventData data) {
            UpdateAxis(Vector2.zero);
            if(HideOnRelease) Hide(true);
        }

        public void OnDrag(PointerEventData data) {
            UpdateStick(data.position);
        }
    }
}
