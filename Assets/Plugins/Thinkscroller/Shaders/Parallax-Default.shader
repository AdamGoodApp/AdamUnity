Shader "Thinkscroller/Parallax Default"
{
	Properties { _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" }
	
	Category
	{
		Blend SrcAlpha OneMinusSrcAlpha
		
		Tags { Queue=Transparent }
		
		SubShader
		{
			Pass
			{
				ZWrite Off
				ZTest LEqual
				Lighting Off
				SetTexture [_MainTex] { Combine texture, texture }
			}
		}
	}
}