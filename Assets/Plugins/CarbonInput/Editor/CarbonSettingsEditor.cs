using UnityEngine;
using UnityEditor;

namespace CarbonInput {
    /// <summary>
    /// Editor for <see cref="CarbonSettings"/>.
    /// </summary>
    [CustomEditor(typeof(CarbonSettings))]
    public class CarbonSettingsEditor : Editor {
        /// <summary>
        /// Short info text for <see cref="AnyBehaviour"/>
        /// </summary>
        private static string[] BehaviourHelp = {
            "UseMappingOne:\nUse the same mapping PlayerIndex.One uses, but listen on any gamepad for that mapping.",
            "UseControllerOne:\nAlways use PlayerIndex.One whenever PlayerIndex.Any is used.",
            "CheckAll:\nGo over all players and use first match. Slightly slower than the other two behaviours, but it is the most accurate."
        };

        private CarbonSettings Settings { get { return (CarbonSettings)target; } }

        public override void OnInspectorGUI() {
            GUI.changed = false;
            EditorGUILayout.HelpBox(BehaviourHelp[(int)Settings.Behaviour], MessageType.Info);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Behaviour", "Defines the behaviour of PlayerIndex.Any"), GUILayout.Width(100));
            EditorGUI.BeginChangeCheck();
            AnyBehaviour value = (AnyBehaviour)EditorGUILayout.EnumPopup(Settings.Behaviour);
            if(EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(Settings, "Changed Behaviour to " + value.ToString());
                Settings.Behaviour = value;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();

            EditorGUILayout.HelpBox(
                "The default behaviour of any axis is as follows:\n" +
                "X axis goes from -1 (left) to +1(right)\n" +
                "Y axis goes from -1 (up) to +1 (down)", MessageType.Info);
            EditorGUILayout.LabelField("Inverted Axis");
            EditorGUILayout.BeginHorizontal();
            AxisToggle(CAxis.LX); AxisToggle(CAxis.RX); AxisToggle(CAxis.DX);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            AxisToggle(CAxis.LY); AxisToggle(CAxis.RY); AxisToggle(CAxis.DY);
            EditorGUILayout.EndHorizontal();

            if(GUI.changed) EditorUtility.SetDirty(target);
        }

        /// <summary>
        /// Helper method used to invert an axis, providing an undo action.
        /// </summary>
        /// <param name="axis"></param>
        private void AxisToggle(CAxis axis) {
            EditorGUI.BeginChangeCheck();
            bool value = EditorGUILayout.ToggleLeft(axis.ToString(), Settings[axis], GUILayout.Width(40));
            if(EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(Settings, "Inverted Axis " + axis.ToString());
                Settings[axis] = value;
            }
        }
    }
}
