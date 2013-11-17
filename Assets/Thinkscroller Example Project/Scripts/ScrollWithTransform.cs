using UnityEngine;
using System.Collections;
using ThinksquirrelSoftware.Thinkscroller;

/// <summary>
/// Example script - scrolls based on the object's transform.
/// </summary>
[AddComponentMenu("Thinkscroller Example Project/Scroll With Transform")]
public class ScrollWithTransform : MonoBehaviour {
	
	private Transform myTransform;
	private Vector3 oldPosition;
	private Vector3 scrollVector;
	
	void Start()
	{
		myTransform = transform;
	}
	
	void Update()
	{
		scrollVector = myTransform.position - oldPosition;
		Parallax.Scroll(scrollVector.x, scrollVector.y);
		oldPosition = myTransform.position;
	}
}
