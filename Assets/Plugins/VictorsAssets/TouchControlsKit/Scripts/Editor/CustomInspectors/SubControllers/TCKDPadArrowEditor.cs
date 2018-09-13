/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using UnityEditor;

namespace TouchControlsKit.Inspector
{
    [CustomEditor( typeof( TCKDPadArrow ) )]
    public class TCKDPadArrowEditor : Editor
    {
        SerializedProperty arrowTypeProp;


        // OnEnable
        void OnEnable()
        {
            arrowTypeProp = serializedObject.FindProperty( "arrowType" );
        }

        // OnInspectorGUI
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.Space();
            TCKEditorHelper.DrawEnumAsToolbar( arrowTypeProp, false );
            serializedObject.ApplyModifiedProperties();
        }
    };
}