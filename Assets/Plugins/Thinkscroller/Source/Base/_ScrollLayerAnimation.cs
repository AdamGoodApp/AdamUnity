using UnityEngine;
using System.Collections;

/*! \cond PRIVATE */
[RequireComponent(typeof(_ScrollLayer))]
[AddComponentMenu("Thinkscroller/Scroll Layer Animation")]
[ExecuteInEditMode]
public sealed class _ScrollLayerAnimation : ThinksquirrelSoftware.Thinkscroller.ScrollLayerAnimation
{
}
/*! \endcond */

namespace ThinksquirrelSoftware.Thinkscroller
{
	/// <summary>
	/// Scroll layer animation component.
	/// </summary>
	/// <remarks>
	/// This class exposes the properties of a scroll layer for Unity animation curves.
	/// </remarks>
	/*! \page scroll_layer_animation Scroll Layer Animation
	 *
	 * \section overview Overview
	 * The Scroll Layer Animation component exposes the properties of a scroll layer for Unity animation curves.
	 * \see \ref animation
	 * 
	 * \image html scroll_layer_animation.png
	 * 
	 * \subsection Properties
	 * 
	 * This component does not contain any editable properties.
	 * 
	 * \subsection Navigation
	 * 
	 * \li Back: \ref components
	 * 
	 */
	public class ScrollLayerAnimation : ThinkscrollerBase
	{
		/*! \cond PRIVATE */
		[HideInInspector] public float weight;
		[HideInInspector] public float scrollSpeed;
		[HideInInspector] public float scrollMod;
		[HideInInspector] public Vector2 offset;
		[HideInInspector] public Vector2 pixelOffset;
		[HideInInspector] public Vector2 padding;
		/*! \endcond */
		
		private IScrollLayer scrollLayer;
		
		/// <summary>
		/// Syncs the component to the scroll layer.
		/// </summary>
		/// <remarks>
		/// If you edit a scroll layer's properties through code and this component is present, you will need to call this method.
		/// </remarks>
		public void SyncToScrollLayer()
		{
			weight = scrollLayer.GetWeight();
			scrollSpeed = scrollLayer.GetScrollSpeed();
			scrollMod = scrollLayer.GetScrollMod();
			offset = scrollLayer.offset;
			pixelOffset = scrollLayer.pixelOffset;
			padding = scrollLayer.padding;
		}
		
		void Awake()
		{
			scrollLayer = this.GetComponentFromInterface<IScrollLayer>();
			
			SyncToScrollLayer();
		}
		
		void LateUpdate()
		{
			bool refresh = false;
			
			scrollLayer.SetWeight(weight);
			scrollLayer.SetScrollSpeed(scrollSpeed);
			scrollLayer.SetScrollMod(scrollMod);
			
			if (scrollLayer.offset != offset)
			{
				refresh = true;
				scrollLayer.offset = offset;
			}
			
			if (scrollLayer.pixelOffset != pixelOffset)
			{
				refresh = true;
				scrollLayer.pixelOffset = pixelOffset;
			}
			
			if (scrollLayer.padding != padding)
			{
				refresh = true;
				scrollLayer.padding = padding;
			}
			
			if (refresh && scrollLayer.isAutoBillboard)
			{
				scrollLayer.RefreshBillboard(true, false, false);
			}
		}
	}
}