using UnityEngine;

namespace CarbonInput {
    /// <summary>
    /// Attach this to the canvas all touch controls are in. 
    /// On startup this script will check if there are any real gamepads and if so, it will disable the touch controls.
    /// </summary>
    public class DisableTouchInput : MonoBehaviour {
        [Tooltip("If true, touch controls will be disabled on Console Platforms, even if there are no gamepads connected.")]
        public bool HideOnConsole = true;
        [Tooltip("If true, touch controls will be disabled in Web Player, even if there are no gamepads connected.")]
        public bool HideOnWeb = true;
        [Tooltip("If true, touch controls will be disabled in the Editor, even if there are no gamepads connected.")]
        public bool HideOnEditMode = false;
        [Tooltip("If true, touch controls will be disabled on Windows, Linux and Mac, even if there are no gamepads connected.")]
        public bool HideOnPC = true;
        void Start() {
#if UNITY_EDITOR
            if(HideOnEditMode) { Hide(); return; }
#endif
#if UNITY_WEB
            if(HideOnWeb) { Hide(); return; }
#endif
#if UNITY_STANDALONE
            if(HideOnPC) { Hide(); return; }
#endif
            if(HideOnConsole && Application.isConsolePlatform 
                || GamePad.GamePadCount > 0) { // There are gamepads so we don't need touchcontrols
                Hide();
            }
        }

        /// <summary>
        /// Deactivates all children with a <see cref="BaseTouchInput"/> component.
        /// </summary>
        private void Hide() {
            // Iterate over all children
            foreach(RectTransform rect in GetComponentsInChildren<RectTransform>()) {
                if(rect.GetComponent<BaseTouchInput>() != null) // Deactivate all TouchControls
                    rect.gameObject.SetActive(false);
            }
        }
    }
}
