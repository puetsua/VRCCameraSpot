// Based on CameraJackShader from rhaamo/CameraSystem by Ottpossum.
// CameraJackShader credits kurotori and is released under the MIT License.
// See THIRD_PARTY_NOTICES.md for attribution and license details.

Shader "PuetsuaWorkshop/VRCCameraSpotOverlay"
{
    Properties
    {
        _MainTex("Camera Feed", 2D) = "black" {}
        _Distance("Activation Distance", Range(0.01, 2.0)) = 0.2
        _LimitFoV_H("Maximum Horizontal FoV", Float) = 90.0
        _Aspect("Aspect h/v", Float) = 1.7777777
        _ForceSpot("Force Desktop Spot", Float) = 0.0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Overlay+6000"
            "IgnoreProjector" = "True"
        }

        Pass
        {
            ZTest Always
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define RAD2DEG 180.0f / UNITY_PI

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Distance;
            float _LimitFoV_H;
            float _Aspect;
            float _ForceSpot;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = float4((2.0f * v.uv.x) - 1.0f, 1.0f - (2.0f * v.uv.y), 1.0f, 1.0f);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                if (_ProjectionParams.x > 0.0f)
                {
                    o.uv.y = 1.0f - o.uv.y;
                }

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 objectOrigin = mul(unity_ObjectToWorld, float4(0.0f, 0.0f, 0.0f, 1.0f));
                float distance = length(objectOrigin.xyz - _WorldSpaceCameraPos);

                if (!(_ForceSpot > 0.0f))
                {
                    if (distance > _Distance) { discard; }

                    float fovH = atan(1.0f / unity_CameraProjection._m11) * RAD2DEG * 2.0f;
                    if (fovH > _LimitFoV_H) { discard; }
                }

                i.uv.y *= _Aspect * _ScreenParams.y / _ScreenParams.x;
                i.uv.y -= ((_Aspect * _ScreenParams.y / _ScreenParams.x) - 1.0f) * 0.5f;

                if (i.uv.y > 1.0f || i.uv.y < 0.0f)
                {
                    return float4(0.0f, 0.0f, 0.0f, 1.0f);
                }

                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
