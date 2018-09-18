using UnityEngine;

// Users don't have to use this directly, so there is no need to have this in global namespace
namespace CarbonInput {
    /// <summary>
    /// Describes a mapping for a specific controller. This mapping is independend of the PlayerIndex.
    /// Each CarbonController defines how buttons and axes are mapped correctly to the Unity Input.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCarbonMapping", menuName = "Carbon Input/GamePad Mapping")]
    public class CarbonController : ScriptableObject {
        /// <summary>
        /// Number of buttons defined in the <see cref="CButton"/> enumeration.
        /// </summary>
        public const int ButtonCount = 10; // Must match the CButton enum!
        /// <summary>
        /// Number of axes defined in the <see cref="AxisCount"/> enumeration.
        /// </summary>
        public const int AxisCount = 8; // Must match the CAxis enum!
        /// <summary>
        /// Number of generated axes,
        /// </summary>
        public const int InputAxisCount = 16; // Must match the number of generated Input axes!
        /// <summary>
        /// Number of joystick buttons supported by unity.
        /// </summary>
        public const int JoystickButtonCount = 20; // Number of joystick buttons supported by Unity
        /// <summary>
        /// Number of entries in <see cref="PlayerIndex"/> enumeration.
        /// </summary>
        public const int PlayerIndices = 9; // Any, One, ..., Eight
        /// <summary>
        /// Prefix of all generated axes.
        /// </summary>
        public const string Tag = "cin_Axis";

        /// <summary>
        /// Mapping of [<see cref="PlayerIndex"/>, JoystickAxis] to its name.
        /// </summary>
        private static readonly string[,] AxisNames;
        static CarbonController() {
            // construct all strings beforehand
            AxisNames = new string[PlayerIndices, InputAxisCount];
            for(int id = 0; id < PlayerIndices; id++) {
                for(int axis = 0; axis < InputAxisCount; axis++) {
                    AxisNames[id, axis] = CreateName(id, axis);
                }
            }
        }
        /// <summary>
        /// Create the input axis name for <see cref="PlayerIndex"/> and axis.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static string CreateName(int id, int axis) {
            return Tag + id + "_" + axis.ToString("D2");
        }

        /// <summary>
        /// Regular expression used to check if this mapping can be used for a controller.
        /// </summary>
        public string RegEx;
        /// <summary>
        /// Platforms supported by this mapping.
        /// </summary>
        public CPlatform Platform;
        /// <summary>
        /// Priority of this mapping. On startup the system will try to find a correct mapping for all controller. Lower priority mappings will be tester earlier.
        /// </summary>
        public int Priority = 1000;
		/// <summary>
		/// If true, this mapping will only be used once, even if it could be used multiple times.
		/// </summary>
	    public bool UseOnce;
		/// <summary>
		/// If true, this mapping can be replaced by touch mappings.
		/// </summary>
	    public bool Replacable;
        /// <summary>
        /// All mappings for all possible <see cref="CAxis"/>. This array must have exactly <see cref="AxisCount"/> many entries.
        /// </summary>
        public AxisMapping[] Axes = new AxisMapping[AxisCount];
        /// <summary>
        /// All mappings for all possible <see cref="CButton"/>s. This array must have exactly <see cref="ButtonCount"/> many entries.
        /// </summary>
        public ButtonMapping[] Buttons = new ButtonMapping[ButtonCount];

        /// <summary>
        /// Returns true if this mapping is a fallback mapping. 
        /// A mapping is considered a fallback, if it doesn't have a proper <see cref="RegEx"/>.
        /// By default, the keyboard is considered a fallback mapping.
        /// </summary>
        /// <returns></returns>
        public bool IsFallback() {
            return string.IsNullOrEmpty(RegEx);
        }

        public CarbonController() {
            for(int i = 0; i < Buttons.Length; i++) Buttons[i] = new ButtonMapping();
            for(int i = 0; i < Axes.Length; i++) Axes[i] = new AxisMapping();
        }

