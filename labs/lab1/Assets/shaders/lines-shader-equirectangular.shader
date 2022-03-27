Shader "Hidden/lines-shader-equirectangular"
{
  Properties
  {
      _MainTex ("Texture", 2D) = "white" {}

      _LinesColor ("Lines Color", Color) = (1,1,1,1)
      _HorizontalLines ("Horizontal lines", Range(0,100)) = 10
      _LineWidth ("Line width", Range(0,2)) = 0.001
      _LineBlur ("Line blur", Range(0,2)) = 0.001
      _VerticalLines ("Vertical lines", Range(0,100)) = 10

  }
  SubShader
  {
      // No culling or depth
      Cull Front

      Pass
      {
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

          float4 _LinesColor;
          float _HorizontalLines, _VerticalLines;
          float _LineWidth, _LineBlur;

          // From carthesian to spherical coords
          inline float2 RadialCoords(float3 a_coords)
          {
              float3 a_coords_n = normalize(a_coords);
              float lon = atan2(a_coords_n.z, a_coords_n.x);
              float lat = acos(a_coords_n.y);
              float2 sphereCoords = float2(lon, lat) * (1.0 / UNITY_PI);
              return float2(sphereCoords.x * 0.5 + 0.5, 1 - sphereCoords.y);
          }

          float max(float2 v) {
            return max(v.x,v.y);
          }

          float4 frag(v2f IN) : COLOR
          {
            // Fix the coordinates (spherical)
            float2 equiUV = RadialCoords(IN.normal);
            // number of horizontal and vertical lines (around the whole sphere):
            float2 nLines = floor(float2(_HorizontalLines, _VerticalLines)); // Make it "int"
            // alpha:
            float a = max(
                        max(_LineWidth - nLines * fmod(equiUV, 1.0 / nLines)), // To the right
                        max(_LineWidth - nLines * fmod(1.0-equiUV, 1.0 / nLines)) // To the left
                      ); // maximum of the horizontal and vertical alpha
            a = smoothstep(0, _LineBlur, a); // cap it with the blur amount
            // Mix the texture and the line according to a:
            return lerp(tex2D(_MainTex, equiUV), _LinesColor, a);
          }
          ENDCG
      }
  }
}

// old ugly versions
// if (fmod(equiUV.x, 1.0 / _HorizontalLines) < _LineWidth ||
//     fmod(equiUV.y, 1.0 / _VerticalLines)   < _LineWidth) {
//       return _LinesColor;
// }
// return tex2D(_MainTex, equiUV);
// float blend = 1.0-fmod(equiUV.x, 1.0 / _HorizontalLines) / _LineWidth;
// blend = smoothstep(0,)
// float4 c = blend * tex2D(_MainTex, equiUV) + (1.0-blend) * _LinesColor;
// return float4(c.rgb, 1.0);
