Shader "Leafling"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
        _HeadColor ("Head", Color) = (1, 1, 1, 1)
        _LeftArmColor ("Left Arm", Color) = (1, 1, 1, 1)
        _RightArmColor ("Right Arm", Color) = (1, 1, 1, 1)
        _LeftThighColor ("Left Thigh", Color) = (1, 1, 1, 1)
        _RightThighColor ("Right Thigh", Color) = (1, 1, 1, 1)
        _LeftFootColor ("Left Foot", Color) = (1, 1, 1, 1)
        _RightFootColor ("Right Foot", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnityCG.cginc"

            // NOTE: this is the exact content of "UnitySprites.cginc"
            // with only a slight modification to the fragment shader
            // to introduce color mapping
            #ifdef UNITY_INSTANCING_ENABLED

                UNITY_INSTANCING_BUFFER_START(PerDrawSprite)
                    // SpriteRenderer.Color while Non-Batched/Instanced.
                    UNITY_DEFINE_INSTANCED_PROP(fixed4, unity_SpriteRendererColorArray)
                    // this could be smaller but that's how bit each entry is regardless of type
                    UNITY_DEFINE_INSTANCED_PROP(fixed2, unity_SpriteFlipArray)
                UNITY_INSTANCING_BUFFER_END(PerDrawSprite)

                #define _RendererColor  UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteRendererColorArray)
                #define _Flip           UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteFlipArray)

            #endif // instancing

            CBUFFER_START(UnityPerDrawSprite)
            #ifndef UNITY_INSTANCING_ENABLED
                fixed4 _RendererColor;
                fixed2 _Flip;
            #endif
                float _EnableExternalAlpha;
            CBUFFER_END

            // Material Color.
            fixed4 _Color;
            fixed4 _HeadColor;
            fixed4 _LeftArmColor;
            fixed4 _RightArmColor;
            fixed4 _LeftThighColor;
            fixed4 _RightThighColor;
            fixed4 _LeftFootColor;
            fixed4 _RightFootColor;

            static const int KeysLength = 7;
            static const fixed3 _Keys[KeysLength] = 
            {
                fixed3(1, 1, 1), 
                fixed3(1, 0, 0),
                fixed3(0, 1, 1),
                fixed3(0, 1, 0),
                fixed3(1, 0, 1),
                fixed3(0, 0, 1),
                fixed3(1, 1, 0),
            };
            fixed4 GetValue(int index) 
            {
                if (index == 0) return _HeadColor;
                if (index == 1) return _LeftArmColor;
                if (index == 2) return _RightArmColor;
                if (index == 3) return _LeftThighColor;
                if (index == 4) return _RightThighColor;
                if (index == 5) return _LeftFootColor;
                if (index == 6) return _RightFootColor;
                return fixed4(0, 0, 0, 1);
            }

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
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            inline float4 UnityFlipSprite(in float3 pos, in fixed2 flip)
            {
                return float4(pos.xy * flip, pos.z, 1.0);
            }

            v2f SpriteVert(appdata_t IN)
            {
                v2f OUT;

                UNITY_SETUP_INSTANCE_ID (IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
                OUT.vertex = UnityObjectToClipPos(OUT.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color;

                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }

            sampler2D _MainTex;
            sampler2D _AlphaTex;

            fixed4 SampleSpriteTexture (float2 uv)
            {
                fixed4 color = tex2D (_MainTex, uv);

            #if ETC1_EXTERNAL_ALPHA
                fixed4 alpha = tex2D (_AlphaTex, uv);
                color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
            #endif

                return color;
            }

            fixed MinComponent(fixed3 color) 
            {
                return min(min(color.r, color.g), color.b);
            }
            fixed MaxComponent(fixed3 color) 
            {
                return max(max(color.r, color.g), color.b);
            }
            fixed3 RgbToHsv(fixed3 rgb) 
            {
                fixed min = MinComponent(rgb);
                fixed max = MaxComponent(rgb);
                return rgb;
            }
            fixed ColorDistance(fixed3 a, fixed3 b) 
            {
                return length(a - b);
            }
            fixed4 ClosestColor(fixed3 IN) 
            {
                fixed shortestDistance = ColorDistance(_Keys[0], IN);
                fixed4 OUT = GetValue(0);

                for (int i = 1; i < KeysLength; i++) 
                {
                    fixed distance = ColorDistance(_Keys[i], IN);
                    if (distance < shortestDistance) 
                    {
                        shortestDistance = distance;
                        OUT = GetValue(i);
                    }
                }
                return OUT;
            }
            fixed4 RemapHue(fixed4 IN) 
            {
                if (IN.a < 0.1) 
                {
                    return IN;
                }
                if (ColorDistance(IN, fixed3(0, 0, 0)) < 0.1) 
                {
                    return IN;
                }
                return ClosestColor(IN);
            }

            fixed4 SpriteFrag(v2f IN) : SV_Target
            {
                fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
                c = RemapHue(c);
                c.rgb *= c.a;
                return c;
            }
        ENDCG
        }
    }
}