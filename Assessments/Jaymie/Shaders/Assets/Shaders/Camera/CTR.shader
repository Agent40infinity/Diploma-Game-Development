Shader "Custom/CTR"
{
    Properties
	{
		_MainTex ("Base", 2D) = "white" {}
		_MaskTex ("Mask Texture", 2D) = "white" {}
		_DisplacementTex ("Displacement Texture", 2D) = "white" {}
		_Strength ("Sterngth", Float) = 1
		_MaskBlend ("Mask Blending", Float) = 0.5
		_MaskSize ("Mask Size", Float) = 1
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _MaskTex;
			uniform sampler2D _DisplacementTex;

			fixed _MaskBlend;
			fixed _MaskSize;
			fixed _Strength;

			//fixed4 frag (v2f_img i) : COLOR
			//{
			//	fixed4 mask = tex2D(_MaskTex, i.uv * _MaskSize);
			//	fixed4 base = tex2D(_MainTex, i.uv);
			//	return lerp(base, mask, _MaskBlend);
			//}

			float4 frag (v2f_img i) : COLOR
			{
				half2 n = tex2D(_DisplacementTex, i.uv);
				half2 d = n * 2 - 1;
				i.uv += d * _Strength;
				i.uv = saturate(i.uv);
				float4 c = tex2D(_MainTex, i.uv);
				return c;
			}
			ENDCG
		}
	}
}