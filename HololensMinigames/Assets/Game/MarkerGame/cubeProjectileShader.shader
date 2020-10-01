Shader "Unlit/cubeProjectileShader"
{
    Properties
    {
        _MainColor("MainColor", Color) = (1,1,1,0)
        _OutlineColor("MainColor", Color) = (1,1,1,0)
        _OutlineSize("OutlineSize", Float) = 1.1
        _NoiseTexture("Noise", 2D) = "white" {}
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

            #include "UnityCG.cginc"

            struct appdata
            {
                float3 vertex : POSITION;
                float3 normal : Normal;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _MainColor;
            sampler2D _NoiseTexture;

            v2f vert (appdata v)
            {
                v2f o;

                float3 offset = float3(
                    tex2Dlod(_NoiseTexture, float4(v.vertex.x / 100 + _Time[1] / 50, 0, 0, 0)).r,
                    tex2Dlod(_NoiseTexture, float4(0, v.vertex.y / 100 + _Time[1] / 50, 0, 0)).r,
                    tex2Dlod(_NoiseTexture, float4(0, 0, v.vertex.z / 100 + _Time[1] / 50, 0)).r);

                v.vertex += (offset - 0.5) * 2;

                o.vertex = UnityObjectToClipPos(v.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _MainColor;
            }
            ENDCG
        }

        Pass
        {
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float3 vertex : POSITION;
                float3 normal : Normal;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _OutlineColor;
            sampler2D _NoiseTexture;

            v2f vert (appdata v)
            {
                v2f o;

                float3 offset = float3(
                    tex2Dlod(_NoiseTexture, float4(v.vertex.x / 100 + _Time[1] / 50, 0, 0, 0)).r,
                    tex2Dlod(_NoiseTexture, float4(0, v.vertex.y / 100 + _Time[1] / 50, 0, 0)).r,
                    tex2Dlod(_NoiseTexture, float4(0, 0, v.vertex.z / 100 + _Time[1] / 50, 0)).r);

                v.vertex += (offset - 0.5) * 2;

                float size = 1.05;

                o.vertex = UnityObjectToClipPos(v.vertex * size);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

    }
}
