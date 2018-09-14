using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

namespace CarbonInput {
    /// <summary>
    /// Editor extension used to initialize the Unity Input axes. 
    /// </summary>
    public static class CarbonInputMapper {
        /// <summary>
        /// Deadzone for Unity axis.
        /// </summary>
        private const float Dead = 0.1f;
        /// <summary>
        /// Sensitivity for Unity axis.
        /// </summary>
        private const float Sensitivity = 1.0f;

        /// <summary>
        /// Helper class, used to manage the settings for a single axis
        /// </summary>
        public class JoystickAxis {
            public string Name;
            public int Axis;
            public int JoyNum;
            public JoystickAxis(string name, int axis, int joyNum) {
                this.Name = name;
                this.Axis = axis;
                this.JoyNum = joyNum;
            }
        }
        
        /// <summary>
        /// Initializes CarbonInput by setting up all unity axes.
        /// </summary>
        [MenuItem("Edit/Project Settings/Carbon Input/Create Carbon Input Axes")]
        static void Init() {
            if(EditorUtility.DisplayDialog("Init CarbonInput", "This will modify the InputManager settings by adding a bunch of axes.", "OK", "Cancel"))
                AddCarbonAxes();
        }
        /// <summary>
        /// Removes all generated unity axes.
        /// </summary>
        [MenuItem("Edit/Project Settings/Carbon Input/Remove Carbon Input Axes")]
        static void Clear() {
            if(EditorUtility.DisplayDialog("Remove CarbonInput", "This will modify the InputManager settings by removing all axes named \"cin_Axis*\".", "OK", "Cancel"))
                RemoveCarbonAxes();
        }
        /// <summary>
        /// Creates a new mapping used for keyboards.
        /// </summary>
        [MenuItem("Assets/Create/Carbon Input/Keyboard Mapping", false, 1)]
        static void NewFallbackMapping() {
            SaveInNewFile(CarbonController.CreateFallback(), "Keyboard");
        }

        /// <summary>
        /// Helper method, used to store the given asset in a new file
        /// </summary>
        /// <param name="asset"></param>
        private static void SaveInNewFile(ScriptableObject asset, string name) {
            string dir = "Assets";
            if(Selection.activeObject != null)
                dir = AssetDatabase.GetAssetPath(Selection.activeObject);

            int id = 0;
            string file;
            do {
                file = Path.Combine(dir, name + id++ + ".asset");
            } while(File.Exists(file));
            AssetDatabase.CreateAsset(asset, file);
            Selection.activeObject = asset;
            EditorUtility.FocusProjectWindow();
        }

        /// <summary>
        /// Removes all generated axes
        /// </summary>
        private static void RemoveCarbonAxes() {
            SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
            for(int i = axesProperty.arraySize - 1; i >= 0; i--) {
                SerializedProperty prop = axesProperty.GetArrayElementAtIndex(i);
                prop.Next(true);
                if(prop.stringValue.StartsWith(CarbonController.Tag)) axesProperty.DeleteArrayElementAtIndex(i);
            }
            serializedObject.ApplyModifiedProperties();
        }
        /// <summary>
        /// Generates all axes.
        /// </summary>
        private static void AddCarbonAxes() {
            RemoveCarbonAxes(); // clean up first
            // Any, Player One, ..., Player Eight
            for(int id = 0; id < CarbonController.PlayerIndices; id++) {
                for(int i = 0; i < CarbonController.InputAxisCount; i++)
                    //cin_AxisID_I example: cin_Axis0_00 => axis 0 for any joystick
                    AddAxis(new JoystickAxis(CarbonController.CreateName(id, i), i, id));
            }
        }
        /// <summary>
        /// Adds a single unity axis to the InputManager.
        /// </summary>
        /// <param name="axis"></param>
        private static void AddAxis(JoystickAxis axis) {
            SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");
            axesProperty.arraySize++;
            serializedObject.ApplyModifiedProperties();
            SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);
            SetAxis(axisProperty, axis);
            serializedObject.ApplyModifiedProperties();
        }
        /// <summary>
        /// Sets the values of a single axis.
        /// </summary>
        /// <param name="axisProperty"></param>
        /// <param name="axis"></param>
        private static void SetAxis(SerializedProperty axisProperty, JoystickAxis axis) {
            axisProperty.Next(true);
            axisProperty.stringValue = axis.Name;
            do {
                switch(axisProperty.name) {
                    case "dead": axisProperty.floatValue = Dead; break;
                    case "sensitivity": axisProperty.floatValue = Sensitivity; break;
                    case "type": axisProperty.intValue = 2; break; // 2 = Joystick Axis
                    case "axis": axisProperty.intValue = axis.Axis; break;
                    case "joyNum": axisProperty.intValue = axis.JoyNum; break;
                }
            } while(axisProperty.Next(false));
        }
    }
}
