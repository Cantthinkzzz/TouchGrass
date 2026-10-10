Shader "Custom/SpriteMonochrome"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _MonochromeBlend ("Monochrome Blend (0 = Mono, 1 = Normal)", Range(0, 1)) = 0.0
    }

    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent" 
            "RenderType"="Transparent" 
            "RenderPipeline"="UniversalPipeline" 
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 uv       : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos      : SV_POSITION;
                float4 color    : COLOR;
                float2 uv       : TEXCOORD0;
            };

            Texture2D _MainTex;
            SamplerState sampler_MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _MonochromeBlend;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 tex_color = _MainTex.Sample(sampler_MainTex, i.uv) * i.color;
                
                // Standard luminance conversion.
                float gray = dot(tex_color.rgb, float3(0.2126, 0.7152, 0.0722));
                float3 mono_color = float3(gray, gray, gray);
                float3 final_color = lerp(mono_color, tex_color.rgb, _MonochromeBlend);

                return float4(final_color, tex_color.a);
            }
            ENDHLSL
        }
    }
}