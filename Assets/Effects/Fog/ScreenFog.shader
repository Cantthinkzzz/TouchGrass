Shader "Custom/ScreenFog"
{
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
        }

        Pass
        {
            ZWrite Off
            ZTest Always
            Cull Off

            HLSLPROGRAM

            #pragma vertex Vert
            #pragma fragment Frag
            #pragma multi_compile _ _USE_DRAW_PROCEDURAL

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            float _FogEnabled;
            float4 _FogPlayerPosition;
            float4 _FogColor;
            float _RadiusExtension;
            float _StartRadius;
            float _EndRadius;
            float _EdgeSoftness;

            half4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                float2 uv = input.texcoord;

                half4 sceneColor = SAMPLE_TEXTURE2D_X(
                    _BlitTexture,
                    sampler_LinearClamp,
                    uv
                );

                if (_FogEnabled < 0.5)
                {
                    return sceneColor;
                }

                float rawDepth = SampleSceneDepth(uv);

                #if UNITY_REVERSED_Z
                    bool isSky = rawDepth < 0.0001;
                #else
                    bool isSky = rawDepth > 0.9999;
                #endif

                float deviceDepth = rawDepth;

                #if !UNITY_REVERSED_Z
                    deviceDepth = lerp(
                        UNITY_NEAR_CLIP_VALUE,
                        1.0,
                        rawDepth
                    );
                #endif

                float3 worldPos = ComputeWorldSpacePosition(
                    uv,
                    deviceDepth,
                    UNITY_MATRIX_I_VP
                );

                // Distance on the ground plane.
                float2 groundPos = worldPos.xz;
                float2 playerPos = _FogPlayerPosition.xz;

                float distanceToPlayer = distance(
                    groundPos,
                    playerPos
                );

                // Slider 0 = Start Radius, 1 = End Radius.
                float radius = lerp(
                    _EndRadius,
                    _StartRadius,
                    saturate(_RadiusExtension)
                );

                float edgeWidth = max(_EdgeSoftness, 0.001);

                // Clear inside the radius, fog outside.
                float fogMask = smoothstep(
                    radius,
                    radius + edgeWidth,
                    distanceToPlayer
                );

                // Keep sky/background untouched.
                if (isSky)
                {
                    fogMask = 0.0;
                }

                float fogAlpha = fogMask;

                return half4(
                    lerp(
                        sceneColor.rgb,
                        _FogColor.rgb,
                        fogAlpha * _FogColor.a
                    ),
                    sceneColor.a
                );
            }

            ENDHLSL
        }
    }
}