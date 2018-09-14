using UnityEditor;

namespace CarbonInput {
	[CustomEditor(typeof(ReInit))]
	public class ReInitEditor : Editor {
		public override void OnInspectorGUI() {
			EditorGUILayout.HelpBox("The automatically generated \"GamePad ReInit\" gameobject " +
			                        "and this script are used to detect if a gamepad has (dis)connected.", MessageType.Info);
		}
	}
}