///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 26/09/2017
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Adán Baró Balboa
/// Description: Tool that allows modify the pivot of selected sprites.
///

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
 
// Local Namespace
namespace Assets.CGF.Editor.Tools
{

    /// \english
    /// <summary>
    /// Tool that allows modify the pivot of selected sprites.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Herramienta que permite modifica el pivote de los sprites seleccionados.
    /// </summary>
    /// \endspanish
    public class CGFSpriteBulkEditorTool : EditorWindow
    {

        #region Public Variables

			public enum SpriteAlignment
			{

				Center = 0,

				TopLeft = 1,

				Top = 2,

				TopRight = 3,

				Left = 4,

				Right = 5,

				BottomLeft = 6,

				Bottom = 7,

				BottomRight = 8,

				Custom = 9,
			}

        #endregion

        #region Private Variables

            private SpriteAlignment _pivotType;

            private Vector2 _customPivotPosition;

        #endregion

        #region Main Methods

			[MenuItem("Window/Chloroplast Games Framework/Sprite Bulk Editor Tool")]
			static void ShowWindow()
			{

				EditorWindow.GetWindow(typeof(CGFSpriteBulkEditorTool), false, "Sprite Bulk Editor Tool", true);
				
			}

			void OnGUI()
			{

				EditorGUILayout.BeginHorizontal();

				_pivotType = (SpriteAlignment)EditorGUILayout.EnumPopup(_pivotType);

				EditorGUILayout.EndHorizontal();

				EditorGUILayout.Space();

				EditorGUILayout.BeginHorizontal();

				GUI.enabled = (_pivotType == SpriteAlignment.Custom) ? true : false;

				_customPivotPosition = EditorGUILayout.Vector2Field("Custom Position", _customPivotPosition);

				GUI.enabled = true;

				EditorGUILayout.EndHorizontal();

				EditorGUILayout.Space();

				GUI.enabled = (Selection.objects.Length > 0) ? true : false;

				if (GUILayout.Button("Change Pivot"))
				{

					SetPivot();

					SetPivot();

				}

				GUI.enabled = true;

			}

        #endregion

        #region Utility Methods

			/// <summary>
			/// Modifica el pivote del sprite.
			/// </summary>
			void SetPivot()
			{

				foreach (Object go in Selection.objects)
				{

					if (go.GetType() == typeof(Texture2D))
					{

						TextureImporter textureImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(go)) as TextureImporter;

						textureImporter.isReadable = true;

						TextureImporterSettings texSettings = new TextureImporterSettings();

						textureImporter.ReadTextureSettings(texSettings);

						if (textureImporter.spriteImportMode == SpriteImportMode.Multiple)
						{

							List<SpriteMetaData> newMetaData = new List<SpriteMetaData>();

							for (int i = 0; i < textureImporter.spritesheet.Length; i++)
							{
								SpriteMetaData slicedSprite = textureImporter.spritesheet[i];

								slicedSprite.alignment = (int)_pivotType;

								if (_pivotType == SpriteAlignment.Custom)
								{

									slicedSprite.pivot = _customPivotPosition;

								}

								newMetaData.Add(slicedSprite);
							}

							textureImporter.spritesheet = newMetaData.ToArray();

						}
						else if (textureImporter.spriteImportMode == SpriteImportMode.Single)
						{

							texSettings.spriteAlignment = (int)_pivotType;

							if (_pivotType == SpriteAlignment.Custom)
							{

								texSettings.spritePivot = _customPivotPosition;

							}
						
						}
						else if (textureImporter.spriteImportMode == SpriteImportMode.Polygon)
						{

							texSettings.spriteAlignment = (int)_pivotType;

							if (_pivotType == SpriteAlignment.Custom)
							{

								texSettings.spritePivot = _customPivotPosition;

							}

						}

						textureImporter.SetTextureSettings(texSettings);

						AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(go), ImportAssetOptions.ForceUpdate);

					}

				}

			}
    
        #endregion

        #region Utility Events

        #endregion

    }

}
