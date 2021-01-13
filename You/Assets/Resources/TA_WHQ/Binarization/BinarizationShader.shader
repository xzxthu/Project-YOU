Shader "Unlit/Binarization"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TargetPosition("Target Position", vector) = (0, 0, 0, 0)
        _ColorFaceToTarget("Color Face To Target", Color) = (1, 1, 1, 1)
        _ColorFaceAgainstTarget("Color Face Against Target", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TargetPosition;
            float4 _ColorFaceToTarget;
            float4 _ColorFaceAgainstTarget;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normal = UnityObjectToWorldNormal(v.normal);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                float3 targetDir = _TargetPosition - i.worldPos;
                float coe = dot(targetDir, i.normal);
                if (coe > 0) {
                    //col = lerp(col, _ColorFaceToTarget, coe);
                    col = _ColorFaceToTarget;
                }
                else {
                    //col = lerp(_ColorFaceToTarget, _ColorFaceAgainstTarget, -coe);
                    col = _ColorFaceAgainstTarget;
                }
                return col;
            }
            ENDCG
        }
    }
}
