using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ThinksquirrelSoftware.Common.Extensions;

/*! \cond PRIVATE */
[ExecuteInEditMode]
[AddComponentMenu("Thinkscroller/Parallax Manager")]
public sealed class _Parallax : ThinksquirrelSoftware.Thinkscroller.Parallax
{
}
/*! \endcond */

namespace ThinksquirrelSoftware.Thinkscroller
{
	/// <summary>
	/// Parallax manager component.
	/// </summary>
	/// <remarks>
	/// The parallax manager manages all of the scroll layers in the scene.
	/// Only one parallax manager can be in a scene at a time.
	/// Parallax managers are responsible for the global properties and the global update cycle.
	/// </remarks>
	/*! \page parallaxmanager Parallax Manager
	 *
	 * \section overview Overview
	 * The Parallax Manager component manages all of the scroll layers in a scene. Only one parallax manager can be in a scene at a time.
	 * \see ThinksquirrelSoftware.Thinkscroller.Parallax
	 * 
	 * \image html parallaxmanager.png
	 * 
	 * \subsection Properties
	 * 
	 * - <b>Main</b>
	 * 		- <b>Camera</b> - The camera to use for auto-billboards, and to determine scroll weights.
	 * 		- <b>Scroll Axes</b> - The axes to scroll (in camera space).
	 * 		- <b>Base Speed</b> - The base speed for all calculations. Modify this value to speed up and slow down the entire scene.
	 * 		- <b>Automatic</b> - If enabled, pixel-perfect layers will resize based on the camera's pixel height.
	 * 		- <b>Min/Max Size</b> - If automatic is disabled, pixel-perfect layers will clamp to the minimum and maximum screen sizes. This is useful for keeping a constant scale with multiple-resolution targets.
	 * 		- <b>Auto-Refresh Meshes</b> - If enabled, meshes will automatically refresh. It is recommended to disable this when targetting a platform with a single, set resolution.
	 * 		- <b>Set All Layers to Auto</b> - This will make all layers use the automatic weight system.
	 * 		- <b>About \htmlonly Thinkscroller\endhtmlonly</b> - Contains version information, and provides a link to rate \htmlonly Thinkscroller\endhtmlonly on the Asset Store.
	 * 
	 * \subsection Navigation
	 * 
	 * \li Back: \ref components
	 * 
	 */
	[ExecuteInEditMode]
	public class Parallax : ThinkscrollerBase, IParallax
	{
		/* Begin singleton */
		
		[SerializeField]
		private static Parallax _instance = null;
		[SerializeField]
		private static readonly object padlock = new object();
		
		/// <summary>
		/// The singleton instance of the parallax manager.
		/// </summary>
		public static Parallax instance
		{
			get
			{
				lock (padlock)
				{
					if (!_instance)
					{
						_instance = ThinkscrollerBase.FindObjectOfType(typeof(Parallax)) as Parallax;
					}
				
					return _instance;
				}
			}
		}
		
		/// <summary>
		/// Scrolls the parallax manager.
		/// </summary>
		/// <remarks>
		/// This function controls the global scroll value and is updated every LateUpdate().
		/// </remarks>
		/// <code>
/// 
///using UnityEngine;
///using System.Collections;
///using ThinksquirrelSoftware.Thinkscroller;
/// 
///public class ScrollWithTransform : MonoBehaviour {
///	private Transform myTransform;
///	private Vector3 oldPosition;
///	private Vector3 scrollVector;
/// 
///	void Start () {
///		myTransform = transform;
///	}
/// 
///	void Update () {
///		scrollVector = myTransform.position - oldPosition;
///		Parallax.Scroll(scrollVector.x, scrollVector.y);
///		oldPosition = myTransform.position;
///	}
///}
		/// </code>
		public static void Scroll(float xScrollValue, float yScrollValue)
		{
			instance.DoScroll(xScrollValue, yScrollValue);
		}
		
