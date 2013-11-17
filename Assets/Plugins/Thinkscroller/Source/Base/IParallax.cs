using UnityEngine;

namespace ThinksquirrelSoftware.Thinkscroller
{
	/// <summary>
	/// Parallax manager interface type.
	/// </summary>
	public interface IParallax
	{	
		/// <summary>
		/// The list of active scroll layers in the scene, ordered by weight.
		/// </summary>
		ScrollLayer this[int i]
		{
			get;
		}
		
		/// <summary>
		/// The number of active scroll layers in the scene.
		/// </summary>
		int Length
		{
			get;
		}
		
		/// <summary>
		/// If true, the camera's viewport is set automatically.
		/// </summary>
		bool automatic
		{
			get;
			set;
		}
		
		/// <summary>
		/// If the camera viewport is not automatic, the minimum viewport size.
		/// </summary>
		Vector2 minSize
		{
			get;
			set;
		}
		
		/// <summary>
		/// If the camera viewport is not automatic, the maximum viewport size.
		/// </summary>
		Vector2 maxSize
		{
			get;
			set;
		}
		
		/// <summary>
		/// If true, billboard meshes are automatically regenerated every LateUpdate().
		/// </summary>
		bool autoRefreshBillboards
		{
			get;
			set;
		}
		
		/// <summary>
		/// Gets the parallax camera.
		/// </summary>
		Camera GetParallaxCamera();
		
		/// <summary>
		/// Gets the width of the parallax camera viewport.
		/// </summary>
		float GetCameraWidth();
		
		/// <summary>
		/// Gets the height of the parallax camera viewport.
		/// </summary>
		float GetCameraHeight();
		
		/// <summary>
		/// Gets the aspect ratio of the parallax camera viewport.
		/// </summary>
		float GetCameraAspectRatio();
		
		/// <summary>
		/// Sets the parallax camera.
		/// </summary>
		void SetParallaxCamera(Camera parallaxCamera);
		
		/// <summary>
		/// Gets the current scroll movement constriants.
		/// </summary>
		ScrollConstraints GetScrollConstraints();
		
		/// <summary>
		/// Sets the current scroll movement constraints.
		/// </summary>
		void SetScrollConstraints(ScrollConstraints scrollConstraints);
		
		/// <summary>
		/// Gets the global base speed for parallax scrolling.
		/// </summary>
		float GetBaseSpeed();
		
		/// <summary>
		/// Sets the global base speed for parallax scrolling.
		/// </summary>
		void SetBaseSpeed(float baseSpeed);

		/// <summary>
		/// Scrolls the parallax manager.
		/// </summary>
		/// <remarks>
		/// Can also be accessed statically (Parallax.Scroll()).
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
		void DoScroll(Vector2 scrollValue);
		
		/// <summary>
		/// Scrolls the parallax manager.
		/// </summary>
		/// <remarks>
		/// Can also be accessed statically (Parallax.Scroll()).
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
		void DoScroll(float xScrollValue, float yScrollValue);
		
		
		/// <summary>
		/// Resets the position of object layers.
		/// </summary>
		void DoResetPosition();
		
		/// <summary>
		/// Resets the position of object layers.
		/// </summary>
		void DoResetPosition(Vector3 resetPosition);
		
		/// <summary>
		/// Refreshes the scroll layers.
		/// </summary>
		void RefreshLayers();
	}
}
