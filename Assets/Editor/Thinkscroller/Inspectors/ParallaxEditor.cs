using UnityEngine;
using UnityEditor;
using ThinksquirrelSoftware.Thinkscroller;

[CustomEditor(typeof(_Parallax))]
public class ParallaxEditor : Editor
{
	private const string version = "Thinkscroller Version 1.5.1";
	
	private Parallax manager;
	private bool listeningForGuiChanges;
	private bool guiChanged;
	private static bool aboutFoldout = EditorPrefs.GetBool("Thinkscroller.AboutFoldout", true);
	
	private void OnEnable()
	{
		manager = target as Parallax;
	}
	
	public override void OnInspectorGUI()
	{	
		CheckUndo();
		
		// Camera
		manager.SetParallaxCamera((Camera)EditorGUILayout.ObjectField("Camera", manager.GetParallaxCamera(), typeof(Camera)
#if !UNITY_3_3
		                                                              , true
#endif
		                                                              ));
		
		// Scroll Axes
		manager.SetScrollConstraints((ScrollConstraints)EditorGUILayout.EnumPopup("Scroll Axes", manager.GetScrollConstraints()));
		
		// Base Speed
		manager.SetBaseSpeed(EditorGUILayout.FloatField("Base Speed", manager.GetBaseSpeed()));
		
		// Automatic
		bool auto = EditorGUILayout.Toggle("Automatic", manager.automatic);
		
		if (auto != manager.automatic)
		{
			manager.automatic = auto;
			Parallax.RefreshAllBillboards();
		}
		
		if (!manager.automatic)
		{
			manager.minSize = EditorGUILayout.Vector2Field("Min Size", manager.minSize);
			manager.maxSize = EditorGUILayout.Vector2Field("Max Size", manager.maxSize);
		}
		
		// Autorefresh
		bool autoRefresh = EditorGUILayout.Toggle("Auto-Refresh Meshes", manager.autoRefreshBillboards);
		
		if (autoRefresh != manager.autoRefreshBillboards)
		{
			manager.autoRefreshBillboards = autoRefresh;
			Parallax.RefreshAllBillboards();
		}
		
		// Reset All Layers
		GUI.color = Color.red;
		
		if (GUILayout.Button("Set All Layers to Auto"))
		{
			for (int s = 0; s < manager.Length; s++)
			{
				manager[s].isAutoConfigured = true;
			}
		}
		
		GUI.color = Color.white;
		
		if (GUI.changed)
		{
			EditorPrefs.SetBool("Thinkscroller.AboutFoldout", aboutFoldout);
			EditorUtility.SetDirty(target);
			Repaint();
			guiChanged = true;
		}
		
		if (Event.current.type == EventType.ExecuteCommand)
		{
			Undo.CreateSnapshot();
			Undo.RegisterSnapshot();
		}
		
		aboutFoldout = EditorGUILayout.Foldout(aboutFoldout, " About Thinkscroller");
		
		if (aboutFoldout)
		{
			GUILayout.Label(version);
			GUILayout.Label(
				"(c) 2011-2012 Thinksquirrel Software, LLC.\n" +
				"All rights reserved.");
			GUILayout.Label("Designed by Josh Montoute");
			if (GUILayout.Button("Rate this package! (Unity Asset Store)"))
			{
				Application.OpenURL("com.unity3d.kharma:content/2024");
			}		
		}
		
		EditorGUILayout.Separator();
	}
	
	private void CheckUndo()
	{
		Event e = Event.current;

		if (e.type == EventType.MouseDown && e.button == 0 || e.type == EventType.KeyUp && (e.keyCode == KeyCode.Tab))
		{
			Undo.SetSnapshotTarget(manager, "Change Parallax Manager Properties");
			Undo.CreateSnapshot();
			Undo.ClearSnapshotTarget();
			listeningForGuiChanges = true;
			guiChanged = false;
		}

		if (listeningForGuiChanges && guiChanged)
		{
			Undo.SetSnapshotTarget(manager, "Change Parallax Manager Properties");
			Undo.RegisterSnapshot();
			Undo.ClearSnapshotTarget();
			listeningForGuiChanges = false;
		}
	}
}