		/// <summary>
		/// Scrolls the parallax manager.
		/// </summary>
		/// <remarks>
		/// This function controls the global scroll value and is updated every LateUpdate().
		/// </remarks>
		/// <code>
/// 
///using UnityEngine;
///using System.Collections;
///using ThinksquirrelSoftware.Thinkscroller;
/// 
///public class ScrollWithTransform : MonoBehaviour {
///	private Transform myTransform;
///	private Vector3 oldPosition;
///	private Vector3 scrollVector;
/// 
///	void Start () {
///		myTransform = transform;
///	}
/// 
///	void Update () {
///		scrollVector = myTransform.position - oldPosition;
///		Parallax.Scroll(scrollVector.x, scrollVector.y);
///		oldPosition = myTransform.position;
///	}
///}
		/// </code>
		public static void Scroll(Vector2 scrollValue)
		{
			instance.DoScroll(scrollValue);
		}
		
		/// <summary>
		/// Resets the position of object layers.
		/// </summary>
		public static void ResetAllObjectLayers()
		{
			instance.DoResetPosition();
		}
		
		/// <summary>
		/// Resets the position of object layers.
		/// </summary>
		public static void ResetAllObjectLayers(Vector3 resetPosition)
		{
			instance.DoResetPosition(resetPosition);
		}
		
		/// <summary>
		/// Refreshes all billboard meshes. Note that this only updates vertices.
		/// </summary>
		public static void RefreshAllBillboards()
		{
			if (instance)
			{
				foreach(var scrollLayer in instance.scrollLayers)
				{
					if (!scrollLayer)
						continue;
					
					if (scrollLayer.isAutoBillboard)
					{
						scrollLayer.RefreshBillboard(true, false, false);
					}
				}
			}
		}
		
		/// <summary>
		/// Gets a layer's raw scroll vector from an input value.
		/// </summary>
		/// <remarks>
		/// Useful for figuring out how much a layer will scroll without moving it.
		/// </remarks>
		public static Vector2 GetRawScrollVector(ScrollLayer layer, Vector2 scrollValue)
		{
			if (!instance)
			{
				Debug.LogError("Error: Trying to get raw scroll vector without a parallax manager!");
				return Vector2.zero;
			}
			
			if (layer.isObjectLayer && !layer.objectLayerPixelSpace)
			{	
				if (instance.GetScrollConstraints() == ScrollConstraints.X)
				{
					return instance.GetParallaxCamera().transform.right * -scrollValue.x * instance.baseSpeed * layer.GetScrollSpeed() * layer.GetScrollMod();
				}
				else if (instance.GetScrollConstraints() == ScrollConstraints.Y)
				{
					return instance.GetParallaxCamera().transform.up * -scrollValue.y * instance.baseSpeed * layer.GetScrollSpeed() * layer.GetScrollMod();
				}
				else
				{
					Vector3 sV = (instance.GetParallaxCamera().transform.right * -scrollValue.x) + (instance.GetParallaxCamera().transform.up * -scrollValue.y);
					return sV * instance.baseSpeed * layer.GetScrollSpeed() * layer.GetScrollMod();								
				}
			}
			else
			{
				return scrollValue * instance.baseSpeed * layer.GetScrollSpeed() * layer.GetScrollMod();
			}
		}
			
		void Awake()
		{
			Init();
		}
		
		/// <summary>
		/// Creates a parallax manager if no current manager exists.
		/// </summary>
		public static void CreateManager()
		{
			if (_instance)
				return;
			
			_instance = new GameObject("Parallax Manager").AddComponent<_Parallax>() as Parallax;
		}
		private bool CheckForDuplicate()
		{
			if (instance != this)
			{
				Debug.LogError("Parallax Manager: There can only be one parallax manager per scene.", this);
				return true;
			}
			return false;
		}
		
		/* End singleton */
		
