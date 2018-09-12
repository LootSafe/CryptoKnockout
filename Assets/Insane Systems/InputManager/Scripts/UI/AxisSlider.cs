using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.InputManager.UI
{
    [RequireComponent(typeof(Slider))]
    public class AxisSlider : MonoBehaviour
    {
        AxisAction editableAction;

        [SerializeField] Text nameText;
        RectTransform rectTransform;
        Slider slider;

        SliderCallback actionOnChange;

        public delegate void SliderCallback(float value);

        protected void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(delegate { OnChange(); });
        }

        public void SetAnchoredPosition(Vector2 position)
        {
            rectTransform.anchoredPosition = position;
        }

        public void SetName(string name)
        {
            if (nameText != null)
                nameText.text = name;
        }

        public void SetupActionOnChange(SliderCallback newActionOnChange)
        {
            actionOnChange = newActionOnChange;
        }

        public void SetEditableAxisAction(AxisAction action)
        {
            editableAction = action;
        }

        public void OnChange()
        {
            if (actionOnChange != null)
                actionOnChange(slider.value);
        }

        public float Value
        {
            get { return slider.value; }
            set { slider.value = value; }
        }
    }
}