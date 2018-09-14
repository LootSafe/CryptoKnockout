using UnityEngine;
using UnityEditor;

namespace CarbonInput {
    /// <summary>
    /// Editor for <see cref="CarbonController"/>.
    /// </summary>
    [CustomEditor(typeof(CarbonController))]
    public class CarbonMappingEditor : Editor {
        /// <summary>
        /// Foldout buttons
        /// </summary>
        private bool showButtons = true;
        /// <summary>
        /// Foldout axes.
        /// </summary>
        private bool showAxes = true;

        public override void OnInspectorGUI() {
            GUI.changed = false;
            CarbonController mapping = (CarbonController)target;

            EditorGUI.BeginChangeCheck();
            string regex = EditorGUILayout.TextField(new GUIContent("RegEx", "Regular expression used to match joystick names."), mapping.RegEx);
            if(EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(mapping, "Changed Gamepad RegEx");
                mapping.RegEx = regex;
            }

            EditorGUI.BeginChangeCheck();
            int priority = EditorGUILayout.IntField(new GUIContent("Priority", "Lower values are checked earlier."), mapping.Priority);
            if(EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(mapping, "Changed Gamepad Priority");
                mapping.Priority = priority;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Platform");
            EditorGUI.BeginChangeCheck();
            CPlatform platform = (CPlatform)EditorGUILayout.EnumMaskField(mapping.Platform);
            if(EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(mapping, "Changed Gamepad Platform");
                mapping.Platform = platform;
            }
            EditorGUILayout.EndHorizontal();

	        EditorGUI.BeginChangeCheck();
	        bool useOnce = EditorGUILayout.Toggle(new GUIContent("Use Once", "Whether this mapping should only be used for one joystick."), mapping.UseOnce);
	        if(EditorGUI.EndChangeCheck()) {
		        Undo.RecordObject(mapping, "Changed Gamepad Use Once");
		        mapping.UseOnce = useOnce;
	        }

			showButtons = EditorGUILayout.Foldout(showButtons, "Buttons");
            if(showButtons) {
                EditorGUILayout.BeginVertical();
                for(int i = 0; i < CarbonController.ButtonCount; i++) {
                    ButtonMapping btn = mapping.Buttons[i];
                    ButtonMapping tmp = new ButtonMapping(btn);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(((CButton)i).ToString(), GUILayout.MaxWidth(50f));
                    tmp.Type = (ButtonMapping.ButtonType)EditorGUILayout.EnumPopup(btn.Type, GUILayout.MaxWidth(100f));
                    if(btn.Type == ButtonMapping.ButtonType.Wrapper) {
                        tmp.Key = (KeyCode)EditorGUILayout.EnumPopup(btn.Key, GUILayout.MaxWidth(100f));
                    } else {
                        tmp.Button = Mathf.Clamp(EditorGUILayout.IntField(btn.Button, GUILayout.MaxWidth(100f)), 0, 19);
                    }
                    EditorGUILayout.EndHorizontal();
                    if(EditorGUI.EndChangeCheck()) {
                        Undo.RecordObject(mapping, "Changed Button Mapping");
                        btn.CopyFrom(tmp); // copy back
                    }
                }
                EditorGUILayout.EndVertical();
            }

            showAxes = EditorGUILayout.Foldout(showAxes, "Axes");
            if(showAxes) {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Name", GUILayout.MaxWidth(50f));
                EditorGUILayout.LabelField("Axis", GUILayout.MaxWidth(70f));
                EditorGUILayout.LabelField("Invert", GUILayout.MaxWidth(40f));
                EditorGUILayout.LabelField("Type", GUILayout.MaxWidth(80f));
                EditorGUILayout.EndHorizontal();
                for(int i = 0; i < CarbonController.AxisCount; i++) {
                    AxisMapping axis = mapping.Axes[i];
                    AxisMapping tmp = new AxisMapping(axis);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(((CAxis)i).ToString(), GUILayout.MaxWidth(50f));
                    if(axis.Type == AxisMapping.AxisType.KeyWrapper || axis.Type == AxisMapping.AxisType.ButtonWrapper2) {
                        EditorGUILayout.LabelField("", GUILayout.MaxWidth(80f));
                    } else if(axis.Type == AxisMapping.AxisType.ButtonWrapper) {
                        tmp.Axis = Mathf.Clamp(EditorGUILayout.IntField(axis.Axis, GUILayout.MaxWidth(80f)), 0, CarbonController.JoystickButtonCount - 1);
                    } else {
                        tmp.Axis = Mathf.Clamp(EditorGUILayout.IntField(axis.Axis, GUILayout.MaxWidth(80f)), 0, CarbonController.InputAxisCount - 1);
                    }
                    tmp.Invert = EditorGUILayout.Toggle(axis.Invert, GUILayout.MaxWidth(20f));
                    tmp.Type = (AxisMapping.AxisType)EditorGUILayout.EnumPopup(axis.Type, GUILayout.MaxWidth(100f));
                    EditorGUILayout.EndHorizontal();

                    switch(axis.Type) {
                        case AxisMapping.AxisType.KeyWrapper:
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.Space();
                            EditorGUILayout.LabelField("Negative", GUILayout.MaxWidth(60f));
                            tmp.Key1 = (KeyCode)EditorGUILayout.EnumPopup(axis.Key1, GUILayout.MaxWidth(80f));
                            EditorGUILayout.LabelField("Positive", GUILayout.MaxWidth(60f));
                            tmp.Key2 = (KeyCode)EditorGUILayout.EnumPopup(axis.Key2, GUILayout.MaxWidth(80f));
                            EditorGUILayout.EndHorizontal();
                            break;
                        case AxisMapping.AxisType.ButtonWrapper:
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.Space();
                            EditorGUILayout.LabelField("Released", GUILayout.MaxWidth(60f));
                            tmp.Min = EditorGUILayout.FloatField(axis.Min, GUILayout.MaxWidth(40f));
                            EditorGUILayout.LabelField("Pressed", GUILayout.MaxWidth(60f));
                            tmp.Max = EditorGUILayout.FloatField(axis.Max, GUILayout.MaxWidth(40f));
                            EditorGUILayout.EndHorizontal();
                            break;
                        case AxisMapping.AxisType.ButtonWrapper2:
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.Space();
                            EditorGUILayout.LabelField("Negative", GUILayout.MaxWidth(60f));
                            tmp.Axis = Mathf.Clamp(EditorGUILayout.IntField(axis.Axis, GUILayout.MaxWidth(40f)), 0, CarbonController.JoystickButtonCount - 1);
                            EditorGUILayout.LabelField("Positive", GUILayout.MaxWidth(60f));
                            tmp.Alternative = Mathf.Clamp(EditorGUILayout.IntField(axis.Alternative, GUILayout.MaxWidth(40f)), 0, CarbonController.JoystickButtonCount - 1);
                            EditorGUILayout.EndHorizontal();
                            break;
                        case AxisMapping.AxisType.Clamped:
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.Space();
                            EditorGUILayout.LabelField("Min", GUILayout.MaxWidth(40f));
                            tmp.Min = EditorGUILayout.FloatField(axis.Min, GUILayout.MaxWidth(40f));
                            EditorGUILayout.LabelField("Max", GUILayout.MaxWidth(40f));
                            tmp.Max = EditorGUILayout.FloatField(axis.Max, GUILayout.MaxWidth(40f));
                            EditorGUILayout.EndHorizontal();
                            break;
                    }
                    if(EditorGUI.EndChangeCheck()) {
                        Undo.RecordObject(mapping, "Changed Axis Mapping");
                        axis.CopyFrom(tmp); // copy back
                    }
                }
                EditorGUILayout.EndVertical();
            }

            if(GUI.changed) {
                EditorUtility.SetDirty(target);
            }
        }
    }
}