        /// <summary>
        /// Checks if controller button btn of player id is pressed.
        /// </summary>
        /// <param name="btn">GamePad button</param>
        /// <param name="id">Index of player</param>
        /// <returns></returns>
        public virtual bool GetButton(CButton btn, int id) {
            ButtonMapping key = Buttons[(int)btn];
            if(key.Type == ButtonMapping.ButtonType.Wrapper) {
                if(key.Key != KeyCode.None) return Input.GetKey(key.Key);
            } else {
                //JoystickButton0 = 330 ... JoystickButton19 = 349
                //Joystick1Button0 = 350 ... Joystick1Button19 = 369
                // ...
                //Joystick8Button0 = 490 ... Joystick8Button19 = 509
                return Input.GetKey(KeyCode.JoystickButton0 + id * JoystickButtonCount + key.Button);
            }
            return false;
        }

        /// <summary>
        /// Returns the value of the virtual axis of player <paramref name="id"/> identified by the <paramref name="axis"/> parameter;
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual float GetAxis(CAxis axis, int id) {
            AxisMapping mapping = Axes[(int)axis];
            float result = 0;
            switch(mapping.Type) {
                case AxisMapping.AxisType.Default:
                    result = Input.GetAxis(AxisNames[id, mapping.Axis]);
                    break;
                case AxisMapping.AxisType.ButtonWrapper:
                    result = Input.GetKey(KeyCode.JoystickButton0 + id * JoystickButtonCount + mapping.Axis) ? mapping.Max : mapping.Min;
                    break;
                case AxisMapping.AxisType.KeyWrapper:
                    bool key1 = Input.GetKey(mapping.Key1);
                    bool key2 = Input.GetKey(mapping.Key2);
                    if(key1 && !key2) result = -1;
                    else if(!key1 && key2) result = 1;
                    else result = 0;
                    break;
                case AxisMapping.AxisType.Clamped:
                    result = Mathf.Clamp(Input.GetAxis(AxisNames[id, mapping.Axis]), mapping.Min, mapping.Max);
                    break;
                case AxisMapping.AxisType.ButtonWrapper2:
                    key1 = Input.GetKey(KeyCode.JoystickButton0 + id * JoystickButtonCount + mapping.Axis);
                    key2 = Input.GetKey(KeyCode.JoystickButton0 + id * JoystickButtonCount + mapping.Alternative);
                    if(key1 && !key2) result = -1;
                    else if(!key1 && key2) result = 1;
                    else result = 0;
                    break;
                case AxisMapping.AxisType.TriggerLimiter:
                    result = (Input.GetAxis(AxisNames[id, mapping.Axis]) + 1f) / 2f;
                    break;
            }
            if(mapping.Invert) return -result;
            return result;
        }

		/// <summary>
		/// Returns true if this mapping is supported on the execution platform.
		/// </summary>
		/// <returns></returns>
		public bool SupportedOnThisPlatform() {
			switch(Application.platform) {
				case RuntimePlatform.WebGLPlayer: return Has(CPlatform.WebGL);
				case RuntimePlatform.Android: return Has(CPlatform.Android);
				case RuntimePlatform.IPhonePlayer: return Has(CPlatform.IOS);
				case RuntimePlatform.LinuxPlayer: return Has(CPlatform.Linux);
				case RuntimePlatform.OSXEditor:
				case RuntimePlatform.OSXPlayer:
#if !UNITY_5_4_OR_NEWER && !UNITY_2017_1_OR_NEWER
				case RuntimePlatform.OSXWebPlayer:
#endif
					return Has(CPlatform.OSX);
#if !UNITY_5_5_OR_NEWER && !UNITY_2017_1_OR_NEWER
				case RuntimePlatform.PS3: return Has(CPlatform.PS3);
#endif
				case RuntimePlatform.PS4: return Has(CPlatform.PS4);
				case RuntimePlatform.PSP2: return Has(CPlatform.PSP2);
#if UNITY_5_3_OR_NEWER || UNITY_2017_1_OR_NEWER
				case RuntimePlatform.WiiU: return Has(CPlatform.Wii);
#endif
				case RuntimePlatform.WindowsEditor:
				case RuntimePlatform.WindowsPlayer: return Has(CPlatform.Windows);
#if !UNITY_5_4_OR_NEWER && !UNITY_2017_1_OR_NEWER
				case RuntimePlatform.WindowsWebPlayer: return Has(CPlatform.Windows);
				case RuntimePlatform.WP8Player: return Has(CPlatform.WP8);
#endif
				case RuntimePlatform.WSAPlayerARM:
				case RuntimePlatform.WSAPlayerX64:
				case RuntimePlatform.WSAPlayerX86: return Has(CPlatform.WSA);
#if !UNITY_5_5_OR_NEWER && !UNITY_2017_1_OR_NEWER
				case RuntimePlatform.XBOX360: return Has(CPlatform.XBox360);
#endif
				case RuntimePlatform.XboxOne: return Has(CPlatform.XBoxOne);
			}
			return false;
		}


