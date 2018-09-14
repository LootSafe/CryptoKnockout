using UnityEngine;

// User don't have to use this directly, so there is no need to have this in global namespace
namespace CarbonInput {
    /// <summary>
    /// Describes a mapping for a single gamepad button. 
    /// For normal gamepads, IsWrapper is false and therefor the Button attribute is used. 
    /// If IsWrapper is set to true, the KeyCode is used. 
    /// </summary>
    [System.Serializable]
    public class ButtonMapping {
        /// <summary>
        /// If <see cref="Type"/> is <see cref="ButtonType.Default"/> this is the joystick button id.
        /// </summary>
        public int Button;
        /// <summary>
        /// If <see cref="Type"/> is <see cref="ButtonType.Wrapper"/> this is the key used to emulate this gamepad button.
        /// </summary>
        public KeyCode Key;
        /// <summary>
        /// Defines if this mapping is a wrapper or not.
        /// </summary>
        public ButtonType Type = ButtonType.Default;

        public ButtonMapping() { }
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="other"></param>
        public ButtonMapping(ButtonMapping other) { CopyFrom(other); }

        /// <summary>
        /// Copy all values from the parameter.
        /// </summary>
        /// <param name="other"></param>
        public void CopyFrom(ButtonMapping other) {
            Button = other.Button;
            Key = other.Key;
            Type = other.Type;
        }

        /// <summary>
        /// Defines if a button is wrapper or not.
        /// </summary>
        public enum ButtonType {
            /// <summary>
            /// Button is a real gamepad button.
            /// </summary>
            Default,
            /// <summary>
            /// Uses a keyboard key to emulate a gamepad button.
            /// </summary>
            Wrapper
        }
    }

    /// <summary>
    /// Describes a mapping for a single gamepad axis. 
    /// Every axis can be inverted. 
    /// </summary>
    [System.Serializable]
    public class AxisMapping {
        /// <summary>
        /// Index of gamepad axis, used if Type is Default or Clamped.
        /// Used as button index if Type is ButtonWrapper or ButtonWrapper2.
        /// </summary>
        public int Axis;
        /// <summary>
        /// Only used if Type is ButtonWrapper2.
        /// Button index for positive value.
        /// </summary>
        public int Alternative;
        /// <summary>
        /// Whether this axis will be inverted.
        /// </summary>
        public bool Invert = false;
        /// <summary>
        /// Defines how this mapping behaves.
        /// </summary>
        public AxisType Type = AxisType.Default;
        /// <summary>
        /// If Type is ButtonWrapper, this is the value returned if the button is not pressed. 
        /// If Type is Clamped, this is the lower bound of the axis.
        /// </summary>
        public float Min = 0.0f;
        /// <summary>
        /// If Type is ButtonWrapper, this is the value returned if the button is pressed.
        /// If Type is Clamped, this is the upper bound of the axis.
        /// </summary>
        public float Max = 1.0f;
        /// <summary>
        /// Used for KeyWrapper. Axis value is -1 if this key is pressed and Key2 is not pressed.
        /// </summary>
        public KeyCode Key1;
        /// <summary>
        /// Used for KeyWrapper. Axis value is 1 if this key is pressed and Key1 is not pressed.
        /// </summary>
        public KeyCode Key2;

        public AxisMapping() { }
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="other"></param>
        public AxisMapping(AxisMapping other) { CopyFrom(other); }

        /// <summary>
        /// Copy all values from the parameter.
        /// </summary>
        /// <param name="other"></param>
        public void CopyFrom(AxisMapping other) {
            Axis = other.Axis;
            Alternative = other.Alternative;
            Invert = other.Invert;
            Type = other.Type;
            Min = other.Min;
            Max = other.Max;
            Key1 = other.Key1;
            Key2 = other.Key2;
        }

        /// <summary>
        /// Enumeration of all possible axis types.
        /// </summary>
        public enum AxisType {
            /// <summary>
            /// Axis is a normal gamepad axis.
            /// </summary>
            Default,
            /// <summary>
            /// Gamepad does not have this axis, but it has a button for that axis
            /// </summary>
            ButtonWrapper,
            /// <summary>
            /// Gamepad does not have anything for this, fallback to KeyCodes
            /// </summary>
            KeyWrapper,
            /// <summary>
            /// The range of this axis is not in the normal range.
            /// </summary>
            Clamped,
            /// <summary>
            /// Gamepad does not have this axis, but it can be emulated by two buttons.
            /// </summary>
            ButtonWrapper2,
            /// <summary>
            /// Gamepad axis goes from -1 to 1, but it should go from 0 to 1. 
            /// </summary>
            TriggerLimiter
        }
    }
}
