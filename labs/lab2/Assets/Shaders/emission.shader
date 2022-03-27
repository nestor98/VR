Shader "Custom/emission" {
  Properties {
    _customColor ("Main color", Color) = (1, 1, 1, 1)
  }
  SubShader {
    CGPROGRAM
    #pragma surface surf Standard
    struct Input {
      float2 uv_MainTex;
    };
    fixed4 _customColor;
    void surf (Input IN, inout SurfaceOutputStandard o) {
      o.Emission = _customColor.rgb;
    }
    ENDCG
  }
  Fallback "Diffuse"
}