		[SerializeField]
		[HideInInspector]
		private Camera parallaxCamera;
		[SerializeField]
		[HideInInspector]
		private float cameraWidth;
		[SerializeField]
		[HideInInspector]
		private float cameraHeight;
		[SerializeField]
		[HideInInspector]
		private bool _automatic = true;
		[SerializeField]
		[HideInInspector]
		private bool _autoRefreshBillboards = false;
		[SerializeField]
		[HideInInspector]
		private Vector2 _minSize;
		[SerializeField]
		[HideInInspector]
		private Vector2 _maxSize;
		[SerializeField]
		[HideInInspector]
		private float cameraAspectRatio;
		[SerializeField]
		[HideInInspector]
		private ScrollConstraints scrollConstraints = ScrollConstraints.X;
		[SerializeField]
		[HideInInspector]
		private Vector2 scrollValue;
		[SerializeField]
		[HideInInspector]
		private float baseSpeed = 1;
		[SerializeField]
		[HideInInspector]
		private ScrollLayer[] scrollLayers;
		
		public ScrollLayer this[int i]
		{
			get
			{
				return scrollLayers[i];
			}
		}
		
		public int Length
		{
			get
			{
				return scrollLayers.Length;
			}
		}
		
		public bool automatic
		{
			get
			{
				return _automatic;
			}
			set
			{
				_automatic = value;
			}
		}
		
		public bool autoRefreshBillboards
		{
		
			get
			{
				return _autoRefreshBillboards;
			}
			set
			{
				_autoRefreshBillboards = value;
			}
		}
		
		public Vector2 minSize
		{
			get
			{
				return _minSize;
			}
			set
			{
				_minSize = value;
			}
		}
		
		public Vector2 maxSize
		{
			get
			{
				return _maxSize;
			}
			set
			{
				_maxSize = value;
			}
		}
		
		public Camera GetParallaxCamera()
		{
			if (!parallaxCamera)
				SetParallaxCamera(Camera.main);
			
			return parallaxCamera;
		}
		
		public float GetCameraWidth()
		{
			if (cameraWidth == 0)
			{
				GetCameraSize_INTERNAL();
			}
			return cameraWidth;
		}
		
		public float GetCameraHeight()
		{
			if (cameraHeight == 0)
			{
				GetCameraSize_INTERNAL();
			}
			return cameraHeight;
		}
		
		public float GetCameraAspectRatio()
		{
			if (cameraAspectRatio == 0)
			{
				return GetParallaxCamera().aspect;
			}
			return cameraAspectRatio;
		}
		
		public void SetParallaxCamera(Camera parallaxCamera)
		{
			this.parallaxCamera = parallaxCamera;
		}
		
		public ScrollConstraints GetScrollConstraints()
		{
			return scrollConstraints;
		}
		
		public void SetScrollConstraints(ScrollConstraints scrollConstraints)
		{
			this.scrollConstraints = scrollConstraints;
		}
		
		public float GetBaseSpeed()
		{
			return baseSpeed;
		}
		
		public void SetBaseSpeed(float baseSpeed)
		{
			this.baseSpeed = baseSpeed;
		}
		
		public void DoScroll(Vector2 scrollValue)
		{
			this.scrollValue = scrollValue;
		}
		
		public void DoScroll(float xScrollValue, float yScrollValue)
		{
			Scroll(new Vector2(xScrollValue, yScrollValue));
		}
		
		public void DoResetPosition()
		{
			foreach (ScrollLayer s in scrollLayers)
			{
				s.ResetPosition();
			}
		}
		
		public void DoResetPosition(Vector3 resetPosition)
		{
			foreach (ScrollLayer s in scrollLayers)
			{
				s.ResetPosition(resetPosition);
			}
		}
		
		/* Behaviours */
		
		public void RefreshLayers()
		{
			List<ScrollLayer> layerList = new List<ScrollLayer>(ThinkscrollerBase.FindObjectsOfType(typeof(ScrollLayer)) as ScrollLayer[]);
			
			// If auto-configured, assign weights
			foreach (ScrollLayer layer in layerList)
			{
				if (layer.isAutoConfigured && GetParallaxCamera())
				{
					layer.SetWeight(Vector3.Distance(GetParallaxCamera().transform.position, layer.transform.position));
				}
			}
			
			// Sort scroll layers by weight
#if UNITY_FLASH
			layerList.Sort(ScrollLayer.Comparison);
#else
			layerList.Sort();
#endif
			scrollLayers = layerList.ToArray();
		}
		
