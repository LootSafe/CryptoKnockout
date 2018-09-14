using UnityEngine;
using UnityEngine.UI;

namespace CarbonInput {
	public class SwitchPS4Gamepad : MonoBehaviour {
		public CarbonController Wired;
		public CarbonController Bluetooth;
		public Toggle Toggle;

		private int highPriority;
		private int lowPriority;

		private void Start() {
			highPriority = Mathf.Min(Wired.Priority, Bluetooth.Priority);
			lowPriority = Mathf.Max(Wired.Priority, Bluetooth.Priority);
			if(Toggle != null) {
				Toggle.isOn = Bluetooth.Priority < Wired.Priority;
			}
		}

		public void ChangeMapping(bool useBluetooth) {
			if(useBluetooth) {
				Bluetooth.Priority = highPriority;
				Wired.Priority = lowPriority;
			} else {
				Wired.Priority = highPriority;
				Bluetooth.Priority = lowPriority;
			}
			GamePad.ReInit();
		}
	}
}
