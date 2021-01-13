Shader "Hidden/MotionBlur"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Amplitude("Amplitude", Float) = 1
    }
        SubShader
        {
            // No culling or depth
            Cull Off ZWrite Off ZTest Always
           Tags{
                "RenderType" = "Opaque"
    }
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"
                uniform sampler2D _CameraDepthTexture;


                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                sampler2D _MainTex;
                float4x4 _PreVPInverse;
                float4x4 _PreVP;
                float4x4 _CurrentVPInverse;
                float _Amplitude;
                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    float2 texCoord = i.uv;
                    float zOverW = tex2D(_CameraDepthTexture, texCoord).r;
                    float4 H = float4(texCoord.x * 2 - 1, texCoord.y * 2 - 1, zOverW, 1);
                    float4 D = mul(_CurrentVPInverse, H);
                    float4 worldPos = D / D.w;

                    float4 currentPos = H;
                    float4 previousPos = mul(_PreVP, worldPos);
                    previousPos /= previousPos.w;
                    float2 velocity = (currentPos - previousPos);
                    float4 color = tex2D(_MainTex, texCoord);
                    texCoord += velocity;
                    int numSamples = 3;
                    _Amplitude /= (zOverW * 2000);

                    for (int i = 1; i < numSamples; ++i, texCoord += velocity * _Amplitude)
                    {
                        float4 currentColor = tex2D(_MainTex, texCoord);
                        color += currentColor;
                    }
                    float4 finalColor = color / numSamples;
                    return finalColor;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}
