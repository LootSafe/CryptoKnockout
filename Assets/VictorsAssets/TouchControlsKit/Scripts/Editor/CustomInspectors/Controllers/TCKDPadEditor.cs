/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/

using System;
using UnityEngine;
using UnityEditor;

namespace TouchControlsKit.Inspector
{
    [CustomEditor( typeof( TCKDPad ) )]
    public class TCKDPadEditor : AxesBasedControllerEditor
    {
        SerializedProperty normalSpriteProp, pressedSpriteProp;
        SerializedProperty normalColorProp, pressedColorProp;
        
        SerializedObject[] arrowsImages;


        // OnEnable
        protected override void OnEnable()
        {
            base.OnEnable();

            normalSpriteProp = serializedObject.FindProperty( "normalSprite" );
            pressedSpriteProp = serializedObject.FindProperty( "pressedSprite" );

            normalColorProp = serializedObject.FindProperty( "normalColor" );
            pressedColorProp = serializedObject.FindProperty( "pressedColor" );

            Transform tr = ( target as Component ).transform;
            arrowsImages = new SerializedObject[ tr.childCount ];

            for( int i = 0; i < arrowsImages.Length; i++ )
            {
                arrowsImages[ i ] = new SerializedObject( tr.GetChild( i ).GetComponent<UnityEngine.UI.Image>() );
            }
        }

        
        // ShowParameters
        protected override void ShowParameters()
        {
            base.ShowParameters();
            
            DrawIdentProp();
            DrawSensitivityProp();
            DrawTouchZone();

            GUILayout.Space( 5f );
            GUI.enabled = eavIsOk;

            using( var ecc = new TCKEditorChangeCheck() )
            {
                TCKEditorHelper.DrawSpriteAndColor( normalSpriteProp, normalColorProp, normalSpriteProp.GetLabel() );

                ecc.OnChangeCheck = () => 
                {
                    Array.ForEach( arrowsImages, imgObj =>
                    {
                        imgObj.Update();
                        imgObj.FindProperty( "m_Sprite" ).objectReferenceValue = normalSpriteProp.objectReferenceValue;
                        imgObj.FindProperty( "m_Color" ).colorValue = normalColorProp.colorValue;
                        imgObj.ApplyModifiedProperties();
                    } );
                };
            }

            TCKEditorHelper.DrawSpriteAndColor( pressedSpriteProp, pressedColorProp, pressedSpriteProp.GetLabel() );
            GUI.enabled = true;
        }
    };
}