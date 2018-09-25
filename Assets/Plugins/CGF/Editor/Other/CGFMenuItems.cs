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

    internal class CGFMenuItems : EditorWindow
    {

        [MenuItem("Window/Chloroplast Games Framework/About")]
        private static void ShowAboutWindow()
        {

            EditorWindow windowWithRect =
                EditorWindow.GetWindowWithRect<CGFAboutWindow>(new Rect(100f, 100f, 570f, 340f), true, "About CGF");

            windowWithRect.position = new Rect(100f, 100f, 570f, 340f);

        }

        [MenuItem("Window/Chloroplast Games Framework/Help/User Manual")]
        private static void ShowtUserManual()
        {

            Application.OpenURL("http://chloroplastgames.com/cg-framework-user-manual/");

        }

        [MenuItem("Window/Chloroplast Games Framework/Help/API Reference")]
        private static void ShowAPIReference()
        {

            Application.OpenURL("http://www.chloroplastgames.com/wp-content/documentation/cgf/");

        }

        [MenuItem("Window/Chloroplast Games Framework/Help/Unity Forum Topic")]
        private static void ShowUnityForumTopic()
        {

            Application.OpenURL("http://www.chloroplastgames.com");

        }

        [MenuItem("Window/Chloroplast Games Framework/Help/Support Forum Topic")]
        private static void ShowSupportForumTopic()
        {

            Application.OpenURL("http://chloroplastgames.com/forums/forum/support/");

        }

        [MenuItem("Window/Chloroplast Games Framework/Help/Contact")]
        private static void ShowContact()
        {

            Application.OpenURL("http://chloroplastgames.com/contact/");

        }

        [MenuItem("Window/Chloroplast Games Framework/Help/Suggest New Features")]
        private static void ShowSuggestFeatures()
        {

            Application.OpenURL("http://chloroplastgames.com/contact/");

        }

        [MenuItem("Window/Chloroplast Games Framework/Links/Website")]
        private static void ShowWebsite()
        {

            Application.OpenURL("http://chloroplastgames.com/");

        }

        [MenuItem("Window/Chloroplast Games Framework/Links/Facebook")]
        private static void ShowFacebook()
        {

            Application.OpenURL("https://www.facebook.com/ChloroplastGames");

        }

        [MenuItem("Window/Chloroplast Games Framework/Links/Twitter")]
        private static void ShowTwitter()
        {

            Application.OpenURL("https://twitter.com/ChloroplastGame");

        }

        [MenuItem("Window/Chloroplast Games Framework/Links/YouTube")]
        private static void ShowYoutube()
        {

            Application.OpenURL("https://www.youtube.com/user/ChloroplastGames");

        }

    }

}