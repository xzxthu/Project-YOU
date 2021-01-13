Shader "Unlit/EdgeHighLight"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
		_OutlineWidth("Outline Width", Float) = 1
		_EmissionIntensity("EmissionIntensity", Float) = 3
		[Toggle]_IsEnable("IsEnable", Float) = 1
	}

		CGINCLUDE
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

			struct appdata
		{
			float4 vertex : POSITION;
			float4 uv : TEXCOORD0;
			float3 normal : NORMAL;
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;
		float4 _OutlineColor;
		float _OutlineWidth;
		float _Scribbliness;
		float _RedrawRate;
		float _EmissionIntensity;
		float _IsEnable;
		fixed4 frag(v2f i) : SV_Target
		{
			float4 result = _OutlineColor * _EmissionIntensity;
			return result;
		}
			ENDCG
			SubShader
		{
			Tags{
				"RenderType" = "Opaque"
			}

				Pass{
					Name "Mask"
					Lighting Off
					Blend Zero One
					Cull Back
					CGPROGRAM

					v2f vert(appdata v) {
						v2f o;
						o.vertex = UnityObjectToClipPos(v.vertex);
						return o;
					}

				ENDCG

			}
				Pass{
				Name "FirstOutline"
				Blend SrcAlpha OneMinusSrcAlpha
				Lighting Off
				Cull Front
				CGPROGRAM
				v2f vert(appdata v) {
					v2f o;
					float sign = dot(normalize(v.vertex), normalize(v.normal)) < 0 ? -1 : 1;
					float4 vertexLocalPos = lerp(v.vertex, float4(v.vertex.xyz + sign * normalize(v.vertex) * _OutlineWidth, 1), _IsEnable);
					o.vertex = UnityObjectToClipPos(vertexLocalPos);
					return o;
				}
				ENDCG

				}
		}
}
