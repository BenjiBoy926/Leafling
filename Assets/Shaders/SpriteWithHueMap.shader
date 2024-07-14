Shader "Sprite with Hue Map"
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
            // to introduce hue remapping
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

            fixed4 _Color;
            
            // Typedefs
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

            // Vertex shader
            v2f SpriteVert(appdata_t IN)
            {
                v2f OUT;

                UNITY_SETUP_INSTANCE_ID (IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
                OUT.vertex = UnityObjectToClipPos(OUT.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _RendererColor * _Color;

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

            // Fragment shader & hue mapping

            // NOTE: these properties MUST be set from C# code to enable the hue mapping functionality
            int _MapSize = 0;
            static const int _MapCapacity = 16;
            fixed3 _MapKeys[_MapCapacity];
            fixed4 _MapValues[_MapCapacity];

            fixed MinComponent(fixed3 color) 
            {
                return min(min(color.r, color.g), color.b);
            }
            fixed MaxComponent(fixed3 color) 
            {
                return max(max(color.r, color.g), color.b);
            }
            fixed CalculateHue(fixed3 rgb, fixed max, fixed minMaxDiff) 
            {
                if (minMaxDiff < 0.001) 
                {
                    return 0;
                }

                fixed redAmount = (max - rgb.r) / minMaxDiff; 
                fixed greenAmount = (max - rgb.g) / minMaxDiff;
                fixed blueAmount = (max - rgb.b) / minMaxDiff;
                fixed h;

                if (rgb.r == max) 
                {
                    h = blueAmount - greenAmount;
                }
                else if (rgb.g == max) 
                {
                    h = 2 + redAmount - blueAmount;
                }
                else 
                {
                    h = 4 + greenAmount - redAmount;
                }
                return (h / 6.0) % 1.0;
            }
            fixed3 RgbToHsv(fixed3 rgb)
            {
                fixed min = MinComponent(rgb);
                fixed max = MaxComponent(rgb);
                fixed diff = max - min;

                fixed h = CalculateHue(rgb, max, diff);
                fixed s = 0;
                if (max > 0.001)
                {
                    s = diff / max;
                }
                fixed v = max;
                return fixed3(h, s, v);
            }
            bool HasHue(fixed3 rgb) 
            {
                fixed min = MinComponent(rgb);
                fixed max = MaxComponent(rgb);
                return (max - min) > 0.001;
            }
            fixed3 HsvColorAmount(fixed hueRange, fixed chroma, fixed chi)
            {
                if (hueRange < 1) 
                {
                    return fixed3(chroma, chi, 0);
                }
                else if (hueRange < 2)
                {
                    return fixed3(chi, chroma, 0);
                }
                else if (hueRange < 3)
                {
                    return fixed3(0, chroma, chi);
                }
                else if (hueRange < 4)
                {
                    return fixed3(0, chi, chroma);
                }
                else if (hueRange < 5)
                {
                    return fixed3(chi, 0, chroma);
                }
                else
                {
                    return fixed3(chroma, 0, chi);
                }
            }
            fixed3 HsvToRgb(fixed3 hsv) 
            {
                fixed hueRange = hsv.x * 6;
                fixed chroma = hsv.y * hsv.z;
                fixed chi = chroma * (1 - abs(hueRange % 2 - 1));
                fixed3 colorAmount = HsvColorAmount(hueRange, chroma, chi);
                fixed chromaDiff = hsv.z - chroma;
                return fixed3(colorAmount.r + chromaDiff, colorAmount.g + chromaDiff, colorAmount.b + chromaDiff);
            }
            fixed ColorDistance(fixed3 hsvA, fixed3 hsvB)
            {
                // Codey: if we compare the distance between hue = 0 and hue = 0.99,
                // we should get a distance of 0.01, not 0.99.
                // To do this we take the min of (0.99 - 0) and (1 - 0.99)

                fixed minHue = min(hsvA.x, hsvB.x);
                fixed maxHue = max(hsvA.x, hsvB.x);

                fixed lower = minHue;
                fixed middle = maxHue;
                fixed upper = minHue + 1;
                
                fixed diff1 = middle - lower;
                fixed diff2 = upper - middle;
                return min(diff1, diff2);
            }
            fixed4 ClosestColor(fixed3 hsvColor) 
            {
                if (_MapSize == 0) 
                {
                    return fixed4(hsvColor, 1);
                }

                float shortestDistance = ColorDistance(_MapKeys[0], hsvColor);
                fixed4 result = _MapValues[0];
                for (int i = 1; i < _MapSize; i++) 
                {
                    float distance = ColorDistance(_MapKeys[i], hsvColor);
                    if (distance < shortestDistance) 
                    {
                        shortestDistance = distance;
                        result = _MapValues[i];
                    }
                }
                return result;
            }
            fixed4 RemapHue(fixed4 input) 
            {
                if (HasHue(input) && input.a > 0.1) 
                {
                    fixed3 hsvInput = RgbToHsv(input);
                    fixed4 hsvOutput = ClosestColor(hsvInput);
                    hsvOutput.y *= hsvInput.y;
                    hsvOutput.z *= hsvInput.z;
                    return fixed4(HsvToRgb(hsvOutput), hsvOutput.a);
                }
                return input;
            }
            fixed4 SpriteFrag(v2f IN) : SV_Target
            {
                fixed4 c = SampleSpriteTexture (IN.texcoord);
                c = RemapHue(c);
                c *= IN.color;
                c.rgb *= c.a;
                return c;
            }
        ENDCG
        }
    }
}