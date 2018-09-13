using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsaneSystems.InputManager
{
    /// <summary>
    /// Class for menu with controls configuration. 
    /// </summary>
    public class ControlsSettings : MonoBehaviour
    {
        public static ControlsSettings singleton { get; private set; }

        public const float minAxisValueToChange = 0.25f;
        public const float preSelectKeyDelay = 0.25f;

        enum State
        {
            Idle,
            PrepareToKeySetup,
            SetupKey,
            SetupAxis
        }

        State state = State.Idle;

        KeyAction keyToSetup;
        bool isAlternative;

        AxisAction axisToSetup;

        float[] previousAxisValuesForAutodetect;
        bool autodectedIntialized = false;

        AutoAxisSetupCallback autoAxisCallback;
        KeyApplyCallback keyApplyCallback;

        public delegate void AutoAxisSetupCallback(string selectedAxisName);
        public delegate void KeyApplyCallback(KeyCode key, bool isAlternative);

        float preSelectKeyTimer = 0f;

        private void Awake()
        {
            singleton = this;
        }

        void Update()
        {
            if (state == State.SetupAxis && Input.GetKeyDown(KeyCode.Escape))
            {
                state = State.Idle;
                UI.Settings.singleton.SetHelpTextShown(false);
                return;
            }

            switch (state)
            {
                case State.PrepareToKeySetup:
                    {
                        if (preSelectKeyTimer <= 0)
                            state = State.SetupKey;
                        else
                            preSelectKeyTimer -= Time.fixedDeltaTime;

                        break;
                    }
                case State.SetupKey:
                    {
                        KeyCode? pressedKey = GetPressedKey();

                        if (pressedKey == KeyCode.Escape)
                            pressedKey = KeyCode.None;

                        if (pressedKey != null)
                        {
                            keyToSetup.UpdateKey((KeyCode)pressedKey, isAlternative);
                            state = State.Idle;

                            if (keyApplyCallback != null)
                            {
                                keyApplyCallback((KeyCode)pressedKey, isAlternative);
                                UI.Settings.singleton.SetHelpTextShown(false);
                            }
                        }

                        break;
                    }
                case State.SetupAxis:
                    {
                        string changedAxis = GetChangedAxis();

                        if (changedAxis != null)
                        {
                            axisToSetup.UpdateAxis(changedAxis);
                            state = State.Idle;

                            if (autoAxisCallback != null)
                            {
                                autoAxisCallback(changedAxis);
                                UI.Settings.singleton.SetHelpTextShown(false);
                            }
                        }

                        break;
                    }
            }
        }

        KeyCode? GetPressedKey()
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                if (Input.GetKey(vKey))
                    return vKey;

            return null;
        }

        string GetChangedAxis()
        {
            string[] allAxis = InputStorage.Singleton.UsingAxis;

            if (!autodectedIntialized)
                previousAxisValuesForAutodetect = new float[allAxis.Length];

            for (int i = 0; i < allAxis.Length; i++)
            {
                if (!autodectedIntialized)
                    previousAxisValuesForAutodetect[i] = Input.GetAxis(allAxis[i]);
                else
                {
                    if (Mathf.Abs(Input.GetAxis(allAxis[i])) - Mathf.Abs(previousAxisValuesForAutodetect[i]) > minAxisValueToChange)
                        return allAxis[i];
                }

                previousAxisValuesForAutodetect[i] = Input.GetAxis(allAxis[i]);
            }

            if (!autodectedIntialized)
                autodectedIntialized = true;

            return null;
        }

        public void StartSetupKey(KeyAction keyAction, bool isAlternative, KeyApplyCallback keyApplyCallback = null)
        {
            keyToSetup = keyAction;
            this.isAlternative = isAlternative;

            if (keyApplyCallback != null)
                this.keyApplyCallback = keyApplyCallback;

            preSelectKeyTimer = preSelectKeyDelay;
            
            state = State.PrepareToKeySetup;
        }

        public void StartAutoSetupAxis(AxisAction axisAction, AutoAxisSetupCallback autoAxisCallback = null)
        {
            axisToSetup = axisAction;

            if (autoAxisCallback != null)
                this.autoAxisCallback = autoAxisCallback;

            state = State.SetupAxis;
            autodectedIntialized = false;
        }

        /// <summary>
        /// This method saves all changes to PlayerPrefs on destroy (closing game or scene).
        /// </summary>
        void OnDestroy()
        {
            InputStorage.SaveToPlayerPrefs();
        }
    }
}