		private void Init()
		{
			if (CheckForDuplicate())
				return;
			
			RefreshLayers();
		}
		
		void GetCameraSize_INTERNAL()
		{
			cameraWidth = GetParallaxCamera().pixelWidth;
			cameraHeight = GetParallaxCamera().pixelHeight;
			
			if (automatic)
			{
				minSize = new Vector2(cameraWidth, cameraHeight);
				maxSize = new Vector2(cameraWidth, cameraHeight);
			}
			
			cameraWidth = Mathf.Clamp(cameraWidth, minSize.x, maxSize.x);
			cameraHeight = Mathf.Clamp(cameraWidth, minSize.y, maxSize.y);
			
			cameraAspectRatio = cameraWidth / cameraHeight;
		}
		
		void LateUpdate()
		{
			if (CheckForDuplicate())
				return;
			
			if (!GetParallaxCamera())
				return;
			
			GetCameraSize_INTERNAL();
			
#if UNITY_EDITOR
				// Force automatic refreshing in the editor
				if (!Application.isPlaying || autoRefreshBillboards)
				{
					RefreshAllBillboards();
				}
#else		
			if (autoRefreshBillboards)
			{
				RefreshAllBillboards();
			}
#endif			
			foreach (ScrollLayer scrollLayer in scrollLayers)
			{
					
				if (!scrollLayer)
					continue;
				
				if (scrollLayer.enabled)
				{
					if (scrollLayer.isObjectLayer)
					{
						float xx = 1;
						float yy = 1;
						float ww = GetParallaxCamera().pixelWidth / 2;
						float hh = GetParallaxCamera().pixelHeight / 2;
						
						if (scrollLayer.objectLayerPixelSpace)
						{							
							xx = GetParallaxCamera().GetPixelWidth(scrollLayer.transform.position) * ww;
							yy = GetParallaxCamera().GetPixelWidth(scrollLayer.transform.position) * hh;
						}
						
						if (scrollConstraints == ScrollConstraints.X)
						{
							scrollLayer.transform.position += GetParallaxCamera().transform.right * xx * -scrollValue.x * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod();
						}
						else if (scrollConstraints == ScrollConstraints.Y)
						{
							scrollLayer.transform.position += GetParallaxCamera().transform.up * yy * -scrollValue.y * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod();				
						}
						else
						{
							Vector3 sV = (GetParallaxCamera().transform.right * xx * -scrollValue.x) + (GetParallaxCamera().transform.up * yy * -scrollValue.y);
							scrollLayer.transform.position += sV * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod();								
						}
					}
					else if (scrollLayer.GetMaterial())
					{
						foreach (string textureName in scrollLayer.GetTextureNames())
						{
							if (string.IsNullOrEmpty(textureName))
								continue;
							
							if (scrollLayer.GetMaterial().HasProperty(textureName))
							{
								if (scrollConstraints == ScrollConstraints.X)
								{
									scrollLayer.GetMaterial().SetTextureOffset(textureName, WrapVector(scrollLayer.GetMaterial().GetTextureOffset(textureName) + new Vector2(scrollValue.x, 0) * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod()));
								}
								else if (scrollConstraints == ScrollConstraints.Y)
								{
									scrollLayer.GetMaterial().SetTextureOffset(textureName, WrapVector(scrollLayer.GetMaterial().GetTextureOffset(textureName) + new Vector2(0, scrollValue.y) * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod()));
								}
								else
								{
									scrollLayer.GetMaterial().SetTextureOffset(textureName, WrapVector(scrollLayer.GetMaterial().GetTextureOffset(textureName) + scrollValue * baseSpeed * scrollLayer.GetScrollSpeed() * scrollLayer.GetScrollMod()));				
								}
							}
						}
					}
				}
			}
			
			scrollValue = Vector2.zero;
		}
	
		private Vector2 WrapVector(Vector2 input)
		{
			return new Vector2(input.x - (int)input.x, input.y - (int)input.y);	
		}
		
	}
}
