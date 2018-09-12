using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.InputManager.UI
{
    public class KeyEntry : SettingsEntry
    {
        KeyAction keyAction;

        [SerializeField] KeyButton keyField;
        [SerializeField] KeyButton alternativeKeyField;

        public void SetupByKeyAction(KeyAction keyAction)
        {
            this.keyAction = keyAction;

            SetName(keyAction.Name);

            keyField.SetActualKeyText(keyAction.Key);
            keyField.SetKeyPressAction(StartSetupKey);

            alternativeKeyField.SetActualKeyText(keyAction.AlternativeKey);
            alternativeKeyField.SetKeyPressAction(StartSetupKey);
        }

        void StartSetupKey(bool isAlternative)
        {
			ControlsSettings.singleton.StartSetupKey(keyAction, isAlternative, this.OnEndKeySetup);
        }

        public void OnEndKeySetup(KeyCode key, bool isAlternative)
        {
            if (isAlternative)
                alternativeKeyField.SetActualKeyText(key);
            else
                keyField.SetActualKeyText(key);
        }
    }
}