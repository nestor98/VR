Shader "Custom/Disappearing"
{
  Properties
  {
    _Color ("Color", Color) = (1,1,1,1)
    _Transparency ("Transparency", Range(0, 1)) = 1
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
    void surf (Input IN, inout SurfaceOutputStandard o)
    {
      o.Albedo = _Color.rgb;
      o.Alpha = _Transparency;
    }
    ENDCG
  }
  FallBack "Standard"
}
