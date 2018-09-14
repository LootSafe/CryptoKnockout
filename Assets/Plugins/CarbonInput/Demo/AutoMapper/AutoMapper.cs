using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CarbonInput;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using UnityEngine.UI;

namespace CarbonInput.Demo {
    public class AutoMapper : MonoBehaviour {
        private CarbonController Mapping;

        public RectTransform Highlight;
        public RectTransform Left;
        public RectTransform Right;
        public RectTransform Up;
        public RectTransform Down;

        public GameObject PressLT;
        public GameObject PressRT;
        public GameObject Finished;

        private void SetLRT(bool lt, bool rt) { PressLT.SetActive(lt); PressRT.SetActive(rt); }

        private int step;
        private RectTransform[] Buttons;
        private float[] Snapshot = new float[CarbonController.InputAxisCount];

        void Start() {
            // deactivate labels
            SetLRT(false, false);
            Finished.SetActive(false);
            //create mapping
            Mapping = ScriptableObject.CreateInstance<CarbonController>();
            //collect Button rects
            Buttons = new RectTransform[CarbonController.ButtonCount];
            for(int i = 0; i < CarbonController.ButtonCount; i++)
                Buttons[i] = GetByName(((CButton)i).ToString());
            //start
            StartCoroutine(Blink());
            StartCoroutine(Handle());
        }

        /// <summary>
        /// Find rect by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private RectTransform GetByName(string name) {
            foreach(RectTransform rect in transform.GetComponentsInChildren<RectTransform>()) {
                if(rect.name == name) return rect;
            }
            return null;
        }

        void Update() {
            // make a snapshot of axis states
            for(int i = 0; i < CarbonController.InputAxisCount; i++)
                Snapshot[i] = Input.GetAxis(CarbonController.CreateName(0, i));
        }

        /// <summary>
        /// Increment step
        /// </summary>
        public void Next() { step++; }
        /// <summary>
        /// Move the highlighted panel to the given rect.
        /// </summary>
        /// <param name="rect"></param>
        private void MoveTo(RectTransform rect) { Highlight.position = rect.position; }

        /// <summary>
        /// Handles a single button.
        /// </summary>
        private void HandleButton() {
            MoveTo(Buttons[step]);
            int btn;
            if(GetButton(out btn)) {
                Mapping.Buttons[step].Button = btn;
                Next();
            }
        }

        /// <summary>
        /// Handles a single axis. 
        /// </summary>
        private void HandleAxis() {
            int index;
            if(GetAxis(out index)) {
                Mapping.Axes[step].Axis = index;
                Mapping.Axes[step].Invert = Snapshot[index] > 0;
                Next();
            }
        }

        /// <summary>
        /// Handles left/right trigger. If a button was pressed, <see cref="AxisMapping.AxisType.ButtonWrapper"/> will be used.
        /// </summary>
        /// <param name="axis"></param>
        private void HandleTrigger(CAxis axis) {
            AxisMapping mapping = Mapping.Axes[(int)axis];
            int index;
            if(GetButton(out index)) { // wrapper
                mapping.Axis = index;
                mapping.Type = AxisMapping.AxisType.ButtonWrapper;
                mapping.Min = 0;
                mapping.Max = 1;
                Next();
            } else if(GetAxis(out index)) {
                mapping.Axis = index;
                Next();
            }
        }

        /// <summary>
        /// Handles the DPad input. If a button was pressed, <see cref="AxisMapping.AxisType.ButtonWrapper2"/> will be used.
        /// </summary>
        /// <param name="axis"></param>
        private void HandleDPad(CAxis axis) {
            AxisMapping mapping = Mapping.Axes[(int)axis];
            int index;
            // some gamepads only have buttons for the dpad
            if(GetButton(out index)) {
                if(step == 0) mapping.Axis = index;
                else mapping.Alternative = index;
                mapping.Type = AxisMapping.AxisType.ButtonWrapper2;
                Next();
            } else if(GetAxis(out index)) {
                mapping.Axis = index;
                mapping.Invert = Snapshot[index] > 0;
                if(axis == CAxis.DY) mapping.Invert = !mapping.Invert;
                Next();
            }
        }

