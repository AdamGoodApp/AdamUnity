using UnityEngine;
using System.Collections;
using ThinksquirrelSoftware.Common.Extensions;

/*! \cond PRIVATE */
[AddComponentMenu("Thinkscroller/Scroll Layer")]
public sealed class _ScrollLayer : ThinksquirrelSoftware.Thinkscroller.ScrollLayer
{
}
/*! \endcond */

namespace ThinksquirrelSoftware.Thinkscroller
{
	/// <summary>
	/// Scroll layer component.
	/// </summary>
	/// <remarks>
	/// Scroll layers represent each parallax layer, and can be both in the foreground and background.
	/// Scroll layers are sorted by their weight - this can be done automatically, or manually overriden.
	/// Layers can also be stretched across the screen or pixel-perfect.
	/// Note that scroll layers are independent of Unity's layer system.
	/// </remarks>
	/*! \page scroll_layer_a Scroll Layer (Auto-Billboard Layer)
	 *
	 * \section overview Overview
	 * The Auto-Billboard Scroll Layer automatically creates a billboard (camera-facing mesh) and scrolls the UVs of a texture placed on it. Only orthographic cameras are officially supported in this mode.
	 * \see ThinksquirrelSoftware.Thinkscroller.ScrollLayer
	 * 
	 * \image html scroll_layer_a.png
	 * 
	 * \subsection Properties
	 * 
	 * - <b>Main</b>
	 * 		- <b>Current Mode</b> - Displays the current scrolling mode.
	 * 		- <b>Auto Weights</b> - If enabled, weights will automatically be calculated based on the layer's distance from the parallax camera.
	 * 		- <b>Update Layers</b> - Updates the weights in the parallax manager. This is usually automatic.
	 * 		- <b>Weight</b> - The layer's weight. Heavier weights (higher values) move more slowly than lighter weights.
	 * 		- <b>Layer Speed</b> - Displays the speed of the layer, calculated by its weight.
	 * 		- <b>Speed Modifier</b> - A modifier that is multiplied with the layer speed.
	 * 		- <b>Auto Billboard</b> - This must be enabled for this mode.
	 * 		- <b>X, Y</b> - The size of the billboard (in world units). This is disabled if the layer is in pixel-perfect mode.
	 * 		- <b>Stretch</b> - Stretches the billboard to fill the screen. This is disabled if the layer is in pixel-perfect mode.
	 * 		- <b>Pixel Perfect</b> - If enabled, the billboard's screen size will match the size of the texture, for pixel-perfect rendering.
	 * 		- <b>Texture</b> - The texture to scroll.
	 * 		- <b>Dimensions</b> - Displays the dimensions of the selected texture.
	 * - <b>Advanced</b>
	 * 		- <b>Alignment</b> - The screen alignment for the billboard.
	 * 		- <b>Offset X, Offset Y</b> - The offset for the billboard, in pixel space (unless overriden through script).
	 * 		- <b>UV Padding</b> - The inset for the layer UVs, in UV (0..1) space. If there are lines visible at the edges, set this to a low negative value.
	 * 		- <b>Tile X</b> - If enabled, repeat the billboard on the X axis. (Pixel-perfect only)
	 * 		- <b>Tile Y</b> - If enabled, repeat the billboard on the Y axis. (Pixel-perfect only)
	 * 		- <b>Border X, Border Y</b> - Additional texture correction, in pixel space. This should usually stay at 0. (Pixel-perfect only)
	 * 		- <b>Normals, Tangents</b> - Determines whether normal or tangent information should be calculated for the billboard.
	 * 		- <b>On Awake()...</b> - Determines if and how billboards are refreshed on Awake.
	 * 		- <b>Refresh Now</b> - Refresh the billboard now, in the editor. This is usually automatic.
	 * 	- <b>Texture Names</b>
	 * 		- <b>Texture Names Array</b> - Use this when using a custom shader. Texture Names correspond to the names in a shader. \htmlonly Thinkscroller\endhtmlonly will only scroll the listed texture names. Defaults to _MainTex.
	 * 
	 * \subsection Navigation
	 * 
	 * \li Back: \ref components
	 * 
	 */
	/*! \page scroll_layer_b Scroll Layer (UV Scrolling Layer)
	 *
	 * \section overview Overview
	 * The UV Scrolling Layer will scroll the textures on a mesh.
	 * \see ThinksquirrelSoftware.Thinkscroller.ScrollLayer
	 * 
	 * \image html scroll_layer_b.png
	 * 
	 * \subsection Properties
	 * 
	 * - <b>Main</b>
	 * 		- <b>Current Mode</b> - Displays the current scrolling mode.
	 * 		- <b>Auto Weights</b> - If enabled, weights will automatically be calculated based on the layer's distance from the parallax camera.
	 * 		- <b>Update Layers</b> - Updates the weights in the parallax manager. This is usually automatic.
	 * 		- <b>Weight</b> - The layer's weight. Heavier weights (higher values) move more slowly than lighter weights.
	 * 		- <b>Layer Speed</b> - Displays the speed of the layer, calculated by its weight.
	 * 		- <b>Speed Modifier</b> - A modifier that is multiplied with the layer speed.
	 * 		- <b>Auto Billboard</b> - This must be disabled for this mode.
	 * 		- <b>Object Layer</b> - This must be disabled for this mode.
	 * 		- <b>Renderer</b> - The mesh renderer to scroll.
	 * 	- <b>Texture Names</b>
	 * 		- <b>Texture Names Array</b> - Use this when using a custom shader. Texture Names correspond to the names in a shader. \htmlonly Thinkscroller\endhtmlonly will only scroll the listed texture names. Defaults to _MainTex.
	 * 
	 * \subsection Navigation
	 * 
	 * \li Back: \ref components
	 * 
	 */
	/*! \page scroll_layer_c Scroll Layer (Object Layer)
	 *
	 * \section overview Overview
	 * The Object Layer will scroll the transform of a game object.
	 * \see ThinksquirrelSoftware.Thinkscroller.ScrollLayer
	 * 
	 * \image html scroll_layer_c.png
	 * 
	 * \subsection Properties
	 * 
	 * - <b>Main</b>
	 * 		- <b>Current Mode</b> - Displays the current scrolling mode.
	 * 		- <b>Auto Weights</b> - If enabled, weights will automatically be calculated based on the layer's distance from the parallax camera.
	 * 		- <b>Update Layers</b> - Updates the weights in the parallax manager. This is usually automatic.
	 * 		- <b>Weight</b> - The layer's weight. Heavier weights (higher values) move more slowly than lighter weights.
	 * 		- <b>Layer Speed</b> - Displays the speed of the layer, calculated by its weight.
	 * 		- <b>Speed Modifier</b> - A modifier that is multiplied with the layer speed.
	 * 		- <b>Auto Billboard</b> - This must be disabled for this mode.
	 * 		- <b>Object Layer</b> - This must be enabled for this mode.
	 * 		- <b>Use Pixel Space</b> - If enabled, object layers will use pixel space to scroll. Use this to match scroll speeds with non-object layers.
	 * 
	 * \subsection Navigation
	 * 
	 * \li Back: \ref components
	 * 
	 */
	public class ScrollLayer : ThinkscrollerBase, IScrollLayer
#if !UNITY_FLASH
		, System.IComparable<ScrollLayer>
#endif	
	{
		[SerializeField]
		[HideInInspector]
		private MeshFilter meshFilter;
		[SerializeField]
		[HideInInspector]
		private MeshRenderer meshRenderer;
		[SerializeField]
		[HideInInspector]
		private bool autoBillboard = true;
		[SerializeField]
		[HideInInspector]
		private bool objectLayer = false;
		[SerializeField]
		[HideInInspector]
		private bool _objectLayerPixelSpace = true;
		[SerializeField]
		[HideInInspector]
		private bool auto = true;
		[SerializeField]
		[HideInInspector]
		private float weight = 0;
		[SerializeField]
		[HideInInspector]
		private bool stretch = false;
		[SerializeField]
		[HideInInspector]
		private bool pixelPerfect = true;
		[SerializeField]
		[HideInInspector]
		private bool _tileX = true;
		[SerializeField]
		[HideInInspector]
		private bool _tileY = true;
		[SerializeField]
		[HideInInspector]
		private Vector2 _offset = Vector2.zero;
		[SerializeField]
		[HideInInspector]
		private Vector2 _pixelOffset = Vector2.zero;
		[SerializeField]
		[HideInInspector]
		private ScrollLayerAlignment mAlignment = ScrollLayerAlignment.Center;
		[SerializeField]
		[HideInInspector]
		private Vector2 _padding = Vector2.zero;
		[SerializeField]
		[HideInInspector]
		private float xSize = 10;
		[SerializeField]
		[HideInInspector]
		private float ySize = 10;
		[SerializeField]
		[HideInInspector]
		private float scrollSpeed = 1;
		[SerializeField]
		[HideInInspector]
		private float scrollMod = 1;
		[SerializeField]
		[HideInInspector]
		private Texture2D texture;
		[SerializeField]
		[HideInInspector]
		private string[] textureNames;
		[SerializeField]
		[HideInInspector]
		private bool _refreshVertsOnAwake = true;
		[SerializeField]
		[HideInInspector]
		private bool _refreshMaterialOnAwake = true;
		[SerializeField]
		[HideInInspector]
		private bool _refreshTextureOnAwake = true;
		[SerializeField]
		[HideInInspector]
		private bool _calculateNormals = false;
		[SerializeField]
		[HideInInspector]
		private bool _calculateTangents = false;
		[SerializeField]
		[HideInInspector]
		private Vector3 oldPosition = Vector3.zero;
		/*! \cond PRIVATE */
		[HideInInspector]
		public bool textureNamesFoldout_EDITOR = true;
		[HideInInspector]
		public int foldoutSize_EDITOR = 1;
		[HideInInspector]
		public bool advancedFoldout_EDITOR = false;
		/*! \endcond */
		
		/*! \implements IScrollLayer */
		public bool refreshMaterialOnAwake
		{
			get
			{
				return _refreshMaterialOnAwake;
			}
			set
			{
				_refreshMaterialOnAwake = value;
			}
		}

		public bool refreshTextureOnAwake
		{
			get
			{
				return _refreshTextureOnAwake;
			}
			set
			{
				_refreshTextureOnAwake = value;
			}
		}

		public bool refreshVertsOnAwake
		{
			get
			{
				return _refreshVertsOnAwake;
			}
			set
			{
				_refreshVertsOnAwake = value;
			}
		}
		
		public bool calculateNormals
		{
			get
			{
				return _calculateNormals;
			}
			set
			{
				if (value == false)
				{
					_calculateTangents = false;
				}
				_calculateNormals = value;
			}
		}
		
		public bool calculateTangents
		{
			get
			{
				return _calculateTangents;
			}
			set
			{
				if (value == true)
				{
					_calculateNormals = true;
				}
				_calculateTangents = value;
			}
		}
		
		public void SetWeight(float weight)
		{
			this.weight = weight;
			this.scrollSpeed = 1f / (weight + .00001f);
		}
		
		/*! \implements IScrollLayer */
		/// <summary>
		/// Gets the weight of the scroll layer.
		/// </summary>
		/// <remarks>
		/// Lighter weights scroll faster than heavier weights.
		/// </remarks>
		public float GetWeight()
		{
			return weight;
		}
		
		public float GetScrollSpeed()
		{
			return scrollSpeed;
		}
		
		public void SetScrollSpeed(float scrollSpeed)
		{
			if (this.scrollSpeed != scrollSpeed)
			{
				this.scrollSpeed = scrollSpeed;
			}
		}
		
		public float GetScrollMod()
		{
			return scrollMod;
		}
		
		public void SetScrollMod(float scrollMod)
		{
			if (this.scrollMod != scrollMod)
			{
				this.scrollMod = scrollMod;
			}
		}
		
		public bool isAutoConfigured
		{
			get
			{
				return auto;
			}
			set
			{
				if (value == true && auto == false)
				{
					auto = value;
					if (Parallax.instance)
					{
						Parallax.instance.RefreshLayers();
					}
				}
				else
				{
					auto = value;
				}
			}
		}
		
		public bool isAutoBillboard
		{
			get
			{
				return autoBillboard;
			}
			set
			{
				if (value == true && autoBillboard == false)
				{
					objectLayer = false;
					autoBillboard = value;
					RefreshBillboard();
				}
				else if (value == false && autoBillboard == true)
				{
					if (meshRenderer)
					{
						DestroyImmediate(meshRenderer);
					}
					if (meshFilter)
					{
						DestroyImmediate(meshFilter);
					}
					autoBillboard = value;
				}
					
			}
		}
		
		public bool isObjectLayer
		{
			get
			{
				return objectLayer;
			}
			set
			{
				if (value == true && objectLayer == false)
				{
					isAutoBillboard = false;
				}
				objectLayer = value;
			}
		}
		
		public bool objectLayerPixelSpace
		{
			get
			{
				return _objectLayerPixelSpace;
			}
			set
			{
				_objectLayerPixelSpace = value;
			}
		}
		
		public bool isPixelPerfect
		{
			get
			{
				return pixelPerfect;
			}
			set
			{
				if (value == true && pixelPerfect == false)
				{
					pixelPerfect = value;
					SetBillboardStretch(false);
					RefreshBillboard(true, false, false);
				}
				else if (value == false && pixelPerfect == true)
				{
					pixelPerfect = value;
					RefreshBillboard(true, false, false);
				}
			}
		}
		
		public bool tileX
		{
			get
			{
				return _tileX;
			}
			set
			{
				if (value != _tileX)
				{
					_tileX = value;
					RefreshBillboard(true, false, false);
				}
			}
		}
		
		public bool tileY
		{
			get
			{
				return _tileY;
			}
			set
			{
				if (value != _tileY)
				{
					_tileY = value;
					RefreshBillboard(true, false, false);
				
				}
			}
		}
		
		public Vector2 offset
		{
			get
			{
				return _offset;
			}
			set
			{
				if (value != _offset)
				{
					_offset = value;
					RefreshBillboard(true, false, false);
				}
			}
		}
		
		public Vector2 pixelOffset
		{
			get
			{
				return _pixelOffset;
			}
			set
			{
				if (value != _pixelOffset)
				{
					_pixelOffset = value;
					RefreshBillboard (true, false, false);
				}
			}
		}
		
		public ScrollLayerAlignment alignment
		{
			get
			{
				return mAlignment;
			}
			set
			{
				if (value != mAlignment)
				{
					mAlignment = value;
					RefreshBillboard (true, false, false);
				}
			}
		}
		
		public Vector2 padding
		{
			get
			{
				return _padding;
			}
			set
			{
				if (value != _padding)
				{
					_padding = value;
					RefreshBillboard(true, false, false);
				}
			}
		}
		
		public Vector2 GetBillboardScale()
		{
			return new Vector2(xSize, ySize);
		}
		
		public void SetBillboardScale(Vector2 scale)
		{
			SetBillboardScale(scale.x, scale.y);
		}
		
		public void SetBillboardScale(float xSize, float ySize)
		{
			bool changed = false;
			
			if (this.xSize != xSize)
			{
				this.xSize = xSize;
				changed = true;
			}
			if (this.ySize != ySize)
			{
				this.ySize = ySize;
				changed = true;
			}
			if (changed)
				RefreshBillboard(true, false, false);
		}
		
		public bool GetBillboardStretch()
		{
			return stretch;
		}

		public void SetBillboardStretch(bool stretch)
		{
			this.stretch = stretch;
		}
		
		public MeshRenderer GetRenderer()
		{
			return meshRenderer;
		}
		
		public void SetRenderer(MeshRenderer renderer)
		{
			meshRenderer = renderer;
		}
		
		public Material GetMaterial()
		{
			if (autoBillboard)
			{
				if (!meshRenderer)
				{
					meshRenderer = gameObject.AddComponent<MeshRenderer>();	
					ResetMaterial_INTERNAL();
				}
				else if (meshRenderer.sharedMaterial == null)
				{
					ResetMaterial_INTERNAL();
				}
				
				return meshRenderer.sharedMaterial;
			}
			else if (meshRenderer)
			{
				if (meshRenderer.sharedMaterial != null)
				{
					return meshRenderer.sharedMaterial;
				}
			}
			return null;
		}
		
		public void SetMaterial(Material material)
		{
			if (meshRenderer)
			{
				meshRenderer.sharedMaterial = material;
			}
		}
		
		public Texture2D GetTexture()
		{
			return texture;
		}
		
		public void SetTexture(Texture2D texture)
		{
			if (this.texture != texture)
			{
				this.texture = texture;
			}
		}
		
		public string[] GetTextureNames()
		{
			return textureNames;
		}
		
		public string GetTextureName(int index)
		{
			if (textureNames.Length > index)
			{
				return textureNames[index];
			}
			return "";
		}
		
		public void SetTextureNames(params string[] textureNames)
		{
			this.textureNames = textureNames;
		}
		
		public void SetTextureName(int index, string textureName)
		{
			if (textureNames.Length > index)
			{
				textureNames[index] = textureName;
			}
		}

#if !UNITY_FLASH
		/// <summary>
		/// Compares a scroll layer to another by weight.
		/// </summary>
		/// <remarks>
		/// Implements System.IComparable<ScrollLayer>.
		/// </remarks>
		public int CompareTo(ScrollLayer other)
		{
	        // If other is not a valid object reference, this instance is greater.
			if (other == null) return 1;

			// Compare the underlying weights.
			return GetWeight().CompareTo(other.GetWeight());
		}	
#endif
		internal static int Comparison(ScrollLayer layer, ScrollLayer other)
		{
			// If other is not a valid object reference, this instance is greater.
			if (other == null) return 1;
			
			// Compare the underlying weights.
			float weight = layer.GetWeight();
			float otherWeight = other.GetWeight();
			return weight < otherWeight ? -1 : (weight > otherWeight ? 1 : 0 );	
		}
		/* Behaviours */
		
		private void RefreshVertices_INTERNAL()
		{
			if (!Parallax.instance)
				return;
			
			if (!Parallax.instance.GetParallaxCamera())
				return;
			
			Camera parallaxCam = Parallax.instance.GetParallaxCamera();
			float distance = Vector3.Distance(parallaxCam.transform.position, transform.position);
			Vector3 topLeft = Vector3.zero;
			Vector3 topRight = Vector3.zero;
			Vector3 bottomLeft = Vector3.zero;
			Vector3 bottomRight = Vector3.zero;
			Vector3 ppxy0 = Vector3.zero;
			Vector3 ppxy1 = Vector3.zero;
			
			Matrix4x4 m = parallaxCam.cameraToWorldMatrix;
			Matrix4x4 mm = parallaxCam.projectionMatrix.inverse;
			float x = parallaxCam.orthographicSize * Parallax.instance.GetCameraAspectRatio();
			float width = 0;
			float height = 0;
			
			Vector2 offset = this.offset * parallaxCam.GetPixelWidth(this.transform.position);
			float pixel = parallaxCam.GetPixelWidth(this.transform.position);
			
			switch(alignment)
			{
		
			case ScrollLayerAlignment.TopLeft:
					offset -= new Vector2(-Parallax.instance.GetCameraWidth() * .5f * pixel,
								Parallax.instance.GetCameraHeight() * .5f * pixel);
					break;
			
			case ScrollLayerAlignment.Top:
					offset -= new Vector2(0, Parallax.instance.GetCameraHeight() * .5f * pixel);
					break;
			
			case ScrollLayerAlignment.TopRight:
					offset -= new Vector2(Parallax.instance.GetCameraWidth() * .5f * pixel,
								Parallax.instance.GetCameraHeight() * .5f * pixel);
					break;
			
			case ScrollLayerAlignment.Left:
					offset -= new Vector2(-Parallax.instance.GetCameraWidth() * .5f * pixel, 0);
					break;
			
			case ScrollLayerAlignment.Center:
					offset -= Vector2.zero;
					break;
			
			case ScrollLayerAlignment.Right:
					offset -= new Vector2(Parallax.instance.GetCameraWidth() * .5f * pixel, 0);
					break;
			
			case ScrollLayerAlignment.BottomLeft:
					offset -= new Vector2(-Parallax.instance.GetCameraWidth() * .5f * pixel,
								-Parallax.instance.GetCameraHeight() * .5f * pixel);
					break;
			
			case ScrollLayerAlignment.Bottom:
					offset -= new Vector2(0, -Parallax.instance.GetCameraHeight() * .5f * pixel);
					break;
			
			case ScrollLayerAlignment.BottomRight:
					offset -= new Vector2(Parallax.instance.GetCameraWidth() * .5f * pixel,
								-Parallax.instance.GetCameraHeight() * .5f * pixel);
					break;
			}
			
			if (texture)
			{
				width = texture.width + pixelOffset.x;
				height = texture.height + pixelOffset.y;
				
				if (!stretch && pixelPerfect && tileX && !tileY)
				{
					ppxy0 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(0, 0, -distance));
					ppxy1 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(width / Parallax.instance.GetCameraWidth(), height / Parallax.instance.GetCameraHeight(), -distance));
					
					topLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-x + offset.x, -(ppxy1.y - ppxy0.y) + offset.y, -distance));
					topRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(x + offset.x, -(ppxy1.y - ppxy0.y) + offset.y, -distance));
					bottomLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-x + offset.x, (ppxy1.y - ppxy0.y) + offset.y, -distance));
					bottomRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(x + offset.x, (ppxy1.y - ppxy0.y) + offset.y, -distance));
				}
				else if (!stretch && pixelPerfect && tileY && !tileX)
				{
					ppxy0 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(0, 0, -distance));
					ppxy1 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(width / Parallax.instance.GetCameraWidth(), height / Parallax.instance.GetCameraHeight(), -distance));
					
					float xS = -(ppxy1.y - ppxy0.y) * (width / height);
					
					topLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-xS + offset.x, parallaxCam.orthographicSize + offset.y, -distance));
					topRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(xS + offset.x, parallaxCam.orthographicSize + offset.y, -distance));
					bottomLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-xS + offset.x, -parallaxCam.orthographicSize + offset.y, -distance));
					bottomRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(xS + offset.x, -parallaxCam.orthographicSize + offset.y, -distance));
				}
				else if (!stretch && pixelPerfect && !tileY && !tileX)
				{
					ppxy0 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(0, 0, -distance));
					ppxy1 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(width / Parallax.instance.GetCameraWidth(), height / Parallax.instance.GetCameraHeight(), -distance));
					
					float xS = -(ppxy1.y - ppxy0.y) * (width / height);
					
					topLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-xS + offset.x, -(ppxy1.y - ppxy0.y) + offset.y, -distance));
					topRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(xS + offset.x, -(ppxy1.y - ppxy0.y) + offset.y, -distance));
					bottomLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-xS + offset.x, (ppxy1.y - ppxy0.y) + offset.y, -distance));
					bottomRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(xS + offset.x, (ppxy1.y - ppxy0.y) + offset.y, -distance));
	
				}
				else
				{
					topLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-x + offset.x, parallaxCam.orthographicSize + offset.y, -distance));
					topRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(x + offset.x, parallaxCam.orthographicSize + offset.y, -distance));
					bottomLeft = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(-x + offset.x, -parallaxCam.orthographicSize + offset.y, -distance));
					bottomRight = parallaxCam.transform.position - m.MultiplyPoint3x4(new Vector3(x + offset.x, -parallaxCam.orthographicSize + offset.y, -distance));
				}
			}
			
			
			Mesh mesh = new Mesh();
			mesh.vertices = new Vector3[4]{
				new Vector3(topLeft.x, topLeft.y, 0),
				new Vector3(topRight.x, topRight.y, 0),
				new Vector3(bottomLeft.x, bottomLeft.y, 0),
				new Vector3(bottomRight.x, bottomRight.y, 0)
			};
			mesh.triangles = new int[6]{0,1,2,1,3,2};
			
			if (calculateNormals)
			{
				mesh.RecalculateNormals();
			}
			
			if (!stretch)
			{
				float xx = -(x / xSize);
				float yy = -(parallaxCam.orthographicSize / ySize);
				
				if (pixelPerfect && texture)
				{
					if (tileX && tileY)
					{
						ppxy0 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(0, 0, -distance));
						ppxy1 = parallaxCam.transform.position - mm.MultiplyPoint3x4(new Vector3(width / Parallax.instance.GetCameraWidth(), height / Parallax.instance.GetCameraHeight(), -distance));
					}
					
					ySize = -(ppxy1.y - ppxy0.y);
					
					xSize = ySize * width / height;
					
					if (!tileX && !tileY)
					{
						xx = -1 - padding.x;
						yy = -1 - padding.y;
					}
					else
					{
						if (tileX)
						{
							xx = -(x / xSize) - padding.x;
						}
						else
						{
							xx = -1 - padding.x;
						}
						
						if (tileY)
						{
							yy = -(parallaxCam.orthographicSize / ySize) - padding.y;
						}
						else
						{
							yy = -1 - padding.y;
						}
					}
					
				}
				
				mesh.uv = new Vector2[4]{
					new Vector2(padding.x, yy),
					new Vector2(xx, yy),
					new Vector2(padding.x, padding.y),
					new Vector2(xx, padding.y),
				};
			}
			else
			{
				mesh.uv = new Vector2[4]{
					new Vector3(padding.x, - padding.y),
					new Vector3(-1 - padding.x, - padding.y),
					new Vector3(padding.x, 1 - padding.y),
					new Vector3(-1 - padding.x, 1 - padding.y)
				};
			}
			
			if (calculateNormals && calculateTangents)
			{
				mesh.RecalculateTangents();
			}
			
			mesh.Optimize();
			
			if (meshFilter.sharedMesh)
			{
				DestroyImmediate(meshFilter.sharedMesh);
			}
			meshFilter.sharedMesh = mesh;
		}
		
		private void ResetMaterial_INTERNAL()
		{
			if (meshRenderer.sharedMaterial)
			{
				DestroyImmediate(meshRenderer.sharedMaterial);
			}
			meshRenderer.sharedMaterial = new Material(Shader.Find("Thinkscroller/Parallax Default"));
			textureNames = new string[1]{ "_MainTex" };
		}
		
		private void RefreshTexture_INTERNAL()
		{
			if (texture)
			{
				meshRenderer.sharedMaterial.mainTexture = texture;
			}
		}
		
		public void RefreshBillboard ()
		{
			RefreshBillboard (true, true, true);
		}
		
		public void RefreshBillboard (bool refreshVertices, bool resetMaterial, bool refreshTexture)
		{
			if (!meshFilter && gameObject.GetComponent<MeshFilter>())
			{
				meshFilter = gameObject.GetComponent<MeshFilter>();
			}
			else if (!meshFilter)
			{
				meshFilter = gameObject.AddComponent<MeshFilter>();
			}
			
			if (gameObject.GetComponent<MeshRenderer>())
			{
				meshRenderer = gameObject.GetComponent<MeshRenderer>();
				meshRenderer.castShadows = false;
				meshRenderer.receiveShadows = false;
			}
			else if (gameObject.GetComponent<MeshRenderer>() == null)
			{
				meshRenderer = gameObject.AddComponent<MeshRenderer>();
				meshRenderer.castShadows = false;
				meshRenderer.receiveShadows = false;
			}
			
			if (refreshVertices)
				RefreshVertices_INTERNAL();
			if (resetMaterial)
				ResetMaterial_INTERNAL();
			if (refreshTexture)
				RefreshTexture_INTERNAL();
		}
		
		public void Reset()
		{	
			if (!Parallax.instance)
			{
				return;
			}
			if (autoBillboard)
			{
				RefreshBillboard();
			}
			Parallax.instance.RefreshLayers();
		}
		
		public void ResetPosition()
		{
			if (isObjectLayer)
			{
				transform.position = oldPosition;
			}
		}
		
		public void ResetPosition(Vector3 resetPosition)
		{
			if (isObjectLayer)
			{
				transform.position = resetPosition;
				oldPosition = transform.position;
			}
		}
		
		IEnumerator ResetDelay_INTERNAL()
		{
			yield return new WaitForEndOfFrame();
			if (Parallax.instance)
			{
				if (autoBillboard && (_refreshMaterialOnAwake || _refreshTextureOnAwake || _refreshVertsOnAwake))
				{
					RefreshBillboard(_refreshVertsOnAwake, _refreshMaterialOnAwake, _refreshTextureOnAwake);
				}
				Parallax.instance.RefreshLayers();
			}
		}
		
		/* MonoBehaviours */
		
		void Awake()
		{
			oldPosition = transform.position;
			StartCoroutine(ResetDelay_INTERNAL());
		}
		
		void OnDisable()
		{
			if (Parallax.instance)
			{
				Parallax.instance.RefreshLayers();
			}
		}
		
		void OnEnable()
		{
			if (Parallax.instance)
			{
				Parallax.instance.RefreshLayers();
			}
		}
		
	}
}
