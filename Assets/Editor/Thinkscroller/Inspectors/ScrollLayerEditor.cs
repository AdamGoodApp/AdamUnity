using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using ThinksquirrelSoftware.Thinkscroller;

[CustomEditor(typeof(_ScrollLayer))]
public class ScrollLayerEditor : Editor
{
	private ScrollLayer scrollLayer;
	private bool listeningForGuiChanges;
	private bool guiChanged;
	private string[] texNames;
	
	private void OnEnable()
	{
		scrollLayer = target as ScrollLayer;
		texNames = scrollLayer.GetTextureNames();
	}
	public override void OnInspectorGUI ()
	{
		CheckUndo ();
		
		// Scroll Layer
		EditorGUILayout.Separator ();
		EditorGUILayout.LabelField ("", "Scroll Layer");
		EditorGUILayout.Space ();
		
		// Current Mode
		if (scrollLayer.isAutoBillboard)
		{
			string currentMode = null;
			
			if (scrollLayer.isPixelPerfect)
			{
				currentMode = "Pixel Perfect";
				if (scrollLayer.tileX)
				{
					currentMode += " | Tile X";
				}
				if (scrollLayer.tileY)
				{
					currentMode += " | Tile Y";
				}
				
			}
			else if (scrollLayer.GetBillboardStretch())
			{
				currentMode = "Stretched";
			}
			else
			{
				currentMode = "Tiled (X and Y)";
			}
			EditorGUILayout.LabelField("Current Mode", currentMode);
		}
		else if (scrollLayer.isObjectLayer)
		{
			EditorGUILayout.LabelField("Current Mode", "Object Layer");
		}
		else
		{
			EditorGUILayout.LabelField("Current Mode", "Manual UV Scrolling");
		}
		// Parallax Manager
		if (!Parallax.instance)
		{
			GUILayout.Label ("No Parallax Manager found!");
			GUI.color = Color.red;
			
			if (GUILayout.Button ("Create Manager"))
			{
				Parallax.CreateManager ();
			}
			
			GUI.color = Color.white;
		}
		
		// Auto-configuration
		bool auto = EditorGUILayout.Toggle ("Auto Weights", scrollLayer.isAutoConfigured);
		if (Parallax.instance)
		{
			if (GUILayout.Button ("Update Layers"))
			{
				Parallax.instance.RefreshLayers ();
			}
		}
		
		if (auto != scrollLayer.isAutoConfigured)
		{
			scrollLayer.isAutoConfigured = auto;
			if (Parallax.instance)
			{
				Parallax.instance.RefreshLayers ();
			}
		}
		
		if (scrollLayer.isAutoConfigured)
		{
			GUI.enabled = false;
		}
		
		// Weight
		scrollLayer.SetWeight (EditorGUILayout.FloatField ("Weight", scrollLayer.GetWeight ()));
		
		// Scroll Speed
		EditorGUILayout.LabelField ("Layer Speed:", scrollLayer.GetScrollSpeed ().ToString ());
		
		if (scrollLayer.isAutoConfigured)
		{
			GUI.enabled = true;
		}
		
		// Scroll Speed Mod
		scrollLayer.SetScrollMod (EditorGUILayout.FloatField ("Speed Modifier:", scrollLayer.GetScrollMod ()));
		
		// Auto-billboard
		scrollLayer.isAutoBillboard = EditorGUILayout.Toggle ("Auto Billboard", scrollLayer.isAutoBillboard);
		
		if (scrollLayer.isAutoBillboard)
		{
			if (scrollLayer.isPixelPerfect)
			{
				GUI.enabled = false;
			}
			
			if (scrollLayer.GetBillboardStretch())
			{
				GUI.enabled = false;
			}
			
			// Size
			float x = EditorGUILayout.FloatField ("X:", scrollLayer.GetBillboardScale ().x);
			float y = EditorGUILayout.FloatField ("Y:", scrollLayer.GetBillboardScale ().y);
			
			if (scrollLayer.GetBillboardStretch())
			{
				GUI.enabled = true;
			}
			
			// Stretch
			bool stretch = scrollLayer.GetBillboardStretch ();
			scrollLayer.SetBillboardStretch (EditorGUILayout.Toggle ("Stretch", scrollLayer.GetBillboardStretch ()));
			
			if (scrollLayer.isPixelPerfect)
			{
				GUI.enabled = true;
			}
			
			// Pixel Perfect
			scrollLayer.isPixelPerfect = EditorGUILayout.Toggle ("Pixel Perfect", scrollLayer.isPixelPerfect);
			
			if (x != scrollLayer.GetBillboardScale ().x || y != scrollLayer.GetBillboardScale ().y || stretch != scrollLayer.GetBillboardStretch ())
			{
				scrollLayer.SetBillboardScale (x, y);
				scrollLayer.RefreshBillboard (true, false, true);
			}
			
			// Texture
			Texture2D tex = (Texture2D)EditorGUILayout.ObjectField ("Texture", scrollLayer.GetTexture (), typeof(Texture2D)
#if !UNITY_3_3
			                                                        , false
#endif
			                                                        );
			if (tex != scrollLayer.GetTexture ())
			{
				scrollLayer.SetTexture (tex);
				scrollLayer.RefreshBillboard (false, false, true);
			}
			
			if (tex)
			{
				GUILayout.Label("Dimensions: " + tex.width + "x" + tex.height);
			}
			
			EditorGUILayout.Separator();
			
			// Advanced
			scrollLayer.advancedFoldout_EDITOR = EditorGUILayout.Foldout (scrollLayer.advancedFoldout_EDITOR, "  Advanced");
		
			if (scrollLayer.advancedFoldout_EDITOR)
			{
				// Positioning
				scrollLayer.alignment = (ScrollLayerAlignment)EditorGUILayout.EnumPopup("Alignment:", scrollLayer.alignment);
				
				// Billboard Offset
				float bbOffsetX = EditorGUILayout.FloatField ("Offset X:", scrollLayer.offset.x);
				float bbOffsetY = EditorGUILayout.FloatField ("Offset Y:", scrollLayer.offset.y);
				scrollLayer.offset = new Vector2 (bbOffsetX, bbOffsetY);
				
				// Padding
				float paddingX = EditorGUILayout.FloatField ("UV Padding X:", scrollLayer.padding.x);
				float paddingY = EditorGUILayout.FloatField ("UV Padding Y:", scrollLayer.padding.y);
				
				if (!scrollLayer.isPixelPerfect)
				{
					GUI.enabled = false;
				}
				EditorGUILayout.Space ();
				GUILayout.Label ("Pixel Perfect Advanced:");
				// Tile X
				scrollLayer.tileX = EditorGUILayout.Toggle ("Tile X", scrollLayer.tileX);
				// Tile Y
				scrollLayer.tileY = EditorGUILayout.Toggle ("Tile Y", scrollLayer.tileY);
				
				
				
				// Pixel Offset
				float pixelOffsetX = EditorGUILayout.FloatField ("Border X:", scrollLayer.pixelOffset.x);
				float pixelOffsetY = EditorGUILayout.FloatField ("Border Y:", scrollLayer.pixelOffset.y);
				
				scrollLayer.pixelOffset = new Vector2 (pixelOffsetX, pixelOffsetY);
								
				scrollLayer.padding = new Vector2 (paddingX, paddingY);
				
				if (!scrollLayer.isPixelPerfect)
				{
					GUI.enabled = true;
				}
				EditorGUILayout.Space();
				GUILayout.Label("Calculate:");
				scrollLayer.calculateNormals = EditorGUILayout.Toggle("Normals", scrollLayer.calculateNormals);
				scrollLayer.calculateTangents = EditorGUILayout.Toggle("Tangents", scrollLayer.calculateTangents);
				EditorGUILayout.Space();
				GUILayout.Label("On Awake():");
				scrollLayer.refreshVertsOnAwake = EditorGUILayout.Toggle("Refresh Vertices", scrollLayer.refreshVertsOnAwake);
				scrollLayer.refreshMaterialOnAwake = EditorGUILayout.Toggle("Refresh Material", scrollLayer.refreshMaterialOnAwake);
				scrollLayer.refreshTextureOnAwake = EditorGUILayout.Toggle("Refresh Texture", scrollLayer.refreshTextureOnAwake);
				
				GUI.color = Color.red;
				if (GUILayout.Button("Refresh Now"))
				{
					scrollLayer.RefreshBillboard(scrollLayer.refreshVertsOnAwake, scrollLayer.refreshMaterialOnAwake, scrollLayer.refreshTextureOnAwake);
				}
				GUI.color = Color.white;
			}
		}
		else
		{
			// Object Layer
			scrollLayer.isObjectLayer = EditorGUILayout.Toggle("Object Layer", scrollLayer.isObjectLayer);
			
			if (!scrollLayer.isObjectLayer)
			{	
				// Renderer
				scrollLayer.SetRenderer((MeshRenderer)EditorGUILayout.ObjectField("Renderer", scrollLayer.GetRenderer(), typeof(MeshRenderer)
#if !UNITY_3_3
				                                                                  , true
#endif
				                        ));
			}
		}
		
		if (!scrollLayer.isObjectLayer)
		{
			// Texture Names
			scrollLayer.textureNamesFoldout_EDITOR = EditorGUILayout.Foldout(scrollLayer.textureNamesFoldout_EDITOR, "  Texture Names");
			
			if (scrollLayer.textureNamesFoldout_EDITOR)
			{
				int x = 0;
				scrollLayer.foldoutSize_EDITOR = EditorGUILayout.IntField("Size", scrollLayer.foldoutSize_EDITOR);
				if (scrollLayer.GetTextureNames() != null)
				{
					if (scrollLayer.GetTextureNames().Length != scrollLayer.foldoutSize_EDITOR)
					{
						string[] newArray = new string[scrollLayer.foldoutSize_EDITOR];
						for (x = 0; x < scrollLayer.foldoutSize_EDITOR; x++)
						{
							if (scrollLayer.GetTextureNames().Length > x)
							{
								newArray[x] = texNames[x];
							}	
						}
						
						scrollLayer.SetTextureNames(newArray);
						texNames = newArray.Clone() as string[];
					}
				}
				
				for (x = 0; x < scrollLayer.GetTextureNames().Length; x++)
				{
					string s = string.IsNullOrEmpty(texNames[x]) ? "New Texture" : texNames[x];
					bool mat = scrollLayer.GetMaterial() != null;
					bool doSet = mat ? scrollLayer.GetMaterial().HasProperty(s) : false;
					GUI.contentColor = doSet || !mat ? Color.white : Color.red;
					texNames[x] = EditorGUILayout.TextField("Element " + x, s);
					GUI.contentColor = Color.white;
					
					if (doSet)
						scrollLayer.SetTextureName(x, s);
				}
			}
		}
		else
		{
			scrollLayer.objectLayerPixelSpace = EditorGUILayout.Toggle("Use Pixel Space", scrollLayer.objectLayerPixelSpace);
		}
		
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
			Repaint();
			guiChanged = true;
		}
		
		if (Event.current.type == EventType.ExecuteCommand)
		{
			Undo.CreateSnapshot();
			Undo.RegisterSnapshot();
		}
	}
	
	private void CheckUndo()
	{
		Event e = Event.current;

		if (e.type == EventType.MouseDown && e.button == 0 || e.type == EventType.KeyUp && (e.keyCode == KeyCode.Tab))
		{
			Undo.SetSnapshotTarget(scrollLayer, "Change Scroll Layer Properties");
			Undo.CreateSnapshot();
			Undo.ClearSnapshotTarget();
			listeningForGuiChanges = true;
			guiChanged = false;
		}

		if (listeningForGuiChanges && guiChanged)
		{
			Undo.SetSnapshotTarget(scrollLayer, "Change Scroll Layer Properties");
			Undo.RegisterSnapshot();
			Undo.ClearSnapshotTarget();
			listeningForGuiChanges = false;
		}
	}
}
