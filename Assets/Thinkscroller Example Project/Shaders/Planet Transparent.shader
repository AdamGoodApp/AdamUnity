// From http://entitycrisis.blogspot.com/2011/01/planet-shader-2.html
Shader "Thinkscroller Example/Planet Transparent" {
 Properties {
  _TintColor("Tint Color", Color) = (1, 1, 1, 1)
  _MainTex ("Diffuse(RGB) Spec(A)", 2D) = "white" {}
  _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
  _RimPower ("Rim Power", Range(0,8.0)) = 3.0
  _SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
  _Shininess ("Shininess", Range (0.01, 1)) = 0.078125

 }
 
 SubShader {
   Tags { "RenderType" = "Transparent" }
   
   CGPROGRAM
   #pragma surface surf Planet alpha noambient
   
   float _Shininess;
   sampler2D _MainTex;
   float4 _TintColor;
   float4 _RimColor;
   float _RimPower;
   
   half4 LightingPlanet (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
    half3 h = normalize (lightDir + viewDir);
    half diff = max (0, dot (s.Normal, lightDir));
    float nh = max (0, dot (s.Normal, h));
    float spec = pow (nh, 48.0);
    half rim = ((1 - (dot (normalize(viewDir), (s.Normal)))) + ((dot (normalize(lightDir), (s.Normal)))));
    half4 c;
    c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec * s.Alpha * _Shininess * _SpecColor) * (atten * 2);
    c.rgb = c.rgb  * (pow (rim, _RimPower) * _RimColor.rgb);
    return c;
   }
   
   struct Input {
    float2 uv_MainTex;
    float2 uv_BumpMap;
    float3 viewDir;
    float3 worldRefl; INTERNAL_DATA
   };
   
   void surf (Input IN, inout SurfaceOutput o) {
    o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _TintColor.rgb;
    o.Alpha = tex2D (_MainTex, IN.uv_MainTex).a * _TintColor.a;
   }
   ENDCG
 
 } 
 Fallback "Transparent/Diffuse"
}