        /// <summary>
        /// Moves the transform by the given offset.
        /// </summary>
        /// <param name="btn">Should be either <see cref="CButton.LS"/> or <see cref="CButton.RS"/></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        private void MoveStick(CButton btn, int offsetX, int offsetY) {
            RectTransform rect = Buttons[(int)btn];
            Vector3 pos = rect.localPosition;
            pos.x += offsetX; pos.y += offsetY;
            rect.localPosition = pos;
        }

        /// <summary>
        /// yields until all axis are near zero.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitForAxes() {
            // wait until all axes are small enough (near zero)
            return new WaitUntil(() => {
                foreach(float value in Snapshot) if(Mathf.Abs(value) > 0.5f) return false;
                return true;
            });
        }


        /// <summary>
        /// Does the actual mapping of all controls
        /// </summary>
        /// <returns></returns>
        IEnumerator Handle() {
            // Handle buttons
            while(step < CarbonController.ButtonCount) { HandleButton(); yield return null; }
            // Handle DPad
            MoveTo(Left);
            step = 0;
            while(step == 0) { HandleDPad(CAxis.DX); yield return WaitForAxes(); }
            // second round if gamepad use buttons instead of axis
            if(Mapping.Axes[(int)CAxis.DX].Type == AxisMapping.AxisType.ButtonWrapper2) {
                MoveTo(Right);
                while(step == 1) { HandleDPad(CAxis.DX); yield return WaitForAxes(); }
            }
            MoveTo(Up);
            step = 0;
            while(step == 0) { HandleDPad(CAxis.DY); yield return WaitForAxes(); }
            // second round if gamepad use buttons instead of axis
            if(Mapping.Axes[(int)CAxis.DY].Type == AxisMapping.AxisType.ButtonWrapper2) {
                MoveTo(Down);
                while(step == 1) { HandleDPad(CAxis.DY); yield return WaitForAxes(); }
            }
            Highlight.gameObject.SetActive(false);

            // Handle LX, LY, RX, RY
            step = 0;
            MoveStick(CButton.LS, -20, 0);
            while(step == 0) { HandleAxis(); yield return WaitForAxes(); }
            MoveStick(CButton.LS, 20, 20);
            while(step == 1) { HandleAxis(); yield return WaitForAxes(); }
            MoveStick(CButton.LS, 0, -20);
            MoveStick(CButton.RS, -20, 0);
            while(step == 2) { HandleAxis(); yield return WaitForAxes(); }
            MoveStick(CButton.RS, 20, 20);
            while(step == 3) { HandleAxis(); yield return WaitForAxes(); }
            MoveStick(CButton.RS, 0, -20);

            // Handle LT, RT
            SetLRT(true, false);
            step = 0;
            while(step == 0) { HandleTrigger(CAxis.LT); yield return WaitForAxes(); }
            SetLRT(false, true);
            step = 0;
            while(step == 0) { HandleTrigger(CAxis.RT); yield return WaitForAxes(); }

            //finished
            SetLRT(false, false);
            Finished.SetActive(true);
            SaveMapping();
        }

        /// <summary>
        /// Lets the Highlight image blink
        /// </summary>
        /// <returns></returns>
        IEnumerator Blink() {
            Image img = Highlight.GetComponent<Image>();
            Color color = img.color;
            float speed = 1f;
            while(true) {
                while(color.a > 0.5f) {
                    color.a -= Time.deltaTime * speed;
                    img.color = color;
                    yield return null;
                }
                while(color.a < 1f) {
                    color.a += Time.deltaTime * speed;
                    img.color = color;
                    yield return null;
                }
            }
        }

        /// <summary>
        /// Was a button pressed down? If so, which one?
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        private bool GetButton(out int btn) {
            for(btn = 0; btn < 20; btn++) {
                if(Input.GetKeyDown(KeyCode.JoystickButton0 + btn)) return true;
            }
            return false;
        }

        /// <summary>
        /// Was an axis moved? If so, which one?
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        private bool GetAxis(out int axis) {
            for(axis = 0; axis < CarbonController.InputAxisCount; axis++) {
                if(Mathf.Abs(Snapshot[axis]) > 0.5f) return true;
            }
            return false;
        }

        /// <summary>
        /// If the mappings directory exist, the mapping will be stored there. Other wise in the root directory.
        /// </summary>
        private void SaveMapping() {
            string dir = "Assets";
            if(Directory.Exists("Assets/CarbonInput/Resources/Mappings"))
                dir = "Assets/CarbonInput/Resources/Mappings";
            int id = 0;
            string file;
            do {
                file = dir + "/CarbonMapping" + (id++) + ".asset";
            } while(File.Exists(file));
#if UNITY_EDITOR
            AssetDatabase.CreateAsset(Mapping, file);
#endif
        }
    }
}
