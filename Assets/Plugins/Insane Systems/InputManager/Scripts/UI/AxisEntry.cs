using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.InputManager.UI
{
    public class AxisEntry : SettingsEntry
    {
        AxisAction axisAction;

        [SerializeField] AxisDropdown axisDropdown;
        [SerializeField] Button autoAxisButton;
        [SerializeField] AxisSlider sensivitySlider;
        [SerializeField] AxisSlider deadZoneSlider;
        [SerializeField] Toggle invertToggle;

        public void SetupByAxisAction(AxisAction axisAction)
        {
            this.axisAction = axisAction;

            SetName(axisAction.Name);

            axisDropdown.SetValueByAxisName(axisAction.AxisName);
            axisDropdown.SetupActionOnChange(axisAction.UpdateAxis);

            autoAxisButton.onClick.AddListener(OnAutoAxisButtonClick);

            deadZoneSlider.Value = axisAction.DeadZone;
            deadZoneSlider.SetEditableAxisAction(axisAction);
            deadZoneSlider.SetupActionOnChange(axisAction.SetDeadZone);

            sensivitySlider.Value = axisAction.Sensivity;
            sensivitySlider.SetEditableAxisAction(axisAction);
            sensivitySlider.SetupActionOnChange(axisAction.SetSensivity);

            invertToggle.isOn = axisAction.IsInverted;
            invertToggle.onValueChanged.AddListener(delegate { OnInvertToggleClick(invertToggle); });
        }

        public void OnAutoAxisButtonClick()
        {
			InsaneSystems.InputManager.ControlsSettings.singleton.StartAutoSetupAxis(axisAction, this.OnEndAxisSetup);
            UI.Settings.singleton.SetHelpTextShown(true);
        }

        public void OnEndAxisSetup(string selectedAxisName)
        {
            axisDropdown.SetValueByAxisName(selectedAxisName);
        }

        public void OnInvertToggleClick(Toggle toggle)
        {
            axisAction.SetIsInverted(toggle.isOn);
        }
    }
}