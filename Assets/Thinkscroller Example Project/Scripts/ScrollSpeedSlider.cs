using UnityEngine;
using ThinksquirrelSoftware.Thinkscroller;
using System.Collections;

[AddComponentMenu("Thinkscroller Example Project/Scroll Speed Slider")]
public class ScrollSpeedSlider : MonoBehaviour
{
	public GUISkin skin;
	
	// Update is called once per frame
	void OnGUI()
	{
		GUI.skin = skin;
		GUILayout.BeginArea(new Rect(10, 10, 450, 100));
		Parallax.instance.SetBaseSpeed(GUILayout.HorizontalSlider(Parallax.instance.GetBaseSpeed(), -10, 10, GUILayout.Width(400)));
		GUILayout.EndArea();
	}
}
