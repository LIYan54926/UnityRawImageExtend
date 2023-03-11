// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "UI/Polygon Mask"
{
    Properties
    {
        [Header(Sprite)]
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        [PerRendererData] _Color("Tint", Color) = (1,1,1,1)
        [PerRendererData] _UVRect("UVRect", vector) = (0, 0, 1, 1)
        [Space]
        [Header(Mask)]
        _MaskMap("Mask Map", 2D) = "white" {}
        [KeywordEnum(NONE,TEXTURE, ELLIPSE, POLYGON, RECTANGLE, ROUNDEDPOLYGON, ROUNDEDRECTANGLE)] _OVERLAY("Mask mode",Float) = 1
        _MaskSmooth("Mask SmoothStep(RG)0-1(BA)1-0", vector) = (0, 0, 1, 1)
        [Header(Mask UV)]
        _MaskPolygonUV("(R)Rotation Degrees(G)Twirl Strength (BA)Twirl Offset", vector) = (0, 0, 0, 0)
        _MaskWidth("Mask Width", float) = 0.5
        _MaskHeight("Mask Height", float) = 0.5
        _MaskSides("Mask Sides", integer) = 3
        _MaskRoundness("Mask Roundness", float) = 0.5
        [Space]
        [Header(Stencil)]
        [PerRendererData] _StencilComp("Stencil Comparison", Float) = 8
        [PerRendererData] _Stencil("Stencil ID", Float) = 0
        [PerRendererData] _StencilOp("Stencil Operation", Float) = 0
        [PerRendererData] _StencilWriteMask("Stencil Write Mask", Float) = 255
        [PerRendererData] _StencilReadMask("Stencil Read Mask", Float) = 255

        [PerRendererData] _ColorMask("Color Mask", Float) = 15

        [PerRendererData] [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Stencil
            {
                Ref[_Stencil]
                Comp[_StencilComp]
                Pass[_StencilOp]
                ReadMask[_StencilReadMask]
                WriteMask[_StencilWriteMask]
            }

            Cull Off
            Lighting Off
            ZWrite Off
            ZTest[unity_GUIZTestMode]
            Blend One OneMinusSrcAlpha
            ColorMask[_ColorMask]

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0

                #include "UnityCG.cginc"
                #include "UnityUI.cginc"

                #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
                #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

                #pragma multi_compile_local _ _OVERLAY_TEXTURE _OVERLAY_ELLIPSE _OVERLAY_POLYGON _OVERLAY_RECTANGLE _OVERLAY_ROUNDEDPOLYGON _OVERLAY_ROUNDEDRECTANGLE

                struct appdata_t
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float4 vertex   : SV_POSITION;
                    fixed4 color : COLOR;
                    float4 texcoord  : TEXCOORD0;
                    float4 worldPosition : TEXCOORD1;
                    float4  mask : TEXCOORD2;
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                sampler2D _MainTex;
                sampler2D _MaskMap;
                fixed4 _Color;
                fixed4 _TextureSampleAdd;
                float4 _ClipRect;
                float4 _MainTex_ST;
                float _UIMaskSoftnessX;
                float _UIMaskSoftnessY;
                float4 _UVRect;
                float4 _MaskSmooth;
                float4 _MaskPolygonUV;
                float _MaskWidth, _MaskHeight, _MaskRoundness;
                int _MaskSides;

                float _MaskU, _MaskV;

                v2f vert(appdata_t v)
                {
                    v2f OUT;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                    float4 vPosition = UnityObjectToClipPos(v.vertex);
                    OUT.worldPosition = v.vertex;
                    OUT.vertex = vPosition;

                    float4 posWorld = mul(unity_ObjectToWorld, v.vertex);

                    float2 pixelSize = vPosition.w;
                    pixelSize /= float2(1, 1) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

                    float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
                    float2 maskUV = (v.vertex.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);
                    OUT.texcoord.xy = v.texcoord.xy *_MainTex_ST.xy + _MainTex_ST.zw;
                    OUT.texcoord.zw = v.texcoord.xy / _UVRect.zw - _UVRect.xy / _UVRect.zw;
                    OUT.mask = float4(v.vertex.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_UIMaskSoftnessX, _UIMaskSoftnessY) + abs(pixelSize.xy)));

                    OUT.color = v.color * _Color;
                    return OUT;
                }
             
                float Ellipse(float2 UV, float Width, float Height)
                {
                    float d = length((UV * 2 - 1) / float2(Width, Height));
                    return saturate((1 - d) / fwidth(d));
                }

                float Polygon(float2 UV, float Sides, float Width, float Height)
                {
                    float pi = 3.14159265359;
                    float aWidth = Width * cos(pi / Sides);
                    float aHeight = Height * cos(pi / Sides);
                    float2 uv = (UV * 2 - 1) / float2(aWidth, aHeight);
                    uv.y *= -1;
                    float pCoord = atan2(uv.x, uv.y);
                    float r = 2 * pi / Sides;
                    float distance = cos(floor(0.5 + pCoord / r) * r - pCoord) * length(uv);
                    return saturate((1 - distance) / fwidth(distance));
                }

                float Rectangle(float2 UV, float Width, float Height)
                {
                    float2 d = abs(UV * 2 - 1) - float2(Width, Height);
                    d = saturate(1 - d / fwidth(d));
                    return min(d.x, d.y);
                }

                float RoundedPolygon(float2 UV, float Width, float Height, float Sides, float Roundness)
                {
                    float PI = 3.14159265359;
                    float HALF_PI = 1.570796326795;
                    UV = UV * 2. + float2(-1., -1.);
                    float epsilon = 1e-6;
                    UV.x = UV.x / (Width + (Width == 0) * epsilon);
                    UV.y = UV.y / (Height + (Height == 0) * epsilon);
                    Roundness = clamp(Roundness, 1e-6, 1.);
                    float i_sides = floor(abs(Sides));
                    float fullAngle = 2. * PI / i_sides;
                    float halfAngle = fullAngle / 2.;
                    float opositeAngle = HALF_PI - halfAngle;
                    float diagonal = 1. / cos(halfAngle);
                    // Chamfer values
                    float chamferAngle = Roundness * halfAngle; // Angle taken by the chamfer
                    float remainingAngle = halfAngle - chamferAngle; // Angle that remains
                    float ratio = tan(remainingAngle) / tan(halfAngle); // This is the ratio between the length of the polygon's triangle and the distance of the chamfer center to the polygon center
                    // Center of the chamfer arc
                    float2 chamferCenter = float2(
                        cos(halfAngle),
                        sin(halfAngle)
                        ) * ratio * diagonal;
                    // starting of the chamfer arc
                    float2 chamferOrigin = float2(
                        1.,
                        tan(remainingAngle)
                        );
                    // Using Al Kashi algebra, we determine:
                    // The distance distance of the center of the chamfer to the center of the polygon (side A)
                    float distA = length(chamferCenter);
                    // The radius of the chamfer (side B)
                    float distB = 1. - chamferCenter.x;
                    // The refence length of side C, which is the distance to the chamfer start
                    float distCref = length(chamferOrigin);
                    // This will rescale the chamfered polygon to fit the uv space
                    // diagonal = length(chamferCenter) + distB;
                    float uvScale = diagonal;
                    UV *= uvScale;
                    float2 polaruv = float2 (
                        atan2(UV.y, UV.x),
                        length(UV)
                        );
                    polaruv.x += HALF_PI + 2 * PI;
                    polaruv.x = fmod(polaruv.x + halfAngle, fullAngle);
                    polaruv.x = abs(polaruv.x - halfAngle);
                    UV = float2(cos(polaruv.x), sin(polaruv.x)) * polaruv.y;
                    // Calculate the angle needed for the Al Kashi algebra
                    float angleRatio = 1. - (polaruv.x - remainingAngle) / chamferAngle;
                    // Calculate the distance of the polygon center to the chamfer extremity
                    float distC = sqrt(distA * distA + distB * distB - 2. * distA * distB * cos(PI - halfAngle * angleRatio));
                    float Out = UV.x;
                    float chamferZone = (halfAngle - polaruv.x) < chamferAngle;
                    Out = lerp(UV.x, polaruv.y / distC, chamferZone);
                    Out = saturate((1 - Out) / fwidth(Out));
                    return Out;
                }

                float RoundedRectangle(float2 UV, float Width, float Height, float Radius)
                {
                    Radius = max(min(min(abs(Radius * 2), abs(Width)), abs(Height)), 1e-5);
                    float2 uv = abs(UV * 2 - 1) - float2(Width, Height) + Radius;
                    float d = length(max(0, uv)) / Radius;
                    float fwd = max(fwidth(d), 1e-5);
                    return saturate((1 - d) / fwd);
                }

                float2 RotateUV(float2 UV, float2 Center, float Rotation)
                {
                    Rotation = Rotation * (3.1415926f / 180.0f);
                    UV -= Center;
                    float s = sin(Rotation);
                    float c = cos(Rotation);

                    float2x2 rMatrix = float2x2(c, -s, s, c);
                    rMatrix *= 0.5;
                    rMatrix += 0.5;
                    rMatrix = rMatrix * 2 - 1;

                    UV.xy = mul(UV.xy, rMatrix);
                    UV += Center;
                    return UV;
                }

                float2 TwirlUV(float2 UV, float2 Center, float Strength, float2 Offset)
                {
                    float2 delta = UV - Center;
                    float angle = Strength * length(delta);
                    float x = cos(angle) * delta.x - sin(angle) * delta.y;
                    float y = sin(angle) * delta.x + cos(angle) * delta.y;
                    return float2(x + Center.x + Offset.x, y + Center.y + Offset.y);
                }

                float SmoothUV(float2 UV, float length)
                {
                    float s1 = smoothstep(_MaskSmooth.x, _MaskSmooth.y, length);
                    float s2 = smoothstep(_MaskSmooth.z, _MaskSmooth.w, length);
                    return s1 - s2;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    const half alphaPrecision = half(0xff);
                    const half invAlphaPrecision = half(1.0 / alphaPrecision);
                    IN.color.a = round(IN.color.a * alphaPrecision) * invAlphaPrecision;

                    half4 color = IN.color * (tex2D(_MainTex, IN.texcoord.xy) + _TextureSampleAdd);

                    #ifdef UNITY_UI_CLIP_RECT
                    half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(IN.mask.xy)) * IN.mask.zw);
                    color.a *= m.x * m.y;
                    #endif

                    #ifdef UNITY_UI_ALPHACLIP
                    clip(color.a - 0.001);
                    #endif

                    color.rgb *= color.a;

                    //Calculation Mask
                    #ifndef _OVERLAY_NONE
                        float2 center = float2(0.5, 0.5);
                        float2 uv = TwirlUV(IN.texcoord.zw, center, _MaskPolygonUV.y, _MaskPolygonUV.zw);
                        uv = RotateUV(uv, center, _MaskPolygonUV.x);
                        half l = length(uv - 0.5);
                        float s = SmoothUV(uv, l);
                    #endif
                     //#ifdef  #ifndef
                    #ifdef _OVERLAY_TEXTURE
                        float mask = tex2D(_MaskMap, uv).a;
                        color *= mask * SmoothUV(uv, mask);
                    #elif _OVERLAY_ELLIPSE 
                        float mask = Ellipse(uv, _MaskWidth, _MaskHeight);
                        color *= mask * s;
                    #elif _OVERLAY_POLYGON
                        float mask = Polygon(uv,_MaskSides, _MaskWidth, _MaskHeight);
                        color *= mask * s;
                    #elif _OVERLAY_RECTANGLE
                        float mask = Rectangle(uv, _MaskWidth, _MaskHeight);
                        color *= mask * s;
                    #elif _OVERLAY_ROUNDEDPOLYGON
                        float mask = RoundedPolygon(uv, _MaskWidth, _MaskHeight, _MaskSides, _MaskRoundness);
                        color *= mask * s;
                    #elif _OVERLAY_ROUNDEDRECTANGLE
                        float mask = RoundedRectangle(uv, _MaskWidth, _MaskHeight, _MaskRoundness);
                        color *= mask * s;
                    #endif

                    return color;
                }

            ENDCG
            }
        }
}