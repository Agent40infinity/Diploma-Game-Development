Shader "Lesson/Camera/Glitch/Analogue"
{
    Properties
    {
        _MainTex ("-", 2D) = "" {}
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	
	sampler2D _MainTex;
	float2 _MainText_TexelSize;

	float2 _ScanLineJitter;
	float2 _VerticalJump;
	float2 _HorizontalShake;
	float2 _ColourDrift;

	float nrand(float x, float y)
	{
		return frac(sin(dot(float2(x, y), float2(12.9898, 78.233))) * 42758.5453);
	}

	half4 frag(v2f_img i): SV_Target
	{
		float u = i.uv.x;
		float v = i.uv.y;
		//Line Scan:
		float jitter = nrand(v, _Time.x) * 2 - 1;
		jitter *= step(_ScanLineJitter.y, abs(jitter)) * _ScanLineJitter.x;

		//Vertical Jump:
		float jump = lerp(v, frac(v + _VerticalJump.y), _VerticalJump.x);

		//Horizontal Jump:
		float shake = (nrand(_Time.x, 2)-0.5) * _HorizontalShake;

		//Colour Drift:
		float drift = sin(jump + _ColourDrift.y) * _ColourDrift.x;
		half4 jsj = tex2D(_MainTex, frac(float2(u + jitter + shake, jump)));
		half4 jsdj = tex2D(_MainTex, frac(float2(u + jitter + shake + drift, jump)));

		return half4(jsj.r, jsdj.g, jsj.b, 1);
	}
	ENDCG

	SubShader
	{
		Pass
		{
			ZTEST Always Cull Off ZWrite Off
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma target 3.0
			ENDCG
		}
	}
}
