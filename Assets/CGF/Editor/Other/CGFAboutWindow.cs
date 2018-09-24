///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 26/01/2017
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmaers: Adan Baró Balboa
/// Description: About window.
///

using System;
using UnityEditor;
using UnityEngine;

namespace Assets.CGF.Editor.Other
{

    internal class CGFAboutWindow : EditorWindow
    {
        private static GUIContent CGFLogo;

        private const string version = "Version 0.1";

        private const string name_01 = "Pau Elias Soriano - Producer and Designer";

        private const string name_02 = "Adan Baro Balboa - Programmer";

        private const string name_03 = "David Cuenca Diez - Programmer";

        private static void LoadLogos()
        {

            CGFLogo = new GUIContent(LoadImage("logo_cgf"));

        }

        protected static Texture2D LoadImage(string name)
        {

            Texture2D output = null;

            string[] guid = AssetDatabase.FindAssets(name + " t:Texture2D");

            if (guid != null && guid.Length > 0 && !string.IsNullOrEmpty(guid[0]))
            {

                string assetPath = AssetDatabase.GUIDToAssetPath(guid[0]);

                output = (Texture2D) AssetDatabase.LoadAssetAtPath(assetPath, typeof (Texture2D));

            }

            return output;

        }

        public void OnEnable()
        {

            LoadLogos();

        }

        public void OnGUI()
        {

            GUILayout.Space(25);

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.Label(CGFLogo, GUIStyle.none, GUILayout.Width(450), GUILayout.Height(125));

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();

            GUILayout.BeginVertical(GUILayout.Height(1));

            GUILayout.BeginHorizontal(GUILayout.Height(1));

            GUILayout.FlexibleSpace();

            GUILayout.Label(version, GUILayout.Height(15));

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.TextArea(name_01 + Environment.NewLine + name_02 + Environment.NewLine + name_03, GUI.skin.label);

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.BeginVertical();

            GUILayout.FlexibleSpace();

            GUILayout.Label("Copyright (C) 2017 Chloroplast Games. All rights reserved. Property of ChloroSoft.",
                "MiniLabel");

            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();

            GUILayout.Space(25);

        }

    }

}