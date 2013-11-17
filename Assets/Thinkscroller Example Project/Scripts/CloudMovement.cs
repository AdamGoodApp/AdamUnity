using UnityEngine;
using System.Collections;

[AddComponentMenu("Thinkscroller Example Project/Cloud Movement")]
public class CloudMovement : MonoBehaviour
{
	public float scale = .0005f;
	void Update()
	{
		transform.position += Vector3.up * Mathf.Sin(Time.time) * scale;
	}
}
