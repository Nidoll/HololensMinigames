// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Unlit/Filler" {
Properties {
    _NoiseTex ("Noise Texture", 2D) = "white" {}
    _MainColor ("Main Color", Color) = (0,0,0,0)
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 100

    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha

    Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                float4 worldSpacePos : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            float4 _MainColor;
            sampler2D _NoiseTex;

            v2f vert (appdata v)
            {
                v2f o;
                float randomNumber = (tex2Dlod(_NoiseTex, float4(_Time[0] / 5 + v.vertex.y + v.vertex.x + v.vertex.z,0,0,0)).r);
                v.vertex.x += (sin(_Time[1] + (v.vertex.y*100))) /100;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldSpacePos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float alpha = tex2Dlod(_NoiseTex, float4((_Time[0] / 5) + i.worldSpacePos.y * 10000,0,0,0)).r;
                float number = (sin((i.worldSpacePos.y * 100 + _Time[1])) + 1) / 2;
                float4 color = _MainColor - float4(number,number,number,0);
                return color;
            }
        ENDCG
    }
}

}