		/// <summary>
		/// Checks if the given <see cref="CPlatform"/> is set in <see cref="Platform"/>.
		/// </summary>
		/// <param name="flag"></param>
		/// <returns></returns>
		private bool Has(CPlatform flag) {
            return (Platform & flag) == flag;
        }

        /// <summary>
        /// This will return a fallback instance, using the keyboard.
        /// </summary>
        /// <returns></returns>
        public static CarbonController CreateFallback() {
            CarbonController cc = CreateInstance<CarbonController>();
            cc.Platform = (CPlatform)(-1);
            MakeKeyWrapper(cc.Buttons[0], KeyCode.RightShift);
            MakeKeyWrapper(cc.Buttons[1], KeyCode.RightControl);
            MakeKeyWrapper(cc.Buttons[2], KeyCode.LeftShift);
            MakeKeyWrapper(cc.Buttons[3], KeyCode.Space);
            MakeKeyWrapper(cc.Buttons[4], KeyCode.Escape);
            MakeKeyWrapper(cc.Buttons[5], KeyCode.Return);
            MakeKeyWrapper(cc.Buttons[6], KeyCode.Q);
            MakeKeyWrapper(cc.Buttons[7], KeyCode.E);
            MakeKeyWrapper(cc.Buttons[8]);
            MakeKeyWrapper(cc.Buttons[9]);
            MakeKeyWrapper(cc.Axes[0], KeyCode.A, KeyCode.D);
            MakeKeyWrapper(cc.Axes[1], KeyCode.W, KeyCode.S);
            for(int i = 2; i < AxisCount; i++) MakeKeyWrapper(cc.Axes[i]);
            return cc;
        }

		/// <summary>
		/// This will return a fallback instance, which doesn't respond to any key.
		/// </summary>
		/// <returns></returns>
	    public static CarbonController CreateDisabledInput() {
		    var cc = CreateInstance<CarbonController>();
		    cc.name = "DisabledInput";
		    cc.Platform = (CPlatform)(-1);
		    cc.Replacable = true;
		    for(int i = 0; i < ButtonCount; i++)
			    MakeKeyWrapper(cc.Buttons[i]);
		    for(int i = 0; i < AxisCount; i++)
				MakeKeyWrapper(cc.Axes[i]);
		    return cc;
	    }

        /// <summary>
        /// Sets the given <see cref="AxisMapping"/> to be a <see cref="AxisMapping.AxisType.KeyWrapper"/>
        /// </summary>
        /// <param name="mapping"></param>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        private static void MakeKeyWrapper(AxisMapping mapping, KeyCode key1 = KeyCode.None, KeyCode key2 = KeyCode.None) {
            mapping.Type = AxisMapping.AxisType.KeyWrapper;
            mapping.Key1 = key1;
            mapping.Min = key1 != KeyCode.None ? -1 : 0;
            mapping.Key2 = key2;
            mapping.Max = key2 != KeyCode.None ? 1 : 0;
        }

        /// <summary>
        /// Sets the given <see cref="ButtonMapping"/> to be a <see cref="ButtonMapping.ButtonType.Wrapper"/>.
        /// </summary>
        /// <param name="mapping"></param>
        /// <param name="key"></param>
        private static void MakeKeyWrapper(ButtonMapping mapping, KeyCode key = KeyCode.None) {
            mapping.Type = ButtonMapping.ButtonType.Wrapper;
            mapping.Key = key;
        }
    }
}
