using UnityEngine;
using System.Collections;

namespace CarbonInput.Demo {
    public class Demo : MonoBehaviour {
        private int count = 0;
	    private int minIndex = 1;

        void Update() {
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            if(state.Pressed(CButton.A))
                count++;
        }
        void OnGUI() {
            GUI.Window(0, new Rect(20, 20, 640, 320), MyWindow, "GamePad");
            GUI.Window(1, new Rect(680, 20, 240, 100), CountWindows, "Count");
        }

        void CountWindows(int winID) {
			GUI.Label(new Rect(20,20,240,40), "Hardware gamepads connected: " + GamePad.GamePadCount);
			GUI.Label(new Rect(20, 60, 240, 40), "Player one has pressed button A\n" + count + " times");
        }

        void MyWindow(int winID) {
			PlayerColumn(PlayerIndex.Any, 40);
	        for(int i = 0; i < 4; i++) {
		        var player = (PlayerIndex)(minIndex + i);
		        PlayerColumn(player, 160 + 120 * i);
	        }
	        var buttonText = minIndex != 1 ? "Show Player 1-4" : "Show Player 5-8";
	        if(GUI.Button(new Rect(480, 260, 120, 20), buttonText)) {
		        minIndex = minIndex != 1 ? 1 : 5;
	        }
	        UnityAxis(20, 260);
        }

        private void PlayerColumn(PlayerIndex id, int x) {
            GUI.contentColor = Color.white;
            GUI.Label(new Rect(x, 20, 100, 20), "Player " + id);
            if(id != PlayerIndex.Any)
                GUI.Label(new Rect(x, 40, 100, 20), GamePad.GetMapping(id).Controller.name);
            for(CButton btn = CButton.A; btn <= CButton.RS; btn++) {
                if(GamePad.GetButton(btn, id)) GUI.contentColor = Color.green;
                else GUI.contentColor = Color.white;
                GUI.Label(new Rect(x, 60 + 20 * (int)btn, 40, 20), btn.ToString());
            }
            for(CAxis axis = CAxis.LX; axis <= CAxis.DY; axis++) {
                float value = GamePad.GetAxis(axis, id);
                if(Mathf.Abs(value) > 0) GUI.contentColor = Color.green;
                else GUI.contentColor = Color.white;
                GUI.Label(new Rect(x + 40, 60 + 20 * (int)axis, 80, 20), axis.ToString() + " " + value.ToString("F"));
            }
        }

        private void UnityAxis(int minx, int miny) {
            GUI.Label(new Rect(minx, miny, 200, 20), "Unity Input Axis");
            int x = minx;
            for(int i = 0; i < CarbonController.JoystickButtonCount; i++) {
                if(Input.GetKey(KeyCode.JoystickButton0 + i)) {
                    GUI.Label(new Rect(x, 280, 120, 20), "JoystickButton" + i);
                    x += 120;
                }
            }
            x = minx;
            for(int i = 0; i < CarbonController.InputAxisCount; i++) {
                float value = Input.GetAxis(CarbonController.CreateName(0, i));
                if(Mathf.Abs(value) > 0.1f) {
                    GUI.Label(new Rect(x, 300, 80, 20), "Axis" + i + ": " + value.ToString("F"));
                    x += 80;
                }
            }
        }
    }
}
