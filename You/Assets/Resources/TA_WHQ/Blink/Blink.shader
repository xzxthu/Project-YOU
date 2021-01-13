Shader "Hidden/Blink"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FocusOnAxisX("Focus On Axis X", Range(0.0, 1.0)) = 0.5
        _FocusOnAxisY("Focus On Axis Y", Range(0.0, 1.0)) = 0.5
    }
        SubShader
        {
            // No culling or depth
            Cull Off ZWrite Off ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"
                //#include "UnityUI.cginc"


            sampler2D _MainTex;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            float _FocusOnAxisX;
            float _FocusOnAxisY;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float x = i.uv.x - 0.5;
                float y = i.uv.y - 0.5;
                float value = (x * x) / (_FocusOnAxisX * _FocusOnAxisX) + (y * y) / (_FocusOnAxisY * _FocusOnAxisY);
                col.a = value;
                return col;
            }
            ENDCG
        }
    }
}
