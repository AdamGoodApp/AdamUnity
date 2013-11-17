using UnityEngine;
using System.Collections;
using ThinksquirrelSoftware.Thinkscroller;

[RequireComponent(typeof(_ScrollLayer))]
[AddComponentMenu("Thinkscroller Example Project/Planet Movement")]
public class PlanetMovement : MonoBehaviour {
	
	public float rotationSpeed = 1000;
	private ScrollLayer scrollLayer;
	
	void Start()
	{
		// Set ambient for example
		RenderSettings.ambientLight = Color.black;
		
		// Get the Scroll Layer
		scrollLayer = GetComponent<ScrollLayer>();
	}

	void LateUpdate()
	{
		// Instead of scrolling UVs, use the raw scroll vector in order to rotate the object instead. This does all of the weight calculation. Nifty, huh?
		// Alternatively, you can use Parallax.Scroll - but in this case, UV scrolling causes artifacts on a sphere mesh.
		Vector2 scrollVector = Parallax.GetRawScrollVector(scrollLayer, Vector2.up * rotationSpeed * Time.smoothDeltaTime);
		
		transform.Rotate(0, scrollVector.y, 0);
	}
}
