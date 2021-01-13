Shader "TA_WHQ/Fade/FadeIn"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _TargetPosition("Target Position", vector) = (0, 0, 0, 0)
        _Threshold("Threshold", Range(0, 1)) = 0
        _MaxDistance("Max Distance", Float) = 10
        [Toggle]_IsFadingStage("Is Fading Stage", Int) = 0
        _FadingTimer("Fading Timer", float) = 0
        
    }
        SubShader
        {
            Tags {
                "RenderType" = "Transparent"
                "Queue" = "Transparent"
            }
            LOD 100
            Blend SrcAlpha OneMinusSrcAlpha
            Pass
            {
                CGPROGRAM
            // Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members targetPosition)
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Threshold;
            float4 _TargetPosition;
            float _MaxDistance;
            float _MaxAlpha;
            float _FadingTimer;
            int _IsFadingStage;
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float d = distance(i.worldPos, _TargetPosition);
                d /= _MaxDistance;
                if (_IsFadingStage) {
                    col.a = lerp(0.6, 1, _FadingTimer);
                } else {
                    if (d > _Threshold) {
                        //col.a = lerp(0, 0.6, _Threshold) * _Threshold;
                        col.a = lerp(0, 0.6, _Threshold);
                    }
                    else {
                        col.a = 0.6;
                    }
                    
                }
               
                
                return col;
            }
            ENDCG
        }
    }
            //Fallback "Diffuse"
}
