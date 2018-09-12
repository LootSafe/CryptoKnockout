using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.InputManager.UI
{
    public class SettingsEntry : MonoBehaviour
    {
        [SerializeField] Text nameText;

        protected RectTransform rectTransform;

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        protected virtual void Start() {}
        protected virtual void Update() { }

        public void SetName(string value)
        {
            nameText.text = value;
        }

        public void SetAnchoredPosition(Vector2 position)
        {
            rectTransform.anchoredPosition = position;
        }

        public void SetVerticalPosition(float position)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, position);
        }
    }
}