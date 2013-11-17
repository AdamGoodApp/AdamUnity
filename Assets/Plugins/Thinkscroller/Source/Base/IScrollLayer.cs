using UnityEngine;
using ThinksquirrelSoftware.Common.ObjectModel;

namespace ThinksquirrelSoftware.Thinkscroller
{
	/// <summary>
	/// Scroll layer interface type.
	/// </summary>
	public interface IScrollLayer : IMonoBehaviour
	{
		/// <summary>
		/// Gets the weight of the scroll layer.
		/// </summary>
		/// <remarks>
		/// Lighter weights scroll faster than heavier weights.
		/// </remarks>
		float GetWeight();

		/// <summary>
		/// Sets the weight of the scroll layer.
		/// <remarks>
		/// Lighter weights scroll faster than heavier weights.
		/// </remarks>
		/// </summary>
		void SetWeight(float weight);
		
		/// <summary>
		/// Gets the scrolling speed of the scroll layer.
		/// </summary>
		float GetScrollSpeed();

		/// <summary>
		/// Sets the scrolling speed of the scroll layer directly, overriding the weight.
		/// </summary>
		void SetScrollSpeed(float scrollSpeed);
		
		/// <summary>
		/// Gets the scrolling speed modifier of the scroll layer.
		/// </summary>
		float GetScrollMod();

		/// <summary>
		/// Sets the scrolling speed modifier of the scroll layer.
		/// </summary>
		void SetScrollMod(float scrollSpeed);
		
		/// <summary>
		/// Determines whether the scroll layer is automatically configured.
		/// </summary>
		bool isAutoConfigured
		{
			get;
			set;
		}
		
		/// <summary>
		/// Determines whether the scroll layer has an automatic billboard.
		/// </summary>
		bool isAutoBillboard
		{
			get;
			set;
		}
		
		/// <summary>
		/// Determines whether the scroll layer is an object layer.
		/// </summary>
		/// <remarks>
		/// Object layers scroll the transform and ignore any textures on the object. It can also scroll groups of objects. This offers compatibility with sprite/2D management systems.
		/// </remarks>
		bool isObjectLayer
		{
			get;
			set;
		}
		
		/// <summary>
		/// If true, object layers scroll in pixel space (similar to auto-billboards), as opposed to world space.
		/// </summary>
		/// <remarks>
		/// Note that object layers do not always align perfectly with auto-billboards over long distances, due to floating point calculation.
		/// </remarks>
		bool objectLayerPixelSpace
		{
			get;
			set;
		}
		
		/// <summary>
		/// Determines whether the auto billboard is pixel-perfect.
		/// </summary>
		bool isPixelPerfect
		{
			get;
			set;
		}
		
		/// <summary>
		/// The offset (in pixels) of the parallax billboard.
		/// </summary>
		Vector2 offset
		{
			get;
			set;
		}
		
