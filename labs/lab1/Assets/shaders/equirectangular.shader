// Adapted from https://forum.unity.com/threads/what-is-wrong-with-unitys-spherical-mapping-how-to-fix-it.321205/
// as i noticed there was a problem with the uv coordinates when creating the lines shader
Shader "Hidden/equirectangular"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader{
        Pass {
            Tags {"LightMode" = "Always"}
            Cull front //Added by me
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #pragma glsl
                #pragma target 3.0
                #include "UnityCG.cginc"
                struct appdata {
                   float4 vertex : POSITION;
                   float3 normal : NORMAL;
                };
                struct v2f
                {
                    float4    pos : SV_POSITION;
                    float3    normal : TEXCOORD0;
                };
                v2f vert (appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.normal = v.normal;
                    return o;
                }
                sampler2D _MainTex;

                inline float2 RadialCoords(float3 a_coords)
                {
                    float3 a_coords_n = normalize(a_coords);
                    float lon = atan2(a_coords_n.z, a_coords_n.x);
                    float lat = acos(a_coords_n.y);
                    float2 sphereCoords = float2(lon, lat) * (1.0 / UNITY_PI);
                    return float2(sphereCoords.x * 0.5 + 0.5, 1 - sphereCoords.y);
                }
                float4 frag(v2f IN) : COLOR
                {
                    float2 equiUV = RadialCoords(IN.normal);
                    return tex2D(_MainTex, equiUV);
                }
            ENDCG
        }
    }
    FallBack "VertexLit"
}
