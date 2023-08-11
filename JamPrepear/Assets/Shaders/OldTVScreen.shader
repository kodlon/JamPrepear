Shader "Custom/ScreenDistortion"
{
    Properties
    {
        _MainTex("Screen Texture", 2D) = "white" {}
        _Distortion("Distortion", Range(0,1)) = 0.1
        _NoiseIntensity("Noise Intensity", Range(0,1)) = 0.1
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Distortion;
            float _NoiseIntensity;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                uv.y += sin(uv.y * 3.14) * _Distortion;
                uv.x += cos(uv.x * 3.14) * _Distortion;
                fixed4 col = tex2D(_MainTex, uv);
                float noise = rand(uv + _Time.yy) * _NoiseIntensity;
                col.rgb += noise;
                return col;
            }
            ENDCG
        }
    }
}
