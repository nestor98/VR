Shader "Hidden/lines-shader"
{
  Properties
  {
      _MainTex ("Texture", 2D) = "white" {}

      _LinesColor ("Lines Color", Color) = (1,1,1,1)
      _HorizontalLines ("Horizontal lines", Range(0,100)) = 10
      _LineWidth ("Line width", Range(0,2)) = 0.001
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

          #include "UnityCG.cginc"

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

          v2f vert (appdata v)
          {
              v2f o;
              o.vertex = UnityObjectToClipPos(v.vertex);
              o.uv = v.uv;
              return o;
          }





          sampler2D _MainTex;

          float4 _LinesColor;
          float _HorizontalLines, _VerticalLines;
          float _LineWidth;


          inline float2 RadialCoords(float3 a_coords)
          {
              float3 a_coords_n = normalize(a_coords);
              float lon = atan2(a_coords_n.z, a_coords_n.x);
              float lat = acos(a_coords_n.y);
              float2 sphereCoords = float2(lon, lat) * (1.0 / UNITY_PI);
              return float2(sphereCoords.x * 0.5 + 0.5, 1 - sphereCoords.y);
          }
          

          fixed4 frag (v2f i) : SV_Target
          {
            if (fmod(i.uv.x, 1.0 / _HorizontalLines) < _LineWidth ||
                fmod(i.uv.y, 1.0 / _VerticalLines)   < _LineWidth) {
                  return _LinesColor;
            }
            return tex2D(_MainTex, i.uv);
            // alpha:
            // float a = _HorizontalLines * fmod(i.uv.x, 1.0 / _HorizontalLines);
            // a = smoothstep(0.5-0.5*_LineWidth, 0.5+0.5*_LineWidth, a); //0.5+0.5*_LineWidth
            // return lerp(tex2D(_MainTex, i.uv), _LinesColor, a);
            // if ( < _LineWidth ||
            //     fmod(i.uv.y, 1.0 / _VerticalLines)   < _LineWidth) return _LinesColor;
            // return tex2D(_MainTex, i.uv);
            // //return lerp(texcol, _LinesColor, blend);
            // // horizontal:
            // //float blend = (fmod(i.uv.x, step_size)) < _LineWidth;
            // if (fmod(i.uv.x, 1.0 / _HorizontalLines) < _LineWidth) return _LinesColor;
            // //texcol = lerp(texcol, _LinesColor, blend);
            // // vertical:
            // // blend = (fmod(i.uv.y, 1.0 / _VerticalLines)) < _LineWidth;
            // if (fmod(i.uv.y, 1.0 / _VerticalLines) < _LineWidth) return _LinesColor;
            // return tex2D(_MainTex, i.uv);
            // //return lerp(texcol, _LinesColor, blend);
          }
          ENDCG
      }
  }
}
