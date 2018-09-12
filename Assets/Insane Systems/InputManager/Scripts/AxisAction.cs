using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsaneSystems.InputManager
{
    [System.Serializable]
    public class AxisAction : InputAction
    {
        [SerializeField] string inputManagerAxis;
        [SerializeField] [Range(0f, 1f)] float deadZone;
        [SerializeField] [Range(0f, 2f)] float sensivity;
        [SerializeField] bool isInverted;

        float maxValue = 1f; // some joysticks use value ranges bigger than [-1, 1]. So, it's needed to be detected, and place max value here.

        public string AxisName
        {
            get { return inputManagerAxis; }
        }

        public float DeadZone
        {
            get { return deadZone; }
        }

        public float Sensivity
        {
            get { return sensivity; }
        }
        
        public bool IsInverted
        {
            get { return isInverted; }
        }

        public override bool IsActive()
        {
            return Input.GetAxis(inputManagerAxis) != 0f;
        }

        /// <summary>
        /// Returns input value of axis.
        /// </summary>
        public float GetValue()
        {
            float readedInput = Input.GetAxis(inputManagerAxis);
            float absoluteReadedInput = Mathf.Abs(readedInput);

            if (absoluteReadedInput > maxValue)
                maxValue = absoluteReadedInput;

            float unscaledInput = (readedInput / maxValue);

            if (Mathf.Abs(unscaledInput) < deadZone)
                return 0;

            readedInput = unscaledInput * sensivity;
            readedInput *= isInverted ? -1 : 1;
            
            return readedInput;
        }

        public float GetRawValue()
        {
            return Input.GetAxis(inputManagerAxis);
        }

        public void UpdateAxis(string newAxis)
        {
            inputManagerAxis = newAxis;
        }

        public void SetDeadZone(float value)
        {
            deadZone = value;
        }

        public void SetSensivity(float value)
        {
            sensivity = value;
        }

        public void SetIsInverted(bool value)
        {
            isInverted = value;
        }
    }
}