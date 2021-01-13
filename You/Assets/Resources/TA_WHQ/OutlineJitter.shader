Shader "Unlit/OutlineJitter"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
		_OutlineWidth("Outline Width", Float) = 0.01
		_Scribbliness("Scribbliness", Float) = 0.01
		_RedrawRate("Redraw Rate", Float) = 6
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

		float hash(float2 seed){
			return frac(sin(dot(seed.xy ,float2(12.9898,78.233))) * 43758.5453);
		}

		fixed4 frag(v2f i) : SV_Target
		{
			float4 result = _OutlineColor;
			return result;
		}
			ENDCG
			SubShader
		{
			Tags{
				"Queue" = "Geometry"
				//"IgnoreProjector" = "true"
			}

				Pass{
					Name "Mask"
					Blend Zero One
					Lighting Off

					Cull Back
					CGPROGRAM

					v2f vert(appdata v) {
						v2f o;
						float4 offset = float4(v.normal, 0) * _Scribbliness * (hash(v.uv.xy + floor(_Time.y * _RedrawRate)) - 0.5);
						o.vertex = UnityObjectToClipPos(v.vertex - offset);
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
					float4 offset = float4(v.normal,0) * _OutlineWidth + ((hash(v.uv.xx + floor(_Time.y * _RedrawRate)) - 0.5) * _Scribbliness);
					o.vertex = UnityObjectToClipPos(v.vertex + offset);
					return o;
				}
				ENDCG

				}
					Pass{
					Name "SecondOutline"
					Blend SrcAlpha OneMinusSrcAlpha
					Lighting Off
					Cull Front
					CGPROGRAM
					v2f vert(appdata v) {
						v2f o;
						float4 offset = float4(v.normal,0) * (_OutlineWidth + (hash(v.uv.yy + floor(_Time.y * _RedrawRate)) - 0.5) * _Scribbliness);
						o.vertex = UnityObjectToClipPos(v.vertex + offset);
						return o;
					}
					ENDCG
				}
					Pass{
				Name "ThirdOutline"
				Blend SrcAlpha OneMinusSrcAlpha
				Lighting Off
				Cull Front
				CGPROGRAM
				v2f vert(appdata v) {
					v2f o;
					float4 offset = float4(v.normal,0) * (_OutlineWidth + (hash(v.uv.zz + floor(_Time.y * _RedrawRate)) - 0.5) * _Scribbliness);
					o.vertex = UnityObjectToClipPos(v.vertex + offset);
					return o;
				}
				ENDCG
					}

						Pass{
					Name "ForthOutline"
					Blend SrcAlpha OneMinusSrcAlpha
					Lighting Off
					Cull Front
					CGPROGRAM
					v2f vert(appdata v) {
						v2f o;
						float4 offset = float4(v.normal,0) * (_OutlineWidth + (hash(v.uv.yz + floor(_Time.y * _RedrawRate)) - 0.5) * _Scribbliness);
						o.vertex = UnityObjectToClipPos(v.vertex + offset);
						return o;
					}
					ENDCG
				}

		}
}
