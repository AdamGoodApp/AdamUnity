using UnityEngine;
using UnityEditor;
using System.Collections;
using ThinksquirrelSoftware.Thinkscroller;

public static class ThinkscrollerCommands
{
	/// <summary>
	/// Creates a complete parallax scene.
	/// </summary>
	/// <remarks>
	/// Uses an orthographic camera, if selected. Otherwise, uses the main camera if it is orthographic, or creates a new camera if not.
	/// </remarks>
	[MenuItem ("Thinksquirrel/Thinkscroller/Create Parallax Scene")]
	public static void CreateParallaxScene()
	{
		// Selection is an orthographic camera
		if (Selection.activeGameObject)
		{
			if (Selection.activeGameObject.camera)
			{
				if (Selection.activeGameObject.camera.isOrthoGraphic)
				{
					CreateParallaxScene_INTERNAL(Selection.activeGameObject.camera);
					return;
				}
			}
		}
		
		// Main camera is orthographic
		if (Camera.main)
		{
			if (Camera.main.isOrthoGraphic)
			{
				CreateParallaxScene_INTERNAL(Camera.main);
				return;
			}
		}
		
		// Create a camera
		CreateParallaxScene_INTERNAL(null);
	}
	
	private static void CreateParallaxScene_INTERNAL(Camera cam)
	{
		if (!cam)
		{
			GameObject camObject = new GameObject("Parallax Camera");
			cam = camObject.AddComponent<Camera>();
			cam.orthographic = true;
			cam.orthographicSize = 1;
			
			cam.farClipPlane = 100;
			cam.nearClipPlane = -100;
			
			cam.backgroundColor = new Color(0.439f, 0.780f, 0.941f, 0.020f);
			
			if (!Camera.main)
			{
				cam.tag = "MainCamera";
				cam.gameObject.AddComponent<AudioListener>();
			}
		}
		
		// Create 3 game objects at the camera's position, and move them 5 units, 10 units, and 20 units away
		GameObject parentObject = new GameObject("Parallax Manager");
		GameObject foreground = new GameObject("Scroll Layer: Foreground");
		GameObject middleground = new GameObject("Scroll Layer: Middleground");
		GameObject background = new GameObject("Scroll Layer: Background");
		
		// Move parent transform to camera
		parentObject.transform.parent = cam.transform;
		parentObject.transform.localScale = Vector3.one;
		parentObject.transform.localPosition = Vector3.zero;
		
		// Move layers
		foreground.transform.parent = parentObject.transform;
		foreground.transform.localScale = Vector3.one;
		foreground.transform.localPosition = Vector3.forward * 5f;
		
		middleground.transform.parent = parentObject.transform;
		middleground.transform.localScale = Vector3.one;
		middleground.transform.localPosition = Vector3.forward * 10f;
		
		background.transform.parent = parentObject.transform;
		background.transform.localScale = Vector3.one;
		background.transform.localPosition = Vector3.forward * 20f;
		
		// Add components
		var fgLayer = foreground.AddComponent<_ScrollLayer>();
		var mgLayer = middleground.AddComponent<_ScrollLayer>();
		var bgLayer = background.AddComponent<_ScrollLayer>();
	
		var manager = parentObject.AddComponent<_Parallax>();
		
		// Add textures
		fgLayer.SetTexture(Resources.LoadAssetAtPath("Assets/Editor/Thinkscroller/Helpers/Layers/FG.psd", typeof(Texture2D)) as Texture2D);
		mgLayer.SetTexture(Resources.LoadAssetAtPath("Assets/Editor/Thinkscroller/Helpers/Layers/MG.psd", typeof(Texture2D)) as Texture2D);
		bgLayer.SetTexture(Resources.LoadAssetAtPath("Assets/Editor/Thinkscroller/Helpers/Layers/BG.psd", typeof(Texture2D)) as Texture2D);
		
		// Change settings
		fgLayer.alignment = ScrollLayerAlignment.Bottom;
		fgLayer.offset = new Vector2(0, -101);
		fgLayer.tileY = false;
		fgLayer.advancedFoldout_EDITOR = true;
		
		mgLayer.alignment = ScrollLayerAlignment.Bottom;
		mgLayer.offset = new Vector2(0, -92);
		mgLayer.tileY = false;
		mgLayer.advancedFoldout_EDITOR = true;
		
		bgLayer.tileY = false;
		bgLayer.advancedFoldout_EDITOR = true;
		
		manager.SetParallaxCamera(cam);
		
		cam.ResetProjectionMatrix();
		
		fgLayer.RefreshBillboard();
		mgLayer.RefreshBillboard();
		bgLayer.RefreshBillboard();
		
		// Set all objects as dirty
		EditorUtility.SetDirty(fgLayer);
		EditorUtility.SetDirty(mgLayer);
		EditorUtility.SetDirty(bgLayer);
		EditorUtility.SetDirty(manager);
		
		// Set selection to manager
		Selection.activeGameObject = parentObject;
		
	}
	
	/// <summary>
	/// Creates a GameObject with a pixel-perfect scroll layer.
	/// </summary>
	[MenuItem ("Thinksquirrel/Thinkscroller/Create Pixel-Perfect Scroll Layer")]
	public static void CreatePixelPerfectScrollLayer()
	{
		GameObject scrollLayerObject = new GameObject("Scroll Layer");
		scrollLayerObject.AddComponent<_ScrollLayer>();
		
	}
	
	/// <summary>
	/// Creates a GameObject with a tiled scroll layer.
	/// </summary>
	[MenuItem ("Thinksquirrel/Thinkscroller/Create Tiled Scroll Layer")]
	public static void CreateTiledScrollLayer()
	{
		GameObject scrollLayerObject = new GameObject("Scroll Layer");
		var layer = scrollLayerObject.AddComponent<_ScrollLayer>();
		layer.isPixelPerfect = false;
	}
	
	/// <summary>
	/// Creates a GameObject with a stretched scroll layer.
	/// </summary>
	[MenuItem ("Thinksquirrel/Thinkscroller/Create Stretched Scroll Layer")]
	public static void CreateStretchedScrollLayer()
	{
		GameObject scrollLayerObject = new GameObject("Scroll Layer");
		var layer = scrollLayerObject.AddComponent<_ScrollLayer>();
		layer.isPixelPerfect = false;
		layer.SetBillboardStretch(true);
	}
	
	/// <summary>
	/// Creates a GameObject with an object scroll layer.
	/// </summary>
	[MenuItem ("Thinksquirrel/Thinkscroller/Create Object Scroll Layer")]
	public static void CreateObjectScrollLayer()
	{
		GameObject scrollLayerObject = new GameObject("Scroll Layer");
		var layer = scrollLayerObject.AddComponent<_ScrollLayer>();
		layer.isObjectLayer = true;
	}
	
	/// <summary>
	/// Creates a GameObject with a parallax manager.
	/// </summary>
	[MenuItem ("Thinksquirrel/Thinkscroller/Create Parallax Manager")]
	public static void CreateParallaxManager()
	{
		GameObject managerObject = new GameObject("Parallax Manager");
		managerObject.AddComponent<_Parallax>();
	}
	
	/// <summary>
	/// Opens the reference manual.
	/// </summary>
	[MenuItem ("Thinksquirrel/Thinkscroller/Reference Manual...")]
	public static void OpenReferenceManual()
	{
		WebWindow.Load("Documentation", "http://support.thinksquirrel.com/docs/thinkscroller/reference");
	}
}