		/// <summary>
		/// The offset (in pixels) of the texture size. Used for floating point error compensation.
		/// </summary>
		Vector2 pixelOffset
		{
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the scroll layer's alignment.
		/// </summary>
		ScrollLayerAlignment alignment
		{
			get;
			set;
		}
		
		/// <summary>
		/// UV padding. Used for floating point error compensation.
		/// </summary>		
		Vector2 padding
		{
			get;
			set;
		}
		
		/// <summary>
		/// Determines whether to tile on the X axis.
		/// </summary>
		bool tileX
		{
			get;
			set;
		}
		
		/// <summary>
		/// Determines whether to tile on the Y axis
		/// </summary>
		bool tileY
		{
			get;
			set;
		}
		
		/// <summary>
		/// If true, refresh materials on Awake().
		/// </summary>
		/// <remarks>
		/// Only applicable for auto billboards.
		/// </remarks>
		bool refreshMaterialOnAwake
		{
			get;
			set;
		}
		
		/// <summary>
		/// If true, refresh the texture on Awake().
		/// </summary>
		/// <remarks>
		/// Only applicable for auto billboards.
		/// </remarks>
		bool refreshTextureOnAwake
		{
			get;
			set;
		}
		
		/// <summary>
		/// If true, refresh vertices on Awake().
		/// </summary>
		/// <remarks>
		/// Only applicable for auto billboards.
		/// </remarks>
		bool refreshVertsOnAwake
		{
			get;
			set;
		}
		
		/// <summary>
		/// If true, calculate normals on the billboard mesh.
		/// </summary>
		/// <remarks>
		/// Only applicable for auto billboards.
		/// </remarks>
		bool calculateNormals
		{
			get;
			set;
		}
		
		/// <summary>
		/// If true, calculate tangents on the billboard mesh.
		/// </summary>
		/// <remarks>
		/// Only applicable for auto billboards.
		/// </remarks>
		bool calculateTangents
		{
			get;
			set;
		}
		
		/// <summary>
		/// Gets the auto billboard scale, in world units.
		/// </summary>
		Vector2 GetBillboardScale();
		
		/// <summary>
		/// Sets the auto billboard scale, in world units.
		/// </summary>
		void SetBillboardScale(Vector2 scale);
		
		/// <summary>
		/// Sets the auto billboard scale, in world units.
		/// </summary>
		void SetBillboardScale(float xSize, float ySize);
		
		/// <summary>
		/// If true, the billboard will stretch to fit the camera viewport.
		/// </summary>
		/// <remarks>
		/// Only works while in play mode.
		/// </remarks>
		bool GetBillboardStretch();
		
		/// <summary>
		/// Sets whether the billboard will stretch.
		/// </summary>
		void SetBillboardStretch(bool stretch);
		
		/// <summary>
		/// Gets the texture associated with the scroll layer.
		/// </summary>
		Texture2D GetTexture();
		
		/// <summary>
		/// Sets the texture associated with the scroll layer.
		/// </summary>
		void SetTexture(Texture2D texture);
		
		/// <summary>
		/// Gets the mesh renderer associated with the scroll layer.
		/// </summary> 
		MeshRenderer GetRenderer();
		
		/// <summary>
		/// Sets the mesh renderer associated with the scroll layer.
		/// </summary> 
		void SetRenderer(MeshRenderer renderer);
		
		/// <summary>
		/// Gets the material associated with the scroll layer.
		/// </summary>
		Material GetMaterial();
		
		/// <summary>
		/// Sets the material associated with the scroll layer.
		/// </summary>
		void SetMaterial(Material material);
			
		/// <summary>
		/// Gets the texture names associated with a scroll layer.
		/// </summary>
		/// <remarks>
		/// Texture names are used to determine which textures on a material to scroll.
		/// </remarks>
		string[] GetTextureNames();
		
		/// <summary>
		/// Gets the texture name associated with a scroll layer at the specified index.
		/// </summary>
		/// <remarks>
		/// Texture names are used to determine which textures on a material to scroll.
		/// </remarks>
		string GetTextureName(int index);
			
		/// <summary>
		/// Sets the texture names associated with a scroll layer.
		/// </summary>
		/// <remarks>
		/// Texture names are used to determine which textures on a material to scroll.
		/// </remarks>
		void SetTextureNames(params string[] textureNames);
		
		/// <summary>
		/// Sets the texture name associated with a scroll layer at the specified index.
		/// </summary>
		/// <remarks>
		/// Texture names are used to determine which textures on a material to scroll.
		/// </remarks>
		void SetTextureName(int index, string textureName);
			
		/* Behaviours */
		
		/// <summary>
		/// Refreshes the auto billboard mesh.
		/// </summary>
		void RefreshBillboard();
		
		/// <summary>
		/// Refreshes the auto billboard mesh.
		/// </summary>
		/// <param name='refreshVertices'>
		/// If true, refresh vertices.
		/// </param>
		/// <param name='resetMaterial'>
		/// If true, reset material.
		/// </param>
		/// <param name='refreshTexture'>
		/// If true, refresh texture.
		/// </param>
		void RefreshBillboard(bool refreshVertices, bool resetMaterial, bool refreshTexture);
		
		/// <summary>
		/// Reset the scroll layer.
		/// </summary>
		void Reset();
		
		/// <summary>
		/// Reset the scroll layer's position (Object layers only).
		/// </summary>
		void ResetPosition();
		
		/// <summary>
		/// Reset the scroll layer's position (Object layers only).
		/// </summary>
		void ResetPosition(Vector3 resetPosition);
	}
}
