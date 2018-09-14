using UnityEngine;
using System.Collections;

namespace CarbonInput {
	/// <summary>
	/// This class will check if a new gamepad was connected or if a gamepad lost its connection.
	/// If this is the case, they are reinitialized.
	/// </summary>
	public class ReInit : MonoBehaviour {
		private string[] _names;

		void Start() {
			_names = Input.GetJoystickNames();
			StartCoroutine(CheckRoutine());
			DontDestroyOnLoad(gameObject);
		}

		/// <summary>
		/// Checks once a second if any gamepad has lost connection or was reconnected.
		/// </summary>
		/// <returns></returns>
		private IEnumerator CheckRoutine() {
			yield return new WaitForSeconds(0.25f);
			// On UWP platform Unity needs a few milliseconds to init all gamepads,
			// therefore it might be the case that they will be initilized now
			while(true) {
				if(JoysticksChanged()) {
					_names = Input.GetJoystickNames();
					GamePad.ReInit();
				}
				yield return new WaitForSeconds(1f);
			}
		}

		private bool JoysticksChanged() {
			var names = Input.GetJoystickNames();
			if(names.Length != _names.Length) return true;
			for(var i = 0; i < names.Length; i++) {
				if(names[i] != _names[i]) return true;
			}
			return false;
		}
	}
}