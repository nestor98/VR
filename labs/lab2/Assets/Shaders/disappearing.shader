Shader "Custom/Disappearing"
{
  Properties
  {
    _Color ("Color", Color) = (1,1,1,1)
    _Transparency ("Transparency", Range(0, 1)) = 1
    _Metallic ("Metallic", Range(0,1)) = 0.5
  }
  SubShader
  {
    Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
    CGPROGRAM
    #pragma surface surf Standard fullforwardshadows alpha:fade
    sampler2D _MainTex;
    struct Input {
      float2 uv_MainTex;
    };
    fixed4 _Color;
    fixed _Transparency;
    half _Metallic;
    void surf (Input IN, inout SurfaceOutputStandard o)
    {
      o.Albedo = _Color.rgb;
      o.Metallic = _Metallic;
      o.Alpha = _Transparency;
    }
    ENDCG
  }
  FallBack "Standard"
}
