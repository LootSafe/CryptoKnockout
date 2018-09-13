using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.InputManager.UI
{
    [RequireComponent(typeof(Button))]
    public class KeyButton : MonoBehaviour
    {
        [SerializeField] bool isAlternative;
        [SerializeField] Text buttonText;
        Button selfButton;

        EventKeyPress keyPressAction;

        public delegate void EventKeyPress(bool isAlternative);

        void Awake()
        {
            selfButton = GetComponent<Button>();
            selfButton.onClick.AddListener(OnClick);
        }

        public void SetActualKeyText(KeyCode key)
        {
            buttonText.text = key.ToString();
        }

        public void SetKeyPressAction(EventKeyPress action)
        {
            keyPressAction = action;
        }

        void OnClick()
        {
            if (keyPressAction != null)
            {
                keyPressAction(isAlternative);
                Settings.singleton.SetHelpTextShown(true);
            }
        }
    }
}