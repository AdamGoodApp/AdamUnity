using UnityEngine;
using System.Collections;
using ThinksquirrelSoftware.Thinkscroller;

/// <summary>
/// Example script - scrolls constantly every frame.
/// </summary>
[AddComponentMenu("Thinkscroller Example Project/Constant Scroll")]
public class ConstantScroll : MonoBehaviour {
	
	public Vector2 scrollVector = Vector2.one;
	
	void Update()
	{
		Parallax.Scroll(scrollVector * Time.deltaTime);
	}
}
