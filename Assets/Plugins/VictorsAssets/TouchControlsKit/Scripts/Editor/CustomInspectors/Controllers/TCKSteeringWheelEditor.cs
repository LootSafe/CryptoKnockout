/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using UnityEngine;
using UnityEditor;

namespace TouchControlsKit.Inspector
{
    [CustomEditor( typeof( TCKSteeringWheel ) )]
    public class TCKSteeringWheelEditor : AxesBasedControllerEditor
    {
        SerializedProperty maxSteeringAngleProp, releasedSpeedProp;           


        // OnEnable
        protected override void OnEnable()
        {
            base.OnEnable();

            maxSteeringAngleProp = serializedObject.FindProperty( "maxSteeringAngle" );
            releasedSpeedProp = serializedObject.FindProperty( "releasedSpeed" );

            endAxisIndexToDraw--;
        }


        
        // ShowParameters
        protected override void ShowParameters()
        {
            base.ShowParameters();
            
            DrawIdentProp();
            DrawSensitivityProp();

            GUILayout.Space( 5f );            
            TCKEditorHelper.DrawPropertyField( maxSteeringAngleProp );
            TCKEditorHelper.DrawPropertyField( releasedSpeedProp );

            GUILayout.Space( 5f );
            TCKEditorHelper.DrawSpriteAndColor( baseImageObj, "Wheel" );
        }
    };
}